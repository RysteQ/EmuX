using EmuX_Unit_Tests.Tests.InternalConstants;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions.Interfaces;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;

namespace EmuX_Unit_Tests.Tests.Instructions;

[TestClass]
public sealed class OperandDecoderTest : TestWideInternalConstants
{
    [TestMethod]
    public void GetImmediateOperandQuadTest()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<ulong>(827129, operandDecoder.GetOperandQuad(virtualMachine, GenerateValueOperand("827129")));
        Assert.AreEqual<ulong>(9828794, operandDecoder.GetOperandQuad(virtualMachine, GenerateValueOperand("9828794")));
    }

    [TestMethod]
    public void GetImmediateCharOperandByteTest()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<byte>(65, operandDecoder.GetOperandByte(virtualMachine, GenerateValueOperand("'A'")));
        Assert.AreEqual<byte>(66, operandDecoder.GetOperandByte(virtualMachine, GenerateValueOperand("'B'")));
    }

    [TestMethod]
    public void GetImmediateBinaryOperandWordTest()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<ushort>(0b_1111_0000_1010_1001, operandDecoder.GetOperandWord(virtualMachine, GenerateValueOperand("1111000010101001b")));
        Assert.AreEqual<ushort>(0b_0001_0110_1000_1001, operandDecoder.GetOperandWord(virtualMachine, GenerateValueOperand("0001011010001001b")));
    }

    [TestMethod]
    public void GetImmediateHexOperandDoubleTest()
    {
        IOperandDecoder operandDecoder = GenerateOperandDecoder();
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        Assert.AreEqual<uint>(0x47299174, operandDecoder.GetOperandDouble(virtualMachine, GenerateValueOperand("47299174h")));
        Assert.AreEqual<uint>(0x13820482, operandDecoder.GetOperandDouble(virtualMachine, GenerateValueOperand("13820482h")));
    }

    private IOperand GenerateValueOperand(string value)
    {
        return new Operand(value, OperandVariant.Value, Size.Byte, [], string.Empty);
    }
}