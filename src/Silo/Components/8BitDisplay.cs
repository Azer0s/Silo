using System.Linq;
using Silo.Util;

namespace Silo.Components
{
    /// <summary>
    /// Input Map:<para/>
    /// 7: A<para/>
    /// 6: B<para/>
    /// 5: C<para/>
    /// 4: D<para/>
    /// 3: E<para/>
    /// 2: F<para/>
    /// 1: G<para/>
    /// 0: H<para/>
    /// </summary>
    public class EightBitDisplay : Component
    {
        /// <summary>
        /// Byte representation of 8 bit input
        /// </summary>
        public byte Value;

        /// <summary>
        /// Initialize a new 8 bit display
        /// </summary>
        public EightBitDisplay() : base(8, 0)
        {
        }

        /// <summary>
        /// Update the component
        /// </summary>
        public override void DoUpdate()
        {
            Value = InPorts.Select(a => a.State).ToArray().ConvertToByte();
        }

        /// <summary>
        /// Print the value of the display
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Value: {Value}";
        }
    }
}