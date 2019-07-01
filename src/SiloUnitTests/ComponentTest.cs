using NUnit.Framework;
using Silo;
using Silo.Components;
using Silo.Util;

namespace SiloUnitTests
{
    public class ComponentTest
    {
        [Test]
        public void SwitchStateTest()
        {
            var a = new Switch {State = true};
            Assert.AreEqual(true, a.State);
        }

        [Test]
        public void DisplayTest()
        {
            var dis = new EightBitDisplay();
            Assert.AreEqual(0, dis.Value);
            Assert.AreEqual("Value: 0", dis.ToString());
        }
        
        [Test]
        public void EightBitInputState()
        {
            var input = new EightBitInput {State = 20};

            Assert.AreEqual(20, input.State);
        }

        [Test]
        public void FailTest()
        {
            Component c = new Button();
            Assert.Catch(() => { c.DoUpdate(); });
            
            c = new Switch();
            Assert.Catch(() => { c.DoUpdate(); });

            c = new Clock(5.Hz());
            Assert.Catch(() => { c.DoUpdate(); });
            
            c = new EightBitInput();
            Assert.Catch(() => { c.DoUpdate(); });
        }
        
        [Test]
        public void ToStringTest()
        {
            var a = new Switch();
            Assert.AreEqual(a.ToString(), "False\n 0. ");
        }

        [Test]
        public void InverterTest()
        {
            var a = new Switch();
            var inv = new Inverter();
            
            a.AttachTo(inv, 0);
            Assert.IsTrue(inv.OutState());

            a.State = true;
            Assert.IsFalse(inv.OutState());
        }
    }
}