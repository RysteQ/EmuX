filename: db 'l', '.', 't', 'x', 't', 0
contents: db 'a', 'b', 'c', 0

section.text

mov cx, filename
mov dx, contents
int 16