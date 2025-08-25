using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Interfaces;
using EmuXCore.Interpreter;
using EmuXCore.Interpreter.Enums;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
using EmuXCore.Interpreter.Internal.Models;
using EmuXCore.Interpreter.LexicalSyntax;
using EmuXCore.VM;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.BIOS;
using EmuXCore.VM.Internal.BIOS.Interfaces;
using EmuXCore.VM.Internal.BIOS.Internal;
using EmuXCore.VM.Internal.CPU;
using EmuXCore.VM.Internal.Disk;
using EmuXCore.VM.Internal.GPUs;
using EmuXCore.VM.Internal.Memory;
using EmuXCore.VM.Internal.RTC;
using System.Collections.ObjectModel;

namespace EmuXCore;

/// <summary>
/// The DI factory is responsible for creating the instances of all interface implementations
/// </summary>
public static class DIFactory
{
    // Common

    /// <summary>
    /// Generates an instance of IInstruction
    /// </summary>
    /// <typeparam name="T">The implementation type of IInstruction</typeparam>
    /// <param name="variant">The variant of IInstruction</param>
    /// <param name="prefix">The prefix, if any, of the IInstruction</param>
    /// <param name="firstOperand">The first operand, if any, of the IInstruction</param>
    /// <param name="secondOperand">The second operand, if any, of the IInstruction</param>
    /// <param name="thirdOperand">The third operand, if any, of the IInstruction</param>
    /// <param name="operandDecoder">The operand decoder needed for decoding the operands of the IInstruction</param>
    /// <param name="flagStateProcessor">The flag state processor of the IInstruction needed for updating the CPU flags</param>
    /// <returns>The implementation of IInstruction</returns>
    public static IInstruction GenerateIInstruction<T>(InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) where T : IInstruction => (T)Activator.CreateInstance(typeof(T), variant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor)!;

    /// <summary>
    /// Generates an instance of IInstruction
    /// </summary>
    /// <param name="type">The type of IInstruction</param>
    /// <param name="variant">The variant of IInstruction</param>
    /// <param name="prefix">The prefix, if any, of the IInstruction</param>
    /// <param name="firstOperand">The first operand, if any, of the IInstruction</param>
    /// <param name="secondOperand">The second operand, if any, of the IInstruction</param>
    /// <param name="thirdOperand">The third operand, if any, of the IInstruction</param>
    /// <param name="operandDecoder">The operand decoder needed for decoding the operands of the IInstruction</param>
    /// <param name="flagStateProcessor">The flag state processor of the IInstruction needed for updating the CPU flags</param>
    /// <returns>The implementation of IInstruction</returns>
    public static IInstruction GenerateIInstruction(Type type, InstructionVariant variant, IPrefix? prefix, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) => (IInstruction)Activator.CreateInstance(type, variant, prefix, firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor)!;

    /// <summary>
    /// Generates an instance of IInstruction
    /// </summary>
    /// <param name="type">The type of IInstruction</param>
    /// <param name="variant">The variant of IInstruction</param>
    /// <param name="prefix">The prefix type, if any, of the IInstruction</param>
    /// <param name="firstOperand">The first operand, if any, of the IInstruction</param>
    /// <param name="secondOperand">The second operand, if any, of the IInstruction</param>
    /// <param name="thirdOperand">The third operand, if any, of the IInstruction</param>
    /// <param name="operandDecoder">The operand decoder needed for decoding the operands of the IInstruction</param>
    /// <param name="flagStateProcessor">The flag state processor of the IInstruction needed for updating the CPU flags</param>
    /// <returns>The implementation of IInstruction</returns>
    public static IInstruction GenerateIInstruction(Type type, Type prefix, InstructionVariant variant, IOperand? firstOperand, IOperand? secondOperand, IOperand? thirdOperand, IOperandDecoder operandDecoder, IFlagStateProcessor flagStateProcessor) => (IInstruction)Activator.CreateInstance(type, variant, Activator.CreateInstance(prefix), firstOperand, secondOperand, thirdOperand, operandDecoder, flagStateProcessor)!;

    /// <summary>
    /// Generates an instance of IMemoryOffset
    /// </summary>
    /// <param name="type">The type of the memory offset</param>
    /// <param name="operand">The operand of the memory offset</param>
    /// <param name="fullOperand">The full text as is from the source code</param>
    /// <returns>The implementation of IMemoryOffset</returns>
    public static IMemoryOffset GenerateIMemoryOffset(MemoryOffsetType type, MemoryOffsetOperand operand, string fullOperand) => new MemoryOffset(type, operand, fullOperand);
    
    /// <summary>
    /// Generates an instance of IOperand
    /// </summary>
    /// <param name="fullOperand">The full text as is from the source code</param>
    /// <param name="variant">The variant of the operand</param>
    /// <param name="operandSize">The size of the operand</param>
    /// <param name="offsets">The memort offsets, if any, of the operand</param>
    /// <returns>The implementation of IOperand</returns>
    public static IOperand GenerateIOperand(string fullOperand, OperandVariant variant, Size operandSize, IMemoryOffset[] offsets) => new Operand(fullOperand, variant, operandSize, offsets);
    
    /// <summary>
    /// Generates an instance of IPrefix
    /// </summary>
    /// <typeparam name="T">The implementation type of IPrefix</typeparam>
    /// <returns>The implementation of IPrefix</returns>
    public static IPrefix GenerateIPrefix<T>() where T : IPrefix => Activator.CreateInstance<T>();

    /// <summary>
    /// Generates an instance of IPrefix
    /// </summary>
    /// <param name="type">The type of IPrefix</param>
    /// <returns>The implementation of IPrefix</returns>
    public static IPrefix GenerateIPrefix(Type type) => (IPrefix)Activator.CreateInstance(type)!;

    // Instruction logic

    /// <summary>
    /// Generates an instance of IFlagStateProcessor
    /// </summary>
    /// <returns>The implementation of IFlagStateProcessor</returns>
    public static IFlagStateProcessor GenerateIFlagStateProcessor() => new FlagStateProcessor();

    /// <summary>
    /// Generates an instance of IOperandDecoder
    /// </summary>
    /// <returns>The implementation of IOperandDecoder</returns>
    public static IOperandDecoder GenerateIOperandDecoder() => new OperandDecoder();

    // Interpreter

    /// <summary>
    /// Generates an instance of IBytecode
    /// </summary>
    /// <param name="instruction">The instruction, if it exists, that the bytecode will later transform into</param>
    /// <param name="label">The label, if it exists, that the bytecode will later transform into</param>
    /// <returns>The implementation of IBytecode</returns>
    public static IBytecode GenerateIBytecode(IInstruction? instruction = null, ILabel? label = null) => new Bytecode(instruction, label);

    /// <summary>
    /// Generates an instance of ILabel
    /// </summary>
    /// <param name="labelName">The name of the label</param>
    /// <param name="line">The line that label exists in the source code</param>
    /// <returns>The implementation of ILabel</returns>
    public static ILabel GenerateILabel(string labelName, int line) => new Label(labelName, line);

    /// <summary>
    /// Generates an instance of ILexeme
    /// </summary>
    /// <param name="cpuToTranslateFor">The IVirtualCPU implementation required for the ILexeme to translate into</param>
    /// <param name="sourceCodeLine">The source code line as is from the source code</param>
    /// <param name="prefix">The prefix, if any, of the ILexeme token</param>
    /// <param name="opcode">The opcode of the ILexeme</param>
    /// <param name="firstOperand">The first operand of the ILexeme</param>
    /// <param name="secondOperand">The second operand of the ILexeme</param>
    /// <param name="thirdOperand">The third operand of the ILexeme</param>
    /// <returns>The implementation of ILexeme</returns>
    public static ILexeme GenerateILexeme(IVirtualCPU cpuToTranslateFor, ISourceCodeLine sourceCodeLine, string prefix, string opcode, string firstOperand, string secondOperand, string thirdOperand) => new Lexeme(cpuToTranslateFor, sourceCodeLine, prefix, opcode, firstOperand, secondOperand, thirdOperand);

    /// <summary>
    /// Generates an instance of ILexerResult
    /// </summary>
    /// <param name="instructions">The instructions the ILexerResult will hold</param>
    /// <param name="labels">The labels the ILexerResult will hold</param>
    /// <param name="errors">The parse errors the ILexerResult will hold</param>
    /// <returns>The implementation of ILexerResult</returns>
    public static IParserResult GenerateILexerResult(IList<IInstruction> instructions, IList<ILabel> labels, IList<string> errors) => new ParserResult(instructions, labels, errors);

    /// <summary>
    /// Generates an instance of IInstructionEncoderResult
    /// </summary>
    /// <param name="errors">The parsed bytes the IInstructionEncoderResult will hold</param>
    /// <param name="errors">The parse errors the ILexerResult will hold</param>
    /// <returns>The implementation of IInstructionEncoderResult</returns>
    public static IInstructionEncoderResult GenerateIInstructionEncoderResult(byte[] bytes, ReadOnlyCollection<string> errors) => new InstructionEncoderResult(bytes, errors);

    /// <summary>
    /// Generates an instance of ISourceCodeLine
    /// </summary>
    /// <param name="sourceCode">The source code line as is from the source code</param>
    /// <param name="line">The line the source code line is from the source code</param>
    /// <returns>The implementation of ISourceCodeLine</returns>
    public static ISourceCodeLine GenerateISourceCodeLine(string sourceCode, int line) => new SourceCodeLine(sourceCode, line);
    
    /// <summary>
    /// Generates an instance of IInterpreter
    /// </summary>
    /// <returns>The implementation of IInterepreter</returns>
    public static IInterpreter GenerateIInterpreter() => new Interpreter.Interpreter();
    
    /// <summary>
    /// Generates an instance of ILexer
    /// </summary>
    /// <param name="cpu">The IVirtualCPU implementation the ILexer will tokenise from</param>
    /// <returns>The implementation of ILexer</returns>
    public static ILexer GenerateILexer(IVirtualCPU cpu, IInstructionLookup instructionLookup, IPrefixLookup prefixLookup) => new Lexer(cpu, instructionLookup, prefixLookup);

    /// <summary>
    /// Generates an instance of IInstructionLookup
    /// </summary>
    /// <returns>The implementation of IInstructionLookup</returns>
    public static IInstructionLookup GenerateIInstructionLookup() => new InstructionLookup();

    /// <summary>
    /// Generates an instance of IPrefixLookup
    /// </summary>
    /// <returns>The implementation of IPrefixLookup</returns>
    public static IPrefixLookup GenerateIPrefixLookup() => new PrefixLookup();

    /// <summary>
    /// Generates an instance of IParser
    /// </summary>
    /// <returns>The implementation of IParser</returns>
    public static IParser GenerateIParser(IVirtualCPU virtualCPU, IInstructionLookup instructionLookup, IPrefixLookup prefixLookup) => new Parser(virtualCPU, instructionLookup, prefixLookup);

    /// <summary>
    /// Generates an instance of IParser
    /// </summary>
    /// <returns>The implementation of IParser</returns>
    public static IToken GenerateIToken(TokenType type, string sourceCode) => new Token(type, sourceCode);

    // VM

    /// <summary>
    /// Generates an instance of IDeviceInterruptHandler
    /// </summary>
    /// <returns>The implementation of IDeviceInterruptHandler</returns>
    public static IDeviceInterruptHandler GenerateIDeviceInterruptHandler() => new DeviceInterruptHandler();

    /// <summary>
    /// Generates an instance of IDiskInterruptHandler
    /// </summary>
    /// <returns>The implementation of IDiskInterruptHandler</returns>
    public static IDiskInterruptHandler GenerateIDiskInterruptHandler() => new DiskInterruptHandler();

    /// <summary>
    /// Generates an instance of IRTCInterruptHandler
    /// </summary>
    /// <returns>The implementation of IRTCInterruptHandler</returns>
    public static IRTCInterruptHandler GenerateIRTCInterruptHandler() => new RTCInterruptHandler();

    /// <summary>
    /// Generates an instance of IVideoInterruptHandler
    /// </summary>
    /// <returns>The implementation of IVideoInterruptHandler</returns>
    public static IVideoInterruptHandler GenerateIVideoInterruptHandler() => new VideoInterruptHandler();

    /// <summary>
    /// Generates an instance of IVirtualBIOS
    /// </summary>
    /// <param name="diskInterruptHandler">The IVirtualDiskInterruptHandler the IVirtualBIOS will call upon for disk related interrupts</param>
    /// <param name="rtcInterruptHandler">The IRTCInterruptHandler the IVirtualBIOS will call upon for RTC related interrupts</param>
    /// <param name="videoInterruptHandler">The IVideoInterruptHandler the IVirtualBIOS will call upon for video related interrupts</param>
    /// <param name="deviceInterruptHandler">The IDeviceInterruptHandler the IVirtualBIOS will call upon for device related interrupts</param>
    /// <param name="parentVirtualMachine">The virtual machine the IVirtualBIOS is part of</param>
    /// <returns>The implementation of IVirtualBIOS</returns>
    public static IVirtualBIOS GenerateIVirtualBIOS(IDiskInterruptHandler diskInterruptHandler, IRTCInterruptHandler rtcInterruptHandler, IVideoInterruptHandler videoInterruptHandler, IDeviceInterruptHandler deviceInterruptHandler, IVirtualMachine? parentVirtualMachine = null) => new VirtualBIOS(diskInterruptHandler, rtcInterruptHandler, videoInterruptHandler, deviceInterruptHandler, parentVirtualMachine);

    /// <summary>
    /// Generates an instance of IMemoryLabel
    /// </summary>
    /// <param name="labelName">The name of the label</param>
    /// <param name="address">The address the label is in the VM memory</param>
    /// <param name="line">The line the label is in the source code</param>
    /// <returns>The implementation of IMemoryLabel</returns>
    public static IMemoryLabel GenerateIMemoryLabel(string labelName, int address, int line) => new MemoryLabel(labelName, address, line);

    /// <summary>
    /// Generates an instance of IVirtualRegister
    /// </summary>
    /// <typeparam name="T">The implementation type of IVirtualRegister</typeparam>
    /// <returns>The implementation of IVirtualRegister</returns>
    public static IVirtualRegister GenerateIVirtualRegister<T>() where T : IVirtualRegister => Activator.CreateInstance<T>();

    /// <summary>
    /// Generates an instance of IVirtualCPU
    /// </summary>
    /// <param name="virtualMachine">The virtual machine the IVirtualBIOS is part of</param>
    /// <returns>The implementation of IVirtualCPU</returns>
    public static IVirtualCPU GenerateIVirtualCPU(IVirtualMachine? virtualMachine = null) => new VirtualCPU(virtualMachine);

    /// <summary>
    /// Generates an instance of IVirtualDevice
    /// </summary>
    /// <typeparam name="T">The implementation type of IVirtualDevice</typeparam>
    /// <returns>The implementation of IVirtualDevice</returns>
    public static IVirtualDevice GenerateIVirtualDevice<T>() where T : IVirtualDevice => Activator.CreateInstance<T>();

    /// <summary>
    /// Generates an instance of IVirtualDisk
    /// </summary>
    /// <param name="diskNumber">The number of the disk</param>
    /// <param name="platters">The amount of platters the disk has</param>
    /// <param name="tracksPerPlatter">The tracks per platter</param>
    /// <param name="sectorsPerTrack">The sectors per track</param>
    /// <param name="parentVirtualMachine">The virtual machine the IVirtualDisk is part of</param>
    /// <returns>The implementation of IVirtualDisk</returns>
    public static IVirtualDisk GenerateIVirtualDisk(byte diskNumber, byte platters, ushort tracksPerPlatter, byte sectorsPerTrack, IVirtualMachine? parentVirtualMachine = null) => new VirtualDisk(diskNumber, platters, tracksPerPlatter, sectorsPerTrack, parentVirtualMachine);
    
    /// <summary>
    /// Generates an instance of IVirtualGPU
    /// </summary>
    /// <param name="parentVirtualMachine">The virtual machine the IVirtualGPU is part of</param>
    /// <returns>The implementation of IVirtualGPU</returns>
    public static IVirtualGPU GenerateIVirtualGPU(IVirtualMachine? parentVirtualMachine = null) => new VirtualGPU(parentVirtualMachine);
    
    /// <summary>
    /// Generates an instance of IVirtualMemory
    /// </summary>
    /// <param name="parentVirtualMachine">The virtual machine the IVirtualMemory is part of</param>
    /// <returns>The implementation of IVirtualMemory</returns>
    public static IVirtualMemory GenerateIVirtualMemory(IVirtualMachine? parentVirtualMachine = null) => new VirtualMemory(parentVirtualMachine);
    
    /// <summary>
    /// Generates an instance of IVirtualRTC
    /// </summary>
    /// <param name="parentVirtualMachine">The virtual machine the IVirtualRTC is part of</param>
    /// <returns>The implementation of IVIrtualRTC</returns>
    public static IVirtualRTC GenerateIVirtualRTC(IVirtualMachine? parentVirtualMachine = null) => new VirtualRTC(parentVirtualMachine);

    /// <summary>
    /// Generates an instance of IVirtualMachine
    /// </summary>
    /// <param name="cpu">The IVirtualCPU implementation for the IVirtualMachine</param>
    /// <param name="memory">The IVirtualMemory implementation for the IVirtualMachine</param>
    /// <param name="disks">The IVirtualDisk implementations for the IVirtualMachine</param>
    /// <param name="bios">The IVirtualBIOS implementation for the IVirtualMachine</param>
    /// <param name="virtualRTC">The IVirtualRTC implementation for the IVirtualMachine</param>
    /// <param name="virtualGPU">The IVirtualGPU implementation for the IVirtualMachine</param>
    /// <param name="virtualDevices">The IVirtualDevice implementations for the IVirtualMachine</param>
    /// <returns>The implementation of IVirtualMachine</returns>
    public static IVirtualMachine GenerateIVirtualMachine(IVirtualCPU cpu, IVirtualMemory memory, IVirtualDisk[] disks, IVirtualBIOS bios, IVirtualRTC virtualRTC, IVirtualGPU virtualGPU, IVirtualDevice[] virtualDevices) => new VirtualMachine(cpu, memory, disks, bios, virtualRTC, virtualGPU, virtualDevices);

    /// <summary>
    /// Generates an instance of IVirtualMachineBuilder
    /// </summary>
    /// <returns>The implementation of IVirtualMachineBuilder</returns>
    public static IVirtualMachineBuilder GenerateIVirtualMachineBuilder() => new VirtualMachineBuilder();
}