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
public sealed class InstructionIMULTests : InstructionConstants<InstructionIMUL>
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandMemory_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("ax", OperandVariant.Register, Size.Word));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterMemory_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, []));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsMemoryRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantThreeOperandsRegisterRegisterValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantThreeOperandsRegisterMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("ax", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(100, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX);
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyMemory()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, 5);
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(50, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX);
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegisterRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("cx", OperandVariant.Register, Size.Word), GenerateOperand("dx", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 7;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(70, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegisterMemory()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, 5);
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(50, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegisterRegisterValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GenerateOperand("cx", OperandVariant.Register, Size.Word), GenerateOperand("dx", OperandVariant.Register, Size.Word), GenerateOperand("5", OperandVariant.Value, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 7;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(35, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
    }

    [TestMethod]
    public void TestExecuteMethod_MultiplyRegisterMemoryValue()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GenerateOperand("cx", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), GenerateOperand("5", OperandVariant.Value, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, 5);
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(25, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
    }
}