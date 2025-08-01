﻿using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces.Components.Internal;

namespace EmuXCore.VM.Internal.CPU.Registers.SubRegisters;

public class VirtualRegisterCS : IVirtualRegister
{
    public VirtualRegisterCS()
    {
        CS = (ushort)Random.Shared.Next();
    }

    public ulong Get() => CS;
    public void Set(ulong value) => CS = (ushort)value;

    public ushort CS { get; set; }

    public string Name => "CS";

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(CS), Size.Word }
    };
}