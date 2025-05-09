using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

namespace EmuX_Unit_Tests.Tests.Common;

[TestClass]
public sealed class OperandDecoderTest : TestWideInternalConstants
{
    [TestMethod]
    public void GetImmediateCharOperandByteTest()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<byte>(65, operandDecoder.GetOperandByte(virtualMachine, GenerateOperand("'A'", OperandVariant.Value, Size.Word, [])));
        Assert.AreEqual<byte>(66, operandDecoder.GetOperandByte(virtualMachine, GenerateOperand("'B'", OperandVariant.Value, Size.Word, [])));
    }

    [TestMethod]
    public void GetImmediateBinaryOperandWordTest()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<ushort>(0b_1111_0000_1010_1001, operandDecoder.GetOperandWord(virtualMachine, GenerateOperand("1111000010101001b", OperandVariant.Value, Size.Word, [])));
        Assert.AreEqual<ushort>(0b_0001_0110_1000_1001, operandDecoder.GetOperandWord(virtualMachine, GenerateOperand("0001011010001001b", OperandVariant.Value, Size.Word, [])));
    }

    [TestMethod]
    public void GetImmediateHexOperandDoubleTest()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<uint>(0x47299174, operandDecoder.GetOperandDouble(virtualMachine, GenerateOperand("47299174h", OperandVariant.Value, Size.Word, [])));
        Assert.AreEqual<uint>(0x13820482, operandDecoder.GetOperandDouble(virtualMachine, GenerateOperand("13820482h", OperandVariant.Value, Size.Word, [])));
    }

    [TestMethod]
    public void GetImmediateOperandQuadTest()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<ulong>(827129, operandDecoder.GetOperandQuad(virtualMachine, GenerateOperand("827129", OperandVariant.Value, Size.Word, [])));
        Assert.AreEqual<ulong>(9828794, operandDecoder.GetOperandQuad(virtualMachine, GenerateOperand("9828794", OperandVariant.Value, Size.Word, [])));
    }

    [TestMethod]
    public void GetMemoryOffset_Label_Test()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IOperand operand = GenerateOperand(string.Empty, OperandVariant.NaN, Size.Quad, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]);
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 100, 0));

        Assert.AreEqual<int>(100, operandDecoder.GetPointerMemoryAddress(virtualMachine, operand));
    }

    [TestMethod]
    public void GetMemoryOffset_Register_Test()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IOperand operand = GenerateOperand(string.Empty, OperandVariant.NaN, Size.Quad, [GenerateMemoryOffset(MemoryOffsetType.Register, MemoryOffsetOperand.NaN, "rax")]);
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 200;

        Assert.AreEqual<int>(200, operandDecoder.GetPointerMemoryAddress(virtualMachine, operand));
    }

    [TestMethod]
    public void GetMemoryOffset_Integer_Test()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IOperand operand = GenerateOperand(string.Empty, OperandVariant.NaN, Size.Quad, [GenerateMemoryOffset(MemoryOffsetType.Integer, MemoryOffsetOperand.NaN, "300h")]);
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<int>(0x_300, operandDecoder.GetPointerMemoryAddress(virtualMachine, operand));
    }

    [TestMethod]
    public void GetMemoryOffset_LabelInteger_Test()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IOperand operand = GenerateOperand(string.Empty, OperandVariant.NaN, Size.Quad, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label"), GenerateMemoryOffset(MemoryOffsetType.Integer, MemoryOffsetOperand.Addition, "300h")]);
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 100, 0));

        Assert.AreEqual<int>(100 + 0x_300, operandDecoder.GetPointerMemoryAddress(virtualMachine, operand));
    }

    [TestMethod]
    public void GetMemoryOffset_LabelRegisterInteger_Test()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IOperand operand = GenerateOperand(string.Empty, OperandVariant.NaN, Size.Quad, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label"), GenerateMemoryOffset(MemoryOffsetType.Register, MemoryOffsetOperand.Addition, "rax"), GenerateMemoryOffset(MemoryOffsetType.Integer, MemoryOffsetOperand.Addition, "300h")]);
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 100, 0));
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 200;

        Assert.AreEqual<int>(100 + 200 + 0x_300, operandDecoder.GetPointerMemoryAddress(virtualMachine, operand));
    }

    [TestMethod]
    public void GetMemoryOffset_LabelRegisterRegisterInteger_Test()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IOperand operand = GenerateOperand(string.Empty, OperandVariant.NaN, Size.Quad, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label"), GenerateMemoryOffset(MemoryOffsetType.Register, MemoryOffsetOperand.Addition, "rax"), GenerateMemoryOffset(MemoryOffsetType.Register, MemoryOffsetOperand.Addition, "rcx"), GenerateMemoryOffset(MemoryOffsetType.Integer, MemoryOffsetOperand.Addition, "300h")]);
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 100, 0));
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 200;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 400;

        Assert.AreEqual<int>(100 + 200 + 400 + 0x_300, operandDecoder.GetPointerMemoryAddress(virtualMachine, operand));
    }

    [TestMethod]
    public void GetMemoryOffset_LabelRegisterRegisterScaleInteger_Test()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IOperand operand = GenerateOperand(string.Empty, OperandVariant.NaN, Size.Quad, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label"), GenerateMemoryOffset(MemoryOffsetType.Register, MemoryOffsetOperand.Addition, "rax"), GenerateMemoryOffset(MemoryOffsetType.Register, MemoryOffsetOperand.Addition, "rcx"), GenerateMemoryOffset(MemoryOffsetType.Integer, MemoryOffsetOperand.Multiplication, "4"), GenerateMemoryOffset(MemoryOffsetType.Integer, MemoryOffsetOperand.Addition, "300h")]);
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", 100, 0));
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().RAX = 200;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().RCX = 400;

        Assert.AreEqual<int>(((100 + 200 + 400) * 4) + 0x_300, operandDecoder.GetPointerMemoryAddress(virtualMachine, operand));
    }
}