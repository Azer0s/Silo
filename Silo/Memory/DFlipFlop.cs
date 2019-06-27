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

        //Last input
        private bool[] _last = { false, false };
        
        public override void Update()
        {
            var current = _inPorts.Select(a => a.State).ToList();
            if (current.SequenceEqual(_last))
            {
                //do nothing
                return;
            }

            if (current[0] == _last[0])
            {
                //value stayed the same
                //clock changed

                if (current[1])
                {
                    UpdateOutput(0, current[0]);
                    UpdateOutput(1, !current[0]);
                }
            }
            else
            {
                //clock stayed the same
                //do nothing
            }
            
            _last = current.ToArray();
        }
    }
}