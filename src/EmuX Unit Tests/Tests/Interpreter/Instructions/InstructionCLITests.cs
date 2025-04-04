using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionCLITests : InstructionConstants<InstructionCLI>
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue());

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister());

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
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue());

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister());

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestExecuteMethod_ClearIFlah()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetFlag(EFlags.IF, true);
        virtualMachine.SetFlag(EFlags.VIF, true);
        virtualMachine.SetIOPL(true, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.IF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.VIF));
    }

    [TestMethod]
    public void TestExecuteMethod_ClearVIFlah()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetFlag(EFlags.CF, true);
        virtualMachine.SetFlag(EFlags.IF, true);
        virtualMachine.SetFlag(EFlags.VIF, true);
        virtualMachine.SetIOPL(false, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.IF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.VIF));
    }
}