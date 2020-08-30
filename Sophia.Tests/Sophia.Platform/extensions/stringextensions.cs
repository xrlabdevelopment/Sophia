using Sophia.Platform.Extension;

using NUnit.Framework;

namespace Sophia.Tests.Platform
{
    [TestFixture]
    public class testsuit_StringExtensions
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_StringExtensions_splitCamelCase()
        {
            string s = string.Empty;

            Assert.That(s.splitCamelCase(), Is.EqualTo(s));

            s = "SomethingInCamelCase";

            Assert.That(s.splitCamelCase(), Is.EqualTo("Something In Camel Case"));
        }
    }
}
