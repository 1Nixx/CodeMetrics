using Lab1ConsoleProg.Enties;
using Lab1ConsoleProg.ProgramAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1ConsoleProg
{
	class Program
	{
		class OperandsAndOperators
		{
			public string Value { get; set; }
			public int NumOfRep { get; set; }

		}

		static void Main(string[] args)
		{
			List<ProgramEntity> programEntities = new List<ProgramEntity>(){
				new ProgramEntity(ProgramType.Operand,"for"),
				new ProgramEntity(ProgramType.Operator,"x"),
				new ProgramEntity(ProgramType.Operand,"while"),
				new ProgramEntity(ProgramType.Operand,"while"),
				new ProgramEntity(ProgramType.Operand,"for"),
				new ProgramEntity(ProgramType.Operator,"x")
			};

			List<OperandsAndOperators> ListOfOperands = new List<OperandsAndOperators>();
			List<OperandsAndOperators> ListOfOperators = new List<OperandsAndOperators>();

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
					} else
                    {
						ListOfOperators.Add(new OperandsAndOperators() { Value = group.Value, NumOfRep = group.Count });
					}
					break;
                }
			}

			foreach (OperandsAndOperators p in ListOfOperands)
			{
				Console.WriteLine($"{p.Value} {p.NumOfRep}");
			}
			Console.WriteLine();
			foreach (OperandsAndOperators p in ListOfOperators)
			{
				Console.WriteLine($"{p.Value} {p.NumOfRep}");
			}
			Console.ReadKey();
		}
	}
}
