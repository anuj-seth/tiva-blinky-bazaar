\ Program Name: blinky-4.fs  for Mecrisp-Stellaris
\ This program blinks a green led and writes a string to the terminal using cooperative multitasking
\ Hardware: Tiva Launchpad board
\ Requires: The e4thcom Serial Terminal to preload files via the #require command. 
\ E4thcom, Copyright (C) 2013-2017 Manfred Mahlow and licensed under the GP. https://wiki.forth-ev.de/doku.php/en:projects:e4thcom#e4thcom-061

compiletoram

#require portb_init.fs
#require multitask.fs

32 constant pb5
5 pin->addr-mask portb-data + constant pb5-addr

: delay ( n -- )
  0 do pause loop
;

: green-led.on? ( -- flag )
  pb5-addr @ pb5 =
;

: green-led.on
  pb5 pb5-addr !
;

: green-led.off
  0 pb5-addr !
;

: blink
  green-led.on
  500000 delay
  green-led.off
  500000 delay
;
 
task: blinktask
: blinky& ( -- )
  blinktask activate
  begin blink again
;

: say-hello
  ." hello " cr
  100000 delay
;

task: hellotask
: hello& ( -- )
  hellotask activate
  begin say-hello again
;

\ init
\ multitask
\ blinky& hello&

