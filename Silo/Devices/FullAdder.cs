using Silo.Gates;

namespace Silo.Devices
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: A<para/>
    /// 1: B<para/>
    /// 2: Carry In<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Sum<para/>
    /// 1: Carry<para/>
    /// </summary>
    public class FullAdder : Component
    {
        private readonly Component _and1;
        private readonly Component _and2;
        private readonly Component _or;
        private readonly Component _xor1;
        private readonly Component _xor2;

        public FullAdder() : base(3, 2)
        {
            _xor1 = new XorGate();
            _xor2 = new XorGate();
            _and1 = new AndGate();
            _and2 = new AndGate();
            _or = new OrGate();

            _xor1.AttachTo(_xor2, 0);
            _xor1.AttachTo(_and2, 0);
            _and2.AttachTo(_or, 0);
            _and1.AttachTo(_or, 1);
        }

        public override void Update()
        {
            _xor1.SetPortState(0, GetPortInState(0));
            _xor1.SetPortState(1, GetPortInState(1));

            _and1.SetPortState(0, GetPortInState(0));
            _and1.SetPortState(1, GetPortInState(1));

            _xor2.SetPortState(1, GetPortInState(2));
            _and2.SetPortState(1, GetPortInState(2));

            UpdateOutput(0, _xor2.OutState());
            UpdateOutput(1, _or.OutState());
        }
    }
}