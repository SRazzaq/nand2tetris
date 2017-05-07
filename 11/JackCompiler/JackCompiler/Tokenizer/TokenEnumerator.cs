using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JackCompiler
{
    public class TokenEnumerator : IEnumerator<Token>
    {
        private string[] Keywords = {
                            "boolean",
                            "char",
                            "class",
                            "constructor",
                            "do",
                            "else",
                            "false",
                            "field",
                            "function",
                            "if",
                            "int",
                            "let",
                            "null",
                            "method",
                            "return",
                            "static",
                            "this",
                            "true",
                            "var",
                            "void",
                            "while"
                            };

        private char[] Symbols = {
                            '{', '}',
                            '(', ')',
                            '[', ']',
                            '.', ',',
                            '+', '-',
                            '*', '/',
                            '&', '|',
                            '<', '>',
                            '=', '~',
                            ';'
                            };

        private char[] Whitespace = { ' ', '\t', '\n', '\r' };

        private StreamReader reader;
        private Token current;

        public TokenEnumerator(StreamReader reader)
        {
            this.reader = reader;
        }

        public Token Current
        {
            get
            {
                return current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public bool MoveNext()
        {
            do
            {
                var ch = (char)reader.Peek();

                // Burn whitespaces.
                if (IsWhiteSpace(ch))
                {
                    ScanWhiteSpace(reader);
                    continue;
                }

                // Keyword and Identifier start with letter or underscore
                if (IsLetterOrUnderscore(ch))
                {
                    current = ScanIdent(reader);
                    return true;
                }

                // String Constant starts with quote
                if (IsQuote(ch))
                {
                    current = ScanString(reader);
                    return true;
                }

                // Integer Constant starts with number
                if (IsNumber(ch))
                {
                    current = ScanInteger(reader);
                    return true;
                }

                // Symbol
                if (IsSymbol(ch))
                {
                    ch = (char)reader.Read();
                    if (ch == '/')
                    {
                        // "//" Single line comment
                        if (reader.Peek() == '/')
                        {
                            ScanSingleLineComment(reader);
                            continue;
                        }

                        // "/*" Multi line comment
                        if (reader.Peek() == '*')
                        {
                            ScanMultiLineComment(reader);
                            continue;
                        }
                    }

                    current = new SymbolToken(ch.ToString());
                    return true;
                }

            } while (!reader.EndOfStream);

            return false;
        }

        public void Reset()
        {
            reader.BaseStream.Position = 0;
            reader.DiscardBufferedData();
        }

        public void Dispose()
        {
            // Do nothing
        }

        private bool IsWhiteSpace(char ch)
        {
            return Whitespace.Contains(ch);
        }

        private bool IsLetterOrUnderscore(char ch)
        {
            return char.IsLetter(ch) || ch == '_';
        }

        private bool IsQuote(char ch)
        {
            return ch == '"';
        }

        private bool IsNumber(char ch)
        {
            return char.IsNumber(ch);
        }

        private bool IsSymbol(char ch)
        {
            return Symbols.Contains(ch);
        }

        private bool IsKeyword(string ident)
        {
            return Keywords.Contains(ident);
        }


        private void ScanWhiteSpace(StreamReader reader)
        {
            // Burn whitespaces.
            reader.Read();
        }

        private void ScanSingleLineComment(StreamReader reader)
        {
            // Burn till end of line.
            reader.ReadLine();
        }

        private void ScanMultiLineComment(StreamReader reader)
        {
            // Burn starting asterisk
            reader.Read();

            do
            {
                // Read until asterisk is encountered.
                reader.ReadWhile(x => x != '*');
                if (reader.EndOfStream) throw new Exception("Unterminated Comment");

                // Burn ending asterisk
                reader.Read();
            } while (reader.Read() != '/');
        }

        private Token ScanIdent(StreamReader reader)
        {
            // Read all letters, underscores and numbers.
            var ident = reader.ReadWhile(x => IsLetterOrUnderscore(x) || IsNumber(x));

            if (IsKeyword(ident))
            {
                return new KeywordToken(ident);
            }
            else
            {
                return new IdentifierToken(ident);
            }
        }

        private Token ScanInteger(StreamReader reader)
        {
            // Read all numbers
            var integer = reader.ReadWhile(x => IsNumber(x));
            return new IntegerToken(integer);
        }

        private Token ScanString(StreamReader reader)
        {
            // Burn the starting quote
            reader.Read();

            var s = reader.ReadWhile(x => x != '"');
            if (reader.EndOfStream) throw new Exception("Unterminated String");

            // Burn the ending quote
            reader.Read();

            return new StringToken(s);
        }
    }

    internal static class ReaderExtension
    {
        internal static string ReadWhile(this StreamReader reader, Func<char, bool> condition)
        {
            var sb = new StringBuilder();

            char ch = (char)reader.Peek();
            while (!reader.EndOfStream && condition(ch))
            {
                sb.Append((char)reader.Read());
                ch = (char)reader.Peek();
            }

            return sb.ToString();
        }
    }
}
