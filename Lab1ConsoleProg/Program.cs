using Lab1ConsoleProg.Enties;
using Lab1ConsoleProg.ProgramAnalyzer;
using System;
using System.IO;

namespace Lab1ConsoleProg
{
	class Program
	{
		static void Main(string[] args)
		{
			StreamReader stream = new StreamReader(@"..\..\..\Data\TextFile1.txt");

			var a = new Lexer(stream.ReadToEnd());
			//var a = new Lexer("List<Token>;");
			//var items = a.TokenizeCode();
			stream.Close();
			var items = new Parcer(a.TokenizeCode()).Parce();
			HalsteadMetric metric = new HalsteadMetric(items);
			var metricData = metric.CreateListsOfOperandsAndOperators();
			//var items = a.TokenizeCode();
			foreach (var item in metricData.ListOfOperands)
			{
				Console.WriteLine("Numb : {0, 5} Value : {1}", item.NumOfRep, item.Value);
			}
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			foreach (var item in metricData.ListOfOperators)
			{
				Console.WriteLine("Numb : {0, 5} Value : {1}", item.NumOfRep, item.Value);
			}
		}
	}
}
