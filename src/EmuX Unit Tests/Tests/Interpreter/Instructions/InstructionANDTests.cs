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
public sealed class InstructionANDTests : InstructionConstants<InstructionAND>
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue());

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister());

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory());

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("10h", OperandVariant.Value, Size.Quad));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("rax", OperandVariant.Register, Size.Quad));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory());

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, null, "test_label"), GenerateOperand("10h", OperandVariant.Value, Size.Quad));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, null, "test_label"), GenerateOperand("rax", OperandVariant.Register, Size.Quad));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsRegisterValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("0000111111110000b", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0110_1001_1111_1111_1010_0101_0101_1010;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0101_0101_0000, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsRegisterRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("rcx", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0110_1001_1111_1111_1010_0101_0101_1010;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_1111_1111_0000;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_0101_0101_0000, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, [], "test_label"), GenerateOperand("0000111111110000b", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add("test_label", GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 0b_0000_0000_0000_0000_0000_0000_0000_0000_0110_1001_1111_1111_1010_0101_0101_1010);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0101_0101_0000, virtualMachine.GetQuad(0));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, [], "test_label"), GenerateOperand("rcx", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_1111_1111_0000;
        virtualMachine.Memory.LabelMemoryLocations.Add("test_label", GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 0b_0000_0000_0000_0000_0000_0000_0000_0000_0110_1001_1111_1111_1010_0101_0101_1010);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_1111_0000_0000_0101_0101_0000, virtualMachine.GetQuad(0));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }
}