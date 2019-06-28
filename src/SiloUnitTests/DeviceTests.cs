using System.Collections.Generic;
using NUnit.Framework;
using Silo;
using Silo.Components;
using Silo.Devices;

namespace SiloUnitTests
{
    public class DeviceTests
    {
        private void DeviceTest(Component device, int inputs, bool[][] inputVals, bool[][] results)
        {
            var switches = new List<Switch>();

            for (var i = 0; i < inputs; i++)
            {
                switches.Add(new Switch());
                switches[i].AttachTo(device, i);
            }

            for (var i = 0; i < inputVals.Length; i++)
            {
                for (var j = 0; j < switches.Count; j++)
                {
                    switches[j].State = inputVals[i][j];
                }

                for (var j = 0; j < results[i].Length; j++)
                {
                    Assert.AreEqual(device.GetPortState(j), results[i][j]);
                }
            }
        }
       
        [Test]
        public void HalfAdderTest()
        {
            DeviceTest(new HalfAdder(), 2, 
            new []
            {
                new[] {true, true},
                new[] {true, false},
                new[] {false, true},
                new[] {false, false}
            }, 
            new []
            {
                new []{false, true},
                new []{true, false},
                new []{true, false},
                new []{false, false}
            });
        }

        [Test]
        public void FullAdderTest()
        {
            DeviceTest(new FullAdder(), 3,
            new []
            {
                new []{false, false, false},
                new []{false, false, true},
                new []{false, true, false},
                new []{false, true, true},
                new []{true, false, false},
                new []{true, false, true},
                new []{true, true, false},
                new []{true, true, true}
            },
            new []
            {
                new []{false, false},
                new []{true, false},
                new []{true, false},
                new []{false, true},
                new []{true, false},
                new []{false, true},
                new []{false, true},
                new []{true, true}
            });
        }

        [Test]
        public void EightBitAdderTest()
        {
            var a = new EightBitInput();
            var b = new EightBitInput();
            var add = new EightBitAdder();
            var display = new EightBitDisplay();
            
            a.AttachTo(add);
            b.AttachTo(add, 8);
            
            add.AttachToAll(display);

            for (byte i = 0; i < 127; i++)
            {
                for (byte j = 0; j < 127; j++)
                {
                    a.State = i;
                    b.State = j;
                    
                    Assert.AreEqual(display.Value, i + j);
                }
            }
        }
    }
}