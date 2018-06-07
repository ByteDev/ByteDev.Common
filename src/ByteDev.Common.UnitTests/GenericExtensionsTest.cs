using NUnit.Framework;

namespace ByteDev.Common.UnitTests
{
    [TestFixture]
    public class GenericExtensionsTest
    {
        [TestFixture]
        public class In
        {
            [Test]
            public void WhenStringContainsOneOfListItems_ThenReturnTrue()
            {
                const string sut = "John";

                var result = sut.In("John", "Peter", "Paul");

                Assert.True(result);
            }

            [Test]
            public void WhenStringDoesNotContainAnyListItems_ThenReturnFalse()
            {
                const string sut = "John Smith";

                var result = sut.In("John", "Peter", "Paul");

                Assert.False(result);
            }
        }
    }
}
