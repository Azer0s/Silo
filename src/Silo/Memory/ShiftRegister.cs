namespace Silo.Memory
{
    /// <summary>
    /// 4 bit shift register
    /// </summary>
    public class ShiftRegister4 : Component
    {
        /// <summary>
        /// <para>Input Map:</para>
        /// 0: Serial data in<para/>
        /// 1: Clk<para/>
        /// 2: Reset<para/>
        /// <para/>
        /// Output Map:<para/>
        /// 0: Output<para/>
        /// 1: Inverted Output<para/>
        /// </summary>
        public ShiftRegister4() : base(3, 2)
        {
            _d1.AttachTo(_d2, 0);
            _d2.AttachTo(_d3, 0);
            _d3.AttachTo(_d4, 0);
            
            UpdateOutput(1, true);
        }
        
        private readonly Component _d1 = new DFlipFlop();
        private readonly Component _d2 = new DFlipFlop();
        private readonly Component _d3 = new DFlipFlop();
        private readonly Component _d4 = new DFlipFlop();

        /// <summary>
        /// Update the component
        /// If pin 2 is hi, reset. Stay in reset as long as pin 2 is hi.<para/>
        /// If the clock (pin 1) was updated and is rising edge, update the component.<para/>
        /// </summary>
        public override void DoUpdate()
        {
            if (Current[2])
            {
                _d4.SetPortState(2, Current[2]);
                _d3.SetPortState(2, Current[2]);
                _d2.SetPortState(2, Current[2]);
                _d1.SetPortState(2, Current[2]);
            }
            
            //Clock updated
            if (Current[1] != Last[1])
            {
                _d4.SetPortState(1, Current[1]);
                _d3.SetPortState(1, Current[1]);
                _d2.SetPortState(1, Current[1]);
                _d1.SetPortState(0, GetPortInState(0));
                _d1.SetPortState(1, Current[1]);

                if (Current[1])
                {

                    UpdateOutput(0, _d4.OutState());
                    UpdateOutput(1, !_d4.OutState());
                }
            }
        }
    }
}