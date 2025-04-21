using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuX_Unit_Tests.Tests.VM.BIOS;

[TestClass]
public sealed class DiskInterruptTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestDiskWriteRead_Random_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte[] randomBuffer = new byte[virtualMachine.Disks[0].BytesPerSector];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().BX = 0;

        Random.Shared.NextBytes(randomBuffer);

        for (int i = 0; i < virtualMachine.Disks[0].BytesPerSector; i++)
        {
            virtualMachine.SetByte(i, randomBuffer[i]);
        }

        virtualMachine.BIOS.HandleDiskInterrupt(DiskInterrupt.WriteTrack);
        virtualMachine.BIOS.HandleDiskInterrupt(DiskInterrupt.ReadTrack);

        CollectionAssert.AreEqual(randomBuffer, virtualMachine.Memory.RAM[0..(int)virtualMachine.Disks[0].BytesPerSector]);
    }
}