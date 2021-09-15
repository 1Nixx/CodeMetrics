﻿using System;
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
				return new Token(TokenType.Operator, token);
			}	
			else if (IsPunctuator())
			{
				var token = GetPunctuator();
				return new Token(TokenType.Punctuator, token);
			}
			else
			{
				Console.WriteLine("Can't read symbol - {0}", _code[_currentPos]);
				return null;
			}
		}

		#region GetToken
		private string GetPunctuator()
		{
			return _code[_currentPos++].ToString();
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
			StringBuilder stringBuilder = new StringBuilder();
			while (IsOperator())
			{
				stringBuilder.Append(_code[_currentPos]);
				_currentPos++;
			}
			return stringBuilder.ToString();
		}

		private (TokenType, string) GetLiteral()
		{
			if (_code[_currentPos] == 't' || _code[_currentPos] == 'f' || _code[_currentPos] == 'n')
			{
				int pos = _currentPos;
				var lexem = new StringBuilder();
				while (char.IsLetter(_code[pos]))
				{
					lexem.Append(_code[pos]);
					pos++;
				}
				if (lexem.ToString() == "true" || lexem.ToString() == "false")
				{
					_currentPos = pos;
					return (TokenType.BoolLiteral, lexem.ToString());
				}
				else if (lexem.ToString() == "null")
				{
					_currentPos = pos;
					return (TokenType.NullLiteral, lexem.ToString());
				}
			}

			if (_code[_currentPos] == '\'')
			{
				int startPos = _currentPos;
				_currentPos++;
				while (_code[_currentPos] != '\'')
				{
					_currentPos++;
				}
				//Why +1??? Test
				return (TokenType.CharacterLiteral, _code.Substring(startPos, _currentPos - startPos + 1));
			}

			#warning Do normal string handler
			if (_code[_currentPos] == '"')
			{
				int startPos = _currentPos;
				_currentPos++;
				while (_code[_currentPos] != '"')
				{
					_currentPos++;
				}
				return (TokenType.StringLiteral, _code.Substring(startPos, _currentPos - startPos + 1));
			}

			#warning REDO number handler
			if (char.IsDigit(_code[_currentPos]) || _code[_currentPos] == '.')
			{
				int startPos = _currentPos;
				_currentPos++;
				while (char.IsDigit(_code[_currentPos]) || _code[_currentPos] == '.')
				{
					_currentPos++;
				}
				return (TokenType.NumberLiteral, _code.Substring(startPos, _currentPos - startPos));
			}
			return (TokenType.InterpolatedStringLiteral, null);
		}
		
		#endregion

		#region IsTokenType
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

		private bool IsPunctuator()
		{
			throw new NotImplementedException();
		}

		private bool IsIdentifier()
		{
			throw new NotImplementedException();
		}

		#endregion

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
