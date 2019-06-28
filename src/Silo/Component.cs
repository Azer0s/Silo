using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Silo
{
    public abstract class Component
    {
        protected readonly List<Port> InPorts;
        protected readonly List<Port> OutPorts;

        //Last input
        protected bool[] Last = { };
        //Current input
        protected bool[] Current => InPorts.Select(a => a.State).ToArray();

        protected Component(int inPorts, int outPorts)
        {
            InPorts = new List<Port>(inPorts);
            OutPorts = new List<Port>(outPorts);

            var lasts = new List<bool>();
            
            for (var i = 0; i < inPorts; i++)
            {
                lasts.Add(false);
                InPorts.Add(new Port(true));
            }

            for (var i = 0; i < outPorts; i++)
            {
                OutPorts.Add(new Port(false));
            }

            Last = lasts.ToArray();
        }

        public void AttachTo(Component comp, int inPort)
        {
            AttachTo(comp, 0, inPort);
        }

        public void AttachTo(Component comp, int outPort, int inPort)
        {
            OutPorts[outPort].OnPortUpdate += () =>
            {
                comp.InPorts[inPort].State = OutPorts[outPort].State;
                comp.Update();
            };
            comp.InPorts[inPort].State = OutPorts[outPort].State;
            comp.Update();
        }

        protected void UpdateOutput(int output, bool value)
        {
            var portType = typeof(Port);
            var piVal = portType.GetField("_state", BindingFlags.NonPublic | BindingFlags.Instance);
            var obj = OutPorts[output];
            piVal.SetValue(obj, value);

            OutPorts[output].Update();
        }

        public bool OutState()
        {
            return GetPortState(0);
        }

        public bool GetPortState(int port)
        {
            return OutPorts[port].State;
        }

        protected bool GetPortInState(int port, int offset = 0)
        {
            return InPorts[port + offset].State;
        }

        public void SetPortState(int port, bool state)
        {
            InPorts[port].State = state;
            Update();
        }

        public override string ToString()
        {
            return string.Join("\t", OutPorts.Select(a => a.State)) + "\n"
                 + string.Join("\t", Enumerable.Range(0, OutPorts.Count).Select(a => $" {a}. "));
        }

        protected void SaveCurrentState()
        {
            Last = Current.Select(a => a).ToArray();
        }
        
        public abstract void Update();
    }
}