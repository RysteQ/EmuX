using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionPUSHTests : InstructionConstants<InstructionPUSH>
{
    [TestMethod]
    public void TestIsValidMethod_VariantNoOperands_NotValid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(true, instruction.IsValid());
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
    public void TestExecuteMethod_PushZeroedValueByte()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("0", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0, virtualMachine.PopByte());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedValueWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("0", OperandVariant.Value, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedValueDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("0", OperandVariant.Value, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(0, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomByte()
    {
        byte valueToPushAndPop = (byte)Random.Shared.NextInt64();
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand(valueToPushAndPop.ToString(), OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(valueToPushAndPop, virtualMachine.PopByte());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomWord()
    {
        ushort valueToPushAndPop = (ushort)Random.Shared.NextInt64();
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand(valueToPushAndPop.ToString(), OperandVariant.Value, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(valueToPushAndPop, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomDouble()
    {
        uint valueToPushAndPop = (uint)Random.Shared.NextInt64();
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand(valueToPushAndPop.ToString(), OperandVariant.Value, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(valueToPushAndPop, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedRegisterWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("ax", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedRegisterDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("eax", OperandVariant.Register, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(0, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedRegisterQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0, virtualMachine.PopQuad());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomRegisterWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("ax", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort valueToPushAndPop = (ushort)Random.Shared.NextInt64();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = valueToPushAndPop;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(valueToPushAndPop, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomRegisterDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("eax", OperandVariant.Register, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint valueToPushAndPop = (uint)Random.Shared.NextInt64();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = valueToPushAndPop;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(valueToPushAndPop, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomRegisterQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("rax", OperandVariant.Register, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong valueToPushAndPop = (ulong)Random.Shared.NextInt64();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = valueToPushAndPop;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(valueToPushAndPop, virtualMachine.PopQuad());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedMemoryWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, 0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedMemoryDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetDouble(0, 0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(0, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedMemoryQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetQuad(0, 0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0, virtualMachine.PopQuad());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort valueToPushAndPop = (ushort)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(valueToPushAndPop, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Double));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint valueToPushAndPop = (uint)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetDouble(0, valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(valueToPushAndPop, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Quad));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong valueToPushAndPop = (ulong)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetQuad(0, valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(valueToPushAndPop, virtualMachine.PopQuad());
    }
}