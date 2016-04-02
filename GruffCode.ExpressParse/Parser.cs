
namespace GruffCode.ExpressParse
{
    public class Parser
    {
        private readonly ITokenizer tokenizer;

        public Parser(ITokenizer tokenizer)
        {
            this.tokenizer = tokenizer;
        }

        public double Parse(string expression)
        {
            return 0f;
        }
    }
}
