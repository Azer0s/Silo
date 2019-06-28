using System;

namespace Silo.Components
{
    public class Switch : Component
    {
        public Switch() : base(0, 1)
        {
            OutPorts[0] = new Port(true);
        }

        public bool State
        {
            get => OutPorts[0].State;
            set
            {
                OutPorts[0].State = value;
                OutPorts[0].Update();
            }
        }

        public override void DoUpdate()
        {
            throw new NotImplementedException();
        }
    }
}