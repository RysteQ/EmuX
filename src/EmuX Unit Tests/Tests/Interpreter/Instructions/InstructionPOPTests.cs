using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionPOPTests : InstructionConstants<InstructionPOP>
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("ax", OperandVariant.Register, Size.Word));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandMemory_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, []));

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "[test_label]")]));

        Assert.AreEqual<bool>(false, instruction.IsValid());
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
    public void TestIsValidMethod_VariantThreeOperandsRegisterRegisterValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterRegisterValue(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantThreeOperandsRegisterMemoryValue_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.ThreeOperandsRegisterMemoryValue(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestExecuteMethod_PopZeroedWordToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("ax", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.PushWord(0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
    }

    [TestMethod]
    public void TestExecuteMethod_PopZeroedDoubleToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("eax", OperandVariant.Register, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.PushDouble(0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX);
    }

    [TestMethod]
    public void TestExecuteMethod_PopZeroedQuadToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.PushQuad(0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
    }

    [TestMethod]
    public void TestExecuteMethod_PopRandomWordToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("ax", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort valueToPushAndPop = (ushort)Random.Shared.NextInt64();

        virtualMachine.PushWord(valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(valueToPushAndPop, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
    }

    [TestMethod]
    public void TestExecuteMethod_PopRandomDoubleToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("eax", OperandVariant.Register, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint valueToPushAndPop = (uint)Random.Shared.NextInt64();

        virtualMachine.PushDouble(valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(valueToPushAndPop, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX);
    }

    [TestMethod]
    public void TestExecuteMethod_PopRandomQuadToRegister()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong valueToPushAndPop = (ulong)Random.Shared.NextInt64();

        virtualMachine.PushQuad(valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(valueToPushAndPop, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX);
    }

    [TestMethod]
    public void TestExecuteMethod_PopZeroedWordToMemory()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, ushort.MaxValue);
        virtualMachine.PushWord(0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.GetWord(0));
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedMemoryDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetDouble(0, uint.MaxValue);
        virtualMachine.PushDouble(0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(0, virtualMachine.GetDouble(0));
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedMemoryQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetQuad(0, ulong.MaxValue);
        virtualMachine.PushQuad(0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0, virtualMachine.GetQuad(0));
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort valueToPushAndPop = (ushort)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, ushort.MaxValue);
        virtualMachine.PushWord(valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(valueToPushAndPop, virtualMachine.GetWord(0));
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint valueToPushAndPop = (uint)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetDouble(0, uint.MaxValue);
        virtualMachine.PushDouble(valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(valueToPushAndPop, virtualMachine.GetDouble(0));
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong valueToPushAndPop = (ulong)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetQuad(0, ulong.MaxValue);
        virtualMachine.PushQuad(valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(valueToPushAndPop, virtualMachine.GetQuad(0));
    }
}