using System;

namespace Silo
{
    /// <summary>
    /// Port class
    /// </summary>
    public class Port
    {
        /// <summary>
        /// Delegate type for port updates
        /// </summary>
        public delegate void PortUpdateHandler();

        private bool _state;

        /// <summary>
        /// Initialize a port
        /// </summary>
        /// <param name="mutable">Set the mutability state of the port</param>
        public Port(bool mutable)
        {
            Mutable = mutable;
        }

        /// <summary>
        /// Port state
        /// </summary>
        /// <exception cref="Exception">Throws if the port is immutable and a modification attempt is made</exception>
        public bool State
        {
            get => _state;
            set
            {
                if (Mutable)
                {
                    _state = value;
                }
                else
                {
                    throw new Exception("Can't modify state of an immutable port!");
                }
            }
        }

        /// <summary>
        /// True if the port is mutable
        /// </summary>
        private bool Mutable { get; }
        
        /// <summary>
        /// Port update event
        /// </summary>
        public event PortUpdateHandler OnPortUpdate;

        /// <summary>
        /// Update the port and all components attached to it
        /// </summary>
        public void Update()
        {
            OnPortUpdate?.Invoke();
        }
    }
}
