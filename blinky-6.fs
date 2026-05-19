\ Program Name: blinky-6.fs  for Mecrisp-Stellaris
\ This program blinks a green led using the Systick Interrupt and cooperative multitasking.
\ Hardware: Tiva Launchpad board
\ Requires: The e4thcom Serial Terminal to preload files via the #require command. 
\ E4thcom, Copyright (C) 2013-2017 Manfred Mahlow and licensed under the GP. https://wiki.forth-ev.de/doku.php/en:projects:e4thcom#e4thcom-061
\ Author:  t.porter <terry@tjporter.com.au>

#require portb_init.fs
#require blinky-5-register-memory-map.fs
#require multitask.fs

: systick ( ticks -- )
  STRELOAD ! \ How many ticks between interrupts ?
  7 STCTRL ! \ Enable the systick interrupt.
;

: systick-1Hz ( -- ) 16000000 systick ; \ Tick every second with 16 MHz clock

32 constant pb5
5 pin->addr-mask portb-data + constant pb5-addr

task: blinkytask
: blinky& ( -- )
  blinkytask background
  begin
	  key? if boot-task wake then
    pb5 pb5-addr xor!
    stop
  again
;

: tick ( -- ) blinkytask wake ;

: clock ( -- ) 
  ['] tick irq-systick !
  systick-1Hz
  eint
;


\ init
\ clock
\ multitask
\ blinky&
