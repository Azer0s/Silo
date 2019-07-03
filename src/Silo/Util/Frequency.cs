using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Silo.Util
{
    /// <summary>
    /// Helper to manage Hz and kHz frequencies
    /// </summary>
    public class Frequency
    {
        private static readonly Regex ParseRegex = new Regex("([\\d]+\\.[\\d]+|[\\d]+) *(Hz|kHz)", RegexOptions.Compiled);
        private bool kHz;

        private double value;

        /// <summary>
        /// Parse a string and returns a Frequency object
        /// </summary>
        /// <param name="frequency">Frequency as string</param>
        /// <returns>Frequency object</returns>
        /// <exception cref="FormatException">Throws if the string is not a (decimal) number followed by either Hz other kHz</exception>
        public static Frequency Parse(string frequency)
        {
            var f = new Frequency();
            if (!ParseRegex.IsMatch(frequency))
            {
                throw new FormatException("Invalid Hz format! Expected Hz or kHz after a decimal number!");
            }

            var groups = ParseRegex.Match(frequency).Groups.Cast<Group>().Select(a => a.Value).ToArray();
            if (groups[2] == "kHz")
            {
                f.kHz = true;
            }

            f.value = double.Parse(groups[1]);

            return f;
        }

        /// <summary>
        /// Convert a frequency to a Timespan
        /// </summary>
        /// <returns>The frequency as a Timespan</returns>
        public TimeSpan ToTimeSpan()
        {
            var seconds = 1 / value;

            if (kHz)
            {
                seconds = 1 / (value * 1000);
            }

            return TimeSpan.FromMilliseconds(seconds * 1000);
        }

        /// <summary>
        /// Implicitly convert a frequency to a Timespan
        /// </summary>
        /// <param name="f">Frequency</param>
        /// <returns>The frequency as a Timespan</returns>
        public static implicit operator TimeSpan(Frequency f)
        {
            return f.ToTimeSpan();
        }
    }
}