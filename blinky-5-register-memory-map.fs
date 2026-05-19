\ Program Name: blinky-5-register-memory-map.fs 
\ Required by: blinky-5.fs and blinky-6.fs
\ Hardware: Tiva Launchpad board

$400FE608 constant RCGCGPIO ( GPIO Run Mode Clock Gating Control )
$400FEA08 constant PRGPIO ( General-Purpose Input/Output Peripheral Ready )

$40005420 constant PORTB-AFSEL ( GPIO Alternate Function Select )
$40005000 constant PORTB-DATA ( GPIO Port B data address )
$40005400 constant PORTB-DIR ( GPIO Port B direction register )
$40005510 constant PORTB-PUR ( Pullup Resistor )
$4000551C constant PORTB-DEN ( Digital Enable )
$4000550C constant PORTB-ODR ( Open Drain )

$E000E014 constant STRELOAD ( systick reload value register )
$E000E010 constant STCTRL ( systick control and status register register )



