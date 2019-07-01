using System.Linq;
using Silo.Util;

namespace Silo.Memory
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: Reset<para/>
    /// 1: Load (hi) or count (lo)<para/>
    /// 2: Count up (hi) or down (lo)<para/>
    /// 3: Do count<para/>
    /// 4: Clk<para/>
    /// 12: A1<para/>
    /// 11: B1<para/>
    /// 10: C1<para/>
    /// 9: D1<para/>
    /// 8: E1<para/>
    /// 7: F1<para/>
    /// 6: G1<para/>
    /// 5: H1<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Overflow<para/>
    /// 8: A<para/>
    /// 7: B<para/>
    /// 6: C<para/>
    /// 5: D<para/>
    /// 4: E<para/>
    /// 3: F<para/>
    /// 2: G<para/>
    /// 1: H<para/>
    /// </summary>
    public class Counter : Component
    {
        public Counter() : base(13, 9)
        {
        }

        public override void DoUpdate()
        {
            if (Current[0]) //Reset
            {
                UpdateOutput(0, false);
                UpdateOutputRange(1, 8, false);
                return;
            }

            if (Current[4] != Last[4])
            {
                if (Current[4])
                {
                    if (Current[1]) //Load or count
                    {
                        UpdateOutput(0, false);
                        UpdateOutputRange(1, Current.Skip(5).ToArray());
                    }
                    else
                    {
                        if (Current[3])
                        {
                            var currentVal = Enumerable.Range(1, 8).Select(GetPortState).ToArray().ConvertToByte();
                            if (Current[2]) //Up or down
                            {
                                if (currentVal != byte.MaxValue)
                                {
                                    currentVal++;
                                }
                            }
                            else
                            {
                                //Down
                                currentVal--;
                            }
                            
                            UpdateOutput(0, currentVal == byte.MaxValue);

                            var vals = currentVal.ConvertToBoolArray();
                            UpdateOutputRange(1, vals);
                        }
                    }
                }
            }
        }
    }
}