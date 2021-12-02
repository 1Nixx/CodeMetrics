using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Metrics.ChapinMetric
{
	public class ChapinMetricReturn
	{
		public Dictionary<string, List<string>> ChapinTypes { get; set; }
		public Dictionary<string, int> VariableCount { get; set; }
		public Dictionary<string, List<string>> ChapinIOTypes { get; set; }
		public Dictionary<string, int> VariableIOCount { get; set; }
		public int MetricResult { get; set; }
		public int MetricIOResult { get; set; }
	}
}