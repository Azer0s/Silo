using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Silo.Util
{
    public class Frequency
    {
        private static readonly Regex ParseRegex = new Regex("([\\d]+\\.[\\d]+|[\\d]+) *(Hz|kHz)", RegexOptions.Compiled);

        private double value;
        private bool kHz;
        
        public static Frequency Parse(string frequency)
        {
            var f = new Frequency();
            if (!ParseRegex.IsMatch(frequency))
            {
                throw new FormatException("Invalid Hz format! Expected Hz or kHz after a decimal number!");
            }

            var groups = ParseRegex.Match(frequency).Groups.Select(a => a.Value).ToArray();
            if (groups[2] == "kHz")
            {
                f.kHz = true;
            }

            f.value = double.Parse(groups[1]);

            return f;
        }

        public TimeSpan ToTimeSpan()
        {
            var seconds = 1 / value;

            if (kHz)
            {
                seconds = 1 / (value * 1000);
            }

            return TimeSpan.FromSeconds(seconds);
        }

        public static implicit operator TimeSpan (Frequency f)
        {
            return f.ToTimeSpan();
        }
    }
}