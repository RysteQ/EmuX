using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionINTOTests : InstructionConstants<InstructionINTO>
{
    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantNoOperands_NotValid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsValueRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_AllPrefixes_ValidVariant_CorrectPrefixCheck()
    {
        List<IPrefix> validPrefixes = [];

        foreach (IPrefix prefix in AllPrefixes())
        {
            IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), prefix, GenerateOperand("10", OperandVariant.Value, Size.Byte));

            Assert.AreEqual<bool>(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_WriteAndReadDiskInterrupt()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("13h", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte[] data = new byte[512];
        byte[] readBytes = new byte[512];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 0x_03;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().BX = 0;
        virtualMachine.SetFlag(EFlags.OF, true);

        Random.Shared.NextBytes(data);
        Random.Shared.NextBytes(readBytes);

        for (int i = 0; i < data.Length; i++)
        {
            virtualMachine.SetByte(i, data[i]);
        }

        try
        {
            instruction.Execute(virtualMachine);
        }
        catch
        {
            Assert.Fail();

            return;
        }

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 0x_02;

        for (int i = 0; i < data.Length; i++)
        {
            virtualMachine.SetByte(i, 0);
        }

        try
        {
            instruction.Execute(virtualMachine);
        }
        catch
        {
            Assert.Fail();

            return;
        }

        for (int i = 0; i < data.Length; i++)
        {
            readBytes[i] = virtualMachine.GetByte(i);
        }

        CollectionAssert.AreEquivalent(data, readBytes);
    }

    [TestMethod]
    public void TestExecuteMethod_RTCInterrupt_SetAndReadSystemClock()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("1ah", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        DateTime currentTime = DateTime.MinValue.AddMinutes(10);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = (byte)currentTime.Hour;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = (byte)currentTime.Minute;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = (byte)currentTime.Second;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 0;
        virtualMachine.SetFlag(EFlags.OF, true);
        instruction.Execute(virtualMachine);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        virtualMachine.SetFlag(EFlags.OF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>((byte)currentTime.Hour, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH);
        Assert.AreEqual<byte>((byte)currentTime.Minute, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL);
        Assert.AreEqual<byte>((byte)currentTime.Second, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH);
    }

    [TestMethod]
    public void TestExecuteMethod_RTCInterrupt_SetAndReadRTC()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("1ah", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        DateTime currentTime = DateTime.MinValue.AddMinutes(10);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 3;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = (byte)currentTime.Hour;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = (byte)currentTime.Minute;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = (byte)currentTime.Second;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 0;
        virtualMachine.SetFlag(EFlags.OF, true);
        instruction.Execute(virtualMachine);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 2;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        virtualMachine.SetFlag(EFlags.OF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>((byte)currentTime.Hour, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH);
        Assert.AreEqual<byte>((byte)currentTime.Minute, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL);
        Assert.AreEqual<byte>((byte)currentTime.Second, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH);
    }

    [TestMethod]
    public void TestExecuteMethod_RTCInterrupt_DontSetAndReadSystemClock()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("1ah", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        DateTime currentTime = DateTime.MinValue.AddMinutes(10);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = (byte)currentTime.Hour;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = (byte)currentTime.Minute;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = (byte)currentTime.Second;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 0;
        virtualMachine.SetFlag(EFlags.OF, false);
        instruction.Execute(virtualMachine);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        virtualMachine.SetFlag(EFlags.OF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX);
        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX);
    }

    [TestMethod]
    public void TestExecuteMethod_RTCInterrupt_DontSetAndReadRTC()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("1ah", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        DateTime currentTime = DateTime.MinValue.AddMinutes(10);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 3;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = (byte)currentTime.Hour;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = (byte)currentTime.Minute;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = (byte)currentTime.Second;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 0;
        virtualMachine.SetFlag(EFlags.OF, false);
        instruction.Execute(virtualMachine);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 2;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        virtualMachine.SetFlag(EFlags.OF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX);
        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX);
    }

    [TestMethod]
    public void TestExecuteMethod_NonExistentInterrupt_RuntimeException()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("1ah", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetFlag(EFlags.OF, true);

        try
        {
            instruction.Execute(virtualMachine);
        }
        catch
        {
            Assert.IsTrue(true);

            return;
        }

        Assert.Fail();
    }
}