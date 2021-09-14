using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg.ProgramAnalyzer
{
	class Lexer
	{
		private readonly string _code;
		private int _currentPos = 0;

		public Lexer(string analyzedCode)
		{
			_code = analyzedCode;
		}

		public List<Token> TokenizeCode()
		{
			var tokenList = new List<Token>();
			while (_currentPos < _code.Length)
			{
				var token = GetNextToken();
				tokenList.Add(token);
			}
			return tokenList;
		}

		private Token GetNextToken()
		{
			SkipEmptyСharacter();
			if (IsIdentifier())
			{
				var token = GetIdentifier();
				return new Token(TokenType.Identifier, token);
			}
			else if (IsKeyword())
			{
				var token = GetKeyword();
				return new Token(TokenType.Keyword, token);
			}
			else if(IsLiteral())
			{
				var token = GetLiteral();
				return new Token(token.Item1, token.Item2);
			}
			else if(IsOperator())
			{
				var token = GetOperator();
				return new Token(TokenType.OperatorPunctuator, token);
			}
			else
			{
				Console.WriteLine("Can't read symbol - %c", _code[_currentPos]);
				return null;
			}
		}

		private string GetIdentifier()
		{
			throw new NotImplementedException();
		}

		private string GetOperator()
		{
			throw new NotImplementedException();
		}

		private bool IsOperator()
		{
			throw new NotImplementedException();
		}

		private bool IsLiteral()
		{
			throw new NotImplementedException();
		}

		private string GetKeyword()
		{
			throw new NotImplementedException();
		}

		private bool IsKeyword()
		{
			throw new NotImplementedException();
		}

		private (TokenType, string) GetLiteral()
		{
			throw new NotImplementedException();
		}

		private bool IsIdentifier()
		{
			throw new NotImplementedException();
		}

		private void SkipEmptyСharacter()
		{
			throw new NotImplementedException();
		}
	}
}
