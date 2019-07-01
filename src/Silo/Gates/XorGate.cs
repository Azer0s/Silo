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
    public class XorGate : Component
    {
        /// <summary>
        /// Initialize new XOR Gate
        /// </summary>
        public XorGate() : base(2, 1)
        {
        }

        /// <summary>
        /// Update the component
        /// </summary>
        public override void DoUpdate()
        {
            UpdateOutput(0, GetPortInState(0) ^ GetPortInState(1));
        }
    }
}