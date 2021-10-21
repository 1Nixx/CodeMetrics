using System;
using System.Collections.Generic;
using System.Text;
using Lab1ConsoleProg;
using Metrics.Helpers;

namespace Metrics.JilbMetric
{
	class Parcer
	{
		private readonly List<Token> tokens;
		private int _currentPos = 0;

		public int AmountOfConditionalOp { get; private set; } = 0;
		public int MaxNestingLevel { get; private set; } = 0;

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
					int nextBlockEnd = GetBlockEnd(_currentPos);
					_currentPos++;
					//Й!!!!!!!!!!!!!!!
				}
				else
					_currentPos++;
			}
		}

		private	void UpdateNestingLevel(int currentNestingLevel)
		{
			if (currentNestingLevel > MaxNestingLevel)
			{
				MaxNestingLevel = currentNestingLevel;
			}
		}
	
		private	bool IsConditionalOp(Token token)
		{
			return " if else while do for switch ".Contains(" " + token.Value + " ");
		}

		private int GetBlockEnd(int currentPos)
		{
			currentPos++;
			if (tokens[currentPos].Value == "(")
			{
				(_, currentPos) = TokenHandler.GetPairBracket(tokens, currentPos);
				currentPos++;
			}
			if (tokens[currentPos].Value == "if")
			{
				return GetBlockEnd(currentPos);
			}
			else
			{
				return TokenHandler.GetPairBracket(tokens, currentPos).pos;
			}
		}
	}
}
