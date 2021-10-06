using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1ConsoleProg.ProgramAnalyzer
{
#warning Fix bug out of range exceprion in _currentPos++ and  etc.
	public class Lexer
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
				if (token != null)
				{
					tokenList.Add(token);
				}
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
				Console.WriteLine("Can't read symbol - {0}, {1}", _code[_currentPos], _currentPos);
				_currentPos++;
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

			while (char.IsLetterOrDigit(_code[_currentPos]) || _code[_currentPos] == '_')
				_currentPos++;

			string lexem = _code.Substring(startPos, _currentPos - startPos);

			TokenType tokenType = IsKeyword(lexem) ? TokenType.Keyword : TokenType.Identifier;

			if (tokenType == TokenType.Keyword)
			{
				if (" true false ".Contains(" " + lexem + " "))
				{
					return (TokenType.BoolLiteral, lexem);
				}
				else if (" null ".Contains(" " + lexem + " "))
				{
					return (TokenType.NullLiteral, lexem);
				}
			}

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
					if (_code[_currentPos] == '\\')
					{
						_currentPos++;
					}
					_currentPos++;
				}
				_currentPos++;
				return (TokenType.CharacterLiteral, _code.Substring(startPos, _currentPos - startPos));
			}

			if (_code[_currentPos] == '"')
			{
				int startPos = _currentPos;
				_currentPos++;
				while (_code[_currentPos] != '"')
				{
					if (_code[_currentPos] == '\\')
					{
						_currentPos++;
					}
					_currentPos++;
				}
				_currentPos++;
				return (TokenType.StringLiteral, _code.Substring(startPos, _currentPos - startPos));
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
			return ".:+-*/%&|^!~=<>?".Contains(_code[_currentPos]) ? true : false;
		}
		
		private bool IsLiteral()
		{
			if (char.IsLetter(_code[_currentPos]) && "tfn'\"".Contains(_code[_currentPos]))
			{
				return true;
			}
			else if (char.IsDigit(_code[_currentPos]) || (_code[_currentPos] == '.' && char.IsDigit(_code[_currentPos+1])))
			{
				return true;
			}
			else if (_code[_currentPos] == '\'' || _code[_currentPos] == '\"')
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private bool IsKeyword(string word)
		{
			return (" abstract as base bool break byte case catch char checked class const continue decimal default delegate do double else " +
			"enum event explicit extern finally fixed float for foreach goto if implicit in int interface internal is lock long namespace" +
			" new object operator out override params private protected public readonly ref return sbyte sealed short sizeof stackalloc static" +
			" string struct switch this throw try typeof uint ulong unchecked unsafe ushort using virtual void volatile while true false null ").Contains(" " + word + " ") ? true : false;
		}

		private bool IsPunctuator()
		{
			return "{}[](),;".Contains(_code[_currentPos]) ? true : false;
		}

		private bool IsIdentifier()
		{
			if (char.IsLetter(_code[_currentPos]) || ((_code[_currentPos] == '@') && (char.IsLetter(_code[_currentPos + 1]) || _code[_currentPos + 1] == '_')) || _code[_currentPos] == '_')
				return true;
			else
				return false;
	
		}

		#endregion

		private void SkipEmptyСharacters()
		{
			List<char> lineTerminators = new List<char> { '\u000D', '\u000A', '\u0085', '\u2028', '\u2029', ' ', '\t', '\n' };
			while (_currentPos < _code.Length && (lineTerminators.Contains(_code[_currentPos]) || (_code[_currentPos] == '/' && _code[_currentPos + 1] == '/') || (_code[_currentPos] == '/' && _code[_currentPos + 1] == '*') || (_code[_currentPos] == '#')))
			{
				if (_code[_currentPos] == '/' && _code[_currentPos + 1] == '/')
					SkipLine();
				else if (_code[_currentPos] == '/' && _code[_currentPos + 1] == '*')
					SkipLongComment();
				else if (_code[_currentPos] == '#')
					SkipLine();
				else
					_currentPos++;
			}			
		}

		private void SkipLongComment()
		{
			_currentPos += 2;
			while (_code[_currentPos] != '*' || _code[_currentPos+1] != '/')
			{
				_currentPos++;
			}
			_currentPos += 2;
		}

		private void SkipLine()
		{
			while (_code[_currentPos] != '\n')
			{
				_currentPos++;
			}
			_currentPos++;
		}
	}
}
