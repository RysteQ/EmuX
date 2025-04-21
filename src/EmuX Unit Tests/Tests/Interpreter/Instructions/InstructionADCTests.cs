using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionADCTests : InstructionConstants<InstructionADC>
{
    [TestMethod]
    public void TestIsValidMethod_VariantNoOperands_NotValid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null, "test_label"));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null, "test_label"));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null, "test_label"), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null, "test_label"), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantThreeOperandsRegisterRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantThreeOperandsRegisterMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, [], "test_label"), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsRegisterValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("10h", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 10;
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0x_10 + 10 + 1, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsRegisterRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("rcx", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 20;
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(10 + 20 + 1, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, null, "test_label"), GenerateOperand("10h", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add("test_label", GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 10);
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(10 + 16 + 1, virtualMachine.GetQuad(0));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, null, "test_label"), GenerateOperand("rax", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 20;
        virtualMachine.Memory.LabelMemoryLocations.Add("test_label", GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 10);
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(10 + 20 + 1, virtualMachine.GetQuad(0));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.PF));
    }
}