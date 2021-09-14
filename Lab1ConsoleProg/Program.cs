using Lab1ConsoleProg.Enties;
using Lab1ConsoleProg.ProgramAnalyzer;
using System;

namespace Lab1ConsoleProg
{
	class Program
	{
		static void Main(string[] args)
		{
			var a = new Lexer("aaaaa");
			a.TokenizeCode();
			Console.WriteLine(ass);
		}
	}
}
