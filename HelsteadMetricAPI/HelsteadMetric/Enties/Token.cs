using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg
{
	class Token
	{
		public TokenType Type { get; private set; }
		public string Value { get; private set; }

		public Token(TokenType type, string value)
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
