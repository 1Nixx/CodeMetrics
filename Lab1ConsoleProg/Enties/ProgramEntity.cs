using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg.Enties
{
	class ProgramEntity
	{
		public readonly ProgramType type;
		public readonly string value;

		public ProgramEntity(ProgramType type, string value)
		{
			this.type = type;
			this.value = value;
		}

		public override string ToString()
		{
			return "{ type: " + type.ToString() + "; value: " + value + " }";
		}
	}
}
