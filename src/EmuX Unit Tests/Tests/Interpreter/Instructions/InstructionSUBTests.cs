using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionSUBTests : InstructionConstants<InstructionSUB>
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null));

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
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
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
            IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), prefix, GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

            Assert.AreEqual<bool>(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsRegisterValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("0ah", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0x_10;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0x_10 - 10, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsRegisterRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("rcx", OperandVariant.Register, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 20;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 10;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(20 - 10, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Qword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("0ah", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 16);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(16 - 10, virtualMachine.GetQuad(0));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Qword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("rax", OperandVariant.Register, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 10;
        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 20);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(20 - 10, virtualMachine.GetQuad(0));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }
}