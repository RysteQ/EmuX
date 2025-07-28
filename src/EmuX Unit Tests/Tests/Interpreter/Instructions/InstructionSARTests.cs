using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionSARTests : InstructionConstants<InstructionSAR>
{
    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantNoOperands_NotValid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
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
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("cl", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "[test_label]")]));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("cl", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

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
            IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), prefix, GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

            Assert.AreEqual<bool>(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_ShiftRegisterToValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("4", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 0b_1010_1100;
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<byte>(0b_1111_1010, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
    }

    [TestMethod]
    public void TestExecuteMethod_ShiftRegisterToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("cl", OperandVariant.Register, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 0b_1010_1100;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = 0b_0000_0011;
        virtualMachine.SetFlag(EFlags.CF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<byte>(0b_1111_0101, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
    }

    [TestMethod]
    public void TestExecuteMethod_ShiftMemoryToValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("2", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetByte(0, 0b_0100_1010);
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<byte>(0b_0001_0010, virtualMachine.GetByte(0));
    }

    [TestMethod]
    public void TestExecuteMethod_ShiftMemoryToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("cl", OperandVariant.Register, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = 1;
        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetByte(0, 0b_0100_1010);
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<byte>(0b_0010_0101, virtualMachine.GetByte(0));
    }
}