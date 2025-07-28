using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using System.Runtime.CompilerServices;

namespace EmuXCore.InstructionLogic.Prefixes;

public sealed class PrefixREP : IPrefix
{
    public void Loop(IInstruction instruction, IVirtualMachine virtualMachine)
    {
        if (virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX != 0)
        {
            instruction.Execute(virtualMachine);
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX--;
        }
    }

    public Type Type => typeof(PrefixREP);
    public string Prefix => "REP";
}