namespace Silo.Gates
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: A<para/>
    /// 1: B<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Output<para/>
    /// </summary>
    public class NandGate : Component
    {
        public NandGate() : base(2, 1)
        {
        }

        public override void Update()
        {
            UpdateOutput(0, !(_inPorts[0].State && _inPorts[1].State));
        }
    }
}