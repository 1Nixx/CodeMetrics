using Metrics.Halstead;
using System.Collections.Generic;

namespace Metrics.ReturnData
{
	public class JilbMetricReturn
	{
		public int AmountOfConditionalOperators { get; set; }
		public double RelativeComplexityOfProgram { get; set; }
		public int MaximumNestingLevel { get; set; }
		public List<OperandsAndOperators> ListOfOperators { get; set; }
	}
}
