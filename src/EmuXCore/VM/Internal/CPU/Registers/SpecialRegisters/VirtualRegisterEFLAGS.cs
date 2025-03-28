using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces;

namespace EmuXCore.VM.Internal.CPU.Registers.SpecialRegisters;

public class VirtualRegisterEFLAGS : IVirtualRegister
{
    public VirtualRegisterEFLAGS()
    {
        EFLAGS = 0x00000002;
    }

    public static bool TestCarryFlag(ulong firstValue, ulong secondValue, Size size)
    {
        ulong largestValue = firstValue;

        if (largestValue < secondValue)
        {
            largestValue = secondValue;
        }

        return size switch
        {
            Size.Byte => firstValue + secondValue > byte.MaxValue,
            Size.Word => firstValue + secondValue > ushort.MaxValue,
            Size.Double => firstValue + secondValue > uint.MaxValue,
            Size.Quad => ulong.MaxValue - largestValue < firstValue,
        };
    }

    public static bool TestSignFlag(ulong firstValue, Size size)
    {
        int bitsToShift = 0;

        bitsToShift = size switch
        {
            Size.Byte => 7,
            Size.Word => 15,
            Size.Double => 31,
            Size.Quad => 63
        };

        return firstValue >> bitsToShift == 1;
    }

    public static bool TestOverflowFlag(ulong firstValue, ulong secondValue, Size size)
    {
        int bitsToShift = 0;

        bitsToShift = size switch
        {
            Size.Byte => 7,
            Size.Word => 15,
            Size.Double => 31,
            Size.Quad => 63
        };

        return (firstValue >> bitsToShift == secondValue >> bitsToShift) && (~(firstValue - secondValue) >> bitsToShift) != firstValue >> bitsToShift;
    }

    public static bool TestZeroFlag(ulong value)
    {
        return value == 0;
    }

    public static bool TestAuxilliaryFlag(ulong firstValue, ulong secondValue)
    {
        return ((firstValue & 0x_00_00_00_00_00_00_00_0f) + (secondValue & 0x_00_00_00_00_00_00_00_0f)) > 15;
    }

    public static bool TestParityFlag(ulong value)
    {
        return (value % 2) == 0;
    }

    public ulong Get() => EFLAGS;
    public void Set(ulong value) => EFLAGS = (uint)value;

    public uint EFLAGS { get; set; }

    public ushort FLAGS
    {
        get => (ushort)(EFLAGS & 0x0000ffff);
        set => EFLAGS = (EFLAGS & 0xffff0000) + value;
    }

    public Dictionary<string, Size> RegisterNamesAndSizes => new()
    {
        { nameof(EFLAGS), Size.Quad },
        { nameof(FLAGS), Size.Double }
    };
}