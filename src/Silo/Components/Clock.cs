using System;
using System.Timers;

namespace Silo.Components
{
    /// <summary>
    /// Clock component. Ticks based on a Frequency or TimeSpan.
    /// </summary>
    public class Clock : Component
    {
        /// <summary>
        /// Initialize new clock
        /// </summary>
        /// <param name="frequency">Frequency or TimeSpan the clock should tick</param>
        public Clock(TimeSpan frequency) : base(0, 1)
        {
            var timer = new Timer();
            timer.Elapsed += (sender, args) =>
            {
                UpdateOutput(0, !GetPortState(0));
                OutPorts[0].Update();
            };
            timer.Interval = frequency.TotalMilliseconds;
            timer.Start();
        }

        /// <summary>
        /// Update the component
        /// </summary>
        /// <exception cref="NotImplementedException">This component doesn't have/need a DoUpdate implementation</exception>
        public override void DoUpdate()
        {
            throw new NotImplementedException();
        }
    }
}