using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions;
using EmuXCore.InstructionLogic.Instructions.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.InstructionLogic.Prefixes;
using EmuXCore.Interpreter.Encoder.Models;
using EmuXCore.Interpreter.Interfaces.Logic;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.CPU.Registers;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using System.Net;

namespace EmuXCore.Interpreter.Encoder.Logic;

public sealed class InstructionEncoder : IInstructionEncoder
{
    public InstructionEncoder(IVirtualMachine virtualMachine, IOperandDecoder operandDecoder)
    {
        OperandDecoder = operandDecoder;
        VirtualMachine = virtualMachine;
    }

    public bool Add(IInstruction i)
    {
        if (i is InstructionADD)
        {
            AddSimpleBinary(i, 0x80, 0, 0x00);
        }
        else if (i is InstructionOR)
        {
            AddSimpleBinary(i, 0x80, 1, 0x08);
        }
        else if (i is InstructionADC)
        {
            AddSimpleBinary(i, 0x80, 2, 0x10);
        }
        else if (i is InstructionSBB)
        {
            AddSimpleBinary(i, 0x80, 3, 0x18);
        }
        else if (i is InstructionAND)
        {
            AddSimpleBinary(i, 0x80, 4, 0x20);
        }
        else if (i is InstructionSUB)
        {
            AddSimpleBinary(i, 0x80, 5, 0x28);
        }
        else if (i is InstructionXOR)
        {
            AddSimpleBinary(i, 0x80, 6, 0x30);
        }
        else if (i is InstructionCMP)
        {
            AddSimpleBinary(i, 0x80, 7, 0x38);
        }
        else if (i is InstructionTEST)
        {
            AddSimpleBinary(i, 0xF6, 0, 0x84);
        }

        //These are basically simple binary but they don't accept immediates
        if (i is InstructionXCHG)
        {
            AddSimpleBinary(i, 0x69, 69, 0x87);
        }
        else if (i is InstructionLES)
        {
            AddSimpleBinary(i, 0x69, 69, 0xC4);
        }
        else if (i is InstructionLDS)
        {
            AddSimpleBinary(i, 0x69, 69, 0xC5);
        }

        if (i is InstructionNOT)
        {
            AddSimpleUnary(i, 0xF6, 2);
        }
        else if (i is InstructionNEG)
        {
            AddSimpleUnary(i, 0xF6, 3);
        }
        else if (i is InstructionMUL)
        {
            AddSimpleUnary(i, 0xF6, 4);
        }
        else if (i is InstructionDIV)
        {
            AddSimpleUnary(i, 0xF6, 6);
        }
        else if (i is InstructionINC)
        {
            AddSimpleUnary(i, 0xFE, 0);
        }
        else if (i is InstructionDEC)
        {
            AddSimpleUnary(i, 0xFE, 1);
        }

        if (i is InstructionCMC)
        {
            AddNonary(0xF5);
        }
        else if (i is InstructionCLC)
        {
            AddNonary(0xF8);
        }
        else if (i is InstructionSTC)
        {
            AddNonary(0xF9);
        }
        else if (i is InstructionCLI)
        {
            AddNonary(0xFA);
        }
        else if (i is InstructionSTI)
        {
            AddNonary(0xFB);
        }
        else if (i is InstructionCLD)
        {
            AddNonary(0xFC);
        }
        else if (i is InstructionSTD)
        {
            AddNonary(0xFD);
        }
        else if (i is InstructionAAA)
        {
            AddNonary(0x37);
        }
        else if (i is InstructionAAS)
        {
            AddNonary(0x3F);
        }
        else if (i is InstructionDAA)
        {
            AddNonary(0x27);
        }
        else if (i is InstructionDAS)
        {
            AddNonary(0x2F);
        }
        else if (i is InstructionRET)
        {
            //Missing far returns and retn
            AddNonary(0xC3);
        }
        else if (i is InstructionHLT)
        {
            AddNonary(0xF4);
        }
        else if (i is InstructionSAHF)
        {
            AddNonary(0x9E);
        }
        else if (i is InstructionLAHF)
        {
            AddNonary(0x9F);
        }
        else if (i is InstructionXLAT)
        {
            AddNonary(0xD7);
        }
        else if (i is InstructionINTO)
        {
            AddNonary(0xCE);
        }
        else if (i is InstructionPUSHF)
        {
            if (ModeSize == Size.Dword)
            {
                _output.Add(0x66);
            }
            AddNonary(0x9C);
        }
        else if (i is InstructionPUSHFD)
        {
            if (ModeSize == Size.Word)
            {
                _output.Add(0x66);
            }
            AddNonary(0x9C);
        }
        else if (i is InstructionPUSHFQ)
        {
            AddNonary(0x9C);
        }
        else if (i is InstructionPOPF)
        {
            if (ModeSize == Size.Dword)
            {
                _output.Add(0x66);
            }
            AddNonary(0x9D);
        }
        else if (i is InstructionPOPFD)
        {
            if (ModeSize == Size.Word)
            {
                _output.Add(0x66);
            }
            AddNonary(0x9D);
        }
        else if (i is InstructionPOPFQ)
        {
            AddNonary(0x9D);
        }
        else if (i is InstructionCWD)
        {
            if (ModeSize == Size.Dword)
            {
                _output.Add(0x66);
            }
            AddNonary(0x99);
        }
        else if (i is InstructionCDQ)
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
        else if (i is InstructionSTOSB)
        {
            AddPrefix(i);

            AddNonary(0xAA);
        }
        else if (i is InstructionSTOSW)
        {
            AddPrefix(i);

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

        if (i is InstructionSHL)
        {
            AddShift(i, 0xC0, 0xD2, 4);
        }
        else if (i is InstructionSHR)
        {
            AddShift(i, 0xC0, 0xD2, 5);
        }
        else if (i is InstructionSAR)
        {
            AddShift(i, 0xC0, 0xD2, 7);
        }
        else if (i is InstructionSAL)
        {
            AddShift(i, 0xC0, 0xD2, 4);
        }

        if (i is InstructionROL)
        {
            AddShift(i, 0xC0, 0xD2, 0);
        }
        else if (i is InstructionROR)
        {
            AddShift(i, 0xC0, 0xD2, 1);
        }
        else if (i is InstructionRCL)
        {
            AddShift(i, 0xC0, 0xD2, 2);
        }
        else if (i is InstructionRCR)
        {
            AddShift(i, 0xC0, 0xD2, 3);
        }

        if (i is InstructionMOV)
        {
            //MOV isn't just simple binary because segregs and stuff
            // and what the fuck moffs is
            AddSimpleBinary(i, 0xC6, 0, 0x88);
        }

        if (i is InstructionINT)
        {
            AddNonary(0xCD);
            AddNonary(byte.Parse(i.FirstOperand.FullOperand));
        }

        if (i is InstructionLEA)
        {
            byte rex = 0;
            byte? modrm = null;
            byte? sib = null;
            byte[] disp = { };
            List<string> dispPatches = GetModRM(i.SecondOperand, ref rex, ref modrm, ref sib, ref disp);

            modrm = (byte)(modrm | GetRegisterCode(VirtualCPU, i.FirstOperand.FullOperand) << REG_BIT);

            if (i.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
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

        if (i is InstructionIN)
        {
            if (i.SecondOperand.Variant == OperandVariant.Value)
            {
                if (i.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                {
                    _output.Add(0x66);
                }
                _output.Add((byte)(0xE4 + Convert.ToByte(i.FirstOperand.OperandSize != Size.Byte)));
                _output.AddRange(UlongToBinary(ulong.Parse(i.SecondOperand.FullOperand), Size.Byte));
            }
            else
            {
                if (i.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                {
                    _output.Add(0x66);
                }
                _output.Add((byte)(0xEC + Convert.ToByte(i.FirstOperand.OperandSize != Size.Byte)));
            }
        }

        if (i is InstructionOUT)
        {
            if (i.SecondOperand.Variant == OperandVariant.Value)
            {
                if (i.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                {
                    _output.Add(0x66);
                }
                _output.Add((byte)(0xE6 + Convert.ToByte(i.FirstOperand.OperandSize != Size.Byte)));
                _output.AddRange(UlongToBinary(ulong.Parse(i.SecondOperand.FullOperand), Size.Byte));
            }
            else
            {
                if (i.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
                {
                    _output.Add(0x66);
                }
                _output.Add((byte)(0xEE + Convert.ToByte(i.FirstOperand.OperandSize != Size.Byte)));
            }
        }

        if (i is InstructionJA)
        {
            AddJumpRel32(i, 0x87);
        }
        else if (i is InstructionJAE)
        {
            AddJumpRel32(i, 0x83);
        }
        else if (i is InstructionJB)
        {
            AddJumpRel32(i, 0x82);
        }
        else if (i is InstructionJBE)
        {
            AddJumpRel32(i, 0x86);
        }
        else if (i is InstructionJC)
        {
            AddJumpRel32(i, 0x82);
        }
        else if (i is InstructionJCXZ)
        {
            AddJumpRel32(i, 0xE3);
        }
        else if (i is InstructionJE)
        {
            AddJumpRel32(i, 0x84);
        }
        else if (i is InstructionJG)
        {
            AddJumpRel32(i, 0x8F);
        }
        else if (i is InstructionJGE)
        {
            AddJumpRel32(i, 0x8D);
        }
        else if (i is InstructionJL)
        {
            AddJumpRel32(i, 0x8C);
        }
        else if (i is InstructionJLE)
        {
            AddJumpRel32(i, 0x8E);
        }
        else if (i is InstructionJNA)
        {
            AddJumpRel32(i, 0x86);
        }
        else if (i is InstructionJNAE)
        {
            AddJumpRel32(i, 0x82);
        }
        else if (i is InstructionJNB)
        {
            AddJumpRel32(i, 0x83);
        }
        else if (i is InstructionJNBE)
        {
            AddJumpRel32(i, 0x87);
        }
        else if (i is InstructionJNC)
        {
            AddJumpRel32(i, 0x83);
        }
        else if (i is InstructionJNE)
        {
            AddJumpRel32(i, 0x85);
        }
        else if (i is InstructionJNG)
        {
            AddJumpRel32(i, 0x8E);
        }
        else if (i is InstructionJNGE)
        {
            AddJumpRel32(i, 0x8C);
        }
        else if (i is InstructionJNL)
        {
            AddJumpRel32(i, 0x8D);
        }
        else if (i is InstructionJNLE)
        {
            AddJumpRel32(i, 0x8F);
        }
        else if (i is InstructionJNO)
        {
            AddJumpRel32(i, 0x81);
        }
        else if (i is InstructionJNP)
        {
            AddJumpRel32(i, 0x8B);
        }
        else if (i is InstructionJNS)
        {
            AddJumpRel32(i, 0x89);
        }
        else if (i is InstructionJNZ)
        {
            AddJumpRel32(i, 0x85);
        }
        else if (i is InstructionJO)
        {
            AddJumpRel32(i, 0x80);
        }
        else if (i is InstructionJP)
        {
            AddJumpRel32(i, 0x8A);
        }
        else if (i is InstructionJPE)
        {
            AddJumpRel32(i, 0x8A);
        }
        else if (i is InstructionJPO)
        {
            AddJumpRel32(i, 0x8B);
        }
        else if (i is InstructionJS)
        {
            AddJumpRel32(i, 0x88);
        }
        else if (i is InstructionJZ)
        {
            AddJumpRel32(i, 0x84);
        }

        if (i is InstructionAAD || i is InstructionAAM)
        {
            _output.Add((byte)(i is InstructionAAD ? 0xD5 : 0xD4));
            if (i.Variant == InstructionVariant.NoOperands())
            {
                _output.Add(0x0A);
            }
            else
            {
                _output.Add(byte.Parse(i.FirstOperand.FullOperand));
            }
        }

        if (i is InstructionCBW)
        {
            if (ModeSize != Size.Word)
            {
                _output.Add(0x66);
            }
            AddNonary(0x98);
        }

        if (i is InstructionCWDE)
        {
            if (ModeSize != Size.Dword)
            {
                _output.Add(0x66);
            }
            AddNonary(0x98);
        }

        if (i is InstructionCDQE)
        {
            _output.Add((byte)(REX_BASE | 1 << REX_W));
            AddNonary(0x98);
        }

        if (i is InstructionCMPSB)
        {
            AddPrefix(i);
            AddNonary(0xA6);
        }
        else if (i is InstructionCMPSW)
        {
            AddPrefix(i);
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

        if (i is InstructionLODSB)
        {
            AddPrefix(i);
            AddNonary(0xAC);
        }
        else if (i is InstructionLODSW)
        {
            AddPrefix(i);
            if (ModeSize != Size.Word)
            {
                _output.Add(0x66);
            }
            AddNonary(0xAD);
        }

        if (i is InstructionMOVSB)
        {
            AddPrefix(i);
            AddNonary(0xA4);
        }
        else if (i is InstructionMOVSW)
        {
            AddPrefix(i);
            if (ModeSize != Size.Word)
            {
                _output.Add(0x66);
            }
            AddNonary(0xA5);
        }

        if (i is InstructionSCASB)
        {
            AddPrefix(i);
            AddNonary(0xAE);
        }
        else if (i is InstructionSCASW)
        {
            AddPrefix(i);
            if (ModeSize != Size.Word)
            {
                _output.Add(0x66);
            }
            AddNonary(0xAF);
        }

        if (i is InstructionPUSH)
        {
            if (i.FirstOperand.Variant == OperandVariant.Value)
            {
                if (ModeSize == Size.Word)
                {
                    _output.Add(0x66);
                }
                _output.Add(0x68);
                _output.AddRange(UlongToBinary(ulong.Parse(i.SecondOperand.FullOperand), Size.Dword));
            }
            else
            {
                AddSimpleUnary(i, 0xFF, 6);
            }
        }

        if (i is InstructionPOP)
        {
            AddSimpleUnary(i, 0x8F, 0);
        }

        if (i is InstructionLOOP)
        {
            AddJump(i, false, 0xE2, Size.Byte);
        }

        if (i is InstructionIMUL)
        {
            if (i.SecondOperand == null)
            {
                AddSimpleUnary(i, 0xF6, 5);
            }
            else if (i.ThirdOperand == null)
            {
                _output.Add(0x0F);
                AddSimpleBinary(i, 0x69, 0x69, 0xAF);
            }
            else
            {
                //Cannot use AddSimpleBinary here because it ORs 2 to the opcode because the operand order is "reg, mem"

                byte rex = 0;
                byte? modrm = null;
                byte? sib = null;
                byte[] disp = { };
                List<string> dispPatches = GetModRM(i.SecondOperand, ref rex, ref modrm, ref sib, ref disp);

                modrm = (byte)(modrm | GetRegisterCode(VirtualCPU, i.FirstOperand.FullOperand) << REG_BIT);

                if (i.FirstOperand.OperandSize == Size.Word != (ModeSize == Size.Word))
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

                _output.AddRange(UlongToBinary(ulong.Parse(i.ThirdOperand.FullOperand), i.FirstOperand.OperandSize == Size.Word ? Size.Word : Size.Dword));
            }
        }

        if (i is InstructionIDIV)
        {
            AddSimpleUnary(i, 0xF6, 7);
        }

        if (i is InstructionCALL)
        {
            if (i.FirstOperand.Variant == OperandVariant.Value || i.FirstOperand.Variant == OperandVariant.Label)
            {
                AddJumpRel32(i, 0xE8);
            }
            else
            {
                AddSimpleUnary(i, 0xFF, 2);
            }
        }

        PatchCode();

        return _patches.Count == 0;
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

                constantDisplacement = (long)constantDisplacement + long.Parse(offsets[i].FullOperand);
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
            offsetBase = GetRegisterCode(VirtualCPU, offsets[1].FullOperand);

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
            sib = (byte)(ILog2(ulong.Parse(offsets[1].FullOperand)) << SCALE_BIT | index << INDEX_BIT | 5 << BASE_BIT);
        }
        else if (offsets.Count == 2 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Operand == MemoryOffsetOperand.Multiplication && hasDisplacement)
        {
            index = GetRegisterCode(VirtualCPU, offsets[0].FullOperand);
            scale = uint.Parse(offsets[1].FullOperand);

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
            index = GetRegisterCode(VirtualCPU, offsets[0].FullOperand);
            scale = uint.Parse(offsets[1].FullOperand);
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
            index = GetRegisterCode(VirtualCPU, offsets[0].FullOperand);
            scale = uint.Parse(offsets[1].FullOperand);
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