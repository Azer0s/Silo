using System.Collections.Generic;
using NUnit.Framework;
using Silo;
using Silo.Components;
using Silo.Gates;

namespace SiloUnitTests
{
    public class GateTests
    {
        private readonly bool[][] _truthTable =
        {
            new[] {true, true},
            new[] {true, false},
            new[] {false, true},
            new[] {false, false}
        };

        private void GateTest(Component gate, IReadOnlyList<bool> results)
        {
            var a = new Switch();
            var b = new Switch();
            
            a.AttachTo(gate, 0);
            b.AttachTo(gate, 1);
            
            for (var i = 0; i < _truthTable.Length; i++)
            {
                a.State = _truthTable[i][0];
                b.State = _truthTable[i][1];
                
                Assert.AreEqual(gate.OutState(), results[i]);
            }
        }

        [Test]
        public void AndTest()
        {
            GateTest(new AndGate(), new[] {true, false, false, false});
        }

        [Test]
        public void NandTest()
        {
            GateTest(new NandGate(), new[] {false, true, true, true});
        }

        [Test]
        public void OrTest()
        {
            GateTest(new OrGate(), new[] {true, true, true, false});
        }

        [Test]
        public void XorTest()
        {
            GateTest(new XorGate(), new []{false, true, true, false});
        }
        
        [Test]
        public void XnorTest()
        {
            GateTest(new XnorGate(), new []{true, false, false, true});
        }
    }
}