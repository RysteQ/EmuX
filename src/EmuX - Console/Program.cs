using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

VirtualCPU cpu = new();
Lexer lexer = new(cpu);
ILexerResult result = lexer.Parse("add dword ptr [0x1020], 5");

Console.WriteLine(result.Instructions.Count);