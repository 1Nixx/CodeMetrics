using Lab1ConsoleProg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg
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
