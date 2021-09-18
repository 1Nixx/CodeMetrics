using Lab1ConsoleProg.Enties;
using Lab1ConsoleProg.ProgramAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1ConsoleProg
{
	class Program
	{
		static void Main(string[] args)
		{
			List<ProgramEntity> programEntities = new List<ProgramEntity>(){
				new ProgramEntity(ProgramType.Operand,"for"),
				new ProgramEntity(ProgramType.Operand,"while"),
				new ProgramEntity(ProgramType.Operand,"while"),
				new ProgramEntity(ProgramType.Operand,"for"),
				new ProgramEntity(ProgramType.Operator,"y")
			};

			HalsteadMetric metric = new HalsteadMetric(programEntities);
			metric.CreateListsOfOperandsAndOperators();

			foreach (OperandsAndOperators p in metric.ListOfOperands)
			{
				Console.WriteLine($"{p.Value} {p.NumOfRep}");
			}
			Console.WriteLine();
			foreach (OperandsAndOperators p in metric.ListOfOperators)
			{
				Console.WriteLine($"{p.Value} {p.NumOfRep}");
			}
		}
	}
}
