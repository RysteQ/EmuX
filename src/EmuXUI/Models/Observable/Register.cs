using EmuXCore.Common.Enums;
using EmuXUI.Models.Events;
using EmuXUI.Models.Internal;
using System;

namespace EmuXUI.Models.Observable;

public sealed class Register : ObservableObject
{
    public Register(string name, Size size, ulong value)
    {
        Name = name;
        Size = size;
        Value = value;
    }

    public event EventHandler? ValueChanged;

    public string Name { get; init; }
    public Size Size { get; init; }
    
    public double Value
    {
        get => field;
        set
        {
            ValueChanged?.Invoke(this, new WrittenToRegisterEvent(Name, (ulong)field, (ulong)value));

            OnPropertyChanged(ref field, value);
        }
    }
    
    public double MaximumValue
    {
        get
        {
            return Size switch
            {
                Size.Byte => byte.MaxValue,
                Size.Word => ushort.MaxValue,
                Size.Dword => uint.MaxValue,
                Size.Qword => ulong.MaxValue,
                _ => 0
            };
        }
    }
}