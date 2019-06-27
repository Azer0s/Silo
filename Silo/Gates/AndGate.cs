namespace Silo.Components
{
    public class AndGate : Component
    {
        protected override void Update()
        {
            UpdateOutput(0, _inPorts[0].State && _inPorts[1].State);
        }

        public AndGate() : base(2, 1)
        {
        }
    }
}