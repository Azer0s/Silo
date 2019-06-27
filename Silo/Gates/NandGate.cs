namespace Silo.Gates
{
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