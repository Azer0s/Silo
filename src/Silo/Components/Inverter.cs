namespace Silo.Components
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: A<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Output<para/>
    /// </summary>
    public class Inverter : Component
    {
        /// <summary>
        /// Initialize new inverter
        /// </summary>
        public Inverter() : base(1, 1)
        {
            UpdateOutput(0, true);
        }

        /// <summary>
        /// Update the component
        /// </summary>
        public override void DoUpdate()
        {
            UpdateOutput(0, !GetPortInState(0));
        }
    }
}