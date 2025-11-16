using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Interfaces.Components.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SegmentRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Actions;

[TestClass]
public sealed class RedoActionsTests : TestWideInternalConstants
{
    [TestMethod]
    public void RedoRegisterActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL++;
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<byte>((byte)(previousValue + 1), (byte)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL + 1));
    }

    [TestMethod]
    public void RedoRegisterActionWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX++;
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<ushort>((ushort)(previousValue + 1), (ushort)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX + 1));
    }

    [TestMethod]
    public void RedoRegisterActionDoubleWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX++;
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<uint>((uint)(previousValue + 1), (uint)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX + 1));
    }

    [TestMethod]
    public void RedoRegisterActionQuadWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX++;
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<ulong>((ulong)(previousValue + 1), (ulong)(virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX + 1));
    }

    [TestMethod]
    public void RedoMemoryActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte previousValue = virtualMachine.GetByte(0);

        virtualMachine.SetByte(0, (byte)(previousValue + 1));
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<byte>((byte)(previousValue + 1), (byte)(virtualMachine.GetByte(0) + 1));
    }

    [TestMethod]
    public void RedoMemoryActionWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort previousValue = virtualMachine.GetWord(0);

        virtualMachine.SetWord(0, (ushort)(previousValue + 1));
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<ushort>((ushort)(previousValue + 1), (ushort)(virtualMachine.GetWord(0) + 1));
    }

    [TestMethod]
    public void RedoMemoryActionDoubleWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint previousValue = virtualMachine.GetDouble(0);

        virtualMachine.SetDouble(0, (uint)(previousValue + 1));
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<uint>((uint)(previousValue + 1), (uint)(virtualMachine.GetDouble(0) + 1));
    }

    [TestMethod]
    public void RedoMemoryActionQuadWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong previousValue = virtualMachine.GetQuad(0);

        virtualMachine.SetQuad(0, (ulong)(previousValue + 1));
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<ulong>((ulong)(previousValue + 1), (ulong)(virtualMachine.GetQuad(0) + 1));
    }

    [TestMethod]
    public void RedoDeviceActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)virtualMachine.Devices[0].DeviceId;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 100;
        virtualMachine.Interrupt(InterruptCode.Device, DeviceInterrupt.ExecuteLogic);
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.AreEqual<byte>(100, virtualMachine.Devices[0].Data[0]);
    }

    [TestMethod]
    public void RedoDiskActionByte()
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
        virtualMachine.Actions.Last().Redo(virtualMachine);
        virtualMachine.Interrupt(InterruptCode.Disk, DiskInterrupt.ReadTrack);

        Assert.IsTrue(virtualMachine.Memory.RAM.Take(previousValue.Length).Index().All(selectedByte => selectedByte.Item == previousValue[selectedByte.Index]));
    }

    [TestMethod]
    public void RedoGpuActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte[] previousValue = [virtualMachine.GPU.Data[0], virtualMachine.GPU.Data[1], virtualMachine.GPU.Data[2]];
        byte[] newValue = [255, 255, 255];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)(VideoInterrupt.DrawPixel);
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX = (uint)((virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_ff_00_ff_ff) + (newValue[0] << 16));
        virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX = (uint)((virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_ff_ff_00_ff) + (newValue[1] << 8));
        virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX = (virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_ff_ff_ff_00) + newValue[2];
        virtualMachine.Actions.Clear();
        virtualMachine.Interrupt(InterruptCode.Video, VideoInterrupt.DrawPixel);
        virtualMachine.Actions.Last().Undo(virtualMachine);
        virtualMachine.Actions.Last().Redo(virtualMachine);

        Assert.IsTrue(virtualMachine.GPU.Data.Take(newValue.Length).Index().All(selectedByte => selectedByte.Item == newValue[selectedByte.Index]));
    }
}