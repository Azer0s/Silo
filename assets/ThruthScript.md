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
#define #10 reset
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
        case '10': //instead of writing numbers, one can also write pin states
            result := a * b
        case 3:
            result := a / b
    }
    
    output := result
}

when! (reset is updated) {
    output := 0
    |reset| //do nothing and block other operation while reset is hi
}
```

### Functions
Since some devices do not support constructs like functions, having functions and recursion enabled are ruleset options (functions are on by default, recursion off by default).

```cpp
#rule functions true //this is enabled by default
#rule portreferences true
#rule runtimeportaddressing true

setPort(p: `port, val: state) {
    [p] := val
    //p := would override the port reference
}

setPort(p: uint<4>, val: state) {
    `[p] := val //this was enabled by the runtimeportaddressing rule
}

setPort(`0, hi) //setPort(`0, 1) or setPort(`0, '1') is also valid
setPort(0, lo) //setPort(0, 0) or setPort(0, '0') is also valid

add(a: int<8>, b: int<8>): int<16> {
    puts a + b
}

add(10, 10)
``` 

### Native functions

```cpp
#define uint<8> char
#define char* string

#if native

@cImport("stdio.h") //importing a local header would look like so: @cImport("./[..].h")
@cExtern print(val: string): void

#else if vm

print(val: string) {
    log(val)
}

#else

print(val: string) {
    //ignored
}

#end

```
