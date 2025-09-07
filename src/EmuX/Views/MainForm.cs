using EmuX_Nano.Modules;
using EmuXCore;
using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.Device.USBDrives;

namespace EmuX;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        // Custom
        InitVirtualMachine();
    }

    private void InitVirtualMachine()
    {
        IVirtualMachineBuilder virtualMachineBuilder = DIFactory.GenerateIVirtualMachineBuilder();

        virtualMachineBuilder = virtualMachineBuilder
            .SetCpu(DIFactory.GenerateIVirtualCPU())
            .SetMemory(DIFactory.GenerateIVirtualMemory())
            .SetBios(DIFactory.GenerateIVirtualBIOS(DIFactory.GenerateIDiskInterruptHandler(), DIFactory.GenerateIRTCInterruptHandler(), DIFactory.GenerateIVideoInterruptHandler(), DIFactory.GenerateIDeviceInterruptHandler()))
            .SetRTC(DIFactory.GenerateIVirtualRTC())
            .AddDisk(DIFactory.GenerateIVirtualDisk(1, 16, 16, 255))
            .SetGPU(DIFactory.GenerateIVirtualGPU())
            .AddVirtualDevice(DIFactory.GenerateIVirtualDevice<UsbDrive64Kb>(1));

        _virtualMachine = virtualMachineBuilder.Build();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        InitRegistersView();
    }

    private void InitRegistersView()
    {
        ulong registerValue = 0;

        listBoxVirtualCpuRegisters.Items.Clear();

        foreach (IVirtualRegister virtualRegister in _virtualMachine.CPU.Registers)
        {
            foreach (KeyValuePair<string, EmuXCore.Common.Enums.Size> register in virtualRegister.RegisterNamesAndSizes)
            {
                registerValue = _virtualMachine.CPU.GetRegister(register.Key).Get();

                registerValue = register.Value switch
                {
                    EmuXCore.Common.Enums.Size.Byte => (byte)registerValue,
                    EmuXCore.Common.Enums.Size.Word => (ushort)registerValue,
                    EmuXCore.Common.Enums.Size.Dword => (uint)registerValue,
                    EmuXCore.Common.Enums.Size.Qword => registerValue,
                    _ => throw new ArgumentOutOfRangeException($"Invalid register size {register.Value} for register {register.Key}")
                };

                if (register.Key.EndsWith('L'))
                {
                    registerValue = _virtualMachine.CPU.GetRegister(register.Key).Get() & 0x000000000000ff00;
                    registerValue = registerValue >> 8;
                }

                listBoxVirtualCpuRegisters.Items.Add(string.Concat(register, " - ", registerValue));
            }
        }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Version: 2.0.0", "EmuX", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void listBoxVirtualCpuRegisters_DoubleClick(object sender, EventArgs e) => InvokeRegisterEdit();
    private void listBoxVirtualCpuRegisters_KeyDown(object sender, KeyEventArgs e) => InvokeRegisterEdit();

    private void InvokeRegisterEdit()
    {
        _popupInput = new();

        _popupInput.FormClosing += PopupInputNewRegisterValue_FormClosing;
        _popupInput.Question = "New value";
        
        _popupInput.Show();
    }

    private void PopupInputNewRegisterValue_FormClosing(object? sender, FormClosingEventArgs e)
    {
        string? newRegisterValue = _popupInput.UserInput;
        string selectedRegisterName = string.Empty;
        EmuXCore.Common.Enums.Size registerSize = EmuXCore.Common.Enums.Size.NaN;
        IVirtualRegister selectedRegister;

        if (string.IsNullOrEmpty(newRegisterValue) || listBoxVirtualCpuRegisters.SelectedItem == null)
        {
            return;
        }

        selectedRegisterName = listBoxVirtualCpuRegisters.SelectedItem.ToString()!;
        selectedRegisterName = selectedRegisterName.Trim().Split(' ').First()[1..^1];
        selectedRegister = _virtualMachine.CPU.GetRegister(selectedRegisterName);
        registerSize = selectedRegister.RegisterNamesAndSizes[selectedRegisterName];

        switch (registerSize)
        {
            case EmuXCore.Common.Enums.Size.Byte:
                if (newRegisterValue.EndsWith('H'))
                {
                    if (ushort.TryParse(newRegisterValue, out ushort newByteValueHigh))
                    {
                        selectedRegister.Set((selectedRegister.Get() & 0x_ff_ff_ff_ff_ff_ff_00_ff) + (ulong)(newByteValueHigh << 8));
                    }
                    else
                    {
                        MessageBox.Show($"Error converting value {newRegisterValue} to {nameof(EmuXCore.Common.Enums.Size.Byte)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (ushort.TryParse(newRegisterValue, out ushort newByteValueLow))
                    {
                        selectedRegister.Set((selectedRegister.Get() & 0x_ff_ff_ff_ff_ff_ff_ff_00) + newByteValueLow);
                    }
                    else
                    {
                        MessageBox.Show($"Error converting value {newRegisterValue} to {nameof(EmuXCore.Common.Enums.Size.Byte)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                break;

            case EmuXCore.Common.Enums.Size.Word:
                if (ushort.TryParse(newRegisterValue, out ushort newWordValue))
                {
                    selectedRegister.Set((selectedRegister.Get() & 0x_ff_ff_ff_ff_ff_ff_00_00) + newWordValue);
                }
                else
                {
                    MessageBox.Show($"Error converting value {newRegisterValue} to {nameof(EmuXCore.Common.Enums.Size.Word)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                break;

            case EmuXCore.Common.Enums.Size.Dword:
                if (uint.TryParse(newRegisterValue, out uint newDoubleValue))
                {
                    selectedRegister.Set((selectedRegister.Get() & 0x_ff_ff_ff_ff_00_00_00_00) + newDoubleValue);
                }
                else
                {
                    MessageBox.Show($"Error converting value {newRegisterValue} to {nameof(EmuXCore.Common.Enums.Size.Dword)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                break;

            case EmuXCore.Common.Enums.Size.Qword:
                if (uint.TryParse(newRegisterValue, out uint newQuadValue))
                {
                    selectedRegister.Set(newQuadValue);
                }
                else
                {
                    MessageBox.Show($"Error converting value {newRegisterValue} to {nameof(EmuXCore.Common.Enums.Size.Qword)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                break;

            default:
                MessageBox.Show($"Invalid register size {registerSize}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                break;
        }

        InitRegistersView();
    }

    private PopupInput _popupInput = new();
    private IVirtualMachine _virtualMachine;
}