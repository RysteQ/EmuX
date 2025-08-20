using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using EmuXCoreUnitTests.Tests.Instructions.Internal;

namespace EmuXCoreUnitTests.Tests.Instructions;

[TestClass]
public sealed class InstructionCMPTests : InstructionConstants<InstructionCMP>
{
    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantNoOperands_NotValid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandLabel_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandLabel(), GeneratePrefix(), GenerateOperand("test_label", OperandVariant.Label, Size.Byte, []));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, null), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsValueRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_AllPrefixes_ValidVariant_CorrectPrefixCheck()
    {
        List<IPrefix> validPrefixes = [];

        foreach (IPrefix prefix in AllPrefixes())
        {
            IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), prefix, GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

            Assert.AreEqual(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnCFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("ffffffffffffffffh", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnOFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("8fffffffffffffffh", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0x_80_00_00_00_00_00_00_00;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnSFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("efffffffffffffffh", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.SF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnZFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("1", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.ZF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnAFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("15", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOnPFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("15", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.PF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffCFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("10h", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 100;
        virtualMachine.SetFlag(EFlags.CF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffOFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("6fffffffffffffffh", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 1;
        virtualMachine.SetFlag(EFlags.OF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffSFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("ffffffffffffffffh", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0x_80_00_00_00_00_00_00_00;
        virtualMachine.SetFlag(EFlags.SF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.SF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffZFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("1", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 2;
        virtualMachine.SetFlag(EFlags.ZF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.ZF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffAFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("15", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 15;
        virtualMachine.SetFlag(EFlags.AF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.AF));
    }

    [TestMethod]
    public void TestExecuteMethod_VariantRegisterValue_TurnOffPFlag()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword), GenerateOperand("15", OperandVariant.Value, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().EFLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        virtualMachine.SetFlag(EFlags.PF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.PF));
    }
}