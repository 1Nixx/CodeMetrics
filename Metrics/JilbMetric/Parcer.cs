using System.Collections.Generic;
using Metrics.Helpers;
using Metrics.Tokens;

namespace Metrics.Jilb
{
	class Parcer
	{
		private readonly List<Token> tokens;
		private int _currentPos = 0;

		public int AmountOfConditionalOp { get; private set; } = 0;

		private int _maxNestingLevel = 0;
		public int MaxNestingLevel { get => _maxNestingLevel - 1; }

		public Parcer(IEnumerable<Token> tokens)
		{
			this.tokens = (List<Token>)tokens;
			ParceBlock(this.tokens.Count, 0);
		}

		private void ParceBlock(int blockEndIndex, int currentNestingLevel)
		{
			UpdateNestingLevel(currentNestingLevel);
			while (_currentPos < blockEndIndex)
			{
				if (IsConditionalOp(tokens[_currentPos]))
				{
					Token currToken = tokens[_currentPos];
					UpdateAmountOfConditionalOp(currToken);

					int nextBlockEnd = GetBlockEnd(_currentPos);

					if (currToken.Value == "switch")
					{
						ProcessSwitchOp(nextBlockEnd, currentNestingLevel);
					}
					else 
					{	
						_currentPos++;
						ParceBlock(nextBlockEnd, currentNestingLevel + 1);
						ProcessDoWhileOp(currToken);
					}
				}
				else
					_currentPos++;
			}
		}

		private void ProcessDoWhileOp(Token currToken)
		{
			if (currToken.Value == "do")
			{
				_currentPos += 2;
			}
		}

		private void UpdateAmountOfConditionalOp(Token token)
		{
			if ((token.Value != "else") && (token.Value != "switch"))
			{
				AmountOfConditionalOp++;
			}
		}

		private void ProcessSwitchOp(int blockEnd, int nestingLevel)
		{
			UpdateNestingLevel(nestingLevel);
			while (_currentPos < blockEnd)
			{
				Token token = tokens[_currentPos];
				if ((token.Value == "case") || (token.Value == "default"))
				{
					int nextBlockEnd = GetCaseBlockEnd(_currentPos, blockEnd);				
					if (token.Value == "case")
					{
						AmountOfConditionalOp++;
						nestingLevel++;
					}
					_currentPos++;
					ParceBlock(nextBlockEnd, nestingLevel);
				}
				else
					_currentPos++;
			}
		}

		private int GetCaseBlockEnd(int casePos, int switchEnd)
		{
			casePos++;
			while ((tokens[casePos].Value != "case") && (tokens[casePos].Value != "default") && (casePos < switchEnd))
			{
				if (IsBracket(tokens[casePos]))
				{
					(_, int bracketEnd) = TokenHandler.GetPairBracket(tokens, casePos);
					casePos = bracketEnd;
				}
				casePos++;
			}
			return casePos;
		}

		private	void UpdateNestingLevel(int currentNestingLevel)
		{
			if (currentNestingLevel > _maxNestingLevel)
			{
				_maxNestingLevel = currentNestingLevel;
			}
		}
	
		private	bool IsConditionalOp(Token token)
		{
			return " if else while do for switch ".Contains(" " + token.Value + " ");
		}

		private bool IsBracket(Token token)
		{
			return " { [ ( ".Contains(" " + token.Value + " ");
		}

		private bool IsOperatorWithBrackets(Token token)
		{
			return " switch while for foreach lock using sizeof typeof ".Contains(" " + token.Value + " ");
		}

		private int GetBlockEnd(int currentPos)
		{
			currentPos++;
			if (tokens[currentPos].Value == "(")
			{
				(_, currentPos) = TokenHandler.GetPairBracket(tokens, currentPos);
				currentPos++;
			}
			if (IsOperatorWithBrackets(tokens[currentPos]))
			{
				return GetBlockEnd(currentPos);
			}
			else if (tokens[currentPos].Value == "if")
			{
				return GetFullIfEnd(currentPos);
			}
			else if (tokens[currentPos].Value == "{")
			{
				return TokenHandler.GetPairBracket(tokens, currentPos).pos;
			}
			else
			{
				while (tokens[currentPos].Value != ";")
				{
					if (IsBracket(tokens[currentPos]))
					{
						(_, int bracketEnd) = TokenHandler.GetPairBracket(tokens, currentPos);
						currentPos = bracketEnd;
					}
					currentPos++;
				}
				return currentPos;
			}
		}

		private int GetFullIfEnd(int ifPos)
		{
			ifPos = GetBlockEnd(ifPos);
			if (((ifPos + 1) < tokens.Count) && (tokens[ifPos + 1].Value == "else"))
			{
				return GetBlockEnd(ifPos + 1);
			}
			return ifPos;
		}
	}
}
