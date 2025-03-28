using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionDASTests : InstructionConstants<InstructionDAS>
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

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0x9a;
        virtualMachine.SetFlag(EFlagsEnum.CF, false);
        virtualMachine.SetFlag(EFlagsEnum.AF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0x9a - 0x60 - 6, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlagsEnum.CF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlagsEnum.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_SetAFlagToTrueAndKeepCFlagFalse()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0x0a;
        virtualMachine.SetFlag(EFlagsEnum.CF, false);
        virtualMachine.SetFlag(EFlagsEnum.AF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0x0a - 6, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.CF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlagsEnum.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_SetAFlagToFaleAndCFlagToTrue()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0xa0;
        virtualMachine.SetFlag(EFlagsEnum.CF, false);
        virtualMachine.SetFlag(EFlagsEnum.AF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0xa0 - 0x60, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlagsEnum.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_SetAFlagToFalseAndKeepCFlagFalse()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        virtualMachine.SetFlag(EFlagsEnum.CF, false);
        virtualMachine.SetFlag(EFlagsEnum.AF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlagsEnum.AF));
    }
}