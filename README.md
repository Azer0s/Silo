<img src="assets/logo.png" alt="logo" width="200" align="left"/>

## Simulated Logic

[![Launch](https://img.shields.io/badge/launch-dotnetfiddle-orange.svg)](https://dotnetfiddle.net/bYoTBH)
[![Nuget](https://img.shields.io/nuget/v/Silo.svg?color=blue)](https://nuget.org/packages/Silo/)
[![Build Status](https://travis-ci.org/Azer0s/Silo.svg?branch=master)](https://travis-ci.org/Azer0s/Silo)
[![codecov](https://codecov.io/gh/Azer0s/Silo/branch/master/graph/badge.svg)](https://codecov.io/gh/Azer0s/Silo)
[![License](https://img.shields.io/github/license/Azer0s/Silo.svg)](https://github.com/Azer0s/Silo/blob/master/LICENSE)
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FAzer0s%2FSilo.svg?type=shield)](https://app.fossa.io/projects/git%2Bgithub.com%2FAzer0s%2FSilo?ref=badge_shield)

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

### Counter with clock

```cs
using Silo.Components;
using Silo.Memory;
using Silo.Util;

var ctr = new Counter();

var reset = new Button();
var loadOrCount = new Switch();
var upOrDown = new Switch();
var countToggle = new Switch();
var clock = new Clock(1.kHz());
var input = new EightBitInput();

var display = new EightBitDisplay();

reset.AttachTo(ctr, 0);
loadOrCount.AttachTo(ctr, 1);
upOrDown.AttachTo(ctr, 2);
countToggle.AttachTo(ctr, 3);
clock.AttachTo(ctr, 4);
input.AttachTo(ctr, 5);

ctr.AttachRange(display, 1, 8);

loadOrCount.State = true;
input.State = 50;
Thread.Sleep(100); //Wait for clock to cycle
//Ctr output is now 50

loadOrCount.State = false;
upOrDown.State = true;
countToggle.State = true;
Thread.Sleep(100);
//Ctr output is now ~53

upOrDown.State = false;
Thread.Sleep(100);
//Ctr output is now 50
```


## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FAzer0s%2FSilo.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2FAzer0s%2FSilo?ref=badge_large)