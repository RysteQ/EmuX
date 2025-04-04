using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionCMPSBTests : InstructionConstants<InstructionCMPSB>
{
    [TestMethod]
    public void TestIsValidMethod_VariantNoOperands_Valid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual<bool>(true, instruction.IsValid());
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
    public void TestIsValidMethod_VariantTwoOperandsRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("10h", OperandVariant.Value, Size.Quad));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("rax", OperandVariant.Register, Size.Quad));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory());

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, null, "test_label"), GenerateOperand("10h", OperandVariant.Value, Size.Quad));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad, null, "test_label"), GenerateOperand("rax", OperandVariant.Register, Size.Quad));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestExecuteMethod_IncreaseRegisterRsiAndRdi()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI = 0;
        virtualMachine.SetFlag(EFlags.DF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(1, virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI);
        Assert.AreEqual<ulong>(1, virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI);
    }

    [TestMethod]
    public void TestExecuteMethod_DecreaseRegisterRsiAndRdi()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI = 1;
        virtualMachine.SetFlag(EFlags.DF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRSI>().RSI);
        Assert.AreEqual<ulong>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI);
    }
}