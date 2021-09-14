using Lab1ConsoleProg.Enties;
using System;

namespace Lab1ConsoleProg
{
	class Program
	{
		static void Main(string[] args)
		{
			var ass = new ProgramEntity(ProgramType.Operator, "asdasd");
			Console.WriteLine(ass);
		}
	}
}
