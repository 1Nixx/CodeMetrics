using Metrics.ChapinMetric;
using Metrics.ProgramAnalyzer;
using Metrics.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.SpenMetric
{
	public class SpenMetric
	{
		private List<Token> tokens;

		public SpenMetric(string plainText)
		{
			var lexer = new Lexer(plainText);
			tokens = lexer.TokenizeCode();
		}

		public SpenMetric(List<Token> tokens)
		{
			this.tokens = tokens;
		}

		public SpenMetricReturn CalculateSpenMetric()
		{
			var parcer = new ParcerChapin(tokens);
			var identificators = parcer.Parce();

			var result = from ident in identificators
						 group ident.Value by ident.Value into g
						 select new { g.Key, Count = g.Count() };

			return new SpenMetricReturn() { SpenSet = result.ToDictionary(i => i.Key, e => e.Count), CommonResult = result.Sum(x => x.Count) };
		}
	}
}
