## ThruthScript

### D Flip Flop

```cpp
#define #0 a
#define #1 clk

#define `0 out
#define `1 outInv

when! (out is updated){
	outInv := ~out
}

//when! defines a constant trigger
when! (a is updated) {
    //when without ! defines a trigger that is executed once
    when (clk is hi) {
        //doesnt execute for the current hi state
        out := a
    }
}
```

### And Gate

```cpp
#define #0 a
#define #1 b

#define `0 out

when! ([a,b] is updated) {
    out := a & b
}
```

### 7 Segment display

```cpp
//https://www.jameco.com/Jameco/workshop/TechTip/working-with-seven-segment-displays.html
//Only for numbers

#define #0-#3 input
#define `0-`7 display

when! (input is updated) {
    display := input => {
        '11111100' if '0000'
        '01100000' if '0001'
        '11011010' if '0010'
        '11110010' if '0011'
        '01100110' if '0100'
        '10110110' if '0101'
        '10111110' if '0110'
        '11100000' if '0111'
        '11111110' if '1000'
        '11110110' if '1001'
    }
}
```

