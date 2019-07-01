using NUnit.Framework;
using Silo.Util;

namespace SiloUnitTests
{
    public class UtilTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStringFrequency()
        {
            Assert.AreEqual(Frequency.Parse("1 Hz").ToTimeSpan().TotalMilliseconds, 1000);
            Assert.AreEqual(Frequency.Parse("1 kHz").ToTimeSpan().TotalMilliseconds, 1);
            Assert.Catch(() => { Frequency.Parse("1 gHz"); });
        }
        
        [Test]
        public void TestIntFrequency()
        {
            Assert.AreEqual(1.Hz().ToTimeSpan().TotalMilliseconds, 1000);
            Assert.AreEqual(1.kHz().ToTimeSpan().TotalMilliseconds, 1);
        }

        [Test]
        public void TestDoubleFrequency()
        {
            Assert.AreEqual(1.0.Hz().ToTimeSpan().TotalMilliseconds, 1000);
            Assert.AreEqual(0.5.Hz().ToTimeSpan().TotalMilliseconds, 2000);
            Assert.AreEqual(0.25.kHz().ToTimeSpan().TotalMilliseconds, 4);
        }
    }
}