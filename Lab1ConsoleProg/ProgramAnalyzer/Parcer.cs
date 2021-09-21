using Lab1ConsoleProg.Enties;
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

		public List<ProgramEntity> Parce()
		{
			var entityList = new List<ProgramEntity>();
			while (_currentPos < tokens.Count)
			{
				var entity = GetNextEntity();
				entityList.Add(entity);
			}
			return entityList;
		}

		private ProgramEntity GetNextEntity()
		{

		}
	}
}
