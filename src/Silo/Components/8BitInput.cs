using System;
using Silo.Util;

namespace Silo.Components
{
    /// <summary>
    /// Output Map:<para/>
    /// 7: A<para/>
    /// 6: B<para/>
    /// 5: C<para/>
    /// 4: D<para/>
    /// 3: E<para/>
    /// 2: F<para/>
    /// 1: G<para/>
    /// 0: H<para/>
    /// </summary>
    public class EightBitInput : Component
    {
        private byte _state;

        /// <summary>
        /// Initialize new 8 bit input
        /// </summary>
        public EightBitInput() : base(0, 8)
        {
            OutPorts[0] = new Port(true);
            OutPorts[1] = new Port(true);
            OutPorts[2] = new Port(true);
            OutPorts[3] = new Port(true);
            OutPorts[4] = new Port(true);
            OutPorts[5] = new Port(true);
            OutPorts[6] = new Port(true);
            OutPorts[7] = new Port(true);
        }

        /// <summary>
        /// State of the output pins as a byte
        /// </summary>
        public byte State
        {
            get => _state;
            set
            {
                _state = value;
                var vals = _state.ConvertToBoolArray();

                OutPorts[0].State = vals[0];
                OutPorts[1].State = vals[1];
                OutPorts[2].State = vals[2];
                OutPorts[3].State = vals[3];
                OutPorts[4].State = vals[4];
                OutPorts[5].State = vals[5];
                OutPorts[6].State = vals[6];
                OutPorts[7].State = vals[7];

                OutPorts[0].Update();
                OutPorts[1].Update();
                OutPorts[2].Update();
                OutPorts[3].Update();
                OutPorts[4].Update();
                OutPorts[5].Update();
                OutPorts[6].Update();
                OutPorts[7].Update();
            }
        }

        /// <summary>
        /// Attach the output pins to another component
        /// </summary>
        /// <param name="component">Target component</param>
        /// <param name="offset">Offset on target component</param>
        public new void AttachTo(Component component, int offset = 0)
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