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
        public Inverter() : base(1, 1)
        {
        }

        public override void Update()
        {
            UpdateOutput(0, !GetPortInState(0));
        }
    }
}