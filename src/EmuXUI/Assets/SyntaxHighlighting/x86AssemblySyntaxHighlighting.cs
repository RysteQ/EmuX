using TextControlBoxNS;

namespace EmuXUI.Assets.SyntaxHighlighting;

public sealed class x86Assembly : SyntaxHighlightLanguage
{
    public x86Assembly()
    {
        Name = "x86Assembly";
        Author = "Eustathios Koutsos";
        Filter = [ ".asm" ];
        Description = "Syntax highlighting for the original x86 ISA assembly";
        Highlights =
        [
            new("^((?i)rep|^(?i)repe|^(?i)repne|^(?i)repnz|^(?i)repz)($|\\s)", "#e6005c", "#e6005c", true), // prefixes
            new("((?i)aaa|(?i)aad|(?i)aam|(?i)aas|(?i)adc|(?i)add|(?i)and|(?i)call|(?i)cbw|(?i)clc|(?i)cld|(?i)cli|(?i)cmc|(?i)cmp|(?i)cmpsb|(?i)cmpsw|(?i)cwd|(?i)daa|(?i)das|(?i)dec|(?i)div|(?i)esc|(?i)hlt|(?i)idiv|(?i)imul|(?i)in|(?i)inc|(?i)into|(?i)iret|(?i)ja|(?i)jae|(?i)jb|(?i)jbe|(?i)jc|(?i)je|(?i)jg|(?i)jge|(?i)jl|(?i)jle|(?i)jna|(?i)jnae|(?i)jnb|(?i)jnbe|(?i)jnc|(?i)jne|(?i)jng|(?i)jnge|(?i)jnl|(?i)jnle|(?i)jno|(?i)jnp|(?i)jns|(?i)jnz|(?i)jo|(?i)jp|(?i)jpe|(?i)jpo|(?i)js|(?i)jz|(?i)jcxz|(?i)jmp|(?i)lahf|(?i)lds|(?i)lea|(?i)les|(?i)lock|(?i)lodsb|(?i)lodsw|(?i)loop|(?i)mov|(?i)movsb|(?i)movsw|(?i)mul|(?i)neg|(?i)nop|(?i)not|(?i)or|(?i)out|(?i)pop|(?i)popf|(?i)push|(?i)pushf|(?i)rcl|(?i)rcr|(?i)ret|(?i)retn|(?i)retf|(?i)rol|(?i)ror|(?i)sahf|(?i)sal|(?i)sar|(?i)sbb|(?i)scasb|(?i)scasw|(?i)shl|(?i)shr|(?i)stc|(?i)std|(?i)sti|(?i)stosb|(?i)stosw|(?i)sub|(?i)test|(?i)wait|(?i)xchg|(?i)xlat|(?i)xor)($|\\s)", "#035efc", "#035efc", true), // instructions
            new("(^|\\s|\\[|\\]|\\*|-|\\+)((?i)rax|(?i)eax|(?i)ax|(?i)ah|(?i)al|(?i)rbx|(?i)ebx|(?i)bx|(?i)bh|(?i)bl|(?i)rcx|(?i)ecx|(?i)cx|(?i)ch|(?i)cl|(?i)rdx|(?i)edx|(?i)dx|(?i)dh|(?i)dl|(?i)rdi|(?i)edi|(?i)di|(?i)dil|(?i)rsi|(?i)esi|(?i)si|(?i)sil|(?i)cs|(?i)ds|(?i)ss|(?i)es|(?i)fs|(?i)ds|(?i)rbp|(?i)ebp|(?i)bp|(?i)bpl|(?i)rip|(?i)eip|(?i)ip|(?i)rsp|(?i)esp|(?i)sp|(?i)spl)(^|\\s|\\[|\\]|\\*|-|\\+|\\b|\\s)", "#c4aa00", "#c4aa00"), // registers
            new("0b|0[xX][0-9a-fA-F]+|[0-9]+|0[bB][0-1]+", "#74bd00", "#74bd00"), // values
            new("0b|0[xX]|0[bB]", "#74bd00", "#74bd00"), // values
            new("(^|\\s*)((?i)int)(\\s|\\b)", "#6200ff", "#6200ff", true), // int
            new("(\\[|\\]|\\*|\\+|-])", "#0099ff", "#0099ff"), // brackets
            new("^\\s*[aA-zZ]+:", "#00ab5b", "#00ab5b", italic: true), // labels
            new("\\s*;(.*)", "#04d400", "#04d400"),
            new("((?i)byte|(?i)word|(?i)dword|(?i)qword)($|\\s)", "#fc8803", "#fc8803", true), // sizes
            new("((?i)ptr)($|\\s)", "#fcba03", "#fcba03", true), // ptr
        ];
    }
}
