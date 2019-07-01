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

        /// <summary>
        /// Attach the 0th output port of the device to an input port of another device
        /// </summary>
        /// <param name="comp">Target component</param>
        /// <param name="inPort">Input port on component to connect to</param>
        public void AttachTo(Component comp, int inPort)
        {
            AttachTo(comp, 0, inPort);
        }

        /// <summary>
        /// Attach an output port of the device to an input port of another device
        /// </summary>
        /// <param name="comp">Target component</param>
        /// <param name="outPort">Output port on device</param>
        /// <param name="inPort">Input port on component to connect to</param>
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

        /// <summary>
        /// Attach multiple outputs of one device to multiple inputs of another device
        /// </summary>
        /// <param name="comp">Target component</param>
        /// <param name="from">Start port on device</param>
        /// <param name="to">End port on device</param>
        /// <param name="compStart">Start port on component to connect to</param>
        /// <param name="offset">Offset on component to connect to</param>
        public void AttachRange(Component comp, int from, int to, int compStart = 0, int offset = 0)
        {
            for (var i = from; i <= to; i++)
            {
                AttachTo(comp, i, compStart + offset);
                compStart++;
            }
        }

        /// <summary>
        /// Update output port
        /// </summary>
        /// <param name="output">Output port to update</param>
        /// <param name="value">Value to update output port with</param>
        protected void UpdateOutput(int output, bool value)
        {
            var portType = typeof(Port);
            var piVal = portType.GetField("_state", BindingFlags.NonPublic | BindingFlags.Instance);
            var obj = OutPorts[output];
            piVal.SetValue(obj, value);

            OutPorts[output].Update();
        }

        /// <summary>
        /// Update output ports by range
        /// </summary>
        /// <param name="start">Start output port to update</param>
        /// <param name="end">End output port to update</param>
        /// <param name="value">Value to update output port with</param>
        protected void UpdateOutputRange(int start, int end, bool value)
        {
            for (var i = start; i <= end; i++)
            {
                UpdateOutput(i, value);
            }
        }

        /// <summary>
        /// Update output ports by value array
        /// </summary>
        /// <param name="start">Start output port to update</param>
        /// <param name="values">Values to update output ports with</param>
        protected void UpdateOutputRange(int start, bool[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                UpdateOutput(start + i, values[i]);
            }
        }
        
        /// <summary>
        /// Get the state of the 0th output port
        /// </summary>
        /// <returns>State of the output port</returns>
        public bool OutState()
        {
            return GetPortState(0);
        }

        /// <summary>
        /// Get state of an output port
        /// </summary>
        /// <param name="port">Port number</param>
        /// <returns>State of the port</returns>
        public bool GetPortState(int port)
        {
            return OutPorts[port].State;
        }

        /// <summary>
        /// Get state of an input port
        /// </summary>
        /// <param name="port">Port number</param>
        /// <returns>State of the port</returns>
        protected bool GetPortInState(int port)
        {
            return InPorts[port].State;
        }

        /// <summary>
        /// Set the state of an input port
        /// </summary>
        /// <param name="port">Port number</param>
        /// <param name="state">Value to update port with</param>
        public void SetPortState(int port, bool state)
        {
            InPorts[port].State = state;
            Update();
        }

        /// <summary>
        /// Represent the object as a string
        /// </summary>
        /// <returns>Status of the output ports</returns>
        public override string ToString()
        {
            return string.Join("\t", OutPorts.Select(a => a.State)) + "\n"
                 + string.Join("\t", Enumerable.Range(0, OutPorts.Count).Select(a => $" {a}. "));
        }

        /// <summary>
        /// Save the current input port state
        /// </summary>
        private void SaveCurrentState()
        {
            Last = Current.Select(a => a).ToArray();
        }

        public virtual void Update()
        {
            if (Current.SequenceEqual(Last))
            {
                //do nothing
                return;
            }
            
            DoUpdate();
            
            SaveCurrentState();
        }
        
        /// <summary>
        /// Update the component
        /// </summary>
        public abstract void DoUpdate();
    }
}