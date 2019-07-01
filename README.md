<img src="assets/logo.png" alt="logo" width="200" align="left"/>

## Simulated Logic

[![Launch](https://img.shields.io/badge/launch-dotnetfiddle-orange.svg)](https://dotnetfiddle.net/bYoTBH)
[![Nuget](https://img.shields.io/nuget/v/Silo.svg?color=blue)](https://nuget.org/packages/Silo/)
[![Build Status](https://travis-ci.org/Azer0s/Silo.svg?branch=master)](https://travis-ci.org/Azer0s/Silo)
[![codecov](https://codecov.io/gh/Azer0s/Silo/branch/master/graph/badge.svg)](https://codecov.io/gh/Azer0s/Silo)
[![License](https://img.shields.io/github/license/Azer0s/Silo.svg)](https://github.com/Azer0s/Silo/blob/master/LICENSE)

Silo (**Si**mulated **Lo**gic) is a C# framework which allows you to simulate logic systems in code. Silo has many built-in components like gates, ALU's, displays, etc. that will make simulating complex logic systems easier.

## Samples

### Sample AND gate

```cs
using Silo.Components;
using Silo.Gates;

var a = new Switch();
var b = new Switch();

var andGate = new AndGate();

a.AttachTo(andGate, 0);
b.AttachTo(andGate, 1);

a.State = true;
b.State = true;

Console.WriteLine(andGate);

```

### 8 bit input

```cs
using Silo.Components;

var input = new EightBitInput();

input.State = 200;

Console.WriteLine(input);
```

### 8 bit adder

```cs
using Silo.Components;
using Silo.Devices;

var inputA = new EightBitInput();
var inputB = new EightBitInput();

var adder = new EightBitAdder();

inputA.AttachTo(adder);
inputB.AttachTo(adder, 8);

inputA.State = 10;
inputB.State = 5;

Console.WriteLine(adder);

var display = new EightBitDisplay();
adder.AttachToAll(display);
Console.WriteLine(display);

```

### D Flip-Flop with clock

```cs
using Silo.Components;
using Silo.Memory;
using Silo.Util;

var flipFlop = new DFlipFlop();
var val = new Switch();
var clk = new Clock(Frequency.Parse("1 Hz"));

clk.AttachTo(flipFlop, 1);

val.State = true;
Console.WriteLine(flipFlop);
Thread.Sleep(2000);
Console.WriteLine(flipFlop);

val.State = false;
Console.WriteLine(flipFlop);
Thread.Sleep(2000);
Console.WriteLine(flipFlop);
```
