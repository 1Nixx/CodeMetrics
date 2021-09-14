using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg.ProgramAnalyzer
{
	class Parcer
	{
		private readonly List<Token> tokens;
		private int _currentPos = 0;

		public Parcer(IEnumerable<Token> tokens)
		{
			this.tokens = (List<Token>)tokens;
		}

		private void Parce()
		{
			
		}
	}
}
