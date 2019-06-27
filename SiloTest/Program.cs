using System;
using Silo.Components;
using Silo.Devices;
using Silo.Gates;

namespace SiloTest
{
    internal static class Program
    {
        private static void Main()
        {
            var a = new Switch();
            var b = new Switch();
            var c = new Switch();

            #region AndGate

            var inv = new Inverter();

            b.AttachTo(inv, 0);

            var andGate = new AndGate();

            a.AttachTo(andGate, 0);
            inv.AttachTo(andGate, 1);

            a.State = true;
            b.State = false;

            Console.WriteLine(andGate);

            #endregion

            #region 8 bit input

            var inp = new EightBitInput {State = 200};

            Console.WriteLine(inp);

            #endregion
            
            #region FullAdder

            a.State = false;
            b.State = false;

            var add = new FullAdder();

            a.AttachTo(add, 0);
            b.AttachTo(add, 1);
            c.AttachTo(add, 2);

            a.State = true;

            Console.WriteLine(add);

            b.State = true;

            Console.WriteLine(add);

            b.State = false;
            c.State = true;

            Console.WriteLine(add);

            #endregion

            #region 8 bit Adder

            var in1 = new EightBitInput();
            var in2 = new EightBitInput();
            
            var add1 = new EightBitAdder();
            
            in1.AttachTo(add1);
            in2.AttachTo(add1, 8);

            in1.State = 10;
            in2.State = 5;

            Console.WriteLine(add1);
            
            var display = new EightBitDisplay();
            add1.AttachToFull(display);
            Console.WriteLine(display.Value);

            #endregion

            #region Dual 8 bit adder

            var in3 = new EightBitInput();
            var in4 = new EightBitInput();

            var add2 = new EightBitAdder();

            in3.AttachTo(add2);
            in4.AttachTo(add2, 8);

            in3.State = 4;
            in4.State = 11;
            
            var add3 = new EightBitAdder();
            
            add1.AttachToFull(add3);
            add2.AttachToFull(add3, 8);
            
            var display2 = new EightBitDisplay();
            add3.AttachToFull(display2);
            Console.WriteLine(display2.Value);

            #endregion
        }
    }
}