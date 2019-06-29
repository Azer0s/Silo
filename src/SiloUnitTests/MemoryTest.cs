using System.Collections.Generic;
using NUnit.Framework;
using Silo.Components;
using Silo.Memory;

namespace SiloUnitTests
{
    public class MemoryTest
    {
        [Test]
        public void DFlipFlopTest()
        {
            var a = new Switch();
            var button = new Button();
            var mem = new DFlipFlop();
            
            a.AttachTo(mem, 0);
            button.AttachTo(mem, 1);
            
            Assert.IsFalse(mem.OutState());
            Assert.IsTrue(mem.GetPortState(1));

            a.State = true;
            button.Click();
            
            Assert.IsTrue(mem.OutState());
            Assert.IsFalse(mem.GetPortState(1));

            a.State = false;
            button.Click();
            
            Assert.IsFalse(mem.OutState());
            Assert.IsTrue(mem.GetPortState(1));
        }

        [Test]
        public void SRFlipFlopTest()
        {
            var a = new Switch();
            var b = new Switch();
            var button = new Button();
            var mem = new SRFlipFlop();
            
            a.AttachTo(mem, 0);
            b.AttachTo(mem, 1);
            button.AttachTo(mem, 2);

            a.State = true;
            button.Click();
            
            Assert.IsTrue(mem.OutState());

            b.State = true;
            button.Click();
            Assert.IsTrue(mem.OutState());

            a.State = false;
            button.Click();
            Assert.IsFalse(mem.OutState());
        }

        [Test]
        public void ShiftRegister4Test()
        {
            var serial = new Switch();
            var button = new Button();
            var shift = new ShiftRegister4();

            var bools = new List<bool>();
            
            var trap = new Trap(b1 =>
            {
                bools.Add(b1);
            });
            
            serial.AttachTo(shift, 0);
            button.AttachTo(shift, 1);
            shift.AttachTo(trap, 0);
            
            serial.State = true;
            button.Click();

            serial.State = false;
            button.Click();
            
            serial.State = true;
            button.Click();
            
            serial.State = false;
            button.Click();
            Assert.IsTrue(shift.OutState());

            button.Click();
            button.Click();
            Assert.IsTrue(shift.OutState());
        }
    }
}