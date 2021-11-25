using Metrics.ProgramAnalyzer;
using Metrics.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metrics.ChapinMetric
{
	public class ChapinMetric
	{
		private List<Token> tokens;
		public ChapinMetric(string plainText)
		{
			var lexer = new Lexer(plainText);
			tokens = lexer.TokenizeCode();
		}

		public ChapinMetric(List<Token> tokens)
		{
			this.tokens = tokens;
		}

		public ChapinMetricReturn CalculateChapinMetric()
		{
			var parcer = new ParcerChapin(tokens);
			var identificators = parcer.Parce();

			var groupedVariables = (from item in identificators
								   group item by item.Value into newList
								   select new { Value = newList.Key, Types = newList.Select(p => p.Type) }).ToDictionary(k => k.Value, t => t.Types.ToList());

			Dictionary<ChapinType, List<string>> resultMetric = new Dictionary<ChapinType, List<string>>() { { ChapinType.P, new List<string>() }, { ChapinType.M, new List<string>() }, { ChapinType.C, new List<string>() }, { ChapinType.T, new List<string>() } };
			Dictionary<ChapinType, List<string>> chapinIOMetric = new Dictionary<ChapinType, List<string>>() { { ChapinType.P, new List<string>() }, { ChapinType.M, new List<string>() }, { ChapinType.C, new List<string>() }, { ChapinType.T, new List<string>() } };
			ChapinType typeToAdd;
			foreach (var item in groupedVariables)
			{
				if (item.Value.Count == 1)
					typeToAdd = ChapinType.T;
				else
				{
					if (item.Value.Contains(VariableType.Condition))
						typeToAdd = ChapinType.C;
					else if (item.Value.Contains(VariableType.Assignment))
						typeToAdd = ChapinType.M;
					else
						typeToAdd = ChapinType.P;
				}
				resultMetric[typeToAdd].Add(item.Key);
				if (item.Value.Contains(VariableType.Input) || item.Value.Contains(VariableType.Output))
					chapinIOMetric[typeToAdd].Add(item.Key);
			}

			var result = new ChapinMetricReturn();
			result.chapinIOTypes = new Dictionary<string, List<string>>();
			result.chapinTypes = new Dictionary<string, List<string>>();

			foreach (var resultItem in resultMetric)
				result.chapinTypes.Add(resultItem.Key.ToString(), resultItem.Value);
			foreach (var resultIOItem in chapinIOMetric)
				result.chapinIOTypes.Add(resultIOItem.Key.ToString(), resultIOItem.Value);

			return result;
		}
	}
}
