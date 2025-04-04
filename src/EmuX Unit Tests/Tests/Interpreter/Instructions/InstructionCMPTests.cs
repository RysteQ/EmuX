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
public sealed class InstructionCMPTests : InstructionConstants<InstructionCMP>
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
    public void TestExecuteMethod_VariantRegisterValue_TurnOnCFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("ffffffffffffffffh", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnOFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("8fffffffffffffffh", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0x_80_00_00_00_00_00_00_00;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnSFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("efffffffffffffffh", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.SF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnZFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("1", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.ZF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnAFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("15", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnPFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("15", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffCFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("10h", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 100;
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffOFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("6fffffffffffffffh", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        virtualMachine.SetFlag(EFlags.OF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffSFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("ffffffffffffffffh", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0x_80_00_00_00_00_00_00_00;
        virtualMachine.SetFlag(EFlags.SF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffZFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("1", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 2;
        virtualMachine.SetFlag(EFlags.ZF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffAFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("15", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        virtualMachine.SetFlag(EFlags.AF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffPFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("rax", OperandVariant.Register, Size.Quad), GenerateOperand("15", OperandVariant.Value, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        virtualMachine.SetFlag(EFlags.PF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.PF));
    }
}