using Metrics.Helpers;
using Metrics.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Metrics.ChapinMetric
{
	class ParcerChapin
	{

		private readonly List<Token> tokens;
		private HashSet<int> _skipTokensInd = new HashSet<int>();

		private int _currentPos = 0;

		public ParcerChapin(IEnumerable<Token> tokens)
		{
			this.tokens = (List<Token>)tokens;
		}

		public List<Variable> Parce()
		{
			var variableList = new List<Variable>();
			while (_currentPos < tokens.Count)
			{
				var entity = GetNextVariable();
				if (entity != null)
				{
					variableList.AddRange(entity);
				}
			}
			return variableList;
		}

		private List<Variable> GetNextVariable()
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
				tokens[_currentPos].Type == TokenType.InterpolatedStringLiteral ||
				tokens[_currentPos].Type == TokenType.Operator ||
				tokens[_currentPos].Type == TokenType.Punctuator)
			{
				_currentPos++;
				return null;
			}	
			else if (tokens[_currentPos].Type == TokenType.Identifier)
			{
				VariableType type = VariableType.Expression;
				if (!IsMethod(tokens, _currentPos))
				{
					if (tokens[_currentPos].Value != "Console")
					{
						if (IsTypeKeyword(tokens[_currentPos - 1]))
						{
							type = VariableType.Define;
						}
						if (tokens[_currentPos + 1].Value == "=")
						{
							type = VariableType.Assignment;
						}
						if ((tokens[_currentPos + 1].Value != ";") && (tokens[_currentPos + 2].Value == "Console"))
						{
							_skipTokensInd.Add(_currentPos + 2);
							type = VariableType.Input;
						}
						return new List<Variable>() { new Variable(type, tokens[_currentPos++].Value) };
					}
				} 
				else 
				{
					if (tokens[_currentPos].Value == "WriteLine" || tokens[_currentPos].Value == "Write")
						type = VariableType.Output;
					else
						type = VariableType.Parameter;
					return GetAllMethodVariable(tokens, ref _currentPos, type);
				}
				
			}
			else if (tokens[_currentPos].Type == TokenType.Keyword)
			{
				if (tokens[_currentPos].Value == "using" && tokens[_currentPos + 1].Value != "(")
				{
					SkipToSemicolons();
					return null;
				}

				if (IsConditionOperator(tokens[_currentPos]))
				{
					return GetAllMethodVariable(tokens, ref _currentPos, VariableType.Condition);
				}
			}
			_currentPos++;
			return null;
		}

		private List<Variable> GetAllMethodVariable(List<Token> tokens,ref int currentPos, VariableType type)
		{
			var result = new List<Variable>();
			int methodEnd = TokenHandler.GetPairBracket(tokens, ++currentPos).pos;
			for (; currentPos < methodEnd; currentPos++)
			{
				if ((tokens[currentPos].Type == TokenType.Identifier) && !IsMethod(tokens, currentPos))
					result.Add(new Variable(type, tokens[currentPos].Value));
			}
			return result;
		}

		private bool IsConditionOperator(Token token)
		{
			return " switch while foreach if for ".Contains(" " + token.Value + " ");
		}

		private bool IsTypeKeyword(Token token)
		{
			return " byte sbyte short ushort int uint long ulong float double decimal char bool object string ".Contains(" " + token.Value + " ");
		}

		private bool IsMethod(List<Token> tokens, int identId)
		{
			if (tokens[identId + 1].Value == "(")
				return true;
			else
				return false;
		}

		private void SkipToSemicolons()
		{
			while (tokens[_currentPos++].Value != ";") ;
			_currentPos--;
		}
	}
}
