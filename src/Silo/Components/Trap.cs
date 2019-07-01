using System;

namespace Silo.Components
{
    /// <summary>
    /// Takes in an action and triggers it when the input is updated
    /// </summary>
    public class Trap : Component
    {
        private readonly Action<bool> _onInput;
        
        /// <summary>
        /// Initialize new trap component
        /// </summary>
        /// <param name="onInput">Action that is triggered when the input is updated</param>
        public Trap(Action<bool> onInput) : base(1, 0)
        {
            _onInput = onInput;
        }

        /// <summary>
        /// Trap update. Triggers even if the previous state is the same as the current state.
        /// </summary>
        public override void Update()
        {
            DoUpdate();
        }

        /// <summary>
        /// Update the component
        /// </summary>
        public override void DoUpdate()
        {
            _onInput(GetPortInState(0));
        }
    }
}