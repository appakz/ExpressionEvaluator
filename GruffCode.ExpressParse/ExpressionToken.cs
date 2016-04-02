namespace GruffCode.ExpressParse
{
    public class ExpressionToken
    {
        public ExpressionToken(ExpressionTokenType type, double value)
        {
            Value = value;
            TokenType = type;
        }

        public ExpressionTokenType TokenType { get; }

        public double Value { get; }
    }
}