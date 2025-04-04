using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionDAATests : InstructionConstants<InstructionDAA>
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
    public void TestExecuteMethod_SetAFlagAndCFlagToTrue()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        virtualMachine.SetFlag(EFlags.CF, true);
        virtualMachine.SetFlag(EFlags.AF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0x60 + 6, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_SetAFlagToTrueAndCFlagToFalse()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        virtualMachine.SetFlag(EFlags.CF, false);
        virtualMachine.SetFlag(EFlags.AF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(6, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_SetAFlagToFaleAndCFlagToTrue()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        virtualMachine.SetFlag(EFlags.CF, true);
        virtualMachine.SetFlag(EFlags.AF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0x60, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_SetAFlagAndCFlagToFalse()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        virtualMachine.SetFlag(EFlags.CF, false);
        virtualMachine.SetFlag(EFlags.AF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
    }
}