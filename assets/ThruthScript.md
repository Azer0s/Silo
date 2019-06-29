## ThruthScript

### D Flip Flop

```
define #0 a
define #1 clk

define `0 out
define `1 outInv

//when! defines a constant trigger
when! (a is updated) {
    //when without ! defines a trigger that is executed once
    when (clk is hi) {
        //doesnt execute for the current hi state
        set out a
        set outInv ~a
    }
}
```

### And Gate

```
define #0 a
define #1 b

define `0 out

when! (a is updated or b is updated) {
	res = a & b
	set out res
}
```

