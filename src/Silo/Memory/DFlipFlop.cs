namespace Silo.Memory
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: A<para/>
    /// 1: Clk<para/>
    /// 2: Async Reset<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Output<para/>
    /// 1: Inverted Output<para/>
    /// </summary>
    public class DFlipFlop : Component
    {
        public DFlipFlop() : base(3, 2)
        {
            UpdateOutput(1, true);
        }

        public override void DoUpdate()
        {
            if (Current[2])
            {
                UpdateOutput(0, false);
                UpdateOutput(1, true);
                return;
            }
            
            if (Current[1] != Last[1])
            {
                //value stayed the same
                //clock changed

                if (Current[1])
                {
                    UpdateOutput(0, Current[0]);
                    UpdateOutput(1, !Current[0]);
                }
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                //clock stayed the same
                //do nothing
            }
        }
    }
}