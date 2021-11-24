using System;
using System.Collections.Generic;
using System.Text;

namespace Metrics.ChapinMetric
{
	class Variable
	{
		public VariableType Type { get; private set; }
		public string Value { get; private set; }

		public Variable(VariableType type, string value)
		{
			this.Type = type;
			this.Value = value;
		}
	}
}
