using System;

namespace Silo.Components
{
    public class Switch : Component
    {
        public Switch() : base(0, 1)
        {
            _outPorts[0] = new Port(true);
        }

        public bool State
        {
            get => _outPorts[0].State;
            set
            {
                _outPorts[0].State = value;
                _outPorts[0].Update();
            }
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}