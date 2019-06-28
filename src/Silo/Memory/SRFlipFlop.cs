using System.Linq;

namespace Silo.Memory
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: Set<para/>
    /// 1: Reset<para/>
    /// 2: Clk<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Output<para/>
    /// 1: Inverted Output<para/>
    /// </summary>
    public class SRFlipFlop : Component
    {
        public SRFlipFlop() : base(3,2)
        {
            UpdateOutput(1, true);
        }

        public override void Update()
        {
            if (Current.SequenceEqual(Last))
            {
                //do nothing
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
            
            SaveCurrentState();
        }
    }
}