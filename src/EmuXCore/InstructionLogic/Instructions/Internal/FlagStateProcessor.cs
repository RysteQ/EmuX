using EmuXCore.Common.Enums;
using EmuXCore.InstructionLogic.Instructions.Interfaces;

namespace EmuXCore.InstructionLogic.Instructions.Internal;

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
            Size.Byte => (firstValue + secondValue) > byte.MaxValue,
            Size.Word => (firstValue + secondValue) > ushort.MaxValue,
            Size.Dword => (firstValue + secondValue) > uint.MaxValue,
            Size.Qword => (ulong.MaxValue - largestValue) < firstValue,
        };
    }

    public bool TestSignFlag(ulong initialValue, Size size)
    {
        int bitsToShift = 0;

        bitsToShift = size switch
        {
            Size.Byte => 7,
            Size.Word => 15,
            Size.Dword => 31,
            Size.Qword => 63
        };

        return initialValue >> bitsToShift == 1;
    }

    public bool TestOverflowFlag(ulong initialValue, ulong difference, ulong newValue, Size size)
    {
        int bitsToShift = 0;

        bitsToShift = size switch
        {
            Size.Byte => 7,
            Size.Word => 15,
            Size.Dword => 31,
            Size.Qword => 63
        };

        return initialValue >> bitsToShift == difference >> bitsToShift && initialValue >> bitsToShift != newValue >> bitsToShift;
    }

    public bool TestZeroFlag(ulong value)
    {
        return value == 0;
    }

    public bool TestAuxilliaryFlag(ulong firstValue, ulong secondValue, bool addition)
    {
        if (addition)
        {
            return (firstValue & 0x_00_00_00_00_00_00_00_0f) + (secondValue & 0x_00_00_00_00_00_00_00_0f) > 15;
        }

        for (int i = 0; i < 4; i++)
        {
            if ((firstValue & (ulong)(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001 << i)) < (secondValue & (ulong)(0b_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0000_0001 << i)))
            {
                return true;
            }
        }

        return false;
    }

    public bool TestParityFlag(ulong value)
    {
        return value % 2 == 0;
    }
}