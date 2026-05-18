\ Program Name: blinky-3.fs  for Mecrisp-Stellaris by Anuj Seth
\ This program blinks an led connected to PB5, but this time uses CMSIS-SVD compliant register names rather than raw memmory addressing
\ Hardware: Tiva Launchpad Discovery board
\ Requires: The e4thcom Serial Terminal to preload blinky3-register-memory-map.fs via the #require command. 
\ E4thcom, Copyright (C) 2013-2017 Manfred Mahlow and licensed under the GP. https://wiki.forth-ev.de/doku.php/en:projects:e4thcom#e4thcom-061

#require blinky-3-register-memory-map.fs

decimal

: disable-all-clock-gating ( -- )
  %111111 RCGCGPIO bic! ;

: enable-clock-gating ( port-mask -- )
  RCGCGPIO bis! ;

: peripheral-ready? ( port-mask -- )
  PRGPIO bit@ ;

: enable-clock-gating-port-b ( -- )
  %000010 enable-clock-gating
  begin %000010 peripheral-ready? until 
;

: system-init ( -- )
  disable-all-clock-gating ;

: init ( -- )
  system-init
  enable-clock-gating-port-b 

  %00000000 portb-afsel !  \ clear alternate function bits for all pins
  %00100000 portb-den ! \ Set all pin 5 to digital.
  %00100000 portb-dir ! \ The light emitting diodeon PB5 is set as output
  %00000000 portb-pur ! \ pull up resistors are disabledEnable pull-up resistors for the buttons
  %00000000 portb-odr ! \ open drain is disabled
;

: pin->addr-mask ( pin-no -- addr-mask, bits 9:2 are the address mask )
  2 + \ bit mask starts from bit number 2
  1 swap lshift ;

32 constant pb5
5 pin->addr-mask portb-data + constant pb5-addr

: delay
  900000 0 do loop
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
  do
    green-led.on
    delay
    green-led.off
    delay
  loop
;

\ init
\ blink  \ reset board to stop blinky
