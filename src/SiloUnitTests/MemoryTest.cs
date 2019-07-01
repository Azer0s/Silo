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
            var reset = new Switch();
            var mem = new SRFlipFlop();
            
            a.AttachTo(mem, 0);
            b.AttachTo(mem, 1);
            button.AttachTo(mem, 2);
            reset.AttachTo(mem, 3);

            a.State = true;
            button.Click();
            
            Assert.IsTrue(mem.OutState());

            b.State = true;
            button.Click();
            Assert.IsTrue(mem.OutState());

            a.State = false;
            button.Click();
            Assert.IsFalse(mem.OutState());

            reset.State = true;
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
            
            serial.State = true;
            button.Click();

            serial.State = false;
            button.Click();
            
            serial.State = true;
            button.Click();
            
            var rst = new Button();
            rst.AttachTo(shift, 2);
            rst.Click();
            
            button.Click();
            Assert.IsFalse(shift.OutState());
            button.Click();
            Assert.IsFalse(shift.OutState());
            button.Click();
            Assert.IsFalse(shift.OutState());
            button.Click();
            Assert.IsFalse(shift.OutState());
        }

        [Test]
        public void CounterTest()
        {
            var reset = new Switch();
            var loadOrCount = new Switch();
            var upOrDown = new Switch();
            var countToggle = new Switch();
            var input = new EightBitInput();
            var clock = new Button();
            var counter = new Counter();
            var display  = new EightBitDisplay();
            
            reset.AttachTo(counter, 0);
            loadOrCount.AttachTo(counter, 1);
            upOrDown.AttachTo(counter, 2);
            countToggle.AttachTo(counter, 3);
            clock.AttachTo(counter, 4);
            input.AttachTo(counter, 5);
            
            counter.AttachRange(display, 1, 8);

            loadOrCount.State = true;
            input.State = 50;
            clock.Click();
            
            Assert.AreEqual(50, display.Value);

            loadOrCount.State = false;
            upOrDown.State = true;
            countToggle.State = true;
            clock.Click();
            
            Assert.AreEqual(51, display.Value);

            upOrDown.State = false;
            clock.Click();
            
            Assert.AreEqual(50, display.Value);

            reset.State = true;
            Assert.AreEqual(0, display.Value);

            reset.State = false;
            loadOrCount.State = true;
            input.State = 254;
            clock.Click();

            Assert.IsFalse(counter.OutState());

            loadOrCount.State = false;
            countToggle.State = true;
            upOrDown.State = true;
            clock.Click();
            clock.Click();
            
            Assert.IsTrue(counter.OutState());
        }
    }
}