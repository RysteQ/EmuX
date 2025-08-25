using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCore.VM.Internal.CPU;

public class VirtualCPU(IVirtualMachine? parentVirtualMachine = null) : IVirtualCPU
{
    public T GetRegister<T>() where T : IVirtualRegister
    {
        foreach (IVirtualRegister register in Registers)
        {
            if (register is T foundRegister)
            {
                return foundRegister;
            }
        }

        throw new KeyNotFoundException($"Cannot find any register of type {typeof(T)}");
    }

    public IVirtualRegister GetRegister(string registerName)
    {
        foreach (IVirtualRegister register in Registers)
        {
            if (register.RegisterNamesAndSizes.ContainsKey(registerName.Trim().ToUpper()))
            {
                return register;
            }
        }

        throw new KeyNotFoundException($"Cannot find the register {registerName}");
    }

    public IReadOnlyCollection<IVirtualRegister> Registers { get; init; } =
    [
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRAX>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRBX>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRCX>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRDX>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRSI>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRDI>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRBP>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRIP>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterRSP>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterCS>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterSS>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterDS>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterES>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterFS>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterGS>(),
        DIFactory.GenerateIVirtualRegister<VirtualRegisterEFLAGS>()
    ];

    public bool Halted { get; set; }
    public IVirtualMachine? ParentVirtualMachine { get; set; } = parentVirtualMachine;
}