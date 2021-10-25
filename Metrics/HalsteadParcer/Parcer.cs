using System.Collections.Generic;
using Halstead.Enties;
using Metrics.Halstead;

namespace Halstead.ProgramAnalyzer
{
	public class Parcer
	{
		public readonly List<ProgramEntity> parcedCode;
		public Parcer(IParcer parcer)
		{
			parcedCode = parcer.Parce();
		}
	}
}
