using Metrics.Halstead;
using System.Collections.Generic;

namespace Metrics.ReturnData
{
	public class HalsteadMetricReturn
	{
		public List<OperandsAndOperators> ListOfOperands { get; set; }
		public List<OperandsAndOperators> ListOfOperators { get; set; }
		public int NumOfUniqueOperands { get; set; }
		public int NumOfUniqueOperators { get; set; }
		public int TotalNumOfOperands { get; set; }
		public int TotalNumOfOperators { get; set; }
		public int DictionaryOfProgram { get; set; }
		public int LenOfProgram { get; set; }
		public int VolumeOfProgram { get; set; }

	}
} 
