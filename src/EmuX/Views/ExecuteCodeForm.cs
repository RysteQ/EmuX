using EmuXCore;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Interfaces;

namespace EmuX_Nano.Views;

public partial class ExecuteCodeForm : Form
{
    public ExecuteCodeForm(IVirtualMachine virtualMachine, IList<IInstruction> instructions, IList<ILabel> labels)
    {
        InitializeComponent();

        _interpreter = DIFactory.GenerateIInterpreter();
        _interpreter.VirtualMachine = virtualMachine;
        _interpreter.Instructions = instructions;
        _interpreter.Labels = labels;

        InitUserInterface();
        RegisterVirtualMachineEvents();
    }

    private void InitUserInterface()
    {
        foreach (IInstruction instruction in _interpreter.Instructions)
        {
            listBoxInstructions.Items.Add(ConvertInstructiongToString(instruction));
        }

        foreach (ILabel label in _interpreter.Labels)
        {
            listBoxInstructions.Items.Insert(label.Line - 1, $"{label.Name}:");
        }

        if (listBoxInstructions.Items.Count == 0)
        {
            return;
        }

        InitSelectedInstruction();
        InitVideoOutput();
    }

    private void InitSelectedInstruction()
    {
        listBoxInstructions.SelectedIndex = 0;

        for (int i = 0; i < listBoxInstructions.Items.Count; i++)
        {
            if (listBoxInstructions.Items[i].ToString().EndsWith(':'))
            {
                listBoxInstructions.SelectedIndex++;
            }
            else
            {
                break;
            }
        }
    }

    private void InitVideoOutput()
    {
        Bitmap bitmap = new Bitmap(_interpreter.VirtualMachine.GPU.Width, _interpreter.VirtualMachine.GPU.Height);
        byte[] rgbDataBuffer = new byte[3];
        int x = 0;
        int y = 0;

        for (int i = 0; i < _interpreter.VirtualMachine.GPU.Data.Length; i += 3)
        {
            rgbDataBuffer[0] = _interpreter.VirtualMachine.GPU.Data[i + 0];
            rgbDataBuffer[1] = _interpreter.VirtualMachine.GPU.Data[i + 1];
            rgbDataBuffer[2] = _interpreter.VirtualMachine.GPU.Data[i + 2];

            bitmap.SetPixel(x, y, Color.FromArgb(rgbDataBuffer[0], rgbDataBuffer[1], rgbDataBuffer[2]));

            x++;

            if (x == bitmap.Width)
            {
                x = 0;
                y++;
            }
        }

        pictureBoxVideoOutput.Image = bitmap;
    }

    private void RegisterVirtualMachineEvents()
    {
        _interpreter.VirtualMachine.VideoCardAccessed += VirtualMachine_VideoCardAccessed;
    }

    private void VirtualMachine_VideoCardAccessed(object? sender, EventArgs e)
    {
        InitVideoOutput();
    }

    private string ConvertInstructiongToString(IInstruction instruction)
    {
        if (instruction.Variant == InstructionVariant.NoOperands())
        {
            return instruction.Opcode;
        }

        if (instruction.FirstOperand != null && instruction.SecondOperand == null && instruction.ThirdOperand == null)
        {
            return $"{instruction.Opcode} {instruction.FirstOperand?.FullOperand}";
        }

        if (instruction.FirstOperand != null && instruction.SecondOperand != null && instruction.ThirdOperand == null)
        {
            return $"{instruction.Opcode} {instruction.FirstOperand?.FullOperand}, {instruction.SecondOperand?.FullOperand}";
        }

        return $"{instruction.Opcode} {instruction.FirstOperand?.FullOperand}, {instruction.SecondOperand?.FullOperand}, {instruction.ThirdOperand?.FullOperand}";
    }

    private void buttonExecute_Click(object sender, EventArgs e)
    {
        while (_interpreter.CurrentInstructionIndex < listBoxInstructions.Items.Count - 1)
        {
            _interpreter.ExecuteStep();
            listBoxInstructions.SelectedIndex = _interpreter.CurrentInstructionIndex;
        }

        _interpreter.ExecuteStep();
    }

    private void buttonStep_Click(object sender, EventArgs e)
    {
        _interpreter.ExecuteStep();

        if (_interpreter.CurrentInstructionIndex < listBoxInstructions.Items.Count)
        {
            listBoxInstructions.SelectedIndex = _interpreter.CurrentInstructionIndex;

            if (listBoxInstructions.SelectedItems[0].ToString().EndsWith(':'))
            {
                listBoxInstructions.SelectedIndex--;
            }
        }
    }

    private void buttonUndo_Click(object sender, EventArgs e)
    {
        _interpreter.UndoAction();

        listBoxInstructions.SelectedIndex--;

        if (listBoxInstructions.SelectedIndex == -1)
        {
            listBoxInstructions.SelectedIndex++;

            return;
        }

        if (listBoxInstructions.SelectedIndex != -1)
        {
            if (listBoxInstructions.SelectedItems[0].ToString().EndsWith(':'))
            {
                listBoxInstructions.SelectedIndex--;
            }
        }
    }

    private void buttonRedo_Click(object sender, EventArgs e)
    {
        _interpreter.RedoAction();

        if (_interpreter.CurrentInstructionIndex < listBoxInstructions.Items.Count)
        {
            listBoxInstructions.SelectedIndex = _interpreter.CurrentInstructionIndex;

            if (listBoxInstructions.SelectedItems[0].ToString().EndsWith(':'))
            {
                listBoxInstructions.SelectedIndex++;
            }
        }
    }

    private void buttonReset_Click(object sender, EventArgs e)
    {
        _interpreter.ResetExecution();

        InitSelectedInstruction();

        ResetCodeExecution?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? ResetCodeExecution;

    private readonly IInterpreter _interpreter;
}
