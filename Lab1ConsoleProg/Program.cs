using Lab1ConsoleProg.Enties;
using Lab1ConsoleProg.ProgramAnalyzer;
using System;
using System.Collections.Generic;

namespace Lab1ConsoleProg
{
	class Program
	{
		
		static void Main(string[] args)
		{
			List<ProgramEntity> programEntities = new List<ProgramEntity>(){
				new ProgramEntity(ProgramType.Operand,"for"),
				new ProgramEntity(ProgramType.Operator,"x"),
				new ProgramEntity(ProgramType.Operand,"while")
			};
			programEntities.RemoveAt(0);
			foreach (var i in programEntities)
			{
				Console.WriteLine(i.Type + " : " + i.Value);
			}
			int[] array = { 10, 5, 10, 2, 2, 3, 4, 5, 5, 6, 7, 8, 9, 11, 12, 12 };
			int[] counts = new int[20 + 1];
			for (int i = 0; i < array.Length; i++)
				if (counts[array[i]] == 0)
				{
					for (int j = 0; j < array.Length; j++)
						if (array[i] == array[j])
							counts[array[i]]++;
					Console.WriteLine(array[i] + " повторяется " + counts[array[i]] + " раз");
				}
		}
	}
}
