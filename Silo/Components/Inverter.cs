namespace Silo.Components
{
    public class Inverter : Component
    {
        public Inverter() : base(1, 1)
        {
        }

        public override void Update()
        {
            UpdateOutput(0, !_inPorts[0].State);
        }
    }
}