using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuXCore.VM.Internal.CPU;

public class VirtualCPU : IVirtualCPU
{
    // Find why the fuck the "maintainability" index is 77 out of 100 for some fucking reason
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

    public IVirtualRegister? GetRegister(string registerName)
    {
        foreach (IVirtualRegister register in Registers)
        {
            if (register.RegisterNamesAndSizes.ContainsKey(registerName.ToUpper()))
            {
                return register;
            }
        }

        return null;
    }

    public IReadOnlyCollection<IVirtualRegister> Registers { get; init; } =
    [
        new VirtualRegisterRAX(),
        new VirtualRegisterRBX(),
        new VirtualRegisterRCX(),
        new VirtualRegisterRDX(),
        new VirtualRegisterRSI(),
        new VirtualRegisterRDI(),
        new VirtualRegisterRBP(),
        new VirtualRegisterRIP(),
        new VirtualRegisterRSP(),
        new VirtualRegisterCS(),
        new VirtualRegisterSS(),
        new VirtualRegisterDS(),
        new VirtualRegisterES(),
        new VirtualRegisterFS(),
        new VirtualRegisterGS(),
        new VirtualRegisterEFLAGS()
    ];

    public bool Halted { get; set; }
}