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
public sealed class InterpreterExecutionTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestSingleInstructionExecution()
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