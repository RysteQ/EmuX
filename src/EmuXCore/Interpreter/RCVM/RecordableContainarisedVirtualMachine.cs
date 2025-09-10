using EmuXCore.Common.Enums;
using EmuXCore.Interpreter.RCVM.Enums;
using EmuXCore.Interpreter.RCVM.Interfaces;
using EmuXCore.Interpreter.RCVM.Interfaces.Models;
using EmuXCore.VM;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuXCore.Interpreter.RCVM;

public class RecordableContainarisedVirtualMachine : VirtualMachine, IRecordableContainarisedVirtualMachine
{
    public RecordableContainarisedVirtualMachine(IVirtualCPU cpu, IVirtualMemory memory, IVirtualDisk[] disks, IVirtualBIOS bios, IVirtualRTC virtualRTC, IVirtualGPU virtualGPU, IVirtualDevice[] virtualDevices) :
        base(cpu, memory, disks, bios, virtualRTC, virtualGPU, virtualDevices)
    { }

    public void RedoAction()
    {
        throw new NotImplementedException();
    }

    public void RedoActions(int actionsToRedo)
    {
        if (_actions.Count - _currentActionIndex <= 1)
        {
            throw new ArgumentOutOfRangeException("No action left to redo");
        }

        for (int i = 0; i < actionsToRedo; i++)
        {
            RedoAction();
        }
    }

    public void ResetToInitialState()
    {
        foreach (IVmAction vmAction in _actions)
        {
            UndoAction();
        }
    }

    public void UndoAction()
    {
        throw new NotImplementedException();
    }

    public void UndoActions(int actionsToUndo)
    {
        if (_currentActionIndex == 0)
        {
            throw new ArgumentOutOfRangeException("No action left to undo");
        }

        for (int i = 0; i < actionsToUndo; i++)
        {
            UndoAction();
        }
    }

    public override void SetFlag(EFlags flag, bool value)
    {
        _actions.Add(DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, Size.Qword, BitConverter.GetBytes(CPU.GetRegister<VirtualRegisterEFLAGS>().RFLAGS), CPU.GetRegister<VirtualRegisterEFLAGS>().Name));

        base.SetFlag(flag, value);
    }

    public override void SetIOPL(bool firstBit, bool secondBit)
    {
        _actions.Add(DIFactory.GenerateIVmAction(VmActionCategory.ModifiedRegister, Size.Qword, BitConverter.GetBytes(CPU.GetRegister<VirtualRegisterEFLAGS>().RFLAGS), CPU.GetRegister<VirtualRegisterEFLAGS>().Name));

        base.SetIOPL(firstBit, secondBit);
    }

    public override bool GetFlag(EFlags flag)
    {
        return base.GetFlag(flag);
    }

    public override byte GetIOPL()
    {
        return base.GetIOPL();
    }

    public override void SetByte(int memoryLocation, byte value)
    {
        _actions.Add(DIFactory.GenerateIVmAction(VmActionCategory.ModifiedMemory, Size.Byte, [Memory.RAM[memoryLocation]], memoryPointer: memoryLocation));

        base.SetByte(memoryLocation, value);
    }

    public override void SetWord(int memoryLocation, ushort value)
    {
        _actions.Add(DIFactory.GenerateIVmAction(VmActionCategory.ModifiedMemory, Size.Word, [Memory.RAM[memoryLocation], Memory.RAM[memoryLocation + 1]], memoryPointer: memoryLocation));

        base.SetWord(memoryLocation, value);
    }

    public override void SetDouble(int memoryLocation, uint value)
    {
        _actions.Add(DIFactory.GenerateIVmAction(VmActionCategory.ModifiedMemory, Size.Dword, [Memory.RAM[memoryLocation], Memory.RAM[memoryLocation + 1], Memory.RAM[memoryLocation + 2], Memory.RAM[memoryLocation + 3]], memoryPointer: memoryLocation));

        base.SetDouble(memoryLocation, value);
    }

    public override void SetQuad(int memoryLocation, ulong value)
    {
        _actions.Add(DIFactory.GenerateIVmAction(VmActionCategory.ModifiedMemory, Size.Qword, [Memory.RAM[memoryLocation], Memory.RAM[memoryLocation + 1], Memory.RAM[memoryLocation + 2], Memory.RAM[memoryLocation + 3], Memory.RAM[memoryLocation + 4], Memory.RAM[memoryLocation + 5], Memory.RAM[memoryLocation + 6], Memory.RAM[memoryLocation + 7]], memoryPointer: memoryLocation));

        base.SetQuad(memoryLocation, value);
    }

    public override byte GetByte(int memoryLocation)
    {
        return base.GetByte(memoryLocation);
    }

    public override ushort GetWord(int memoryLocation)
    {
        return base.GetWord(memoryLocation);
    }

    public override uint GetDouble(int memoryLocation)
    {
        return base.GetDouble(memoryLocation);
    }

    public override ulong GetQuad(int memoryLocation)
    {
        return base.GetQuad(memoryLocation);
    }

    public override void PushByte(byte value)
    {
        // TODO

        base.PushByte(value);
    }

    public override void PushWord(ushort value)
    {
        // TODO

        base.PushWord(value);
    }

    public override void PushDouble(uint value)
    {
        // TODO

        base.PushDouble(value);
    }

    public override void PushQuad(ulong value)
    {
        // TODO

        base.PushQuad(value);
    }

    public override byte PopByte()
    {
        // TODO

        return base.PopByte();
    }

    public override ushort PopWord()
    {
        // TODO

        return base.PopWord();
    }

    public override uint PopDouble()
    {
        // TODO

        return base.PopDouble();
    }

    public override ulong PopQuad()
    {
        // TODO

        return base.PopQuad();
    }

    public override void Interrupt(InterruptCode interruptCode, object subInterruptCode)
    {
        // TODO ????

        base.Interrupt(interruptCode, subInterruptCode);
    }

    public IVmAction[] Actions => _actions.ToArray();

    protected List<IVmAction> _actions;
    protected int _currentActionIndex;
}