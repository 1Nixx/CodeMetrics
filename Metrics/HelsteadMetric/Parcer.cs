using Lab1ConsoleProg.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg.ProgramAnalyzer
{
	public class Parcer
	{
		private readonly List<Token> tokens;
		private HashSet<int> _skipTokensInd = new HashSet<int>();

		private int _currentPos = 0;

		public Parcer(IEnumerable<Token> tokens)
		{
			this.tokens = (List<Token>)tokens;
		}

		public List<ProgramEntity> Parce()
		{
			var entityList = new List<ProgramEntity>();
			while (_currentPos < tokens.Count)
			{
				var entity = GetNextEntity();
				if (entity != null)
				{
					entityList.Add(entity);
				}			
			}
			return entityList;
		}

		private ProgramEntity GetNextEntity()
		{
			if (_skipTokensInd.Contains(_currentPos))
			{
				_skipTokensInd.Remove(_currentPos);
				_currentPos++;
				return null;
			}
				
			if (tokens[_currentPos].Type == TokenType.BoolLiteral ||
				tokens[_currentPos].Type == TokenType.NumberLiteral ||
				tokens[_currentPos].Type == TokenType.NullLiteral ||
				tokens[_currentPos].Type == TokenType.CharacterLiteral ||
				tokens[_currentPos].Type == TokenType.StringLiteral ||
				tokens[_currentPos].Type == TokenType.InterpolatedStringLiteral)
			{
				return new ProgramEntity(ProgramType.Operand, tokens[_currentPos++].Value);
			}
			else if (tokens[_currentPos].Type == TokenType.Operator)
			{
				//Add ternarny operator
				return new ProgramEntity(ProgramType.Operator, tokens[_currentPos++].Value);
			}
			else if (tokens[_currentPos].Type == TokenType.Punctuator)
			{
				if ("{[(".Contains(tokens[_currentPos].Value))
				{
					int count = _currentPos++;
					var bracket = GetPairBracket(count);
					_skipTokensInd.Add(bracket.pos);
					return new ProgramEntity(ProgramType.Operator, tokens[count].Value + bracket.bracket);
				}
				else if (";".Contains(tokens[_currentPos].Value))
				{
					return new ProgramEntity(ProgramType.Operator, tokens[_currentPos++].Value);
				}/*
				else if (tokens[_currentPos].Value == ",")
				{
					_currentPos++;
				}
				else
				{
					return new ProgramEntity(ProgramType.Operator, tokens[_currentPos++].Value);
				}*/
			}
			else if (tokens[_currentPos].Type == TokenType.Identifier)
			{
				var entity = GetIdentifier();
				return new ProgramEntity(entity.entityType, entity.entity);
			}
			else if (tokens[_currentPos].Type == TokenType.Keyword)
			{
				string entity = GetKeyword();
				if (entity != null)
				{
					return new ProgramEntity(ProgramType.Operator, entity);
				}				
			}
			_currentPos++;
			return null;
		}

		private (string bracket, int pos) GetPairBracket(int startInd)
		{
			Dictionary<string, string> bracketPairs = new Dictionary<string, string>()
			{
				{ "(", ")" },
				{ "[", "]" },
				{ "<", ">" },
				{ "{", "}" }
			};

			int bracketAmount = 1;
			int indexNow = startInd+1;

			while (indexNow < tokens.Count && bracketAmount != 0)
			{
				if (tokens[indexNow].Value == tokens[startInd].Value)
				{
					bracketAmount++;
				}
				else if (tokens[indexNow].Value == bracketPairs[tokens[startInd].Value])
				{
					bracketAmount--;
				}
				indexNow++;
			}
			if (indexNow > tokens.Count)
			{
				throw new Exception();
			}
			indexNow--;

			return (bracketPairs[tokens[startInd].Value], indexNow);
		}
	
		private (string entity, ProgramType entityType) GetIdentifier()
		{
			if (tokens[_currentPos + 1].Value == "<")
			{
				try
				{
					var nearestBracket = GetPairBracket(_currentPos + 1);
					var potentialGeneric = tokens.GetRange(_currentPos + 2, nearestBracket.pos - _currentPos - 2);
					if (IsGeneric(potentialGeneric))
					{
						_skipTokensInd.Add(nearestBracket.pos);
						int pos = _currentPos;
						_currentPos += 2;
						if (tokens[nearestBracket.pos + 1].Value == "(")
						{
							var bracket = GetPairBracket(nearestBracket.pos + 1);
							_skipTokensInd.Add(nearestBracket.pos + 1);
							_skipTokensInd.Add(bracket.pos);
							
							return (tokens[pos].Value + "<>" + "()", ProgramType.Operator);
						}
						else
						{
							return (tokens[pos].Value + "<>", ProgramType.Operand);
						}
					}
				}	
				catch (Exception)
				{
					return (tokens[_currentPos++].Value, ProgramType.Operand);
				}
			}
			else if (tokens[_currentPos + 1].Value == "(")
			{
				var bracket = GetPairBracket(_currentPos + 1);
				_skipTokensInd.Add(bracket.pos);
				int pos = _currentPos;
				_currentPos += 2;
				return (tokens[pos].Value + "()", ProgramType.Operator);
			}
			return (tokens[_currentPos++].Value, ProgramType.Operand);
		}

		private string GetKeyword()
		{
			if (tokens[_currentPos].Value == "using" && tokens[_currentPos+1].Value != "(")
			{
				SkipToSemicolons();
				return null;
			}

			if (" try if do ".Contains(" " + tokens[_currentPos].Value + " "))
			{
				string entity = null;
				switch (tokens[_currentPos].Value)
				{
					case "try":
						{
							entity = HandlerTry();
							break;
						}
					case "if":
						{
							entity = HandlerIf();
							break;
						}
					case "do":
						{
							entity = HandlerDo();
							break;
						}
				}
				return entity;

			}
			else if (" switch while for foreach lock using sizeof typeof ".Contains(" " + tokens[_currentPos].Value + " "))
			{
				var bracket = GetPairBracket(_currentPos++ + 1);
				_skipTokensInd.Add(bracket.pos);
				return tokens[_currentPos++ - 1].Value + "()";
			}
			else if (" byte sbyte short ushort int uint long ulong float double decimal char bool object string ".Contains(" " + tokens[_currentPos].Value + " "))
			{
				return null;
			}
			else
			{
				return tokens[_currentPos++].Value;
			}
		}

		private string HandlerDo()
		{
			var bracket = GetPairBracket(_currentPos + 1);
			int whilePos = bracket.pos + 1;
			bracket = GetPairBracket(whilePos + 1);
			_skipTokensInd.Add(whilePos);
			_skipTokensInd.Add(whilePos + 1);
			_skipTokensInd.Add(bracket.pos);
			_currentPos++;
			return "do..while()";
		}

		private string HandlerTry()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("try");

			var bracket = GetPairBracket(_currentPos + 1);
			_currentPos++;
			if (tokens[bracket.pos + 1].Value == "catch")
			{
				var catchBrackets = bracket;
				while (tokens[catchBrackets.pos + 1].Value == "catch")
				{
					int bracketPos;
					_skipTokensInd.Add(catchBrackets.pos + 1);
					if (tokens[catchBrackets.pos + 2].Value == "(")
					{
						stringBuilder.Append("..catch()");
						catchBrackets = GetPairBracket(catchBrackets.pos + 2);
						_skipTokensInd.Add(catchBrackets.pos);
						bracketPos = catchBrackets.pos + 1;
					}
					else
					{
						stringBuilder.Append("..catch");
						bracketPos = catchBrackets.pos + 2;
					}
					catchBrackets = GetPairBracket(bracketPos);
				}
				if (tokens[bracket.pos + 1].Value == "finaly")
				{
					_skipTokensInd.Add(bracket.pos + 1);
					stringBuilder.Append("..finaly");
				}
				return stringBuilder.ToString();
			}
			else
			{
				return "try..finaly";
			}
		}

		private string HandlerIf()
		{
			var condBracket = GetPairBracket(_currentPos + 1);
			_currentPos += 2;
			_skipTokensInd.Add(condBracket.pos);
			if (tokens[condBracket.pos + 1].Value == "{")
			{				
				var endThenBranch = GetPairBracket(condBracket.pos + 1);
				if (endThenBranch.pos + 1 < tokens.Count && tokens[endThenBranch.pos + 1].Value == "else")
				{
					_skipTokensInd.Add(endThenBranch.pos + 1);
					return "if()..else"; 
				}
				else
				{
					return "if()";
				}
			}
			else
			{
				int operandAmount = 1;
				int indexNow = condBracket.pos + 1;

				while (indexNow < tokens.Count && operandAmount != 0)
				{
					if (tokens[indexNow].Value == "if")
					{
						operandAmount++;
					}
					else if (tokens[indexNow].Value == "else")
					{
						operandAmount--;
					}
					indexNow++;
				}
				if (indexNow >= tokens.Count)
				{
					return "if()";
				}
				indexNow--;

				_skipTokensInd.Add(indexNow);
				return "if()..else";
			}
		}

		private void SkipToSemicolons()
		{
			while (tokens[_currentPos++].Value != ";");
			_currentPos--;
		}

		private bool IsGeneric(List<Token> tokenList)
		{
			int counter = 0;
			bool isGeneric = true;
			while (counter < tokenList.Count && isGeneric)
			{
				if (tokenList[counter].Type == TokenType.Identifier || tokenList[counter].Type == TokenType.Keyword)
				{ 
					counter++;
					if (counter < tokenList.Count && tokenList[counter].Value == ",")
					{
						counter++;
					}
				}
				else
				{
					isGeneric = false;
				}
			}
			return isGeneric;
		}
	}
}
