using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SegmentRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.VM.Internal.CPU;

public class VirtualCPU : IVirtualCPU
{
    public VirtualCPU(IVirtualMachine? parentVirtualMachine = null)
    {
        ParentVirtualMachine = parentVirtualMachine;

        Registers =
        [
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRAX>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRBX>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRCX>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRDX>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRSI>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRDI>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRBP>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRIP>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterRSP>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterCS>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterSS>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterDS>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterES>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterFS>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterGS>(ParentVirtualMachine),
            DIFactory.GenerateIVirtualRegister<VirtualRegisterEFLAGS>(ParentVirtualMachine)
        ];
    }

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

    public IReadOnlyCollection<IVirtualRegister> Registers { get; init; }

    public bool Halted { get; set; }
    public IVirtualMachine? ParentVirtualMachine { get; set; }
}