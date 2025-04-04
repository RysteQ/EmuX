using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Enums;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionAAATests : InstructionConstants<InstructionAAA>
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
    public void TestExecuteMethod_ModifiesAXRegister()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 256;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 255;
        virtualMachine.SetFlag(EFlags.AF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(256 + 255 + 0x_106, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<byte>(5, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_DoesNotModifyAXRegister()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 8;
        virtualMachine.SetFlag(EFlags.AF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH);
        Assert.AreEqual<byte>(8, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
    }
}