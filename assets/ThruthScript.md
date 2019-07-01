## ThruthScript

### D Flip Flop

```cpp
#define #0 a
#define #1 clk

#define `0 out
#define `1 outInv

rule (out) {
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

#define #0::#3 input
#define `0::`7 display

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

### Big Endian to Little Endian

```cpp
#define #0::#7<E> input
//doing #define #7::#0 input would also be valid
#define `0::`7<e> output //you don't have to annotate with <e> as this is the default

when! (input is updated) {
    output := input //this would set the bits, which we don't want   
    output := (<e>) input //this casts the input bits to little endian
    output := (<?>) input //<?> converts to target endianess
}
```

### Calculator

```cpp
#define #0::#3 a
#define #4::#7 b
#define #8::#9 operation
#define `0::`7 output

var result: uint<8>

when! ([a,b] is updated) {
    switch (operation) {
        case 0:
            result := a + b
            //since a and b are both little endian, 'result' gets front padded
            //if result were of type uint<4>, the front part would be cut off
        case 1:
            result := a - b  
        case '10':
            result := a * b
        case '11':
            result := a / b
    }
    
    output := result
}
```

