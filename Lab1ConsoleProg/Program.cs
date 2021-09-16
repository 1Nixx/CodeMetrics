using Lab1ConsoleProg.ProgramAnalyzer;
using System;
using System.IO;

namespace Lab1ConsoleProg
{
	class Program
	{
		static void Main(string[] args)
		{
			StreamReader stream = new StreamReader(@"..\..\..\Data\TestCode.txt");

			var a = new Lexer(stream.ReadToEnd());
			stream.Close();
			var tokens = a.TokenizeCode();
			foreach (var item in tokens)
			{
				Console.WriteLine("Type : {0, 13} Value : {1}", item.Type, item.Value);
			}
		}
	}
}
