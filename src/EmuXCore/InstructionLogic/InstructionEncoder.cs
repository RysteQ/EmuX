//using EmuXCore.InstructionLogic.Instructions;
//using EmuXCore.InstructionLogic.Instructions.Interfaces;
//using EmuXCore.InstructionLogic.Instructions.Internal;
//using EmuXCore.InstructionLogic.Prefixes;
//using EmuXCore.VM;
//using EmuXCore.VM.Internal.BIOS;
//using EmuXCore.VM.Internal.Memory;
//using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
//using EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;
//using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;
//using EmuXCore.VM.Internal.CPU.Registers;
//using EmuXCore.VM.Interfaces;
//using EmuXCore.VM.Internal.CPU;
//using EmuXCore.VM.Interfaces.Components;
//using EmuXCore.VM.Interfaces.Components.Internal;
//using EmuXCore.Common.Enums;
//using EmuXCore.Common.Interfaces;
//using EmuXCore.Interpreter;
//using System.Diagnostics;
//using EmuXCore.VM.Internal.BIOS.Interfaces;
//using EmuXCore.VM.Internal.BIOS.Internal;
//using EmuXCore.VM.Internal.RTC;
//using EmuXCore.VM.Internal.GPUs;
//using EmuXCore.Interpreter.Interfaces;

//namespace EmuXCore.InstructionLogic;

//public sealed class InstructionEncoder
//{
//	public static readonly byte REX_BASE = 64;
//	public static readonly byte REX_W = 3;
//	public static readonly byte REX_R = 2;
//	public static readonly byte REX_X = 1;
//	public static readonly byte REX_B = 0;
	
//	public static readonly int MOD_BIT = 6;
//	public static readonly int REG_BIT = 3;
//	public static readonly int RM_BIT = 0;
	
//	public static readonly int SCALE_BIT = 6;
//	public static readonly int INDEX_BIT = 3;
//	public static readonly int BASE_BIT = 0;
	
//	public static byte GetRegisterCode(IVirtualCPU cpu, string name) {
//		IVirtualRegister reg = cpu.GetRegister(name)!;
//		bool h = name.EndsWith("H");
		
//		switch (reg) {
//			case VirtualRegisterRAX i:
//				return (byte) (h ? 4 : 0);
//			case VirtualRegisterRBX i:
//				return (byte) (h ? 7 : 3);
//			case VirtualRegisterRCX i:
//				return (byte) (h ? 5 : 1);
//			case VirtualRegisterRDX i:
//				return (byte) (h ? 6 : 2);
//			case VirtualRegisterRSP i:
//				return 4;
//			case VirtualRegisterRBP i:
//				return 5;
//			case VirtualRegisterRSI i:
//				return 6;
//			case VirtualRegisterRDI i:
//				return 7;
//			default:
//				return 69;
//		}
//	}
	
//	public class Patch {
//		public ulong Location;
//		public Size Size;
//		public bool subtract;
//	}
	
//	public IVirtualCPU CPU;
//	public IOperandDecoder od;
//	public IVirtualMachine vm;
	
//	public Size ModeSize {get; set;} = Size.Dword;
	
//	public Dictionary<string, ulong> Labels = new();
//	public Dictionary<string, List<Patch>> Patches = new();
	
//	public List<byte> Output = new();
	
//	public InstructionEncoder(IVirtualMachine vm, IOperandDecoder od)
//	{
//		this.CPU = vm.CPU;
//		this.od = od;
//		this.vm = vm;
//	}
	
//	private void PatchCode()
//	{
//		List<string> doneLabels = new();
		
//		foreach (KeyValuePair<string, List<Patch>> pair in Patches)
//		{
//			string patchLabel = pair.Key;
//			List<Patch> list = pair.Value;
			
//			if (!Labels.ContainsKey(patchLabel))
//			{
//				continue;
//			}
			
//			foreach (Patch p in list)
//			{
//				byte[] b = Output.GetRange((int) p.Location, (int) p.Size).ToArray();
				
//				if (!BitConverter.IsLittleEndian)
//				{
//					Array.Reverse(b);
//				}
				
//				ulong offset = Labels[patchLabel];
//				if(p.subtract) {
//					offset = (ulong) -(long) offset;
//				}
				
//				if (p.Size == Size.Word)
//				{
//					ushort address = BitConverter.ToUInt16(b);
//					address += (ushort) offset;
//					b = BitConverter.GetBytes(address);
//				}
//				else if (p.Size == Size.Dword)
//				{
//					uint address = BitConverter.ToUInt32(b);
//					address += (uint) offset;
//					b = BitConverter.GetBytes(address);
//				}
//				else if (p.Size == Size.Qword)
//				{
//					ulong address = BitConverter.ToUInt64(b);
//					address += (ulong) offset;
//					b = BitConverter.GetBytes(address);
//				}
				
//				if (!BitConverter.IsLittleEndian)
//				{
//					Array.Reverse(b);
//				}
				
//				Output.RemoveRange((int) p.Location, (int) p.Size);
//				Output.InsertRange((int) p.Location, b);
//			}
			
//			doneLabels.Add(patchLabel);
//		}
		
//		foreach (string label in doneLabels)
//		{
//			Patches.Remove(label);
//		}
//	}
	
//	private void AddPatches(List<string> labels)
//	{
//		foreach(string label in labels)
//		{
//			AddPatch(label);
//		}
//	}
	
//	private void AddPatch(string label)
//	{
//		if(!Patches.ContainsKey(label))
//		{
//			Patches.Add(label, [new() {
//				Location = (ulong) Output.Count,
//				Size = ModeSize
//			}]);
//		}
//	}
	
//	public bool Finish()
//	{
//		PatchCode();
		
//		return Patches.Count == 0;
//	}
	
//	public void Mark(string name)
//	{
//		Labels.Add(name, (ulong) Output.Count);
//	}
	
//	public byte[] LongToBinary(long val, int sz) {
//		byte[] b = BitConverter.GetBytes(val);
//		if (!BitConverter.IsLittleEndian) {
//			Array.Reverse(b);
//		}
//		return b.Take(sz).ToArray();
//	}
	
//	static int ILog2(ulong val) {
//		if(val == 0) {
//			return 1;
//		}
		
//		int bits = -1;
//		while (val > 0) {
//			bits++;
//			val >>= 1;
//		}
//		return bits;
//	}
	
//	public List<string> GetModRM(IOperand op, ref byte rex, ref byte? modrm, ref byte? sib, ref byte[] disp) {
//		if(op.Variant == OperandVariant.Register) {
//			int coed = GetRegisterCode(this.CPU, op.FullOperand);
			
//			modrm = (byte) ((3 << MOD_BIT) | (coed << RM_BIT));
//			if(coed >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((coed >> 3) & 1) << REX_B);
//			}
			
//			return new();
//		}
		
//		if(op.Variant != OperandVariant.Memory) {
//			throw new ArgumentException("Operand is not register nor memory");
//		}
		
//		// Normalize by moving displacements to separate array
//		// and moving the scale, if any, to the beginning.
//		// This leaves `offsets` in one of five forms:
//		//   reg
//		//   reg + reg
//		//   reg * scale
//		//   reg * scale + reg
//		//   (empty)
		
//		var offsets = op.Offsets.ToList();
		
//		int scaleOffset = offsets.TakeWhile(o => o.Type != MemoryOffsetType.Scale).Count();
//		if(scaleOffset < offsets.Count) {
//			if(scaleOffset == 0) {
//				throw new ArgumentException("Scale cannot be first");
//			}
			
//			(offsets[0], offsets[scaleOffset - 1]) = (offsets[scaleOffset - 1], offsets[0]);
//			(offsets[1], offsets[scaleOffset - 0]) = (offsets[scaleOffset - 0], offsets[1]);
//		}
		
//		long? constantDisplacement = null;
//		List<string> labelDisplacements = new();
//		for(int i = scaleOffset == offsets.Count ? 0 : 2; i < offsets.Count;) {
//			if(offsets[i].Type == MemoryOffsetType.Scale) {
//				throw new ArgumentException("One scale max");
//			}
			
//			if(offsets[i].Type == MemoryOffsetType.Integer) {
//				if(constantDisplacement == null) {
//					constantDisplacement = 0;
//				}
				
//				constantDisplacement = (long) constantDisplacement + long.Parse(offsets[i].FullOperand);
				
//				offsets.RemoveAt(i);
//			} else if(offsets[i].Type == MemoryOffsetType.Label) {
//				labelDisplacements.Add(offsets[i].FullOperand);
				
//				offsets.RemoveAt(i);
//			} else {
//				i++;
//			}
//		}
//		bool hasDisplacement = constantDisplacement != null || labelDisplacements.Count > 0;
		
//		// From this point `offsets` is only regs & scale.
//		// Displacements must be checked with `hasDisplacement`.
		
//		if(offsets.Count == 0 && !hasDisplacement) {
//			modrm = (byte) ((0 << MOD_BIT) | (5 << RM_BIT));
			
//			// Shouldn't happen but I guess this is an easter egg.
//			disp = LongToBinary(0x69696969, 4);
//		} else if(offsets.Count == 0 && hasDisplacement) {
//			modrm = (byte) ((0 << MOD_BIT) | (5 << RM_BIT));
			
//			disp = LongToBinary((long) constantDisplacement, 4);
//		} else if(offsets.Count == 1 && offsets[0].Type == MemoryOffsetType.Register && !hasDisplacement) {
//			int coed = GetRegisterCode(this.CPU, offsets[0].FullOperand);
			
			//if(coed >= 8) {
		//		rex |= REX_BASE;
		//		rex |= (byte) (((coed >> 3) & 1) << REX_B);
		//		coed -= 8;
		//	}
		//	modrm = (byte) ((0 << MOD_BIT) | (coed << RM_BIT));
			
		//	if(coed == 4) {
		//		sib = (byte) (4 | (4 << 3));
		//	} else if(coed == 5) {
		//		modrm |= (byte) (1 << MOD_BIT);
		//		disp = new byte[] {0};
		//	}
		//} else if(offsets.Count == 1 && offsets[0].Type == MemoryOffsetType.Register && hasDisplacement) {
		//	int coed = GetRegisterCode(this.CPU, offsets[0].FullOperand);
			
//			if(coed >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((coed >> 3) & 1) << REX_B);
//				coed -= 8;
//			}
//			modrm = (byte) ((2 << MOD_BIT) | (coed << RM_BIT));
			
		//	if(coed == 4) {
		//		sib = (byte) (4 | (4 << 3));
		//	}
			
		//	disp = LongToBinary((long) constantDisplacement, 4);
		//} else if(offsets.Count == 2 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Type == MemoryOffsetType.Register) {
		//	int index = GetRegisterCode(this.CPU, offsets[0].FullOperand);
		//	int baes = GetRegisterCode(this.CPU, offsets[1].FullOperand);
			
		//	if(index == 4) {
		//		(index, baes) = (baes, index);
		//	}
			
		//	modrm = (byte) (((hasDisplacement ? 2 : 0) << MOD_BIT) | (4 << RM_BIT));
			
		//	if(index >= 8 || baes >= 8) {
		//		rex |= REX_BASE;
		//		rex |= (byte) (((index >> 3) & 1) << REX_X);
		//		rex |= (byte) (((baes >> 3) & 1) << REX_B);
		//	}
			
		//	sib = (byte) (baes | (index << INDEX_BIT));
			
		//	if(hasDisplacement) {
		//		disp = LongToBinary((long) constantDisplacement, 4);
		//	} else {
		//		if(baes == 5) {
		//			modrm |= (byte) (1 << MOD_BIT);
		//			disp = new byte[] {0};
		//		}
		//	}
		//} else if(offsets.Count == 2 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Type == MemoryOffsetType.Scale && !hasDisplacement) {
		//	int index = GetRegisterCode(this.CPU, offsets[0].FullOperand);
			
//			if(index == 4) {
//				throw new ArgumentException("esp is invalid index");
//			}
			
//			modrm = (byte) ((0 << MOD_BIT) | (4 << RM_BIT));
			
//			if(index >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((index >> 3) & 1) << REX_X);
//				index -= 8;
//			}
			
//			disp = LongToBinary(0, 4);
//			sib = (byte) ((ILog2(ulong.Parse(offsets[1].FullOperand)) << SCALE_BIT) | (index << INDEX_BIT) | (5 << BASE_BIT));
//		} else if(offsets.Count == 2 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Type == MemoryOffsetType.Scale && hasDisplacement) {
//			int index = GetRegisterCode(this.CPU, offsets[0].FullOperand);
//			uint scale = uint.Parse(offsets[1].FullOperand);
			
//			if(index == 4) {
//				throw new ArgumentException("esp is invalid index");
//			}
			
//			modrm = (byte) ((0 << MOD_BIT) | (4 << RM_BIT));
			
//			if(index >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((index >> 3) & 1) << REX_X);
//				index -= 8;
//			}
			
//			disp = LongToBinary((long) constantDisplacement, 4);
//			sib = (byte) ((ILog2(scale) << SCALE_BIT) | (index << INDEX_BIT) | (5 << BASE_BIT));
//		} else if(offsets.Count == 3 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Type == MemoryOffsetType.Scale && offsets[2].Type == MemoryOffsetType.Register && !hasDisplacement) {
//			int index = GetRegisterCode(this.CPU, offsets[0].FullOperand);
//			uint scale = uint.Parse(offsets[1].FullOperand);
//			int baes = GetRegisterCode(this.CPU, offsets[2].FullOperand);
			
//			if(index == 4) {
//				throw new ArgumentException("esp is invalid index");
//			}
			
//			modrm = (byte) ((0 << MOD_BIT) | (4 << RM_BIT));
			
//			if(index >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((index >> 3) & 1) << REX_X);
//				index -= 8;
//			}
			
//			if(baes == 5) {
//				modrm |= (byte) (1 << MOD_BIT);
//				disp = LongToBinary(0, 1);
//			} else if(baes >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((baes >> 3) & 1) << REX_B);
//				baes -= 8;
//			}
			
//			sib = (byte) ((ILog2(scale) << SCALE_BIT) | (index << INDEX_BIT) | (baes << BASE_BIT));
//		} else if(offsets.Count == 3 && offsets[0].Type == MemoryOffsetType.Register && offsets[1].Type == MemoryOffsetType.Scale && offsets[2].Type == MemoryOffsetType.Register && hasDisplacement) {
//			int index = GetRegisterCode(this.CPU, offsets[0].FullOperand);
//			uint scale = uint.Parse(offsets[1].FullOperand);
//			int baes = GetRegisterCode(this.CPU, offsets[2].FullOperand);
			
//			if(index == 4) {
//				throw new ArgumentException("esp is invalid index");
//			}
			
//			modrm = (byte) ((2 << MOD_BIT) | (4 << RM_BIT));
			
//			if(index >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((index >> 3) & 1) << REX_X);
//				index -= 8;
//			}
			
//			if(baes >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((baes >> 3) & 1) << REX_B);
//				baes -= 8;
//			}
			
//			sib = (byte) ((ILog2(scale) << SCALE_BIT) | (index << INDEX_BIT) | (baes << BASE_BIT));
			
//			disp = LongToBinary((long) constantDisplacement, 4);
//		} else {
//			throw new ArgumentException("Unsupported");
//		}
		
//		return labelDisplacements;
//	}
	
//	private void AddSimpleBinary(IInstruction i, byte opcodeImm, byte opcodeImmExt, byte opcodeMain)
//	{
//		byte opcode = 0;
//		byte[] imm = {};
//		byte[] disp = {};
//		byte rex = 0;
//		byte? modrm = null;
//		byte? sib = null;
//		bool size8 = false;
//		bool prefixSize = false;
//		List<string> dispPatches;
		
//		if(i.SecondOperand.Variant == OperandVariant.Value) {
//			opcode = opcodeImm;
			
//			dispPatches = GetModRM(i.FirstOperand, ref rex, ref modrm, ref sib, ref disp);
			
//			modrm = (byte) (modrm | (opcodeImmExt << REG_BIT));
			
//			if(i.FirstOperand.OperandSize == Size.Byte) {
//				size8 = true;
//			} else if((i.FirstOperand.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//				prefixSize = true;
//			}
			
//			imm = LongToBinary((long) od.GetOperandValue(vm, i.SecondOperand), (int) i.FirstOperand.OperandSize);
//		} else {
//			opcode = opcodeMain;
			
//			IOperand rm = i.FirstOperand;
//			IOperand reg = i.SecondOperand;
			
//			if(reg.Variant == OperandVariant.Memory) {
//				opcode |= 2;
//				(rm, reg) = (reg, rm);
//			}
			
//			dispPatches = GetModRM(rm, ref rex, ref modrm, ref sib, ref disp);
			
//			int coed = GetRegisterCode(this.CPU, reg.FullOperand);
			
//			if(coed >= 8) {
//				rex |= REX_BASE;
//				rex |= (byte) (((coed >> 3) & 1) << REX_R);
//				coed -= 8;
//			}
			
//			modrm |= (byte) (coed << REG_BIT);
			
//			if(rm.OperandSize == Size.Byte) {
//				size8 = true;
//			} else if((rm.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//				prefixSize = true;
//			}
//		}
		
//		if(!size8) {
//			opcode |= 1;
//		}
		
//		if(prefixSize) {
//			Output.Add(0x66);
//		}
		
//		if(rex != 0) {
//			Output.Add(rex);
//		}
		
//		Output.Add(opcode);
		
//		if(modrm != null) {
//			Output.Add((byte) modrm!);
//		}
		
//		if(sib != null) {
//			Output.Add((byte) sib!);
//		}
		
//		AddPatches(dispPatches);
//		Output.AddRange(disp);
		
//		Output.AddRange(imm);
//	}
	
//	private void AddSimpleUnary(IInstruction i, byte opcode, byte opcodeExt) {
//		if(i.FirstOperand.Variant == OperandVariant.Value) {
//			throw new ArgumentException("Operand cannot be immediate");
//		}
		
//		byte rex = 0;
//		byte? modrm = null;
//		byte? sib = null;
//		byte[] disp = {};
//		List<string> dispPatches = GetModRM(i.FirstOperand, ref rex, ref modrm, ref sib, ref disp);
		
//		modrm = (byte) (modrm | (opcodeExt << REG_BIT));
		
//		if(i.FirstOperand.OperandSize != Size.Byte) {
//			opcode |= 1;
//		}
		
//		if((i.FirstOperand.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//			Output.Add(0x66);
//		}
		
//		if(rex != 0) {
//			Output.Add(rex);
//		}
		
//		Output.Add(opcode);
		
//		if(modrm != null) {
//			Output.Add((byte) modrm!);
//		}
		
//		if(sib != null) {
//			Output.Add((byte) sib!);
//		}
		
//		AddPatches(dispPatches);
//		Output.AddRange(disp);
//	}
	
//	private void AddNonary(byte opcode) {
//		Output.Add(opcode);
//	}
	
//	private void AddShift(IInstruction i, byte opcodeImm, byte opcodeCL, byte opcodeExt) {
//		if(i.SecondOperand.Variant == OperandVariant.Value) {
//			// imm8
//			AddSimpleUnary(i, opcodeImm, opcodeExt);
//			Output.AddRange(LongToBinary(long.Parse(i.SecondOperand.FullOperand), 1));
//		} else {
//			// CL
//			AddSimpleUnary(i, opcodeCL, opcodeExt);
//		}
//	}
	
//	private void AddJump(IInstruction i, bool zeroeff, byte opcode, int displacementSize) {
//		// TODO: check for overflow
		
//		if(ModeSize != Size.Dword) {
//			Output.Add(0x66);
//		}
		
//		if(zeroeff) {
//			Output.Add(0x0F);
//		}
//		Output.Add(opcode);
		
//		if(i.FirstOperand.Variant == OperandVariant.Value) {
//			Output.AddRange(LongToBinary(long.Parse(i.FirstOperand.FullOperand), displacementSize));
//		} else if(i.FirstOperand.Variant == OperandVariant.Label) {
//			AddPatch(i.FirstOperand.FullOperand);
//			Output.AddRange(LongToBinary(-Output.Count - displacementSize + (zeroeff ? 0 : 1), displacementSize));
//		}
//	}
	
//	private void AddJumpRel32(IInstruction i, byte opcode) {
//		AddJump(i, true, opcode, 4);
//	}
	
//	private void AddPrefix(IInstruction i) {
//		if(i.Prefix == null) {
//			return;
//		}
		
//		if(i.Prefix is PrefixREP) {
//			Output.Add(0xF3);
//		} else if(i.Prefix is PrefixREPE) {
//			Output.Add(0xF3);
//		} else if(i.Prefix is PrefixREPZ) {
//			Output.Add(0xF3);
//		} else if(i.Prefix is PrefixREPNE) {
//			Output.Add(0xF2);
//		} else if(i.Prefix is PrefixREPNZ) {
//			Output.Add(0xF2);
//		}
//	}
	
//	public void Add(IInstruction i) {
//		if(i is InstructionADD) {
//			AddSimpleBinary(i, 0x80, 0, 0x00);
//		} else if(i is InstructionOR) {
//			AddSimpleBinary(i, 0x80, 1, 0x08);
//		} else if(i is InstructionADC) {
//			AddSimpleBinary(i, 0x80, 2, 0x10);
//		} else if(i is InstructionSBB) {
//			AddSimpleBinary(i, 0x80, 3, 0x18);
//		} else if(i is InstructionAND) {
//			AddSimpleBinary(i, 0x80, 4, 0x20);
//		} else if(i is InstructionSUB) {
//			AddSimpleBinary(i, 0x80, 5, 0x28);
//		} else if(i is InstructionXOR) {
//			AddSimpleBinary(i, 0x80, 6, 0x30);
//		} else if(i is InstructionCMP) {
//			AddSimpleBinary(i, 0x80, 7, 0x38);
//		} else if(i is InstructionTEST) {
//			AddSimpleBinary(i, 0xF6, 0, 0x84);
//		}
		
//		// These are basically simple binary but they don't accept immediates
//		if(i is InstructionXCHG) {
//			AddSimpleBinary(i, 0x69, 69, 0x87);
//		} else if(i is InstructionLES) {
//			AddSimpleBinary(i, 0x69, 69, 0xC4);
//		} else if(i is InstructionLDS) {
//			AddSimpleBinary(i, 0x69, 69, 0xC5);
//		}
		
//		if(i is InstructionNOT) {
//			AddSimpleUnary(i, 0xF6, 2);
//		} else if(i is InstructionNEG) {
//			AddSimpleUnary(i, 0xF6, 3);
//		} else if(i is InstructionMUL) {
//			AddSimpleUnary(i, 0xF6, 4);
//		} else if(i is InstructionDIV) {
//			AddSimpleUnary(i, 0xF6, 6);
//		} else if(i is InstructionINC) {
//			AddSimpleUnary(i, 0xFE, 0);
//		} else if(i is InstructionDEC) {
//			AddSimpleUnary(i, 0xFE, 1);
//		}
		
//		if(i is InstructionCMC) {
//			AddNonary(0xF5);
//		} else if(i is InstructionCLC) {
//			AddNonary(0xF8);
//		} else if(i is InstructionSTC) {
//			AddNonary(0xF9);
//		} else if(i is InstructionCLI) {
//			AddNonary(0xFA);
//		} else if(i is InstructionSTI) {
//			AddNonary(0xFB);
//		} else if(i is InstructionCLD) {
//			AddNonary(0xFC);
//		} else if(i is InstructionSTD) {
//			AddNonary(0xFD);
//		} else if(i is InstructionAAA) {
//			AddNonary(0x37);
//		} else if(i is InstructionAAS) {
//			AddNonary(0x3F);
//		} else if(i is InstructionDAA) {
//			AddNonary(0x27);
//		} else if(i is InstructionDAS) {
//			AddNonary(0x2F);
//		} else if(i is InstructionRET) {
//			// Missing far returns and retn
//			AddNonary(0xC3);
//		} else if(i is InstructionHLT) {
//			AddNonary(0xF4);
//		} else if(i is InstructionSAHF) {
//			AddNonary(0x9E);
//		} else if(i is InstructionLAHF) {
//			AddNonary(0x9F);
//		} else if(i is InstructionXLAT) {
//			AddNonary(0xD7);
//		} else if(i is InstructionINTO) {
//			AddNonary(0xCE);
//		} else if(i is InstructionPUSHF) {
//			if(ModeSize == Size.Dword) {
//				Output.Add(0x66);
//			}
//			AddNonary(0x9C);
//		} else if(i is InstructionPUSHFD) {
//			if(ModeSize == Size.Word) {
//				Output.Add(0x66);
//			}
//			AddNonary(0x9C);
//		} else if(i is InstructionPUSHFQ) {
//			AddNonary(0x9C);
//		} else if(i is InstructionPOPF) {
//			if(ModeSize == Size.Dword) {
//				Output.Add(0x66);
//			}
//			AddNonary(0x9D);
//		} else if(i is InstructionPOPFD) {
//			if(ModeSize == Size.Word) {
//				Output.Add(0x66);
//			}
//			AddNonary(0x9D);
//		} else if(i is InstructionPOPFQ) {
//			AddNonary(0x9D);
//		} else if(i is InstructionCWD) {
//			if(ModeSize == Size.Dword) {
//				Output.Add(0x66);
//			}
//			AddNonary(0x99);
//		} else if(i is InstructionCDQ) {
//			if(ModeSize == Size.Word) {
//				Output.Add(0x66);
//			}
//			AddNonary(0x99);
//		} /*else if(i is InstructionCQO) {
//			Output.Add((byte) (REX_BASE | (1 << REX_W)));
//			AddNonary(0x99);
//		}*/ else if(i is InstructionSTOSB) {
//			AddPrefix(i);
			
//			AddNonary(0xAA);
//		} else if(i is InstructionSTOSW) {
//			AddPrefix(i);
			
//			if(ModeSize != Size.Word) {
//				AddNonary(0x66);
//			}
//			AddNonary(0xAB);
//		} /*else if(i is InstructionSTOSD) {
//			if(ModeSize != Size.Dword) {
//				AddNonary(0x66);
//			}
//			AddNonary(0xAB);
//		} else if(i is InstructionSTOSQ) {
//			AddNonary(0x48); // REX.W
//			AddNonary(0xAB);
//		}*/
		
//		if(i is InstructionSHL) {
//			AddShift(i, 0xC0, 0xD2, 4);
//		} else if(i is InstructionSHR) {
//			AddShift(i, 0xC0, 0xD2, 5);
//		} else if(i is InstructionSAR) {
//			AddShift(i, 0xC0, 0xD2, 7);
//		} else if(i is InstructionSAL) {
//			AddShift(i, 0xC0, 0xD2, 4);
//		}
		
//		if(i is InstructionROL) {
//			AddShift(i, 0xC0, 0xD2, 0);
//		} else if(i is InstructionROR) {
//			AddShift(i, 0xC0, 0xD2, 1);
//		} else if(i is InstructionRCL) {
//			AddShift(i, 0xC0, 0xD2, 2);
//		} else if(i is InstructionRCR) {
//			AddShift(i, 0xC0, 0xD2, 3);
//		}
		
//		if(i is InstructionMOV) {
//			// MOV isn't just simple binary because segregs and stuff
//			// and what the fuck moffs is
//			AddSimpleBinary(i, 0xC6, 0, 0x88);
//		}
		
//		if(i is InstructionINT) {
//			AddNonary(0xCD);
//			AddNonary(byte.Parse(i.FirstOperand.FullOperand));
//		}
		
//		if(i is InstructionLEA) {
//			byte rex = 0;
//			byte? modrm = null;
//			byte? sib = null;
//			byte[] disp = {};
//			List<string> dispPatches = GetModRM(i.SecondOperand, ref rex, ref modrm, ref sib, ref disp);
			
//			modrm = (byte) (modrm | (GetRegisterCode(this.CPU, i.FirstOperand.FullOperand) << REG_BIT));
			
//			if((i.FirstOperand.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//				Output.Add(0x66);
//			}
			
//			if(rex != 0) {
//				Output.Add(rex);
//			}
			
//			Output.Add(0x8D);
			
//			Output.Add((byte) modrm!);
			
//			if(sib != null) {
//				Output.Add((byte) sib!);
//			}
			
//			AddPatches(dispPatches);
//			Output.AddRange(disp);
//		}
		
//		if(i is InstructionIN) {
//			if(i.SecondOperand.Variant == OperandVariant.Value) {
//				if((i.FirstOperand.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//					Output.Add(0x66);
//				}
//				Output.Add((byte) (0xE4 + Convert.ToByte(i.FirstOperand.OperandSize != Size.Byte)));
//				Output.AddRange(LongToBinary(long.Parse(i.SecondOperand.FullOperand), 1));
//			} else {
//				if((i.FirstOperand.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//					Output.Add(0x66);
//				}
//				Output.Add((byte) (0xEC + Convert.ToByte(i.FirstOperand.OperandSize != Size.Byte)));
//			}
//		}
		
//		if(i is InstructionOUT) {
//			if(i.SecondOperand.Variant == OperandVariant.Value) {
//				if((i.FirstOperand.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//					Output.Add(0x66);
//				}
//				Output.Add((byte) (0xE6 + Convert.ToByte(i.FirstOperand.OperandSize != Size.Byte)));
//				Output.AddRange(LongToBinary(long.Parse(i.SecondOperand.FullOperand), 1));
//			} else {
//				if((i.FirstOperand.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//					Output.Add(0x66);
//				}
//				Output.Add((byte) (0xEE + Convert.ToByte(i.FirstOperand.OperandSize != Size.Byte)));
//			}
//		}
		
//		if(i is InstructionJA) {
//			AddJumpRel32(i, 0x87);
//		} else if(i is InstructionJAE) {
//			AddJumpRel32(i, 0x83);
//		} else if(i is InstructionJB) {
//			AddJumpRel32(i, 0x82);
//		} else if(i is InstructionJBE) {
//			AddJumpRel32(i, 0x86);
//		} else if(i is InstructionJC) {
//			AddJumpRel32(i, 0x82);
//		} else if(i is InstructionJCXZ) {
//			AddJumpRel32(i, 0xE3);
//		} else if(i is InstructionJE) {
//			AddJumpRel32(i, 0x84);
//		} else if(i is InstructionJG) {
//			AddJumpRel32(i, 0x8F);
//		} else if(i is InstructionJGE) {
//			AddJumpRel32(i, 0x8D);
//		} else if(i is InstructionJL) {
//			AddJumpRel32(i, 0x8C);
//		} else if(i is InstructionJLE) {
//			AddJumpRel32(i, 0x8E);
//		} else if(i is InstructionJNA) {
//			AddJumpRel32(i, 0x86);
//		} else if(i is InstructionJNAE) {
//			AddJumpRel32(i, 0x82);
//		} else if(i is InstructionJNB) {
//			AddJumpRel32(i, 0x83);
//		} else if(i is InstructionJNBE) {
//			AddJumpRel32(i, 0x87);
//		} else if(i is InstructionJNC) {
//			AddJumpRel32(i, 0x83);
//		} else if(i is InstructionJNE) {
//			AddJumpRel32(i, 0x85);
//		} else if(i is InstructionJNG) {
//			AddJumpRel32(i, 0x8E);
//		} else if(i is InstructionJNGE) {
//			AddJumpRel32(i, 0x8C);
//		} else if(i is InstructionJNL) {
//			AddJumpRel32(i, 0x8D);
//		} else if(i is InstructionJNLE) {
//			AddJumpRel32(i, 0x8F);
//		} else if(i is InstructionJNO) {
//			AddJumpRel32(i, 0x81);
//		} else if(i is InstructionJNP) {
//			AddJumpRel32(i, 0x8B);
//		} else if(i is InstructionJNS) {
//			AddJumpRel32(i, 0x89);
//		} else if(i is InstructionJNZ) {
//			AddJumpRel32(i, 0x85);
//		} else if(i is InstructionJO) {
//			AddJumpRel32(i, 0x80);
//		} else if(i is InstructionJP) {
//			AddJumpRel32(i, 0x8A);
//		} else if(i is InstructionJPE) {
//			AddJumpRel32(i, 0x8A);
//		} else if(i is InstructionJPO) {
//			AddJumpRel32(i, 0x8B);
//		} else if(i is InstructionJS) {
//			AddJumpRel32(i, 0x88);
//		} else if(i is InstructionJZ) {
//			AddJumpRel32(i, 0x84);
//		}
		
//		if(i is InstructionAAD || i is InstructionAAM) {
//			Output.Add((byte) (i is InstructionAAD ? 0xD5 : 0xD4));
//			if(i.Variant == InstructionVariant.NoOperands()) {
//				Output.Add(0x0A);
//			} else {
//				Output.Add(byte.Parse(i.FirstOperand.FullOperand));
//			}
//		}
		
//		if(i is InstructionCBW) {
//			if(ModeSize != Size.Word) {
//				Output.Add(0x66);
//			}
//			AddNonary(0x98);
//		}
		
//		if(i is InstructionCWDE) {
//			if(ModeSize != Size.Dword) {
//				Output.Add(0x66);
//			}
//			AddNonary(0x98);
//		}
		
//		if(i is InstructionCDQE) {
//			Output.Add((byte) (REX_BASE | (1 << REX_W)));
//			AddNonary(0x98);
//		}
		
//		if(i is InstructionCMPSB) {
//			AddPrefix(i);
//			AddNonary(0xA6);
//		} else if(i is InstructionCMPSW) {
//			AddPrefix(i);
//			if(ModeSize != Size.Word) {
//				Output.Add(0x66);
//			}
//			AddNonary(0xA7);
//		} /*else if(i is InstructionCMPSD) {
//			if(ModeSize != Size.Dword) {
//				Output.Add(0x66);
//			}
//			Output.Add(0xA7);
//		} else if(i is InstructionCMPSQ) {
//			Output.Add((byte) (REX_BASE | (1 << REX_W)));
//			Output.Add(0xA7);
//		}*/
		
//		if(i is InstructionLODSB) {
//			AddPrefix(i);
//			AddNonary(0xAC);
//		} else if(i is InstructionLODSW) {
//			AddPrefix(i);
//			if(ModeSize != Size.Word) {
//				Output.Add(0x66);
//			}
//			AddNonary(0xAD);
//		}
		
//		if(i is InstructionMOVSB) {
//			AddPrefix(i);
//			AddNonary(0xA4);
//		} else if(i is InstructionMOVSW) {
//			AddPrefix(i);
//			if(ModeSize != Size.Word) {
//				Output.Add(0x66);
//			}
//			AddNonary(0xA5);
//		}
		
//		if(i is InstructionSCASB) {
//			AddPrefix(i);
//			AddNonary(0xAE);
//		} else if(i is InstructionSCASW) {
//			AddPrefix(i);
//			if(ModeSize != Size.Word) {
//				Output.Add(0x66);
//			}
//			AddNonary(0xAF);
//		}
		
//		if(i is InstructionPUSH) {
//			if(i.FirstOperand.Variant == OperandVariant.Value) {
//				if(ModeSize == Size.Word) {
//					Output.Add(0x66);
//				}
//				Output.Add(0x68);
//				Output.AddRange(LongToBinary(long.Parse(i.SecondOperand.FullOperand), 4));
//			} else {
//				AddSimpleUnary(i, 0xFF, 6);
//			}
//		}
		
//		if(i is InstructionPOP) {
//			AddSimpleUnary(i, 0x8F, 0);
//		}
		
//		if(i is InstructionLOOP) {
//			AddJump(i, false, 0xE2, 1);
//		}
		
//		if(i is InstructionIMUL) {
//			if(i.SecondOperand == null) {
//				AddSimpleUnary(i, 0xF6, 5);
//			} else if(i.ThirdOperand == null) {
//				Output.Add(0x0F);
//				AddSimpleBinary(i, 0x69, 0x69, 0xAF);
//			} else {
//				// Cannot use AddSimpleBinary here because it ORs 2 to the opcode because the operand order is "reg, mem"
				
//				byte rex = 0;
//				byte? modrm = null;
//				byte? sib = null;
//				byte[] disp = {};
//				List<string> dispPatches = GetModRM(i.SecondOperand, ref rex, ref modrm, ref sib, ref disp);
				
//				modrm = (byte) (modrm | (GetRegisterCode(this.CPU, i.FirstOperand.FullOperand) << REG_BIT));
				
//				if((i.FirstOperand.OperandSize == Size.Word) != (ModeSize == Size.Word)) {
//					Output.Add(0x66);
//				}
				
//				if(rex != 0) {
//					Output.Add(rex);
//				}
				
//				Output.Add(0x69);
				
//				Output.Add((byte) modrm!);
				
//				if(sib != null) {
//					Output.Add((byte) sib!);
//				}
				
//				AddPatches(dispPatches);
//				Output.AddRange(disp);
				
//				Output.AddRange(LongToBinary(long.Parse(i.ThirdOperand.FullOperand), i.FirstOperand.OperandSize == Size.Word ? 2 : 4));
//			}
//		}
		
//		if(i is InstructionIDIV) {
//			AddSimpleUnary(i, 0xF6, 7);
//		}
		
//		if(i is InstructionCALL) {
//			if(i.FirstOperand.Variant == OperandVariant.Value || i.FirstOperand.Variant == OperandVariant.Label) {
//				AddJumpRel32(i, 0xE8);
//			} else {
//				AddSimpleUnary(i, 0xFF, 2);
//			}
//		}
//	}
	
//	public static void Main(string[] args)
//	{
//		VirtualCPU cpu = new();
//		VirtualMemory vmem = new();
//		VirtualRTC rtc = new();
//		VirtualGPU gpu = new();
//		VirtualBIOS bios = new(new DiskInterruptHandler(), new RTCInterruptHandler(), new VideoInterruptHandler(), new DeviceInterruptHandler());
		
//		VirtualMachine vm = new(cpu, vmem, [], bios, rtc, gpu, []);
////		Lexer lexer = new(cpu);
		
////		ILexerResult result = lexer.Parse(@"
////asdf:
////sub byte ptr [873608210], 5
////add byte ptr [ebx], 5
////sub byte ptr [ebx + 873608210], 5
////add byte ptr [ecx * 4], 5
////sub byte ptr [ecx * 2 + 873608210], 5
////add byte ptr [ecx * 4 + ebp + 873608210], 5
////loop asdf
////xor eax, ebx
////add eax, 873608210
////stosb
////stosw
////cli
////sti
////neg word ptr [873608210]
////neg eax
////not bl
////shl eax, 5
////shl eax, cl
////sar eax, 5
////sar eax, cl
////sal eax, 5
////sal eax, cl
////shr eax, 5
////shr eax, cl
////mov al, 1
////mov ax, 2
////mov eax, 3
////mov al, byte ptr [15]
////mov word ptr [15], bp
////int 19
////lea ax, word ptr [65536]
////je asdf
////aad
////aad 16
////cbw
////cwde
////push eax
////pop eax
////push ax
////pop ax
////lds eax, dword ptr [0]
////imul word ptr [0]
////imul eax, dword ptr [ebx]
////imul eax, dword ptr [ebx], 5
////idiv dword ptr [eax]
////call asdf
////");
		
//		//InstructionEncoder ie = new(vm, new OperandDecoder());
//		//foreach(IInstruction i in result.Instructions) {
//		//	ie.Add(i);
//		//}
		
//		//using (Stream s = Console.OpenStandardOutput())
//		//{
//		//	var a = ie.Output.ToArray();
//		//	s.Write(a, 0, a.Length);
//		//}
//	}
//}
