﻿using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Internal.CPU.Registers.MainRegisters;

public class VirtualRegisterRAX : IVirtualRegister
{
    public VirtualRegisterRAX()
    {
        RAX = (ulong)Random.Shared.NextInt64();
    }

    public ulong Get() => RAX;
    public void Set(ulong value) => RAX = value;

    public ulong RAX { get; set; }

    public uint EAX
    {
        get => (uint)(RAX & 0x00000000ffffffff);
        set => RAX = (RAX & 0xffffffff00000000) + value;
    }

    public ushort AX
    {
        get => (ushort)(EAX & 0x0000ffff);
        set => EAX = (EAX & 0xffff0000) + value;
    }

    public byte AH
    {
        get => (byte)((AX & 0xff00) >> 8);
        set => AX = (ushort)((AX & 0x00ff) + (value << 8));
    }

    public byte AL
    {
        get => (byte)(AX & 0x00ff);
        set => AX = (ushort)((AX & 0xff00) + value);
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(RAX), Size.Quad },
        { nameof(EAX), Size.Double },
        { nameof(AX), Size.Word },
        { nameof(AH), Size.Byte },
        { nameof(AL), Size.Byte }
    };
}