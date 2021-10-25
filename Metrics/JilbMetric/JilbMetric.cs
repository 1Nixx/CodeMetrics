using Metrics.Halstead;
using Metrics.ProgramAnalyzer;
using Metrics.ReturnData;
using Metrics.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;
using HalstedMetricParcer = Halstead.ProgramAnalyzer;

namespace Metrics.Jilb
{
	public class JilbMetric
	{
		private List<Token> tokens;

		public JilbMetric(string plainText)
		{
			var lexer = new Lexer(plainText);
			tokens = lexer.TokenizeCode();
		}

		public async Task<JilbMetricReturn> CalculateMetricAsync()
		{
			var result = new JilbMetricReturn();

			var operatorsParcer = (new HalstedMetricParcer.Parcer(new ParserSimplified(tokens))).parcedCode;
			var halsteadResult = (new HalsteadMetric(operatorsParcer)).CreateListsOfOperandsAndOperators();

			var jilbParcer = new Parcer(tokens);

			result.MaximumNestingLevel = jilbParcer.MaxNestingLevel;
			result.AmountOfConditionalOperators = jilbParcer.AmountOfConditionalOp;
			result.RelativeComplexityOfProgram = (double)jilbParcer.AmountOfConditionalOp / (double)halsteadResult.TotalNumOfOperators;
			result.ListOfOperators = halsteadResult.ListOfOperators;

			return result;
		}
	}
}
