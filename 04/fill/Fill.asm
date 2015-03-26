// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Fill.asm

// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, the
// program clears the screen, i.e. writes "white" in every pixel.

(KBDLOOP)
	@KBD
	D=M
	
	@WHITE
	D;JEQ  // IF KBD==0 FILL WHITE

	@BLACK
	D;JNE  // ELSE FILL BLACK
	
	@KBDLOOP
	0;JMP
(END)
@END
0;JMP

(WHITE)
	D=0
	@color
	M=D    // color = 0;
@FILL
0;JMP	

(BLACK)
	D=-1
	@color
	M=D    // color = -1
@FILL
0;JMP	

(FILL)
	@8192
	D=A
	@counter
	M=D    // counter = 8192
	
	@SCREEN
	D=A
	@position
	M=D    // position = SCREEN
	
	(LOOP)
		@counter
		D=M
		@KBDLOOP
		D;JLE   // IF (counter <= 0) goto KBDLOOP
		
		@color
		D=M    // D = color
		
		@position
		A=M    // A = position
		
		M=D    // Fill position with color
		
		@position
		M=M+1  // position++
		
		@counter
		M=M-1 // counter--;
		
		@LOOP
		0;JMP
	(END)
	
    @KBDLOOP  // return
    0;JMP
