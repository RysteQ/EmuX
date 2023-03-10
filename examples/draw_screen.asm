section.text

; initialize the registers
xor cx, cx
xor dx, dx

; the main loop
draw_screen:
int 12

; check if the end of the line has been reached
inc cx
cmp cx, 100
jne draw_screen

; reset the column and go to the next line
xor cx, cx

; check if the end condition was met or not
inc dx
cmp dx, 100
jne draw_screen