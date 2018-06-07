using System.Text;
using ByteDev.Common.Encoding;
using NUnit.Framework;

namespace ByteDev.Common.UnitTests.Encoding
{
    [TestFixture]
    public class HexTest
    {
        [TestFixture]
        public class ConvertToHex
        {
            [TestCase("", "")]
            [TestCase("John", "4A6F686E")]
            [TestCase("John Smith", "4A6F686E20536D697468")]
            public void WhenTextSupplied_ThenConvertToHex(string text, string expected)
            {
                var bytes = new UTF8Encoding().GetBytes(text);

                var result = Hex.ConvertToHex(bytes);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class ConvertToBytes
        {
            [TestCase("", "")]
            [TestCase("4A6F686E", "John")]
            [TestCase("4A6F686E20536D697468", "John Smith")]
            public void WhenTextSupplied_ThenConvertToHex(string hex, string expected)
            {
                var bytes = Hex.ConvertToBytes(hex);

                var result = new UTF8Encoding().GetString(bytes);

                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}
