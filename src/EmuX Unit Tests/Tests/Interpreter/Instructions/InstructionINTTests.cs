﻿using EmuX_Unit_Tests.Tests.Interpreter.Instructions.Internal;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.Instructions;
using EmuXCore.Instructions.Internal;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

namespace EmuX_Unit_Tests.Tests.Interpreter.Instructions;

[TestClass]
public sealed class InstructionINTTests : InstructionConstants<InstructionINT>
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
    public void TestIsValidMethod_VariantOneOperandRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandRegister(), GenerateOperand("al", OperandVariant.Register, Size.Byte));

        Assert.AreEqual<bool>(false, instruction.IsValid());
    }

    [TestMethod]
    public void TestIsValidMethod_VariantOneOperandMemory_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandMemory(), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

        Assert.AreEqual<bool>(false, instruction.IsValid());
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
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsRegisterMemory(), GenerateOperand("al", OperandVariant.Register, Size.Byte), GenerateOperand("[test_label]", OperandVariant.Memory, Size.Byte, []));

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
    public void TestIsValidMethod_VariantTwoOperandsValueRegister_NotValid()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.TwoOperandsValueRegister(), GenerateOperand("10", OperandVariant.Value, Size.Byte), GenerateOperand("al", OperandVariant.Register, Size.Byte));

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
    public void TestExecuteMethod_WriteAndReadDiskInterrupt()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("13h", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        byte[] data = new byte[512];
        byte[] readBytes = new byte[512];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 0x_03;
        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AL = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterES>().ES = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRBX>().BX = 0;

        Random.Shared.NextBytes(data);
        Random.Shared.NextBytes(readBytes);

        for (int i = 0; i < data.Length; i++)
        {
            virtualMachine.SetByte(i, data[i]);
        }

        try
        {
            instruction.Execute(virtualMachine);
        }
        catch
        {
            Assert.Fail();

            return;
        }

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 0x_02;

        for (int i = 0; i < data.Length; i++)
        {
            virtualMachine.SetByte(i, 0);
        }

        try
        {
            instruction.Execute(virtualMachine);
        }
        catch
        {
            Assert.Fail();

            return;
        }

        for (int i = 0; i < data.Length; i++)
        {
            readBytes[i] = virtualMachine.GetByte(i);
        }

        CollectionAssert.AreEquivalent(data, readBytes);
    }

    [TestMethod]
    public void TestExecuteMethod_RTCInterrupt_SetAndReadSystemClock()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("1ah", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        DateTime currentTime = DateTime.MinValue.AddMinutes(10);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 1;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = (byte)currentTime.Hour;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = (byte)currentTime.Minute;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = (byte)currentTime.Second;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 0;
        instruction.Execute(virtualMachine);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>((byte)currentTime.Hour, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH);
        Assert.AreEqual<byte>((byte)currentTime.Minute, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL);
        Assert.AreEqual<byte>((byte)currentTime.Second, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH);
    }

    [TestMethod]
    public void TestExecuteMethod_RTCInterrupt_SetAndReadRTC()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("1ah", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        DateTime currentTime = DateTime.MinValue.AddMinutes(10);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 3;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH = (byte)currentTime.Hour;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL = (byte)currentTime.Minute;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH = (byte)currentTime.Second;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DL = 0;
        instruction.Execute(virtualMachine);

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = 2;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DX = 0;
        instruction.Execute(virtualMachine);

        Assert.AreEqual<byte>((byte)currentTime.Hour, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CH);
        Assert.AreEqual<byte>((byte)currentTime.Minute, virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().CL);
        Assert.AreEqual<byte>((byte)currentTime.Second, virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().DH);
    }

    [TestMethod]
    public void TestExecuteMethod_NonExistentInterrupt_RuntimeException()
    {
        IInstruction instruction = GenerateInstruction(InstructionVariant.OneOperandValue(), GenerateOperand("1ah", OperandVariant.Value, Size.Byte));
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        try
        {
            instruction.Execute(virtualMachine);
        }
        catch
        {
            Assert.IsTrue(true);

            return;
        }

        Assert.Fail();
    }
}