using System.Linq;

namespace Silo.Memory
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: Set<para/>
    /// 1: Reset<para/>
    /// 2: Clk<para/>
    /// 3: Async Reset<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Output<para/>
    /// 1: Inverted Output<para/>
    /// </summary>
    public class SRFlipFlop : Component
    {
        /// <summary>
        /// Initialize new SR-Flip-FLop
        /// </summary>
        public SRFlipFlop() : base(4, 2)
        {
            UpdateOutput(1, true);
        }

        /// <summary>
        /// Update the component
        /// If pin 3 is hi, reset. Stay in reset as long as pin 2 is hi.<para/>
        /// If the clock (pin 2) was updated and is rising edge, update the component.<para/>
        /// If pin 0 is hi, set the output to hi.<para/>
        /// * Else: If pin 1 is hi, set the output to lo
        /// </summary>
        public override void DoUpdate()
        {
            if (Current[3])
            {
                UpdateOutput(0, false);
                UpdateOutput(1, true);
                return;
            }
            
            if (Current[2] != Last[2])
            {
                if (Current[2])
                {
                    if (Current[0])
                    {
                        //Set output to hi
                        UpdateOutput(0, true);
                        UpdateOutput(1, false);
                    }
                    else if (Current[1])
                    {
                        //Set output to lo
                        UpdateOutput(0, false);
                        UpdateOutput(1, true);
                    }
                }
            }
        }
    }
}