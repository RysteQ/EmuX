using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionLAHFTests : InstructionConstants<InstructionLAHF>
{
    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantNoOperands_Valid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsValueRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_AllPrefixes_ValidVariant_CorrectPrefixCheck()
    {
        List<IPrefix> validPrefixes = [];

        foreach (IPrefix prefix in AllPrefixes())
        {
            IInstruction instruction = GenerateInstruction(null, prefix);

            Assert.AreEqual<bool>(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_AH_00000010()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetFlag(EFlags.CF, false);
        virtualMachine.SetFlag(EFlags.PF, false);
        virtualMachine.SetFlag(EFlags.AF, false);
        virtualMachine.SetFlag(EFlags.ZF, false);
        virtualMachine.SetFlag(EFlags.SF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0b_0000_0010, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH);
    }

    [TestMethod]
    public void TestExecuteMethod_AH_11010111()
    {
        IInstruction instruction = GenerateInstruction();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.SetFlag(EFlags.CF, true);
        virtualMachine.SetFlag(EFlags.PF, true);
        virtualMachine.SetFlag(EFlags.AF, true);
        virtualMachine.SetFlag(EFlags.ZF, true);
        virtualMachine.SetFlag(EFlags.SF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0b_1101_0111, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH);
    }
}