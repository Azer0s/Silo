using Silo.Gates;

namespace Silo.Devices
{
    public class HalfAdder : Component
    {
        private readonly Component _and;
        private readonly Component _xor;

        public HalfAdder() : base(2, 2)
        {
            _xor = new XorGate();
            _and = new AndGate();
        }

        public override void Update()
        {
            _xor.SetPortState(0, GetInPortState(0));
            _xor.SetPortState(1, GetInPortState(1));

            _and.SetPortState(0, GetInPortState(0));
            _and.SetPortState(1, GetInPortState(1));

            _xor.Update();
            _and.Update();

            UpdateOutput(0, _xor.OutState());
            UpdateOutput(1, _and.OutState());
        }
    }
}