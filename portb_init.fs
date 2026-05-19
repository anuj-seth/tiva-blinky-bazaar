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

