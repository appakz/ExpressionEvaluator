using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;

namespace GruffCode.ExpressParse.Tests
{
    [TestFixture]
    public class ParserTests
    {
        private ITokenizer tokenizer;
        private Parser SUT;
        private IList<ExpressionToken> tokens = new List<ExpressionToken>();
        private double result;

        [SetUp]
        public void TestSetUp()
        {
            var mockRepo = new MockRepository();
            tokenizer = mockRepo.StrictMock<ITokenizer>();
            tokens.Clear();
            tokenizer.Stub(t => t.Tokenize(Arg<string>.Is.Anything)).Return(tokens);
            SUT = new Parser(tokenizer);
        }

        [Test]
        public void ParseSingleIntegerTest()
        {
            tokens.Add(new ExpressionToken(ExpressionTokenType.Value, 2));
            result = SUT.Parse("2");
            Assert.AreEqual(2, result);
        }
        
        [Test]
        [ExpectedException(typeof(InvalidExpressionException))]
        public void ParseInvalidExpressionTest()
        {
            SUT.Parse("blah");
        }

        [Test]
        public void ParseSingleAdditionOperand()
        {
            result = SUT.Parse("2 + 3");
            Assert.AreEqual(5, result);
        }
    }
}
