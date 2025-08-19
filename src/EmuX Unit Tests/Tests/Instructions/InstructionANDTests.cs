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
public sealed class InstructionANDTests : InstructionConstants<InstructionAND>
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null));

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
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsValueRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

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
    public void TestExecuteMethod_VariantTwoOperandsRegisterValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("0000111111110000b", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0110_1001_1111_1111_1010_0101_0101_1010;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0101_0101_0000, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsRegisterRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("rcx", OperandVariant.Register, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0110_1001_1111_1111_1010_0101_0101_1010;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_1111_1111_0000;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_0101_0101_0000, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Qword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("0000111111110000b", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 0b_0000_0000_0000_0000_0000_0000_0000_0000_0110_1001_1111_1111_1010_0101_0101_1010);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0101_0101_0000, virtualMachine.GetQuad(0));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Qword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("rcx", OperandVariant.Register, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_1111_1111_0000;
        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 0b_0000_0000_0000_0000_0000_0000_0000_0000_0110_1001_1111_1111_1010_0101_0101_1010);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_0101_0101_0000, virtualMachine.GetQuad(0));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.PF));
    }
}