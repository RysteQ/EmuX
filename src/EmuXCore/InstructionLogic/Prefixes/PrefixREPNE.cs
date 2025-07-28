using EmuXCore.Common.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}