using System;
using System.Timers;

namespace Silo.Components
{
    public class Clock : Component
    {
        public Clock(TimeSpan frequency) : base(0, 1)
        {
            var timer = new Timer();
            timer.Elapsed += (sender, args) =>
            {
                UpdateOutput(0, !GetPortState(0));
                _outPorts[0].Update();
            };
            timer.Interval = frequency.TotalMilliseconds;
            timer.Start();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}