using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.Interpreter.Encoder.Models;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces.Models;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using System.Collections.ObjectModel;
using System.Net;

namespace EmuXCore.Interpreter.Encoder.Logic;

public sealed class InstructionEncoder : IInstructionEncoder
{
    public InstructionEncoder(IVirtualMachine virtualMachine, IOperandDecoder operandDecoder)
    {
        OperandDecoder = operandDecoder;
        VirtualMachine = virtualMachine;
    }

    public IInstructionEncoderResult Parse(IList<IInstruction> instructionsToParse)
    {
        foreach (IInstruction instruction in instructionsToParse)
        {

            if (instruction is InstructionADD)
            {
                AddSimpleBinary(instruction, 0x80, 0, 0x00);
            }
            else if (instruction is InstructionOR)
            {
                AddSimpleBinary(instruction, 0x80, 1, 0x08);
            }
            else if (instruction is InstructionADC)
            {
                AddSimpleBinary(instruction, 0x80, 2, 0x10);
            }
            else if (instruction is InstructionSBB)
            {
                AddSimpleBinary(instruction, 0x80, 3, 0x18);
            }
            else if (instruction is InstructionAND)
            {
                AddSimpleBinary(instruction, 0x80, 4, 0x20);
            }
            else if (instruction is InstructionSUB)
            {
                AddSimpleBinary(instruction, 0x80, 5, 0x28);
            }
            else if (instruction is InstructionXOR)
            {
                AddSimpleBinary(instruction, 0x80, 6, 0x30);
            }
            else if (instruction is InstructionCMP)
            {
                AddSimpleBinary(instruction, 0x80, 7, 0x38);
            }
            else if (instruction is InstructionTEST)
            {
                AddSimpleBinary(instruction, 0xF6, 0, 0x84);
            }

            //These are basically simple binary but they don't accept immediates
            if (instruction is InstructionXCHG)
            {
                AddSimpleBinary(instruction, 0x69, 69, 0x87);
            }
            else if (instruction is InstructionLES)
            {
                AddSimpleBinary(instruction, 0x69, 69, 0xC4);
            }
            else if (instruction is InstructionLDS)
            {
                AddSimpleBinary(instruction, 0x69, 69, 0xC5);
            }

            if (instruction is InstructionNOT)
            {
                AddSimpleUnary(instruction, 0xF6, 2);
            }
            else if (instruction is InstructionNEG)
            {
                AddSimpleUnary(instruction, 0xF6, 3);
            }
            else if (instruction is InstructionMUL)
            {
                AddSimpleUnary(instruction, 0xF6, 4);
            }
            else if (instruction is InstructionDIV)
            {
                AddSimpleUnary(instruction, 0xF6, 6);
            }
            else if (instruction is InstructionINC)
            {
                AddSimpleUnary(instruction, 0xFE, 0);
            }
            else if (instruction is InstructionDEC)
            {
                AddSimpleUnary(instruction, 0xFE, 1);
            }

            if (instruction is InstructionCMC)
            {
                AddNonary(0xF5);
            }
            else if (instruction is InstructionCLC)
            {
                AddNonary(0xF8);
            }
            else if (instruction is InstructionSTC)
            {
                AddNonary(0xF9);
            }
            else if (instruction is InstructionCLI)
            {
                AddNonary(0xFA);
            }
            else if (instruction is InstructionSTI)
            {
                AddNonary(0xFB);
            }
            else if (instruction is InstructionCLD)
            {
                AddNonary(0xFC);
            }
            else if (instruction is InstructionSTD)
            {
                AddNonary(0xFD);
            }
            else if (instruction is InstructionAAA)
            {
                AddNonary(0x37);
            }
            else if (instruction is InstructionAAS)
            {
                AddNonary(0x3F);
            }
            else if (instruction is InstructionDAA)
            {
                AddNonary(0x27);
            }
            else if (instruction is InstructionDAS)
            {
                AddNonary(0x2F);
            }
            else if (instruction is InstructionRET)
            {
                //Missing far returns and retn
                AddNonary(0xC3);
            }
            else if (instruction is InstructionHLT)
            {
                AddNonary(0xF4);
            }
            else if (instruction is InstructionSAHF)
            {
                AddNonary(0x9E);
            }
            else if (instruction is InstructionLAHF)
            {
                AddNonary(0x9F);
            }
            else if (instruction is InstructionXLAT)
            {
                AddNonary(0xD7);
            }
            else if (instruction is InstructionINTO)
            {
                AddNonary(0xCE);
            }
            else if (instruction is InstructionPUSHF)
            {
                if (ModeSize == Size.Dword)
                {
                    _output.Add(0x66);
                }
                AddNonary(0x9C);
            }
            else if (instruction is InstructionPUSHFD)
            {
                if (ModeSize == Size.Word)
                {
                    _output.Add(0x66);
                }
                AddNonary(0x9C);
            }
            else if (instruction is InstructionPUSHFQ)
            {
                AddNonary(0x9C);
            }
            else if (instruction is InstructionPOPF)
            {
                if (ModeSize == Size.Dword)
                {
                    _output.Add(0x66);
                }
                AddNonary(0x9D);
            }
            else if (instruction is InstructionPOPFD)
            {
                if (ModeSize == Size.Word)
                {
                    _output.Add(0x66);
                }
                AddNonary(0x9D);
            }
            else if (instruction is InstructionPOPFQ)
            {
                AddNonary(0x9D);
            }
            else if (instruction is InstructionCWD)
            {
                if (ModeSize == Size.Dword)
                {
                    _output.Add(0x66);
                }
                AddNonary(0x99);
            }
            else if (instruction is InstructionCDQ)
            {
                if (ModeSize == Size.Word)
                {
                    _output.Add(0x66);
                }
                AddNonary(0x99);
            } /*else if(i is InstructionCQO) {
			Output.Add((byte) (REX_BASE | (1 << REX_W)));
			AddNonary(0x99);
		}*/
            else if (instruction is InstructionSTOSB)
            {
                AddPrefix(instruction);

                AddNonary(0xAA);
            }
            else if (instruction is InstructionSTOSW)
            {
                AddPrefix(instruction);

                if (ModeSize != Size.Word)
                {
                    AddNonary(0x66);
                }
                AddNonary(0xAB);
            } /*else if(i is InstructionSTOSD) {
			if(ModeSize != Size.Dword) {
				AddNonary(0x66);
			}
			AddNonary(0xAB);
		} else if(i is InstructionSTOSQ) {
			AddNonary(0x48); // REX.W
			AddNonary(0xAB);
		}*/

            if (instruction is InstructionSHL)
            {
                AddShift(instruction, 0xC0, 0xD2, 4);
            }
            else if (instruction is InstructionSHR)
            {
                AddShift(instruction, 0xC0, 0xD2, 5);
            }
            else if (instruction is InstructionSAR)
            {
                AddShift(instruction, 0xC0, 0xD2, 7);
            }
            else if (instruction is InstructionSAL)
            {
                AddShift(instruction, 0xC0, 0xD2, 4);
            }

            if (instruction is InstructionROL)
            {
                AddShift(instruction, 0xC0, 0xD2, 0);
            }
            else if (instruction is InstructionROR)
            {
                AddShift(instruction, 0xC0, 0xD2, 1);
            }
            else if (instruction is InstructionRCL)
            {
                AddShift(instruction, 0xC0, 0xD2, 2);
            }
            else if (instruction is InstructionRCR)
            {
                AddShift(instruction, 0xC0, 0xD2, 3);
            }

            if (instruction is InstructionMOV)
            {
                //MOV isn't just simple binary because segregs and stuff
                // and what the fuck moffs is
                AddSimpleBinary(instruction, 0xC6, 0, 0x88);
            }

            if (instruction is InstructionINT)
            {
                AddNonary(0xCD);
                AddNonary(byte.Parse(instruction.FirstOperand.FullOperand));
            }

            if (instruction is InstructionLEA)
            {
                byte rex = 0;
                byte? modrm = null;
                byte? sib = null;
                byte[] disp = { };
                List<string> dispPatches = GetModRM(instruction.SecondOperand, ref rex, ref modrm, ref sib, ref disp);

                modrm = (byte)(modrm | GetRegisterCode(VirtualCPU, instruction.FirstOperand.FullOperand) << REG_BIT);

                if (instruction.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                {
                    _output.Add(0x66);
                }

                if (rex != 0)
                {
                    _output.Add(rex);
                }

                _output.Add(0x8D);

                _output.Add((byte)modrm!);

                if (sib != null)
                {
                    _output.Add((byte)sib!);
                }

                AddPatches(dispPatches);
                _output.AddRange(disp);
            }

            if (instruction is InstructionIN)
            {
                if (instruction.SecondOperand.Variant == OperandVariant.Value)
                {
                    if (instruction.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                    {
                        _output.Add(0x66);
                    }
                    _output.Add((byte)(0xE4 + Convert.ToByte(instruction.FirstOperand.OperandSize != Size.Byte)));
                    _output.AddRange(UlongToBinary(ulong.Parse(instruction.SecondOperand.FullOperand), Size.Byte));
                }
                else
                {
                    if (instruction.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                    {
                        _output.Add(0x66);
                    }
                    _output.Add((byte)(0xEC + Convert.ToByte(instruction.FirstOperand.OperandSize != Size.Byte)));
                }
            }

            if (instruction is InstructionOUT)
            {
                if (instruction.SecondOperand.Variant == OperandVariant.Value)
                {
                    if (instruction.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                    {
                        _output.Add(0x66);
                    }
                    _output.Add((byte)(0xE6 + Convert.ToByte(instruction.FirstOperand.OperandSize != Size.Byte)));
                    _output.AddRange(UlongToBinary(ulong.Parse(instruction.SecondOperand.FullOperand), Size.Byte));
                }
                else
                {
                    if (instruction.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                    {
                        _output.Add(0x66);
                    }
                    _output.Add((byte)(0xEE + Convert.ToByte(instruction.FirstOperand.OperandSize != Size.Byte)));
                }
            }

            if (instruction is InstructionJA)
            {
                AddJumpRel32(instruction, 0x87);
            }
            else if (instruction is InstructionJAE)
            {
                AddJumpRel32(instruction, 0x83);
            }
            else if (instruction is InstructionJB)
            {
                AddJumpRel32(instruction, 0x82);
            }
            else if (instruction is InstructionJBE)
            {
                AddJumpRel32(instruction, 0x86);
            }
            else if (instruction is InstructionJC)
            {
                AddJumpRel32(instruction, 0x82);
            }
            else if (instruction is InstructionJCXZ)
            {
                AddJumpRel32(instruction, 0xE3);
            }
            else if (instruction is InstructionJE)
            {
                AddJumpRel32(instruction, 0x84);
            }
            else if (instruction is InstructionJG)
            {
                AddJumpRel32(instruction, 0x8F);
            }
            else if (instruction is InstructionJGE)
            {
                AddJumpRel32(instruction, 0x8D);
            }
            else if (instruction is InstructionJL)
            {
                AddJumpRel32(instruction, 0x8C);
            }
            else if (instruction is InstructionJLE)
            {
                AddJumpRel32(instruction, 0x8E);
            }
            else if (instruction is InstructionJNA)
            {
                AddJumpRel32(instruction, 0x86);
            }
            else if (instruction is InstructionJNAE)
            {
                AddJumpRel32(instruction, 0x82);
            }
            else if (instruction is InstructionJNB)
            {
                AddJumpRel32(instruction, 0x83);
            }
            else if (instruction is InstructionJNBE)
            {
                AddJumpRel32(instruction, 0x87);
            }
            else if (instruction is InstructionJNC)
            {
                AddJumpRel32(instruction, 0x83);
            }
            else if (instruction is InstructionJNE)
            {
                AddJumpRel32(instruction, 0x85);
            }
            else if (instruction is InstructionJNG)
            {
                AddJumpRel32(instruction, 0x8E);
            }
            else if (instruction is InstructionJNGE)
            {
                AddJumpRel32(instruction, 0x8C);
            }
            else if (instruction is InstructionJNL)
            {
                AddJumpRel32(instruction, 0x8D);
            }
            else if (instruction is InstructionJNLE)
            {
                AddJumpRel32(instruction, 0x8F);
            }
            else if (instruction is InstructionJNO)
            {
                AddJumpRel32(instruction, 0x81);
            }
            else if (instruction is InstructionJNP)
            {
                AddJumpRel32(instruction, 0x8B);
            }
            else if (instruction is InstructionJNS)
            {
                AddJumpRel32(instruction, 0x89);
            }
            else if (instruction is InstructionJNZ)
            {
                AddJumpRel32(instruction, 0x85);
            }
            else if (instruction is InstructionJO)
            {
                AddJumpRel32(instruction, 0x80);
            }
            else if (instruction is InstructionJP)
            {
                AddJumpRel32(instruction, 0x8A);
            }
            else if (instruction is InstructionJPE)
            {
                AddJumpRel32(instruction, 0x8A);
            }
            else if (instruction is InstructionJPO)
            {
                AddJumpRel32(instruction, 0x8B);
            }
            else if (instruction is InstructionJS)
            {
                AddJumpRel32(instruction, 0x88);
            }
            else if (instruction is InstructionJZ)
            {
                AddJumpRel32(instruction, 0x84);
            }

            if (instruction is InstructionAAD || instruction is InstructionAAM)
            {
                _output.Add((byte)(instruction is InstructionAAD ? 0xD5 : 0xD4));
                if (instruction.Variant == InstructionVariant.NoOperands())
                {
                    _output.Add(0x0A);
                }
                else
                {
                    _output.Add(byte.Parse(instruction.FirstOperand.FullOperand));
                }
            }

            if (instruction is InstructionCBW)
            {
                if (ModeSize != Size.Word)
                {
                    _output.Add(0x66);
                }
                AddNonary(0x98);
            }

            if (instruction is InstructionCWDE)
            {
                if (ModeSize != Size.Dword)
                {
                    _output.Add(0x66);
                }
                AddNonary(0x98);
            }

            if (instruction is InstructionCDQE)
            {
                _output.Add((byte)(REX_BASE | 1 << REX_W));
                AddNonary(0x98);
            }

            if (instruction is InstructionCMPSB)
            {
                AddPrefix(instruction);
                AddNonary(0xA6);
            }
            else if (instruction is InstructionCMPSW)
            {
                AddPrefix(instruction);
                if (ModeSize != Size.Word)
                {
                    _output.Add(0x66);
                }
                AddNonary(0xA7);
            } /*else if(i is InstructionCMPSD) {
			if(ModeSize != Size.Dword) {
				Output.Add(0x66);
			}
			Output.Add(0xA7);
		} else if(i is InstructionCMPSQ) {
			Output.Add((byte) (REX_BASE | (1 << REX_W)));
			Output.Add(0xA7);
		}*/

            if (instruction is InstructionLODSB)
            {
                AddPrefix(instruction);
                AddNonary(0xAC);
            }
            else if (instruction is InstructionLODSW)
            {
                AddPrefix(instruction);
                if (ModeSize != Size.Word)
                {
                    _output.Add(0x66);
                }
                AddNonary(0xAD);
            }

            if (instruction is InstructionMOVSB)
            {
                AddPrefix(instruction);
                AddNonary(0xA4);
            }
            else if (instruction is InstructionMOVSW)
            {
                AddPrefix(instruction);
                if (ModeSize != Size.Word)
                {
                    _output.Add(0x66);
                }
                AddNonary(0xA5);
            }

            if (instruction is InstructionSCASB)
            {
                AddPrefix(instruction);
                AddNonary(0xAE);
            }
            else if (instruction is InstructionSCASW)
            {
                AddPrefix(instruction);
                if (ModeSize != Size.Word)
                {
                    _output.Add(0x66);
                }
                AddNonary(0xAF);
            }

            if (instruction is InstructionPUSH)
            {
                if (instruction.FirstOperand.Variant == OperandVariant.Value)
                {
                    if (ModeSize == Size.Word)
                    {
                        _output.Add(0x66);
                    }
                    _output.Add(0x68);
                    _output.AddRange(UlongToBinary(ulong.Parse(instruction.SecondOperand.FullOperand), Size.Dword));
                }
                else
                {
                    AddSimpleUnary(instruction, 0xFF, 6);
                }
            }

            if (instruction is InstructionPOP)
            {
                AddSimpleUnary(instruction, 0x8F, 0);
            }

            if (instruction is InstructionLOOP)
            {
                AddJump(instruction, false, 0xE2, Size.Byte);
            }

            if (instruction is InstructionIMUL)
            {
                if (instruction.SecondOperand == null)
                {
                    AddSimpleUnary(instruction, 0xF6, 5);
                }
                else if (instruction.ThirdOperand == null)
                {
                    _output.Add(0x0F);
                    AddSimpleBinary(instruction, 0x69, 0x69, 0xAF);
                }
                else
                {
                    //Cannot use AddSimpleBinary here because it ORs 2 to the opcode because the operand order is "reg, mem"

                    byte rex = 0;
                    byte? modrm = null;
                    byte? sib = null;
                    byte[] disp = { };
                    List<string> dispPatches = GetModRM(instruction.SecondOperand, ref rex, ref modrm, ref sib, ref disp);

                    modrm = (byte)(modrm | GetRegisterCode(VirtualCPU, instruction.FirstOperand.FullOperand) << REG_BIT);

                    if (instruction.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                    {
                        _output.Add(0x66);
                    }

                    if (rex != 0)
                    {
                        _output.Add(rex);
                    }

                    _output.Add(0x69);

                    _output.Add((byte)modrm!);

                    if (sib != null)
                    {
                        _output.Add((byte)sib!);
                    }

                    AddPatches(dispPatches);
                    _output.AddRange(disp);

                    _output.AddRange(UlongToBinary(ulong.Parse(instruction.ThirdOperand.FullOperand), instruction.FirstOperand.OperandSize == Size.Word ? Size.Word : Size.Dword));
                }
            }

            if (instruction is InstructionIDIV)
            {
                AddSimpleUnary(instruction, 0xF6, 7);
            }

            if (instruction is InstructionCALL)
            {
                if (instruction.FirstOperand.Variant == OperandVariant.Value || instruction.FirstOperand.Variant == OperandVariant.Label)
                {
                    AddJumpRel32(instruction, 0xE8);
                }
                else
                {
                    AddSimpleUnary(instruction, 0xFF, 2);
                }
            }

            PatchCode();
        }

        return DIFactory.GenerateIInstructionEncoderResult([.. _output], new ReadOnlyCollection<string>(_errors));
    }

    // TODO - Analyze this further with future additions / updates
    private byte GetRegisterCode(IVirtualCPU cpu, string name)
    {
        IVirtualRegister reg = cpu.GetRegister(name)!;
        bool h = name.EndsWith("H");

        switch (reg)
        {
            case VirtualRegisterRAX i:
                return (byte)(h ? 4 : 0);
            case VirtualRegisterRBX i:
                return (byte)(h ? 7 : 3);
            case VirtualRegisterRCX i:
                return (byte)(h ? 5 : 1);
            case VirtualRegisterRDX i:
                return (byte)(h ? 6 : 2);
            case VirtualRegisterRSP i:
                return 4;
            case VirtualRegisterRBP i:
                return 5;
            case VirtualRegisterRSI i:
                return 6;
            case VirtualRegisterRDI i:
                return 7;
            default:
                return 69;
        }
    }

    private byte[] UlongToBinary(ulong valueToConvert, Size size)
    {
        byte[] convertedBytes = BitConverter.GetBytes(valueToConvert);

        if (!BitConverter.IsLittleEndian)
        {
            Array.Reverse(convertedBytes);
        }

        return convertedBytes.Take((int)size).ToArray();
    }

    private List<string> GetModRM(IOperand operand, ref byte rexByte, ref byte? modRM, ref byte? sib, ref byte[] displacement)
    {
        List<IMemoryOffset> offsets = [];
        int scaleOffset;
        long? constantDisplacement = null;
        List<string> labelDisplacements = [];
        bool hasDisplacement;
        int offsetBase;
        uint scale;
        int index;
        int code;

        if (operand.Variant == OperandVariant.Register)
        {
            code = GetRegisterCode(VirtualCPU, operand.FullOperand);
            modRM = (byte)(3 << MOD_BIT | code << RM_BIT);

            if (code >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((code >> 3 & 1) << REX_B);
            }

            return [];
        }
        else if (operand.Variant != OperandVariant.Memory)
        {
            _errors.Add($"Operand is not of type {OperandVariant.Register} nor {OperandVariant.Memory} - {operand.FullOperand}");

            throw new ArgumentException($"Operand is not of type {OperandVariant.Register} nor {OperandVariant.Memory}");
        }

        //Normalize by moving displacements to separate array
        //and moving the scale, if any, to the beginning.
        // This leaves `offsets` in one of five forms:
        //   reg
        //   reg + reg
        //   reg* scale
        //   reg* scale +reg
        //   (empty)

        offsets = operand.Offsets.ToList();
        scaleOffset = offsets.TakeWhile(selectedOperand => selectedOperand.Operand != MemoryOffsetOperand.Multiplication).Count();

        if (scaleOffset < offsets.Count)
        {
            if (scaleOffset == 0)
            {
                throw new ArgumentException("Scale cannot be first");
            }

            (offsets[0], offsets[scaleOffset - 1]) = (offsets[scaleOffset - 1], offsets[0]);
            (offsets[1], offsets[scaleOffset - 0]) = (offsets[scaleOffset - 0], offsets[1]);
        }

        for (int i = scaleOffset == offsets.Count ? 0 : 2; i < offsets.Count;)
        {
            if (offsets[i].Operand == MemoryOffsetOperand.Multiplication)
            {
                throw new ArgumentException("One scale max");
            }

            if (offsets[i].Type == MemoryOffsetType.Integer)
            {
                if (constantDisplacement == null)
                {
                    constantDisplacement = 0;
                }

                constantDisplacement = (long)constantDisplacement + long.Parse(offsets[i].FullOperand.Trim().Split(' ').Last());
                offsets.RemoveAt(i);
            }
            else if (offsets[i].Type == MemoryOffsetType.Label)
            {
                labelDisplacements.Add(offsets[i].FullOperand);
                offsets.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        hasDisplacement = constantDisplacement != null || labelDisplacements.Count > 0;

        //From this point `offsets` is only regs & scale.
        // Displacements must be checked with `hasDisplacement`.

        if (!offsets.Any() && !hasDisplacement)
        {
            modRM = 0 << MOD_BIT | 5 << RM_BIT;

            //Shouldn't happen but I guess this is an easter egg.
            displacement = UlongToBinary(0x69696969, Size.Dword);
        }
        else if (offsets.Count == 0 && hasDisplacement)
        {
            modRM = 0 << MOD_BIT | 5 << RM_BIT;
            displacement = UlongToBinary((ulong)constantDisplacement, Size.Dword);
        }
        else if (offsets.Count == 1 && offsets[0].Type == MemoryOffsetType.Register && !hasDisplacement)
        {
            code = GetRegisterCode(VirtualCPU, offsets[0].FullOperand);

            if (code >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((code >> 3 & 1) << REX_B);
                code -= 8;
            }

            modRM = (byte)(0 << MOD_BIT | code << RM_BIT);

            if (code == 4)
            {
                sib = 4 | 4 << 3;
            }
            else if (code == 5)
            {
                modRM |= 1 << MOD_BIT;
                displacement = [0];
            }
        }
        else if (offsets.Count == 1 && offsets[0].Type == MemoryOffsetType.Register && hasDisplacement)
        {
            code = GetRegisterCode(VirtualCPU, offsets[0].FullOperand);

            if (code >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((code >> 3 & 1) << REX_B);
                code -= 8;
            }
            modRM = (byte)(2 << MOD_BIT | code << RM_BIT);

            if (code == 4)
            {
                sib = 4 | 4 << 3;
            }

            displacement = UlongToBinary((ulong)constantDisplacement, Size.Dword);
        }
        else if (offsets.Count == 2 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Type == MemoryOffsetType.Register)
        {
            index = GetRegisterCode(VirtualCPU, offsets[0].FullOperand);
            offsetBase = GetRegisterCode(VirtualCPU, offsets[1].FullOperand.Trim().Split(' ').Last());

            if (index == 4)
            {
                (index, offsetBase) = (offsetBase, index);
            }

            modRM = (byte)((hasDisplacement ? 2 : 0) << MOD_BIT | 4 << RM_BIT);

            if (index >= 8 || offsetBase >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((index >> 3 & 1) << REX_X);
                rexByte |= (byte)((offsetBase >> 3 & 1) << REX_B);
            }

            sib = (byte)(offsetBase | index << INDEX_BIT);

            if (hasDisplacement)
            {
                displacement = UlongToBinary((ulong)constantDisplacement, Size.Dword);
            }
            else
            {
                if (offsetBase == 5)
                {
                    modRM |= 1 << MOD_BIT;
                    displacement = [0];
                }
            }
        }
        else if (offsets.Count == 2 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Operand == MemoryOffsetOperand.Multiplication && !hasDisplacement)
        {
            index = GetRegisterCode(VirtualCPU, offsets[0].FullOperand);

            if (index == 4)
            {
                throw new ArgumentException($"The ESP register is not a valid index");
            }

            modRM = 0 << MOD_BIT | 4 << RM_BIT;

            if (index >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((index >> 3 & 1) << REX_X);
                index -= 8;
            }

            displacement = UlongToBinary(0, Size.Dword);
            sib = (byte)(ILog2(ulong.Parse(offsets[1].FullOperand.Trim().Split(' ').Last())) << SCALE_BIT | index << INDEX_BIT | 5 << BASE_BIT);
        }
        else if (offsets.Count == 2 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Operand == MemoryOffsetOperand.Multiplication && hasDisplacement)
        {
            index = GetRegisterCode(VirtualCPU, offsets[0].FullOperand);
            scale = uint.Parse(offsets[1].FullOperand.Trim().Split(' ').Last());

            if (index == 4)
            {
                throw new ArgumentException($"The ESP register is not a valid index");
            }

            modRM = 0 << MOD_BIT | 4 << RM_BIT;

            if (index >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((index >> 3 & 1) << REX_X);
                index -= 8;
            }

            displacement = UlongToBinary((ulong)constantDisplacement, Size.Dword);
            sib = (byte)(ILog2(scale) << SCALE_BIT | index << INDEX_BIT | 5 << BASE_BIT);
        }
        else if (offsets.Count == 3 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Operand == MemoryOffsetOperand.Multiplication && offsets[2].Type == MemoryOffsetType.Register && !hasDisplacement)
        {
            index = GetRegisterCode(VirtualCPU, offsets[0].FullOperand.Trim().Split(' ').Last());
            scale = uint.Parse(offsets[1].FullOperand.Trim().Split(' ').Last());
            offsetBase = GetRegisterCode(VirtualCPU, offsets[2].FullOperand);

            if (index == 4)
            {
                throw new ArgumentException($"The ESP register is not a valid index");
            }

            modRM = 0 << MOD_BIT | 4 << RM_BIT;

            if (index >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((index >> 3 & 1) << REX_X);
                index -= 8;
            }

            if (offsetBase == 5)
            {
                modRM |= 1 << MOD_BIT;
                displacement = UlongToBinary(0, Size.Byte);
            }
            else if (offsetBase >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((offsetBase >> 3 & 1) << REX_B);
                offsetBase -= 8;
            }

            sib = (byte)(ILog2(scale) << SCALE_BIT | index << INDEX_BIT | offsetBase << BASE_BIT);
        }
        else if (offsets.Count == 3 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Operand == MemoryOffsetOperand.Multiplication && offsets[2].Type == MemoryOffsetType.Register && hasDisplacement)
        {
            index = GetRegisterCode(VirtualCPU, offsets[0].FullOperand.Trim().Split(' ').Last());
            scale = uint.Parse(offsets[1].FullOperand.Trim().Split(' ').Last());
            offsetBase = GetRegisterCode(VirtualCPU, offsets[2].FullOperand);

            if (index == 4)
            {
                throw new ArgumentException($"The ESP register is not a valid index");
            }

            modRM = 2 << MOD_BIT | 4 << RM_BIT;

            if (index >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((index >> 3 & 1) << REX_X);
                index -= 8;
            }

            if (offsetBase >= 8)
            {
                rexByte |= REX_BASE;
                rexByte |= (byte)((offsetBase >> 3 & 1) << REX_B);
                offsetBase -= 8;
            }

            sib = (byte)(ILog2(scale) << SCALE_BIT | index << INDEX_BIT | offsetBase << BASE_BIT);
            displacement = UlongToBinary((ulong)constantDisplacement, Size.Dword);
        }
        else
        {
            _errors.Add($"Unsupported - {operand.FullOperand}");

            throw new ArgumentException("Unsupported");
        }

        return labelDisplacements;
    }

    private void PatchCode()
    {
        List<string> doneLabels = [];
        byte[] bytes;
        ulong offset;

        foreach (KeyValuePair<string, List<Patch>> pair in _patches)
        {
            if (!_labels.ContainsKey(pair.Key))
            {
                continue;
            }

            foreach (Patch selectedPatch in pair.Value)
            {
                bytes = _output.GetRange((int)selectedPatch.Offset, (int)selectedPatch.Size).ToArray();
                offset = selectedPatch.Subtract ? (ulong)-(long)_labels[pair.Key] : _labels[pair.Key];
                bytes = BitConverter.GetBytes(BitConverter.ToUInt64(bytes) + offset).Take((int)selectedPatch.Size).ToArray();

                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(bytes);
                }

                _output.RemoveRange((int)selectedPatch.Offset, (int)selectedPatch.Size);
                _output.InsertRange((int)selectedPatch.Offset, bytes);
            }

            doneLabels.Add(pair.Key);
        }

        foreach (string label in doneLabels)
        {
            _patches.Remove(label);
        }
    }

    private void AddPatches(List<string> labels)
    {
        foreach (string label in labels)
        {
            if (!_patches.ContainsKey(label))
            {
                _patches.Add(label, [new((ulong)_output.Count, ModeSize, false)]);
            }
            else
            {
                _patches[label].Add(new((ulong)_output.Count, ModeSize, false));
            }
        }
    }

    private int ILog2(ulong val)
    {
        int bits = -1;

        if (val == 0)
        {
            return 1;
        }

        while (val > 0)
        {
            bits++;
            val >>= 1;
        }

        return bits;
    }

    private void AddSimpleBinary(IInstruction i, byte opcodeImm, byte opcodeImmExt, byte opcodeMain)
    {
        byte opcode = 0;
        byte[] immediate = [];
        byte[] displacement = [];
        byte rex = 0;
        byte? modRM = null;
        byte? sib = null;
        bool size8 = false;
        bool prefixSize = false;
        List<string> displacementPatches = [];
        int code;
        IOperand rm;
        IOperand register;

        if (i.SecondOperand.Variant == OperandVariant.Value)
        {
            opcode = opcodeImm;
            displacementPatches = GetModRM(i.FirstOperand, ref rex, ref modRM, ref sib, ref displacement);
            modRM = (byte)(modRM | opcodeImmExt << REG_BIT);

            if (i.FirstOperand.OperandSize == Size.Byte)
            {
                size8 = true;
            }
            else if (i.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
            {
                prefixSize = true;
            }

            immediate = UlongToBinary(OperandDecoder.GetOperandValue(VirtualMachine, i.SecondOperand), i.FirstOperand.OperandSize);
        }
        else
        {
            opcode = opcodeMain;
            rm = i.FirstOperand;
            register = i.SecondOperand;
            code = GetRegisterCode(VirtualCPU, register.FullOperand);

            if (register.Variant == OperandVariant.Memory)
            {
                opcode |= 2;
                (rm, register) = (register, rm);
            }

            displacementPatches = GetModRM(rm, ref rex, ref modRM, ref sib, ref displacement);

            if (code >= 8)
            {
                rex |= REX_BASE;
                rex |= (byte)((code >> 3 & 1) << REX_R);
                code -= 8;
            }

            modRM |= (byte)(code << REG_BIT);

            if (rm.OperandSize == Size.Byte)
            {
                size8 = true;
            }
            else if (rm.OperandSize == Size.Word != (ModeSize == Size.Word))
            {
                prefixSize = true;
            }
        }

        if (!size8)
        {
            opcode |= 1;
        }

        if (prefixSize)
        {
            _output.Add(0x66);
        }

        if (rex != 0)
        {
            _output.Add(rex);
        }

        _output.Add(opcode);

        if (modRM != null)
        {
            _output.Add((byte)modRM!);
        }

        if (sib != null)
        {
            _output.Add((byte)sib!);
        }

        AddPatches(displacementPatches);

        _output.AddRange(displacement);
        _output.AddRange(immediate);
    }

    private void AddSimpleUnary(IInstruction i, byte opcode, byte opcodeExt)
    {
        byte rex = 0;
        byte? modrm = null;
        byte? sib = null;
        byte[] disp = { };
        List<string> dispPatches;

        if (i.FirstOperand.Variant == OperandVariant.Value)
        {
            _errors.Add($"Operand cannot be immediate - {i.Opcode} {i.FirstOperand} {i.SecondOperand} {i.ThirdOperand}");

            throw new ArgumentException("Operand cannot be immediate");
        }

        dispPatches = GetModRM(i.FirstOperand, ref rex, ref modrm, ref sib, ref disp);
        modrm = (byte)(modrm | opcodeExt << REG_BIT);

        if (i.FirstOperand.OperandSize != Size.Byte)
        {
            opcode |= 1;
        }

        if (i.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
        {
            _output.Add(0x66);
        }

        if (rex != 0)
        {
            _output.Add(rex);
        }

        _output.Add(opcode);

        if (modrm != null)
        {
            _output.Add((byte)modrm!);
        }

        if (sib != null)
        {
            _output.Add((byte)sib!);
        }

        AddPatches(dispPatches);

        _output.AddRange(disp);
    }

    private void AddNonary(byte opcode)
    {
        _output.Add(opcode);
    }

    private void AddShift(IInstruction i, byte opcodeImm, byte opcodeCL, byte opcodeExt)
    {
        if (i.SecondOperand.Variant == OperandVariant.Value)
        {
            //imm8
            AddSimpleUnary(i, opcodeImm, opcodeExt);
            _output.AddRange(UlongToBinary(ulong.Parse(i.SecondOperand.FullOperand), Size.Byte));
        }
        else
        {
            //CL
            AddSimpleUnary(i, opcodeCL, opcodeExt);
        }
    }

    private void AddJump(IInstruction i, bool putZeroFByte, byte opcode, Size displacementSize)
    {
        //TODO: check for overflow

        if (ModeSize != Size.Dword)
        {
            _output.Add(0x66);
        }

        if (putZeroFByte)
        {
            _output.Add(0x0F);
        }

        _output.Add(opcode);

        if (i.FirstOperand.Variant == OperandVariant.Value)
        {
            _output.AddRange(UlongToBinary(ulong.Parse(i.FirstOperand.FullOperand), displacementSize));
        }
        else if (i.FirstOperand.Variant == OperandVariant.Label)
        {
            AddPatches([i.FirstOperand.FullOperand]);
            _output.AddRange(UlongToBinary((ulong)(-_output.Count - (int)displacementSize + (putZeroFByte ? 0 : 1)), displacementSize));
        }
    }

    private void AddJumpRel32(IInstruction i, byte opcode)
    {
        AddJump(i, true, opcode, Size.Dword);
    }

    private void AddPrefix(IInstruction instruction)
    {
        if (instruction.Prefix == null)
        {
            return;
        }

        _output.Add(instruction.Prefix.PrefixMachineCodeByte);
    }

    public IVirtualCPU VirtualCPU { get => VirtualMachine.CPU; }
    public IOperandDecoder OperandDecoder { get; init; }
    public IVirtualMachine VirtualMachine { get; init; }

    public Size ModeSize { get; set; } = Size.Dword;

    private Dictionary<string, ulong> _labels = new();
    private Dictionary<string, List<Patch>> _patches = new();
    private List<byte> _output = [];
    private List<string> _errors = [];

    private const byte REX_BASE = 64;
    private const byte REX_W = 3;
    private const byte REX_R = 2;
    private const byte REX_X = 1;
    private const byte REX_B = 0;

    private const int MOD_BIT = 6;
    private const int REG_BIT = 3;
    private const int RM_BIT = 0;

    private const int SCALE_BIT = 6;
    private const int INDEX_BIT = 3;
    private const int BASE_BIT = 0;
}