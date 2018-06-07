using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class DoubleExtensionsTest
    {
        [TestFixture]
        public class ToMoneyString
        {
            [TestCase(10, ExpectedResult = "£10.00")]
            [TestCase(10.2, ExpectedResult = "£10.20")]
            [TestCase(10.29, ExpectedResult = "£10.29")]
            [TestCase(10.291, ExpectedResult = "£10.29")]
            [TestCase(10.295, ExpectedResult = "£10.30")]
            public string WhenDouble_ThenReturnMoneyString(double sut)
            {
                return sut.ToMoneyString();
            }
        }
    }
}
