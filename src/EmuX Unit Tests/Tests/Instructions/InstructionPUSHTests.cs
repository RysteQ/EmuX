using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Instructions.Internal;

namespace EmuXCoreUnitTests.Tests.Instructions;

[TestClass]
public sealed class InstructionPUSHTests : InstructionConstants<InstructionPUSH>
{
    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantNoOperands_NotValid()
    {
        IInstruction instruction = GenerateInstruction();

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word));

        Assert.AreEqual(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandMemory_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, []));

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
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "[test_label]")]));

        Assert.AreEqual(false, instruction.IsValid());
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

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
            IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), prefix, GenerateOperand("10", OperandVariant.Value, Size.Byte));

            Assert.AreEqual(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedValueByte()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("0", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>(0, virtualMachine.PopByte());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedValueWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("0", OperandVariant.Value, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedValueDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand("0", OperandVariant.Value, Size.Dword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(0, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomByte()
    {
        byte valueToPushAndPop = (byte)Random.Shared.NextInt64();
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand(valueToPushAndPop.ToString(), OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual(valueToPushAndPop, virtualMachine.PopByte());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomWord()
    {
        ushort valueToPushAndPop = (ushort)Random.Shared.NextInt64();
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand(valueToPushAndPop.ToString(), OperandVariant.Value, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(valueToPushAndPop, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomDouble()
    {
        uint valueToPushAndPop = (uint)Random.Shared.NextInt64();
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GeneratePrefix(), GenerateOperand(valueToPushAndPop.ToString(), OperandVariant.Value, Size.Dword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        instruction.Execute(virtualMachine);

        Assert.AreEqual(valueToPushAndPop, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedRegisterWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedRegisterDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("eax", OperandVariant.Register, Size.Dword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(0, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedRegisterQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0, virtualMachine.PopQuad());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomRegisterWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort valueToPushAndPop = (ushort)Random.Shared.NextInt64();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = valueToPushAndPop;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(valueToPushAndPop, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomRegisterDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("eax", OperandVariant.Register, Size.Dword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint valueToPushAndPop = (uint)Random.Shared.NextInt64();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().EAX = valueToPushAndPop;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(valueToPushAndPop, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomRegisterQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GeneratePrefix(), GenerateOperand("rax", OperandVariant.Register, Size.Qword));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong valueToPushAndPop = (ulong)Random.Shared.NextInt64();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = valueToPushAndPop;
        instruction.Execute(virtualMachine);

        Assert.AreEqual(valueToPushAndPop, virtualMachine.PopQuad());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedMemoryWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, 0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedMemoryDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Dword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetDouble(0, 0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<uint>(0, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushZeroedMemoryQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Qword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetQuad(0, 0);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ulong>(0, virtualMachine.PopQuad());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryWord()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ushort valueToPushAndPop = (ushort)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetWord(0, valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(valueToPushAndPop, virtualMachine.PopWord());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryDouble()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Dword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        uint valueToPushAndPop = (uint)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetDouble(0, valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(valueToPushAndPop, virtualMachine.PopDouble());
    }

    [TestMethod]
    public void TestExecuteMethod_PushRandomMemoryQuad()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Qword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        ulong valueToPushAndPop = (ulong)Random.Shared.NextInt64();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 1));
        virtualMachine.SetQuad(0, valueToPushAndPop);
        instruction.Execute(virtualMachine);

        Assert.AreEqual(valueToPushAndPop, virtualMachine.PopQuad());
    }
}