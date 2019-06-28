using System;

namespace Silo.Components
{
    public class Trap : Component
    {
        private readonly Action<bool> OnInput;
        
        public Trap(Action<bool> OnInput) : base(1, 0)
        {
            this.OnInput = OnInput;
        }

        public override void Update()
        {
            DoUpdate();
        }

        public override void DoUpdate()
        {
            OnInput(GetPortInState(0));
        }
    }
}