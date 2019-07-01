using System;

namespace Silo.Components
{
    /// <summary>
    /// Button component. Outputs a signal once when clicked.
    /// </summary>
    public class Button : Component
    {
        /// <summary>
        /// Initialize new button component
        /// </summary>
        public Button() : base(0, 1)
        {
            OutPorts[0] = new Port(true);
        }

        /// <summary>
        /// Click the button once. <para/>
        /// Sets the output to hi, updates the component, sets the output back to lo and updates the component again.
        /// </summary>
        public void Click()
        {
            OutPorts[0].State = true;
            OutPorts[0].Update();
            OutPorts[0].State = false;
            OutPorts[0].Update();
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