$400FE608 constant RCGCGPIO ( GPIO Run Mode Clock Gating Control )
$400FEA08 constant PRGPIO ( General-Purpose Input/Output Peripheral Ready )

$40005420 constant PORTB-AFSEL ( GPIO Alternate Function Select )
$40005000 constant PORTB-DATA ( GPIO Port B data address )
$40005400 constant PORTB-DIR ( Soll der Pin Eingang oder Ausgang sein ? )
$40005510 constant PORTB-PUR ( Pullup Resistor )
$4000551C constant PORTB-DEN ( Digital Enable )
$4000550C constant PORTB-ODR ( Open Drain )

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
