screen_memory_start: dq 8192
byte_limit: dq 82000

section.text

mov rax, quad [screen_memory_start]
mov rcx, quad [byte_limit]

loop:

mov [rax], byte 255
add rax, 3

cmp rax, rcx
jl loop

int 12