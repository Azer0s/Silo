using System;

namespace Silo
{
    public class Port
    {
        public delegate void PortUpdateHandler();

        private bool _state;

        public Port(bool mutable)
        {
            _mutable = mutable;
        }

        public bool State
        {
            get => _state;
            set
            {
                if (_mutable)
                {
                    _state = value;
                }
                else
                {
                    throw new Exception("Can't modify state of an immutable port!");
                }
            }
        }

        private bool _mutable { get; }
        public event PortUpdateHandler OnPortUpdate;

        public void Update()
        {
            OnPortUpdate?.Invoke();
        }
    }
}
