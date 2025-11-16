using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Interfaces.Components.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SegmentRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Actions;

[TestClass]
public sealed class UndoActionsTests : TestWideInternalConstants
{
    [TestMethod]
    public void UndoRegisterActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL++;
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<byte>(previousValue, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
    }

    [TestMethod]
    public void UndoRegisterActionWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX++;
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<ushort>(previousValue, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
    }

    [TestMethod]
    public void UndoRegisterActionDoubleWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX++;
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<uint>(previousValue, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX);
    }

    [TestMethod]
    public void UndoRegisterActionQuadWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX++;
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<ulong>(previousValue, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
    }

    [TestMethod]
    public void UndoMemoryActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte previousValue = virtualMachine.GetByte(0);

        virtualMachine.SetByte(0, (byte)(previousValue + 1));
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<byte>(previousValue, virtualMachine.GetByte(0));
    }

    [TestMethod]
    public void UndoMemoryActionWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort previousValue = virtualMachine.GetWord(0);

        virtualMachine.SetWord(0, (ushort)(previousValue + 1));
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<ushort>(previousValue, virtualMachine.GetWord(0));
    }

    [TestMethod]
    public void UndoMemoryActionDoubleWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint previousValue = virtualMachine.GetDouble(0);

        virtualMachine.SetDouble(0, (uint)(previousValue + 1));
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<uint>(previousValue, virtualMachine.GetDouble(0));
    }

    [TestMethod]
    public void UndoMemoryActionQuadWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong previousValue = virtualMachine.GetQuad(0);

        virtualMachine.SetQuad(0, (ulong)(previousValue + 1));
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<ulong>(previousValue, virtualMachine.GetQuad(0));
    }

    [TestMethod]
    public void UndoDeviceActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte previousValue = virtualMachine.Devices[0].Data[0];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)virtualMachine.Devices[0].DeviceId;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = (byte)(previousValue + 1);
        virtualMachine.Interrupt(InterruptCode.Device, DeviceInterrupt.ExecuteLogic);
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.AreEqual<byte>(previousValue, virtualMachine.Devices[0].Data[0]);
    }

    [TestMethod]
    public void UndoDiskActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte[] previousValue = new byte[virtualMachine.Disks[0].BytesPerSector];
        byte[] newValue = new byte[virtualMachine.Disks[0].BytesPerSector];

        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().RBX = 0;
        virtualMachine.Interrupt(InterruptCode.Disk, DiskInterrupt.ReadTrack);

        previousValue = [.. virtualMachine.Memory.RAM.Take(previousValue.Length)];
        Random.Shared.NextBytes(newValue);

        for (int i = 0; i < newValue.Length; i++)
        {
            virtualMachine.Memory.RAM[i] = newValue[i];
        }

        virtualMachine.Actions.Clear();
        virtualMachine.Interrupt(InterruptCode.Disk, DiskInterrupt.WriteTrack);
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Interrupt(InterruptCode.Disk, DiskInterrupt.ReadTrack);

        Assert.IsTrue(virtualMachine.Memory.RAM.Take(previousValue.Length).Index().All(selectedByte => selectedByte.Item == previousValue[selectedByte.Index]));
    }

    [TestMethod]
    public void UndoGpuActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte[] previousValue = [virtualMachine.GPU.Data[0], virtualMachine.GPU.Data[1], virtualMachine.GPU.Data[2]];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)(VideoInterrupt.DrawPixel);
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.Actions.Clear();
        virtualMachine.Interrupt(InterruptCode.Video, VideoInterrupt.DrawPixel);
        virtualMachine.Actions.Last().Undo(virtualMachine);

        Assert.IsTrue(virtualMachine.GPU.Data.Take(previousValue.Length).Index().All(selectedByte => selectedByte.Item == previousValue[selectedByte.Index]));
    }
}