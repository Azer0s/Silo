using System;
using System.Threading;
using Silo.Components;
using Silo.Devices;
using Silo.Gates;
using Silo.Memory;
using Silo.Util;

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

            Console.WriteLine("AND Gate Test");
            Console.WriteLine("------------------------");
            
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

            Console.WriteLine("\n8 bit input");
            Console.WriteLine("------------------------");

            var inp = new EightBitInput {State = 200};

            Console.WriteLine(inp);

            #endregion
            
            #region FullAdder

            Console.WriteLine("\nFull Adder");
            Console.WriteLine("------------------------");

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

            Console.WriteLine("\n8 bit Adder");
            Console.WriteLine("------------------------");

            var in1 = new EightBitInput();
            var in2 = new EightBitInput();
            
            var add1 = new EightBitAdder();
            
            in1.AttachTo(add1);
            in2.AttachTo(add1, 8);

            in1.State = 10;
            in2.State = 5;

            Console.WriteLine(add1);
            
            var display = new EightBitDisplay();
            add1.AttachToAll(display);
            Console.WriteLine(display);

            #endregion

            #region Dual 8 bit adder

            Console.WriteLine("\nDual 8 bit Adder");
            Console.WriteLine("------------------------");

            var in3 = new EightBitInput();
            var in4 = new EightBitInput();

            var add2 = new EightBitAdder();

            in3.AttachTo(add2);
            in4.AttachTo(add2, 8);

            in3.State = 4;
            in4.State = 11;
            
            var add3 = new EightBitAdder();
            
            add1.AttachToAll(add3);
            add2.AttachToAll(add3, 8);
            
            var display2 = new EightBitDisplay();
            add3.AttachToAll(display2);
            Console.WriteLine(display2);

            #endregion

            #region D FlipFlop with pseudo clock

            Console.WriteLine("\nD FlipFlop with pseudo clock");
            Console.WriteLine("------------------------");

            var d = new DFlipFlop();
            var val = new Switch();
            var clk = new Switch();
            
            val.AttachTo(d, 0);
            clk.AttachTo(d, 1);

            val.State = true;
            clk.State = true;
            Console.WriteLine(d);
            
            val.State = false;
            clk.State = false;
            clk.State = true;
            Console.WriteLine(d);

            #endregion

            #region D Flip Flop with real clock
            
            Console.WriteLine("\nD Flip Flop with real clock");
            Console.WriteLine("------------------------");

            var clk1 = new Clock(Frequency.Parse("1 Hz"));
            clk1.AttachTo(d, 1);
            val.State = true;
            Console.WriteLine(d);
            Thread.Sleep(2000);
            Console.WriteLine(d);

            val.State = false;
            Console.WriteLine(d);
            Thread.Sleep(2000);
            Console.WriteLine(d);

            #endregion

            #region SR Flip Flop with pseudo clock

            Console.WriteLine("\nSR Flip Flop with pseudo clock");
            Console.WriteLine("------------------------");
            
            var sr = new SRFlipFlop();
            var sr1 = new Switch();
            var sr2 = new Switch();
            var sr3 = new Switch();
            
            sr1.AttachTo(sr, 0);
            sr2.AttachTo(sr, 1);
            sr3.AttachTo(sr, 2);

            sr1.State = true;

            sr3.State = true;
            sr3.State = false;
            
            Console.WriteLine(sr);

            sr2.State = true;
            
            sr3.State = true;
            sr3.State = false;
            
            Console.WriteLine(sr);

            sr1.State = false;
            
            sr3.State = true;
            sr3.State = false;
            
            Console.WriteLine(sr);

            #endregion
        }
    }
}