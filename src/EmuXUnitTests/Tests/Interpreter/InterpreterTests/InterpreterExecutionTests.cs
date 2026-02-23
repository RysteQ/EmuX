using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.InterpreterTests;

[TestClass]
public sealed class InterpreterExecutionTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestSingleInstructionExecution()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        IInterpreter interpreter = GenerateInterpreter();
        IInstruction[] instructions =
        [
            GenerateInstruction<InstructionNOT>(InstructionVariant.OneOperandRegister(), null, GenerateOperand("AX", OperandVariant.Register, Size.Word, []), null, null, GenerateOperandDecoder(), GenerateFlagStateProcessor()),
            GenerateInstruction<InstructionNOT>(InstructionVariant.OneOperandRegister(), null, GenerateOperand("AX", OperandVariant.Register, Size.Word, []), null, null, GenerateOperandDecoder(), GenerateFlagStateProcessor())
        ];

        interpreter.VirtualMachine = virtualMachine;
        interpreter.Instructions = instructions;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 0;
        virtualMachine.Actions.Clear();
        interpreter.ExecuteStepOver();

        Assert.AreEqual<ushort>(ushort.MaxValue, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
    }

    [TestMethod]
    public void TestStepOverInstructionExecution_SingleInstructionExecution_DoesNotStepOver()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        IInterpreter interpreter = GenerateInterpreter();
        IInstruction[] instructions =
        [
            GenerateInstruction<InstructionCALL>(InstructionVariant.OneOperandMemory(), null, GenerateOperand("[test_label]", OperandVariant.Memory, Size.Qword, [GenerateMemoryOffset(MemoryOffsetType.Label, MemoryOffsetOperand.NaN, "test_label")]), null, null, GenerateOperandDecoder(), GenerateFlagStateProcessor()),
            GenerateInstruction<InstructionNOT>(InstructionVariant.OneOperandRegister(), null, GenerateOperand("AX", OperandVariant.Register, Size.Word, []), null, null, GenerateOperandDecoder(), GenerateFlagStateProcessor()),
            GenerateInstruction<InstructionNOT>(InstructionVariant.OneOperandRegister(), null, GenerateOperand("AX", OperandVariant.Register, Size.Word, []), null, null, GenerateOperandDecoder(), GenerateFlagStateProcessor()),
            GenerateInstruction<InstructionRET>(InstructionVariant.NoOperands(), null, null, null, null, GenerateOperandDecoder(), GenerateFlagStateProcessor())
        ];

        virtualMachine.Memory.LabelMemoryLocations.Add(GenerateMemoryLabel("test_label", (int)(instructions[0].Bytes), 2));
        interpreter.VirtualMachine = virtualMachine;
        interpreter.Instructions = instructions;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 0;
        virtualMachine.Actions.Clear();
        interpreter.ExecuteStepOver();

        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<ulong>(instructions[0].Bytes, virtualMachine.CPU.GetRegister<VirtualRegisterRIP>().RIP);
    }

    [TestMethod]
    public void TestStepOverInstructionExecution_MultiipleInstructionExecution_DoesStepOver()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        IInterpreter interpreter = GenerateInterpreter();
        IInstruction[] instructions =
        [
            GenerateInstruction<InstructionNOT>(InstructionVariant.OneOperandRegister(), null, GenerateOperand("AX", OperandVariant.Register, Size.Word, []), null, null, GenerateOperandDecoder(), GenerateFlagStateProcessor())
        ];

        interpreter.VirtualMachine = virtualMachine;
        interpreter.Instructions = instructions;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 0;
        virtualMachine.Actions.Clear();
        interpreter.ExecuteStep();

        Assert.AreEqual<ushort>(ushort.MaxValue, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
    }

    [TestMethod]
    public void TestMultipleInstructionsExecution()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        IInterpreter interpreter = GenerateInterpreter();
        IInstruction[] instructions =
        [
            GenerateInstruction<InstructionNOT>(InstructionVariant.OneOperandRegister(), null, GenerateOperand("AX", OperandVariant.Register, Size.Word, []), null, null, GenerateOperandDecoder(), GenerateFlagStateProcessor()),
            GenerateInstruction<InstructionSUB>(InstructionVariant.TwoOperandsRegisterValue(), null, GenerateOperand("AX", OperandVariant.Register, Size.Word, []), GenerateOperand("10", OperandVariant.Value, Size.Word, []), null, GenerateOperandDecoder(), GenerateFlagStateProcessor())
        ];

        interpreter.VirtualMachine = virtualMachine;
        interpreter.Instructions = instructions;

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX = 0;
        virtualMachine.Actions.Clear();
        interpreter.Execute();

        Assert.AreEqual<ushort>(ushort.MaxValue - 10, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
    }
}