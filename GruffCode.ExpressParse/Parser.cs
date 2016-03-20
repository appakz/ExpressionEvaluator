
namespace GruffCode.ExpressParse
{
    public class Parser
    {
        public double Parse(string expression)
        {
            double parseResult;
            if (!double.TryParse(expression, out parseResult))
            {
                throw new InvalidExpressionException();
            }

            return parseResult;
        }
    }
}
