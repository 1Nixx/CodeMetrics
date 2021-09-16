using Lab1ConsoleProg.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1ConsoleProg
{
	class HalsteadMetric
	{
		private readonly List<ProgramEntity> programEntities;

		public List<OperandsAndOperators> ListOfOperands = new List<OperandsAndOperators>();
		public List<OperandsAndOperators> ListOfOperators = new List<OperandsAndOperators>();

		public HalsteadMetric(IEnumerable<ProgramEntity> programEntities)
		{
			this.programEntities = (List<ProgramEntity>)programEntities;
		}

		public void CalculateMetric()
		{	
			var ExpressionGroups = from ProgramEntity in programEntities
								   group ProgramEntity by ProgramEntity.Value into g
								   select new
								   {
									   Value = g.Key,
									   Count = g.Count(),
									   programEntities = from p in g select p
								   };


			foreach (var group in ExpressionGroups)
			{
				foreach (ProgramEntity ProgramEntity in group.programEntities)
				{
					if (ProgramType.Operand == ProgramEntity.Type)
					{
						ListOfOperands.Add(new OperandsAndOperators() { Value = group.Value, NumOfRep = group.Count });
					}
					else
					{
						ListOfOperators.Add(new OperandsAndOperators() { Value = group.Value, NumOfRep = group.Count });
					}
					break;
				}
			}
		}

	}

	class OperandsAndOperators
	{
		public string Value { get; set; }
		public int NumOfRep { get; set; }

	}
}
