// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/04/Mult.asm

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)

// While (R0 > 0) {
//     R2 += R1;
//     R0--;
// }

    @R2
    M=0 // R2 = 0;
(LOOP)
    @R0
    D=M
    @END
    D;JEQ  // IF (R0 == 0) goto END
	
    @R1
    D=M
    @R2
    M=D+M // R2 += R1;
	
    @R0
    M=M-1 // R0--;

    @LOOP
    0;JMP
(END)
@END
0;JMP