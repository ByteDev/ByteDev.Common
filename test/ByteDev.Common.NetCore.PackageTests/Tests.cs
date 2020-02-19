using NUnit.Framework;

namespace ByteDev.Common.NetCore.PackageTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void WhenStartsWithValue_ThenRemoveStartingValue()
        {
            var result = "My name is John".RemoveStartsWith("My");

            Assert.That(result, Is.EqualTo(" name is John"));
        }
    }
}