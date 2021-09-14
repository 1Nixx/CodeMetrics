using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg
{
	class Token
	{
		public readonly TokenType type;
		public readonly string value;

		public Token(TokenType type, string value)
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
