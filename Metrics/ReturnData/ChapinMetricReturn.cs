using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Metrics.ChapinMetric
{
	public class ChapinMetricReturn
	{
		public Dictionary<string, List<string>> chapinTypes { get; set; }

		public Dictionary<string, List<string>> chapinIOTypes { get; set; }
	}
}