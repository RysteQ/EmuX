prompt: db 'F', 'i', 'l', 'e', 'n', 'a', 'm', 'e', ':', ' ', 0
filename: db 'i', 'n', 'p', 'u', 't', '.', 't', 'x', 't', 0
file_pointer: dw 1040

section.text

; make sure you have a file named input.txt in the same directory
; as the executable

; read the file
mov dx, word [file_pointer]
mov cx, filename
int 15

; initialize the dx register
mov dx, 1040

; the main loop
print_contents:
mov cx, byte [dx]

; check if the end condition was met
mov al, cl
cmp al, byte 0
je exit

; increment the memory offset and print the character
inc dx
int 9
jmp print_contents

exit: