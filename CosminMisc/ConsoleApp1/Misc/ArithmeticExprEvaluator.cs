using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Misc
{
    static class ArithmeticExprEvaluator {
        public static void Test() {
            while (true) {
                string line = Console.ReadLine();
                var tokens = Parse(line);
                Console.WriteLine(string.Join(" ", tokens));
                Console.WriteLine();
            }
        }

        public static double Eval(string expr) {
            List<Token> tokens = Parse(expr);
            return 0;
        }

        private static List<Token> Parse(string expr) {
            List<Token> result = new List<Token>();
            double? crtNumber = null;

            for (int i = 0; i < expr.Length; i++) {
                char ch = expr[i];

                if (TryReadDigit(ch, out int digit))
                    crtNumber = (crtNumber ?? 0) * 10 + digit;
                else if (crtNumber.HasValue) {
                    result.Add(new Token(TokenType.Number, crtNumber));
                    crtNumber = null;
                }

                if (IsWhitespace(ch))
                    continue;
                else if (ch == '(')
                    result.Add(new Token(TokenType.LeftParen, "("));
                else if (ch == ')')
                    result.Add(new Token(TokenType.RightParen, ")"));
                else if (ch.IsOneOf("+-*/^"))
                    result.Add(new Token(TokenType.Operator, ch.ToString()));                
            }

            if (crtNumber.HasValue) 
                result.Add(new Token(TokenType.Number, crtNumber));

            return result;
        }

        private static bool TryReadDigit(char ch, out int digit) {
            digit = 0;
            if (ch >= '0' && ch <= '9') {
                digit = ch - '0';
                return true;
            }
            else
                return false;
        }

        private static bool IsWhitespace(char ch) => ch.IsOneOf(" \t\r\n");

        public static bool IsOneOf(this char ch, string chars) => chars.Contains(ch);
            
        private class Token
        {
            public TokenType Type { get; set; }
            public double? NumberValue { get; set; }
            public string OtherValue { get; set; }

            public Token(TokenType type, object value) {
                Type = type;
                if (type == TokenType.Number)
                    NumberValue = (double)value;
                else
                    OtherValue = value.ToString();
            }

            public override string ToString() {
                if (Type == TokenType.Number) {
                    if (NumberValue < 0)
                        return $"({NumberValue})";
                    else
                        return NumberValue.ToString();
                }
                else
                    return OtherValue;
            }
        }

        enum TokenType
        {
            Number,
            LeftParen,
            RightParen,
            Operator
        }
    }
}
