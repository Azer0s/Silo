using Silo.Components;

namespace Silo.Devices
{
    public class EightBitAdder : Component
    {
        private readonly Component _fa1;
        private readonly Component _fa2;
        private readonly Component _fa3;
        private readonly Component _fa4;
        private readonly Component _fa5;
        private readonly Component _fa6;
        private readonly Component _fa7;
        private readonly Component _ha;

        public EightBitAdder() : base(16, 8)
        {
            _ha = new HalfAdder();
            _fa1 = new FullAdder();
            _fa2 = new FullAdder();
            _fa3 = new FullAdder();
            _fa4 = new FullAdder();
            _fa5 = new FullAdder();
            _fa6 = new FullAdder();
            _fa7 = new FullAdder();

            _ha.AttachTo(_fa1, 1, 2);
            _fa1.AttachTo(_fa2, 1, 2);
            _fa2.AttachTo(_fa3, 1, 2);
            _fa3.AttachTo(_fa4, 1, 2);
            _fa4.AttachTo(_fa5, 1, 2);
            _fa5.AttachTo(_fa6, 1, 2);
            _fa6.AttachTo(_fa7, 1, 2);
        }

        public void AttachToFull(Component comp, int offset = 0)
        {
            AttachTo(comp, 0, 0 + offset);
            AttachTo(comp, 1, 1 + offset);
            AttachTo(comp, 2, 2 + offset);
            AttachTo(comp, 3, 3 + offset);
            AttachTo(comp, 4, 4 + offset);
            AttachTo(comp, 5, 5 + offset);
            AttachTo(comp, 6, 6 + offset);
            AttachTo(comp, 7, 7 + offset);
        }
        
        public override void Update()
        {
            _ha.SetPortState(0, GetInPortState(7));
            _ha.SetPortState(1, GetInPortState(7, 8));

            _fa1.SetPortState(0, GetInPortState(6));
            _fa1.SetPortState(1, GetInPortState(6, 8));

            _fa2.SetPortState(0, GetInPortState(5));
            _fa2.SetPortState(1, GetInPortState(5, 8));

            _fa3.SetPortState(0, GetInPortState(4));
            _fa3.SetPortState(1, GetInPortState(4, 8));

            _fa4.SetPortState(0, GetInPortState(3));
            _fa4.SetPortState(1, GetInPortState(3, 8));

            _fa5.SetPortState(0, GetInPortState(2));
            _fa5.SetPortState(1, GetInPortState(2, 8));

            _fa6.SetPortState(0, GetInPortState(1));
            _fa6.SetPortState(1, GetInPortState(1, 8));

            _fa7.SetPortState(0, GetInPortState(0));
            _fa7.SetPortState(1, GetInPortState(0, 8));

            UpdateOutput(0, _fa7.OutState());
            UpdateOutput(1, _fa6.OutState());
            UpdateOutput(2, _fa5.OutState());
            UpdateOutput(3, _fa4.OutState());
            UpdateOutput(4, _fa3.OutState());
            UpdateOutput(5, _fa2.OutState());
            UpdateOutput(6, _fa1.OutState());
            UpdateOutput(7, _ha.OutState());
        }
    }
}