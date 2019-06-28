using System.Linq;
using Silo.Util;

namespace Silo.Components
{
    public class EightBitDisplay : Component
    {
        public byte Value;
        
        public EightBitDisplay() : base(8, 0)
        {
        }

        public override void Update()
        {
            Value = _inPorts.Select(a => a.State).ToArray().ConvertToByte();
        }

        public override string ToString()
        {
            return $"Value: {Value}";
        }
    }
}