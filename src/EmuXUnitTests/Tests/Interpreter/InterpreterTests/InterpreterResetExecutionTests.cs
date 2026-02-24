using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.Interpreter.InterpreterTests;

[TestClass]
public sealed class InterpreterResetExecutionTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestSingleUndoMethod()
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
        interpreter.Execute();

        Assert.AreEqual<ushort>(ushort.MaxValue, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreNotEqual<int>(0, virtualMachine.Actions.Count);
        Assert.AreNotEqual<int>(-1, interpreter.CurrentInstructionIndex);

        interpreter.ResetExecution();

        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
        Assert.AreEqual<int>(0, virtualMachine.Actions.Count);
        Assert.AreEqual<int>(-1, interpreter.CurrentInstructionIndex);
    }

    [TestMethod]
    public void TestMultipleUndoMethod()
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
        interpreter.UndoActions(2);

        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
    }

    [TestMethod]
    public void TestMultipleUndoMethodSeparately()
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

        interpreter.UndoAction();

        Assert.AreEqual<ushort>(ushort.MaxValue, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);

        interpreter.UndoAction();

        Assert.AreEqual<ushort>(0, virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AX);
    }
}