using System;
using Silo.Util;

namespace Silo.Components
{
    public class EightBitInput : Component
    {
        private byte _state;

        public EightBitInput() : base(0, 8)
        {
            _outPorts[0] = new Port(true);
            _outPorts[1] = new Port(true);
            _outPorts[2] = new Port(true);
            _outPorts[3] = new Port(true);
            _outPorts[4] = new Port(true);
            _outPorts[5] = new Port(true);
            _outPorts[6] = new Port(true);
            _outPorts[7] = new Port(true);
        }

        public byte State
        {
            get => _state;
            set
            {
                _state = value;
                var vals = _state.ConvertToBoolArray();

                _outPorts[0].State = vals[0];
                _outPorts[1].State = vals[1];
                _outPorts[2].State = vals[2];
                _outPorts[3].State = vals[3];
                _outPorts[4].State = vals[4];
                _outPorts[5].State = vals[5];
                _outPorts[6].State = vals[6];
                _outPorts[7].State = vals[7];

                _outPorts[0].Update();
                _outPorts[1].Update();
                _outPorts[2].Update();
                _outPorts[3].Update();
                _outPorts[4].Update();
                _outPorts[5].Update();
                _outPorts[6].Update();
                _outPorts[7].Update();
            }
        }

        public void AttachTo(Component component)
        {
            AttachTo(component, 0, 0);
            AttachTo(component, 1, 1);
            AttachTo(component, 2, 2);
            AttachTo(component, 3, 3);
            AttachTo(component, 4, 4);
            AttachTo(component, 5, 5);
            AttachTo(component, 6, 6);
            AttachTo(component, 7, 7);
        }

        public void AttachTo(Component component, int offset)
        {
            AttachTo(component, 0, 0 + offset);
            AttachTo(component, 1, 1 + offset);
            AttachTo(component, 2, 2 + offset);
            AttachTo(component, 3, 3 + offset);
            AttachTo(component, 4, 4 + offset);
            AttachTo(component, 5, 5 + offset);
            AttachTo(component, 6, 6 + offset);
            AttachTo(component, 7, 7 + offset);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}