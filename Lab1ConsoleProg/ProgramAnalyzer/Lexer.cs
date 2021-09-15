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
			SkipEmptyСharacters();
			if (IsIdentifier())
			{
				var token = GetIdentifier();
				return new Token(token.Item1, token.Item2);
			}
			else if (IsLiteral())
			{
				var token = GetLiteral();
				return new Token(token.Item1, token.Item2);
			}
			else if (IsOperator())
			{
				var token = GetOperator();
				return new Token(TokenType.OperatorPunctuator, token);
			}
			else
			{
				Console.WriteLine("Can't read symbol - {0}", _code[_currentPos]);
				return null;
			}
		}

		private (TokenType, string) GetIdentifier()
		{
			int startPos = _currentPos;

			if (_code[startPos] == '@')
				_currentPos++;

			if (char.IsLetter(_code[_currentPos]) || _code[_currentPos] == '_')
				_currentPos++;
			else
				throw new FormatException();

			while (char.IsLetterOrDigit(_code[_currentPos]))
				_currentPos++;

			string lexem = _code.Substring(startPos, _currentPos - startPos);

			TokenType tokenType = IsKeyword(lexem) ? TokenType.Keyword : TokenType.Identifier;

			return (tokenType, lexem);
		}

		private string GetOperator()
		{
			throw new NotImplementedException();
		}

		private string GetKeyword()
		{
			throw new NotImplementedException();
		}

		private (TokenType, string) GetLiteral()
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

		private bool IsKeyword(string word)
		{
			throw new NotImplementedException();
		}

		private bool IsIdentifier()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// !!!Протестировать
		/// </summary>
		private void SkipEmptyСharacters()
		{
			List<char> lineTerminators = new List<char> { '\u000D', '\u000A', '\u0085', '\u2028', '\u2029', ' ' };
			while (lineTerminators.Contains(_code[_currentPos]) || _code[_currentPos] == '/' || _code[_currentPos] == '#')
			{
				_currentPos++;
				if (_code[_currentPos] == '/' && _code[_currentPos + 1] == '/')
					SkipLine();

				if (_code[_currentPos] == '/' && _code[_currentPos + 1] == '*')
					SkipLongComment();

				if (_code[_currentPos] == '#')
					SkipLine();
			}
			
		}

		private void SkipLongComment()
		{
			throw new NotImplementedException();
		}

		private void SkipLine()
		{
			throw new NotImplementedException();
		}
	}
}
