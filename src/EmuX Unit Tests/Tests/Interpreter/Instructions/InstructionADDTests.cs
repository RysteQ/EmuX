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
public sealed class InstructionADDTests : InstructionConstants<InstructionADD>
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("10h", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 10;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0x_10 + 10, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlagsEnum.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsRegisterRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("rcx", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 20;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(10 + 20, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlagsEnum.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, null, "test_label"), GenerateOperand("10h", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add("test_label", GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 10);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(10 + 16, virtualMachine.GetQuad(0));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlagsEnum.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantTwoOperandsMemoryRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, null, "test_label"), GenerateOperand("rax", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 20;
        virtualMachine.Memory.LabelMemoryLocations.Add("test_label", GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetQuad(0, 10);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(10 + 20, virtualMachine.GetQuad(0));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlagsEnum.PF));
    }
}