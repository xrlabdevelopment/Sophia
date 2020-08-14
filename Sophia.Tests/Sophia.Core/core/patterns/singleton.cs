using Sophia.Core.Patterns;

using NUnit.Framework;

namespace Sophia.Tests.Core
{
    [TestFixture]
    public class testsuit_Singleton
    {
        internal class MySingleton : Singleton<MySingleton>
        { }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_singleton_create_destroy()
        {
            MySingleton.createInstance();

            Assert.That(MySingleton.Instance, Is.Not.Null);

            MySingleton.destroyInstance();

#if DEBUG
            Assert.That(MySingleton.Instance, Is.Not.Null);
#else
            Assert.That(MySingleton.Instance, Is.Null);
#endif

        }
    }
}
