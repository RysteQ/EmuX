using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SegmentRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using EmuXCoreUnitTests.Tests.Instructions.Internal;

namespace EmuXCoreUnitTests.Tests.Instructions;

[TestClass]
public sealed class InstructionSCASWTests : InstructionConstants<InstructionSCASW>
{
    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantNoOperands_NotValid()
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

        Assert.AreEqual<bool>(true, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantOneOperandLabel_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandLabel(), GeneratePrefix(), GenerateOperand("test_label", OperandVariant.Label, Size.Byte, []));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterValue(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterRegister(), GeneratePrefix(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("cl", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsRegisterMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GeneratePrefix(), GenerateOperand("ax", OperandVariant.Register, Size.Word), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "[test_label]")]));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryValue_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryValue(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("10", OperandVariant.Value, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsMemoryRegister_Valid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []), GenerateOperand("cl", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_NoPrefix_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsMemoryRegister(), GeneratePrefix(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

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
        List<IPrefix> validPrefixes = [new PrefixREPE(), new PrefixREPNE()];

        foreach (IPrefix prefix in AllPrefixes())
        {
            IInstruction instruction = GenerateInstruction(InstructionVariant.NoOperands(), prefix);

            Assert.AreEqual<bool>(validPrefixes.Any(selectedPrefix => selectedPrefix.Type == prefix.Type), instruction.IsValid());
        }
    }

    [TestMethod]
    public void TestExecuteMethod_ScanStringNoOperands()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.NoOperands());
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 16966;
        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().FLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI = 0;
        virtualMachine.SetByte(0, 0b_0100_0001);
        virtualMachine.SetByte(1, 0b_0100_0010);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0b_0100_0010_0100_0001, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreNotEqual<ulong>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP);
    }

    [TestMethod]
    public void TestExecuteMethod_ScanStringMemoryOperand()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("test_label", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 17216;
        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().FLAGS = 0;
        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetByte(0, 0b_0100_0001);
        virtualMachine.SetByte(1, 0b_0100_0010);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0b_0100_0010_0100_0001, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreNotEqual<ulong>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP);
    }

    [TestMethod]
    public void TestExecuteMethod_ScanStringNoOperands_IncrementEDI()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.NoOperands());
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 16966;
        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().FLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI = 0;
        virtualMachine.SetByte(0, 0b_0100_0001);
        virtualMachine.SetByte(1, 0b_0100_0010);
        virtualMachine.SetFlag(EFlags.DF, true);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0b_0100_0010_0100_0001, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<uint>(2, virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().EDI);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreNotEqual<ulong>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP);
    }

    [TestMethod]
    public void TestExecuteMethod_ScanStringMemoryOperand_DecrementEDI()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GeneratePrefix(), GenerateOperand("test_label", OperandVariant.Memory, Size.Word, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 17216;
        virtualMachine.CPU.GetRegister<VirtualRegisterEFLAGS>().FLAGS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().RDI = 2;
        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 0, 0));
        virtualMachine.SetByte(0, 0b_0100_0001);
        virtualMachine.SetByte(1, 0b_0100_0010);
        virtualMachine.SetFlag(EFlags.DF, false);
        instruction.Execute(virtualMachine);

        Assert.AreEqual<ushort>(0b_0100_0010_0100_0001, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<uint>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRDI>().EDI);
        Assert.AreEqual<bool>(false, virtualMachine.GetFlag(EFlags.OF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.SF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.ZF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.AF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.PF));
        Assert.AreEqual<bool>(true, virtualMachine.GetFlag(EFlags.CF));
        Assert.AreNotEqual<ulong>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP);
    }
}