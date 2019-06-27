using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Silo
{
    public abstract class Component
    {
        protected readonly List<Port> _inPorts;
        protected readonly List<Port> _outPorts;

        protected Component(int inPorts, int outPorts)
        {
            _inPorts = new List<Port>(inPorts);
            _outPorts = new List<Port>(outPorts);

            for (var i = 0; i < inPorts; i++)
            {
                _inPorts.Add(new Port(true));
            }

            for (var i = 0; i < outPorts; i++)
            {
                _outPorts.Add(new Port(false));
            }
        }

        public void AttachTo(Component comp, int inPort)
        {
            AttachTo(comp, 0, inPort);
        }

        public void AttachTo(Component comp, int outPort, int inPort)
        {
            _outPorts[outPort].OnPortUpdate += () =>
            {
                comp._inPorts[inPort].State = _outPorts[outPort].State;
                comp.Update();
            };
            comp._inPorts[inPort].State = _outPorts[outPort].State;
            comp.Update();
        }

        protected void UpdateOutput(int output, bool value)
        {
            var portType = typeof(Port);
            var piVal = portType.GetField("_state", BindingFlags.NonPublic | BindingFlags.Instance);
            var obj = _outPorts[output];
            piVal.SetValue(obj, value);

            _outPorts[output].Update();
        }

        public bool OutState()
        {
            return GetPortState(0);
        }

        public bool GetPortState(int port)
        {
            return _outPorts[port].State;
        }

        protected bool GetPortInState(int port, int offset = 0)
        {
            return _inPorts[port + offset].State;
        }

        public void SetPortState(int port, bool state)
        {
            _inPorts[port].State = state;
            Update();
        }

        public override string ToString()
        {
            return string.Join("\t", _outPorts.Select(a => a.State)) + "\n"
                 + string.Join("\t", Enumerable.Range(0, _outPorts.Count).Select(a => $" {a}. "));
        }

        public abstract void Update();
    }
}