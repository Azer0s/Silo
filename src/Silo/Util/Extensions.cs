using System;
// ReSharper disable InconsistentNaming

namespace Silo.Util
{
    public static class Extensions
    {
        public static Frequency Hz(this int a)
        {
            return Frequency.Parse($"{a} Hz");
        }

        public static Frequency Hz(this double a)
        {
            return Frequency.Parse($"{a} Hz");
        }
        
        public static Frequency kHz(this int a)
        {
            return Frequency.Parse($"{a} kHz");
        }

        public static Frequency kHz(this double a)
        {
            return Frequency.Parse($"{a} kHz");
        }
        
        public static bool[] ConvertToBoolArray(this byte b)
        {
            // prepare the return result
            var result = new bool[8];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (var i = 0; i < 8; i++)
                result[i] = (b & (1 << i)) != 0;

            // reverse the array
            Array.Reverse(result);

            return result;
        }

        public static byte ConvertToByte(this bool[] source)
        {
            byte result = 0;
            // This assumes the array never contains more than 8 elements!
            var index = 8 - source.Length;

            // Loop through the array
            foreach (var b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (byte) (1 << (7 - index));

                index++;
            }

            return result;
        }
    }
}