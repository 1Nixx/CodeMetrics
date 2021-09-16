using Lab1ConsoleProg.Enties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg
{
	class HalsteadMetric
	{
		private readonly List<ProgramEntity> programEntities;

		public HalsteadMetric(IEnumerable<ProgramEntity> programEntities)
		{
			this.programEntities = (List<ProgramEntity>)programEntities;
		}

	}
}
