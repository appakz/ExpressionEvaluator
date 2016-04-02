using System.Collections.Generic;

namespace GruffCode.ExpressParse
{
    public interface ITokenizer
    {
        IEnumerable<ExpressionToken> Tokenize(string expression);
    }
}