using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg.Enties
{
	class ProgramEntity
	{
		public ProgramType Type { get; private set; }
		public string Value { get; private set; }

		public ProgramEntity(ProgramType type, string value)
		{
			this.Type = type;
			this.Value = value;
		}

		public override string ToString()
		{
			return "{ type: " + Type.ToString() + "; value: " + Value + " }";
		}
	}
}
