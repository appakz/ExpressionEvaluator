using NUnit.Framework;

namespace GruffCode.ExpressParse.Tests
{
    [TestFixture]
    public class ParserTests
    {
        private Parser SUT;
        private double result;

        [SetUp]
        public void TestSetUp()
        {
            SUT = new Parser();
        }

        [Test]
        public void ParseSingleIntegerTest()    
        {
            result = SUT.Parse("2");
            Assert.AreEqual(2, result);
        }
        
        [Test]
        [ExpectedException(typeof(InvalidExpressionException))]
        public void ParseInvalidExpressionTest()
        {
            SUT.Parse("blah");
        }
    }
}
