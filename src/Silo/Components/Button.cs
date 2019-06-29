namespace Silo.Components
{
    public class Button : Component
    {
        public Button() : base(0, 1)
        {
            OutPorts[0] = new Port(true);
        }

        public void Click()
        {
            OutPorts[0].State = true;
            OutPorts[0].Update();
            OutPorts[0].State = false;
            OutPorts[0].Update();
        }

        public override void DoUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}