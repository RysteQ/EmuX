using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuXCore.InstructionLogic.Prefixes;

public sealed class PrefixREPNE : IPrefix
{
    public void Loop(IInstruction instruction, IVirtualMachine virtualMachine)
    {
        if (virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX != 0 && !virtualMachine.GetFlag(EFlags.ZF))
        {
            instruction.Execute(virtualMachine);
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX--;
        }
    }

    public Type Type => typeof(PrefixREPNE);
    public string Prefix => "REPNE";
    public byte PrefixMachineCodeByte => 0xF2;
}