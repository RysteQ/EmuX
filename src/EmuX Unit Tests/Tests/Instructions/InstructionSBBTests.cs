using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Instructions.Internal;

namespace EmuXCoreUnitTests.Tests.Instructions;

[TestClass]
public sealed class InstructionSBBTests : InstructionConstants<InstructionSBB>
{
    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantNoOperands_NotValid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("cl", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "[test_label]")]));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("cl", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_AllPrefixes_ValidVariant_CorrectPrefixCheck()
    {
        List<IPrefix> validPrefixes = [];

        foreach (IPrefix prefix in AllPrefixes())
        {
            IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), prefix, GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

            Assert.AreEqual(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_SubtractWithBorrowRegisterToValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("4", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 10;
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(5, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_SubtractWithBorrowRegisterToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("cl", OperandVariant.Register, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = 3;
        virtualMachine.SetFlag(EFlags.CF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(7, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_SubtractWithBorrowRegisterToMemory()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 10;
        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetByte(0, 9);
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_SubtractWithBorrowMemoryToValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("8", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetByte(0, 10);
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(1, virtualMachine.GetByte(0));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_SubtractWithBorrowMemoryToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("cl", OperandVariant.Register, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = 1;
        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetByte(0, 10);
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(8, virtualMachine.GetByte(0));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
    }
}