﻿using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRDI : IVirtualRegister
{
    public VirtualRegisterRDI()
    {
        RDI = (ulong)Random.Shared.NextInt64();
    }

    public ulong Get() => RDI;
    public void Set(ulong value) => RDI = value;

    public ulong RDI { get; set; }

    public uint EDI
    {
        get => (uint)(RDI & 0x00000000ffffffff);
        set => RDI = (RDI & 0xffffffff00000000) + value;
    }

    public ushort DI
    {
        get => (ushort)(EDI & 0x0000ffff);
        set => EDI = (EDI & 0xffff0000) + value;
    }

    public byte DIL
    {
        get => (byte)(DI & 0x00ff);
        set => DI = (ushort)((DI & 0xff00) + value);
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RDI), Size.Quad },
        { nameof(EDI), Size.Double },
        { nameof(DI), Size.Word },
        { nameof(DIL), Size.Byte }
    };
}