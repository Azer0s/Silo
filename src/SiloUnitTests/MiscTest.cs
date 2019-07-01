using NUnit.Framework;
using Silo;

namespace SiloUnitTests
{
    public class MiscTest
    {
        [Test]
        public void MutablePortTest()
        {
            var p = new Port(false);
            Assert.Catch(() => { p.State = true; });
        }
    }
}