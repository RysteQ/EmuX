using EmuX_Nano.Modules;
using EmuXCore;
using EmuXCore.Common.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.Events;
using EmuXCore.VM.Interfaces.Components.Internal;
using EmuXCore.VM.Internal.Device.USBDrives;
using System.Text;
using System.Xml;

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
        _virtualMachine.MemoryAccessed += _virtualMachine_MemoryAccessed;
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

    private void buttonSearchMemory_Click(object sender, EventArgs e)
    {
        int lastRowCellsAmount = 0;
        List<byte> lastRow = [];

        _memorySearchEnd = Convert.ToInt32(numericUpDownMemoryRangeEnd.Value + 1);
        _memorySearchStart = Convert.ToInt32(numericUpDownMemoryRangeStart.Value);
        _virtualMachine.MemoryAccessed -= _virtualMachine_MemoryAccessed;
        lastRowCellsAmount = (_memorySearchEnd - _memorySearchStart) % 8;

        dataGridMemoryView.Rows.Clear();

        for (int i = 0; i < (_memorySearchEnd - _memorySearchStart) / 8; i++)
        {
            dataGridMemoryView.Rows.Add(
            [
                _virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeStart.Value) + 0 + i * 8),
                _virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeStart.Value) + 1 + i * 8),
                _virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeStart.Value) + 2 + i * 8),
                _virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeStart.Value) + 3 + i * 8),
                _virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeStart.Value) + 4 + i * 8),
                _virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeStart.Value) + 5 + i * 8),
                _virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeStart.Value) + 6 + i * 8),
                _virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeStart.Value) + 7 + i * 8),
            ]);
        }

        dataGridMemoryView.Rows.Add();

        for (int i = lastRowCellsAmount - 1; i >= 0; i--)
        {
            lastRow.Add(_virtualMachine.GetByte(Convert.ToInt32(numericUpDownMemoryRangeEnd.Value) - i));
        }

        for (int i = 0; i < lastRow.Count; i++)
        {
            dataGridMemoryView.Rows[dataGridMemoryView.Rows.Count - 1].Cells[i].Value = lastRow[i];
        }

        _virtualMachine.MemoryAccessed += _virtualMachine_MemoryAccessed;
    }

    private void dataGridMemoryView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (_memorySearchEnd <= _memorySearchStart + e.ColumnIndex + 8 * e.RowIndex)
        {
            dataGridMemoryView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;

            return;
        }

        _virtualMachine.SetByte(_memorySearchStart + e.ColumnIndex + 8 * e.RowIndex, (byte)Convert.ToInt16(dataGridMemoryView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
    }

    private void _virtualMachine_MemoryAccessed(object? sender, EventArgs e)
    {
        IMemoryAccess memoryAccessEvent = (IMemoryAccess)e;
        int row = (memoryAccessEvent.MemoryAddress - _memorySearchStart) / 8;
        int column = (memoryAccessEvent.MemoryAddress - _memorySearchStart) % 8;

        if (!checkBoxHighlightAccessedMemory.Checked)
        {
            return;
        }

        switch (memoryAccessEvent.Size)
        {
            case EmuXCore.Common.Enums.Size.Byte:
                dataGridMemoryView.Rows[row].Cells[column].Style = new()
                {
                    BackColor = Color.Red
                };

                break;

            case EmuXCore.Common.Enums.Size.Word:
                for (int i = 0; i < 2; i++)
                {
                    dataGridMemoryView.Rows[row].Cells[column].Style = new()
                    {
                        BackColor = Color.Red
                    };

                    column++;

                    if (column == 8)
                    {
                        row++;
                        column = 0;
                    }
                }

                break;

            case EmuXCore.Common.Enums.Size.Dword:
                for (int i = 0; i < 4; i++)
                {
                    dataGridMemoryView.Rows[row].Cells[column].Style = new()
                    {
                        BackColor = Color.Red
                    };

                    column++;

                    if (column == 8)
                    {
                        row++;
                        column = 0;
                    }
                }

                break;

            case EmuXCore.Common.Enums.Size.Qword:
                for (int i = 0; i < 8; i++)
                {
                    dataGridMemoryView.Rows[row].Cells[column].Style = new()
                    {
                        BackColor = Color.Red
                    };

                    column++;

                    if (column == 8)
                    {
                        row++;
                        column = 0;
                    }
                }

                break;

            default:
                MessageBox.Show("Unsopported size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                break;
        }
    }

    private IVirtualMachine _virtualMachine;

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            FileName = string.Empty,
            Filter = "Assembly files (*.asm)|*.asm|Text files (*.txt)|*.txt|All files (*.*)|*.*",
            InitialDirectory = "C:\\",
            RestoreDirectory = true
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            _filePath = openFileDialog.FileName;
            richTextboxAssemblyCode.Text = new StreamReader(openFileDialog.OpenFile()).ReadToEnd();
        }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_filePath))
        {
            saveAsToolStripMenuItem_Click(sender, e);

            return;
        }

        File.WriteAllText(_filePath, richTextboxAssemblyCode.Text);
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            FileName = string.Empty,
            Filter = "Assembly files (*.asm)|*.asm|Text files (*.txt)|*.txt|All files (*.*)|*.*",
            InitialDirectory = "C:\\",
            RestoreDirectory = true
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            _filePath = saveFileDialog.FileName;
            File.WriteAllText(_filePath, richTextboxAssemblyCode.Text);
        }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void increaseFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Font newFont = new(richTextboxAssemblyCode.Font.FontFamily, richTextboxAssemblyCode.Font.Size + 1);

        richTextboxAssemblyCode.Font = newFont;
    }

    private void decreaseFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Font newFont = new(richTextboxAssemblyCode.Font.FontFamily, richTextboxAssemblyCode.Font.Size - 1);

        richTextboxAssemblyCode.Font = newFont;
    }

    private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FontDialog fontDialog = new();

        if (fontDialog.ShowDialog() == DialogResult.OK)
        {
            richTextboxAssemblyCode.Font = fontDialog.Font;
        }
    }

    private void executeToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // TODO - Finish the IInterpreter interface and implementation
    }

    private void stepByStepToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // TODO - Finish the IInterpreter interface and implementation
    }

    private void executionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // TODO - Finish the IInterpreter interface and implementation
    }

    private PopupInput _popupInput = new();
    private int _memorySearchStart = 0;
    private int _memorySearchEnd = 0;
    private string _filePath = string.Empty;
}