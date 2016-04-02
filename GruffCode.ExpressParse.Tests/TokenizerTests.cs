using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GruffCode.ExpressParse.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        private Tokenizer SUT;
        private string input;
        private IList<ExpressionToken> result;

        [SetUp]
        public void SetUp()
        {
            SUT = new Tokenizer();
        }

        private void RunTest()
        {
            result = SUT.Tokenize(input).ToList();
        }

        [Test]
        public void TokenizeSingleValue()
        {
            input = "2";
            RunTest();
            Assert.AreEqual(1, result.Count, "A single token should be returned in the result.");
            AssertValueToken(result[0], 2);
        }

        [Test]
        public void TokenizeTwoValues()
        {
            input = "2 5.2";
            RunTest();
            Assert.AreEqual(2, result.Count);
            AssertValueToken(result[0], 2);
            AssertValueToken(result[1], 5.2);
        }

        [Test]
        public void TokenizeMultipleValues()
        {
            input = "2 5.2 6.89 1232.56732";
            RunTest();
            Assert.AreEqual(4, result.Count);
            AssertValueToken(result[0], 2);
            AssertValueToken(result[1], 5.2);
            AssertValueToken(result[2], 6.89);
            AssertValueToken(result[3], 1232.56732);
        }

        [Test]
        public void TokenizeWithExtraSpaceBetweenExpressionParts()
        {
            input = "2    5  9      10";
            RunTest();
            Assert.AreEqual(4, result.Count);
            AssertValueToken(result[0], 2);
            AssertValueToken(result[1], 5);
            AssertValueToken(result[2], 9);
            AssertValueToken(result[3], 10);
        }

        [Test]
        public void TokenizeWithValuesAndAdditionOperator()
        {
            input = "2 + 5";
            RunTest();
            AssertOperatorToken(result[1], Operator.Addition);
        }

        [Test]
        public void TokenizeWithValuesAndSubtractionOperator()
        {
            input = "10 - 8";
            RunTest();
            AssertOperatorToken(result[1], Operator.Subtraction);
        }

        [Test]
        public void TokenizeWithValuesAndMultiplicationOperator()
        {
            input = "7 * 2";
            RunTest();
            AssertOperatorToken(result[1], Operator.Multiplication);
        }

        [Test]
        public void TokenizeWithValuesAndDivisionOperator()
        {
            input = " 8 / 2";
            RunTest();
            AssertOperatorToken(result[1], Operator.Division);
        }

        [Test]
        public void TokenizeWithValuesAndOperatorWithoutWhitepsace()
        {
            input = "2+5";
            RunTest();
            AssertOperatorToken(result[1], Operator.Addition);
        }

        private void AssertValueToken(ExpressionToken token, double expectedValue)
        {
            Assert.AreEqual(ExpressionTokenType.Value, token.TokenType, "The token in the result should be a 'value' token.");
            Assert.AreEqual(expectedValue, token.Value, $"The token in the result should have a value of '{expectedValue}'");
        }

        private void AssertOperatorToken(ExpressionToken token, Operator expectedOperator)
        {
            Assert.AreEqual(ExpressionTokenType.Operator, token.TokenType, "The token in the result should be an 'operator' token.");
            Assert.AreEqual(expectedOperator, (Operator)token.Value);
        }
    }
}