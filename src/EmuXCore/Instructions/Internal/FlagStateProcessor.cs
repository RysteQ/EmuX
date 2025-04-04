using EmuXCore.Common.Enums;
using EmuXCore.Instructions.Interfaces;

namespace EmuXCore.Instructions.Internal;

public class FlagStateProcessor : IFlagStateProcessor
{
    public bool TestCarryFlag(ulong firstValue, ulong secondValue, Size size)
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

    public bool TestSignFlag(ulong firstValue, Size size)
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

    public bool TestOverflowFlag(ulong firstValue, ulong secondValue, ulong newValue, Size size)
    {
        int bitsToShift = 0;

        bitsToShift = size switch
        {
            Size.Byte => 7,
            Size.Word => 15,
            Size.Double => 31,
            Size.Quad => 63
        };

        return (firstValue >> bitsToShift == secondValue >> bitsToShift) && (firstValue >> bitsToShift != newValue >> bitsToShift);
    }

    public bool TestZeroFlag(ulong value)
    {
        return value == 0;
    }

    public bool TestAuxilliaryFlag(ulong firstValue, ulong secondValue)
    {
        return ((firstValue & 0x_00_00_00_00_00_00_00_0f) + (secondValue & 0x_00_00_00_00_00_00_00_0f)) > 15;
    }

    public bool TestParityFlag(ulong value)
    {
        return (value % 2) == 0;
    }
}