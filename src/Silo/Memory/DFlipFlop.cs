using System.Linq;

namespace Silo.Memory
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: A<para/>
    /// 1: Clk<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Output<para/>
    /// 1: Inverted Output<para/>
    /// </summary>
    public class DFlipFlop : Component
    {
        public DFlipFlop() : base(2,2)
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

            if (Current[0] == Last[0])
            {
                //value stayed the same
                //clock changed

                if (Current[1])
                {
                    UpdateOutput(0, Current[0]);
                    UpdateOutput(1, !Current[0]);
                }
            }
            else
            {
                //clock stayed the same
                //do nothing
            }
            
            SaveCurrentState();
        }
    }
}