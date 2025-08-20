using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Instructions.Internal;

namespace EmuXCoreUnitTests.Tests.Instructions;

[TestClass]
public sealed class InstructionIMULTests : InstructionConstants<InstructionIMUL>
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
    public void TestIsValidMethod_NoPrefix_VariantOneOperandRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandMemory_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandLabel_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandLabel(), GeneratePrefix(), GenerateOperand("test_label", OperandVariant.Label, Size.Byte, []));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("ax", OperandVariant.Register, Size.Word));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, []));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsValueRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterRegisterValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantThreeOperandsRegisterMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_AllPrefixes_ValidVariant_CorrectPrefixCheck()
    {
        List<IPrefix> validPrefixes = [];

        foreach (IPrefix prefix in AllPrefixes())
        {
            IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), prefix, GenerateOperand("al", OperandVariant.Register, Size.Byte));

            Assert.AreEqual(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(100, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX);
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyMemory()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, 5);
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(50, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX);
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual(true, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegisterRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("cx", OperandVariant.Register, Size.Word), GenerateOperand("dx", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 7;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(70, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegisterMemory()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, 5);
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(50, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegisterRegisterValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GeneratePrefix(), GenerateOperand("cx", OperandVariant.Register, Size.Word), GenerateOperand("dx", OperandVariant.Register, Size.Word), GenerateOperand("5", OperandVariant.Value, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 7;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(35, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegisterMemoryValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GeneratePrefix(), GenerateOperand("cx", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("5", OperandVariant.Value, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, 5);
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(25, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX);
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual(false, virtualMachine.GetFlag(EFlags.OF));
    }
}