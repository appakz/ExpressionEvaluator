using System.Collections.Generic;

namespace GruffCode.ExpressParse
{
    public class Tokenizer : ITokenizer
    {
        private readonly IDictionary<string, Operator> _operatorLookup = new Dictionary<string, Operator>
        {
            { "+", Operator.Addition },
            { "-", Operator.Subtraction },
            { "*", Operator.Multiplication },
            { "/", Operator.Division }
        }; 

        public IEnumerable<ExpressionToken> Tokenize(string expression)
        {
            var tokens = new List<ExpressionToken>();

            foreach (var expressionPart in SplitExpression(expression))
            {
                if (_operatorLookup.ContainsKey(expressionPart))
                {
                    tokens.Add(CreateOperatorToken(_operatorLookup[expressionPart]));
                }
                else
                {
                    tokens.Add(CreateValueToken(expressionPart));
                }
            }

            return tokens;
        }

        private IEnumerable<string> SplitExpression(string expression)
        {
            var expressionCharIndex = 0;
            var expressionParts = new List<string>();

            while (expressionCharIndex < expression.Length)
            {
                var expressionChar = expression[expressionCharIndex];
                if (IsWhiteSpace(expressionChar))
                {
                    expressionCharIndex++;
                    continue;
                }

                if (IsOperator(expressionChar))
                {
                    expressionCharIndex++;
                    expressionParts.Add(expressionChar.ToString());
                }
                else
                {
                    int nextBreakingCharIndex;
                    for (nextBreakingCharIndex = expressionCharIndex + 1; nextBreakingCharIndex < expression.Length; nextBreakingCharIndex++)
                    {
                        var nextChar = expression[nextBreakingCharIndex];
                        if (IsWhiteSpace(nextChar) || IsOperator(nextChar))
                        {
                            break;
                        }
                    }

                    expressionParts.Add(expression.Substring(expressionCharIndex, nextBreakingCharIndex - expressionCharIndex));
                    expressionCharIndex = nextBreakingCharIndex;
                }
            }

            return expressionParts;
        }

        private bool IsOperator(char expressionChar)
        {
            return _operatorLookup.ContainsKey(expressionChar.ToString());
        }

        private bool IsWhiteSpace(char expressionChar)
        {
            return expressionChar == ' ';
        }

        private ExpressionToken CreateOperatorToken(Operator operatorValue)
        {
            return new ExpressionToken(ExpressionTokenType.Operator, (double)operatorValue);
        }

        private ExpressionToken CreateValueToken(string expressionPart)
        {
            double parseResult;
            if (!double.TryParse(expressionPart, out parseResult))
            {
                throw new InvalidExpressionException();
            }

            return new ExpressionToken(ExpressionTokenType.Value, parseResult);
        }
    }
}