using System;
using System.Collections.Generic;
using Metrics.Tokens;

namespace Metrics.Helpers
{
	static class TokenHandler
	{
		public static (string bracket, int pos) GetPairBracket(List<Token> tokens, int startInd)
		{
			Dictionary<string, string> bracketPairs = new Dictionary<string, string>()
			{
				{ "(", ")" },
				{ "[", "]" },
				{ "<", ">" },
				{ "{", "}" }
			};

			int bracketAmount = 1;
			int indexNow = startInd + 1;

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
	}
}
