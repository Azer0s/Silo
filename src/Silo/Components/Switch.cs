using System;

namespace Silo.Components
{
    /// <summary>
    /// Switch component. Outputs a constant signal based on the switch state.
    /// </summary>
    public class Switch : Component
    {
        /// <summary>
        /// Initialize new switch component
        /// </summary>
        public Switch() : base(0, 1)
        {
            OutPorts[0] = new Port(true);
        }

        /// <summary>
        /// Switch output state
        /// </summary>
        public bool State
        {
            get => OutPorts[0].State;
            set
            {
                OutPorts[0].State = value;
                OutPorts[0].Update();
            }
        }

        /// <summary>
        /// Update the component
        /// </summary>
        /// <exception cref="NotImplementedException">This component doesn't have/need a DoUpdate implementation</exception>
        public override void DoUpdate()
        {
            throw new NotImplementedException();
        }
    }
}