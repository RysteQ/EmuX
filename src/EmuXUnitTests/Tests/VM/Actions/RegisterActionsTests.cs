using EmuXCore.Common.Enums;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Interfaces.Components.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SegmentRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Actions;

[TestClass]
public sealed class RegisterActionsTests : TestWideInternalConstants
{
    [TestMethod]
    public void RegisterRegisterActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte previousValue = 0;
        byte newValue = byte.MaxValue;

        previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = newValue;

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedRegister, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Byte, virtualMachine.Actions[0].Size);
        Assert.AreEqual<string>("AL", virtualMachine.Actions[0].RegisterName);
        Assert.IsNull(virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(previousValue == virtualMachine.Actions[0].PreviousValue[0]);
        Assert.IsTrue(newValue == virtualMachine.Actions[0].NewValue[0]);
    }

    [TestMethod]
    public void RegisterRegisterActionWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort previousValue = 0;
        ushort newValue = ushort.MaxValue;

        previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = newValue;

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedRegister, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Word, virtualMachine.Actions[0].Size);
        Assert.AreEqual<string>("AX", virtualMachine.Actions[0].RegisterName);
        Assert.IsNull(virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(virtualMachine.Actions[0].PreviousValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(previousValue)[selectedByte.Index]));
        Assert.IsTrue(virtualMachine.Actions[0].NewValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(newValue)[selectedByte.Index]));
    }

    [TestMethod]
    public void RegisterRegisterActionDoubleWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint previousValue = 0;
        uint newValue = uint.MaxValue;

        previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = newValue;

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedRegister, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Dword, virtualMachine.Actions[0].Size);
        Assert.AreEqual<string>("EAX", virtualMachine.Actions[0].RegisterName);
        Assert.IsNull(virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(virtualMachine.Actions[0].PreviousValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(previousValue)[selectedByte.Index]));
        Assert.IsTrue(virtualMachine.Actions[0].NewValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(newValue)[selectedByte.Index]));
    }

    [TestMethod]
    public void RegisterRegisterActionQuadWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong previousValue = 0;
        ulong newValue = ulong.MaxValue;

        previousValue = virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = newValue;

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedRegister, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Qword, virtualMachine.Actions[0].Size);
        Assert.AreEqual<string>("RAX", virtualMachine.Actions[0].RegisterName);
        Assert.IsNull(virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(virtualMachine.Actions[0].PreviousValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(previousValue)[selectedByte.Index]));
        Assert.IsTrue(virtualMachine.Actions[0].NewValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(newValue)[selectedByte.Index]));
    }

    [TestMethod]
    public void RegisterMemoryActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte previousValue = 0;
        byte newValue = byte.MaxValue;
        int memoryLocation = virtualMachine.Memory.RAM.Count() - 1;

        previousValue = virtualMachine.GetByte(memoryLocation);
        virtualMachine.SetByte(memoryLocation, newValue);

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedMemory, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Byte, virtualMachine.Actions[0].Size);
        Assert.IsNull(virtualMachine.Actions[0].RegisterName);
        Assert.AreEqual<int>(memoryLocation, virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(previousValue == virtualMachine.Actions[0].PreviousValue[0]);
        Assert.IsTrue(newValue == virtualMachine.Actions[0].NewValue[0]);
    }

    [TestMethod]
    public void RegisterMemoryActionWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort previousValue = 0;
        ushort newValue = ushort.MaxValue;
        int memoryLocation = virtualMachine.Memory.RAM.Count() - 2;

        previousValue = virtualMachine.GetWord(memoryLocation);
        virtualMachine.SetWord(memoryLocation, newValue);

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedMemory, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Word, virtualMachine.Actions[0].Size);
        Assert.IsNull(virtualMachine.Actions[0].RegisterName);
        Assert.AreEqual<int>(memoryLocation, virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(virtualMachine.Actions[0].PreviousValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(previousValue)[selectedByte.Index]));
        Assert.IsTrue(virtualMachine.Actions[0].NewValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(newValue)[selectedByte.Index]));
    }

    [TestMethod]
    public void RegisterMemoryActionDoubleWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint previousValue = 0;
        uint newValue = uint.MaxValue;
        int memoryLocation = virtualMachine.Memory.RAM.Count() - 4;

        previousValue = virtualMachine.GetDouble(memoryLocation);
        virtualMachine.SetDouble(memoryLocation, newValue);

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedMemory, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Dword, virtualMachine.Actions[0].Size);
        Assert.IsNull(virtualMachine.Actions[0].RegisterName);
        Assert.AreEqual<int>(memoryLocation, virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(virtualMachine.Actions[0].PreviousValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(previousValue)[selectedByte.Index]));
        Assert.IsTrue(virtualMachine.Actions[0].NewValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(newValue)[selectedByte.Index]));
    }

    [TestMethod]
    public void RegisterMemoryActionQuadWord()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong previousValue = 0;
        ulong newValue = ulong.MaxValue;
        int memoryLocation = virtualMachine.Memory.RAM.Count() - 8;

        previousValue = virtualMachine.GetQuad(memoryLocation);
        virtualMachine.SetQuad(memoryLocation, newValue);

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedMemory, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Qword, virtualMachine.Actions[0].Size);
        Assert.IsNull(virtualMachine.Actions[0].RegisterName);
        Assert.AreEqual<int>(memoryLocation, virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(virtualMachine.Actions[0].PreviousValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(previousValue)[selectedByte.Index]));
        Assert.IsTrue(virtualMachine.Actions[0].NewValue.Index().All(selectedByte => selectedByte.Item == BitConverter.GetBytes(newValue)[selectedByte.Index]));
    }

    [TestMethod]
    public void RegisterDeviceActionByte()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte previousValue = 0;
        byte newValue = (byte)Random.Shared.Next();
        int memoryLocation = (ushort)Random.Shared.Next();

        previousValue = virtualMachine.Devices[0].Data[memoryLocation];
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = (byte)virtualMachine.Devices[0].DeviceId;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = (ushort)memoryLocation;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = newValue;
        virtualMachine.Actions.Clear();
        virtualMachine.Interrupt(InterruptCode.Device, DeviceInterrupt.ExecuteLogic);

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedDevice, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Byte, virtualMachine.Actions[0].Size);
        Assert.IsNull(virtualMachine.Actions[0].RegisterName);
        Assert.AreEqual<int>(memoryLocation, virtualMachine.Actions[0].MemoryPointer);
        Assert.AreEqual<int?>(virtualMachine.Devices[0].DeviceId, virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(previousValue == virtualMachine.Actions[0].PreviousValue[0]);
        Assert.IsTrue(newValue == virtualMachine.Actions[0].NewValue[0]);
    }

    [TestMethod]
    public void RegisterDiskActionByte()
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

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedDisk, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Byte, virtualMachine.Actions[0].Size);
        Assert.IsNull(virtualMachine.Actions[0].RegisterName);
        Assert.AreEqual<int>(0, virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.AreEqual<int?>(1, virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(virtualMachine.Actions[0].PreviousValue.Index().All(selectedByte => selectedByte.Item == previousValue[selectedByte.Index]));
        Assert.IsTrue(virtualMachine.Actions[0].NewValue.Index().All(selectedByte => selectedByte.Item == newValue[selectedByte.Index]));
    }

    [TestMethod]
    public void RegisterGpuActionByte()
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

        Assert.AreEqual<int>(1, virtualMachine.Actions.Count);
        Assert.AreEqual<VmActionCategory>(VmActionCategory.ModifiedGpu, virtualMachine.Actions[0].Action);
        Assert.AreEqual<Size>(Size.Byte, virtualMachine.Actions[0].Size);
        Assert.IsNull(virtualMachine.Actions[0].RegisterName);
        Assert.AreEqual<int?>(0, virtualMachine.Actions[0].MemoryPointer);
        Assert.IsNull(virtualMachine.Actions[0].DeviceId);
        Assert.IsNull(virtualMachine.Actions[0].DiskId);
        Assert.IsTrue(previousValue.Index().All(selectedByte => selectedByte.Item == virtualMachine.Actions[0].PreviousValue[selectedByte.Index]));
        Assert.IsTrue(newValue.Index().All(selectedByte => selectedByte.Item == virtualMachine.Actions[0].NewValue[selectedByte.Index]));
    }
}