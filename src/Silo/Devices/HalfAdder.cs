using Silo.Gates;

namespace Silo.Devices
{
    /// <summary>
    /// <para>Input Map:</para>
    /// 0: A<para/>
    /// 1: B<para/>
    /// <para/>
    /// Output Map:<para/>
    /// 0: Sum<para/>
    /// 1: Carry<para/>
    /// </summary>
    public class HalfAdder : Component
    {
        #region Subcomponents

        private readonly Component _and;
        private readonly Component _xor;

        #endregion
        
        /// <summary>
        /// Initialize new Half Adder
        /// </summary>
        public HalfAdder() : base(2, 2)
        {
            _xor = new XorGate();
            _and = new AndGate();
        }

        /// <summary>
        /// Update the component
        /// </summary>
        public override void DoUpdate()
        {
            _xor.SetPortState(0, GetPortInState(0));
            _xor.SetPortState(1, GetPortInState(1));

            _and.SetPortState(0, GetPortInState(0));
            _and.SetPortState(1, GetPortInState(1));

            _xor.Update();
            _and.Update();

            UpdateOutput(0, _xor.OutState());
            UpdateOutput(1, _and.OutState());
        }
    }
}