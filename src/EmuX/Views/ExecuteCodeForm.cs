using EmuX_Nano.Modules;
using EmuXCore;
using EmuXCore.Common.Enums;
using EmuXCore.Common.Interfaces;
using EmuXCore.InstructionLogic.Instructions.Internal;
using EmuXCore.Interpreter.Encoder.Interfaces.Logic;
using EmuXCore.Interpreter.Interfaces;
using EmuXCore.Interpreter.Models.Interfaces;
using EmuXCore.VM.Events;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Events;

namespace EmuX_Nano.Views;

public partial class ExecuteCodeForm : Form
{
    public ExecuteCodeForm(IVirtualMachine virtualMachine, IList<IInstruction> instructions, IList<ILabel> labels)
    {
        InitializeComponent();

        _instructionEncoder = DIFactory.GenerateIInstructionEncoder(virtualMachine, DIFactory.GenerateIOperandDecoder());
        _interpreter = DIFactory.GenerateIInterpreter();
        _interpreter.VirtualMachine = virtualMachine;
        _interpreter.Instructions = instructions;
        _interpreter.Labels = labels;

        InitUserInterface();
        RegisterVirtualMachineEvents();
        InitEncodedInstructionsInMemory();
        ComputeLabelMemoryLocations();
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

        if (_interpreter.Instructions.Any())
        {
            InitSelectedInstruction();
        }

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
        _bitmap = new(_interpreter.VirtualMachine.GPU.Width, _interpreter.VirtualMachine.GPU.Height);
        byte[] rgbDataBuffer = new byte[3];
        int x = 0;
        int y = 0;

        for (int i = 0; i < _interpreter.VirtualMachine.GPU.Data.Length; i += 3)
        {
            rgbDataBuffer[0] = _interpreter.VirtualMachine.GPU.Data[i + 0];
            rgbDataBuffer[1] = _interpreter.VirtualMachine.GPU.Data[i + 1];
            rgbDataBuffer[2] = _interpreter.VirtualMachine.GPU.Data[i + 2];

            _bitmap.SetPixel(x, y, Color.FromArgb(rgbDataBuffer[0], rgbDataBuffer[1], rgbDataBuffer[2]));

            x++;

            if (x == _bitmap.Width)
            {
                x = 0;
                y++;
            }
        }

        pictureBoxVideoOutput.Image = _bitmap;
        pictureBoxVideoOutput.Update();
    }

    private void UpdateVideoOutput(IVideoCardAccess videoCardAccess)
    {
        using (Graphics graphics = Graphics.FromImage(_bitmap))
        {
            switch (videoCardAccess.Shape)
            {
                case EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts.VideoInterrupt.DrawBox:
                    graphics.DrawRectangle(
                        new Pen(Color.FromArgb(videoCardAccess.Red, videoCardAccess.Green, videoCardAccess.Blue)),
                        videoCardAccess.StartX,
                        videoCardAccess.StartY,
                        videoCardAccess.EndX - videoCardAccess.StartX,
                        videoCardAccess.EndY - videoCardAccess.StartY);

                    break;

                case EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts.VideoInterrupt.DrawLine:
                    graphics.DrawLine(
                        new Pen(Color.FromArgb(videoCardAccess.Red, videoCardAccess.Green, videoCardAccess.Blue)),
                        new Point(videoCardAccess.StartX, videoCardAccess.StartY),
                        new Point(videoCardAccess.EndX, videoCardAccess.EndY));

                    break;

                case EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts.VideoInterrupt.DrawPixel:
                    _bitmap.SetPixel(
                        videoCardAccess.StartX, videoCardAccess.StartY,
                        Color.FromArgb(videoCardAccess.Red, videoCardAccess.Green, videoCardAccess.Blue));

                    break;
            }
        }
    }

    private void RegisterVirtualMachineEvents()
    {
        _interpreter.VirtualMachine.VideoCardAccessed += VirtualMachine_VideoCardAccessed;
    }

    private void InitEncodedInstructionsInMemory()
    {
        int offset = 0;

        _instructionEncoderResult = _instructionEncoder.Parse(_interpreter.Instructions);

        foreach (byte[] encodedInstruction in _instructionEncoderResult.Bytes)
        {
            foreach (byte instructionByte in encodedInstruction)
            {
                _interpreter.VirtualMachine.SetByte(offset, instructionByte);
                offset++;
            }
        }
    }

    private void ComputeLabelMemoryLocations()
    {
        int currentLine = 1;
        int offset = 0;

        foreach (ILabel label in _interpreter.Labels)
        {
            for (int i = currentLine; i < label.Line; i++)
            {
                offset += 5; // DUMMY VALUE
            }

            offset -= 5;

            _interpreter.VirtualMachine.Memory.LabelMemoryLocations.Add(label.Name, DIFactory.GenerateIMemoryLabel(label.Name, offset + 5, label.Line));

            currentLine = label.Line;
        }
    }

    private void VirtualMachine_VideoCardAccessed(object? sender, EventArgs e)
    {
        if (!_videoOutputInitialised)
        {
            _videoOutputInitialised = true;
            InitVideoOutput();

            return;
        }

        UpdateVideoOutput((IVideoCardAccess)e);

        pictureBoxVideoOutput.Image = _bitmap;
        pictureBoxVideoOutput.Update();
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
        try
        {
            while (_interpreter.CurrentInstructionIndex < listBoxInstructions.Items.Count - _interpreter.Labels.Count - 1)
            {
                _interpreter.ExecuteStep();
                listBoxInstructions.SelectedIndex = _interpreter.CurrentInstructionIndex;

                if (listBoxInstructions.SelectedItems[0].ToString().EndsWith(':'))
                {
                    listBoxInstructions.SelectedIndex++;
                }
            }

            _interpreter.ExecuteStep();
        }
        catch (Exception ex)
        {
            _errorsPopup = new([ex.Message]);
            _errorsPopup.Show();
        }
    }

    private void buttonStep_Click(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            _errorsPopup = new([ex.Message]);
            _errorsPopup.Show();
        }
    }

    private void buttonUndo_Click(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            _errorsPopup = new([ex.Message]);
            _errorsPopup.Show();
        }
    }

    private void buttonRedo_Click(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            _errorsPopup = new([ex.Message]);
            _errorsPopup.Show();
        }
    }

    private void buttonReset_Click(object sender, EventArgs e)
    {
        try
        {
            _interpreter.ResetExecution();
            _videoOutputInitialised = false;

            InitSelectedInstruction();

            ResetCodeExecution?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            _errorsPopup = new([ex.Message]);
            _errorsPopup.Show();
        }
    }

    private void listBoxInstructions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_changingIndex)
        {
            return;
        }

        _changingIndex = true;
        listBoxInstructions.SelectedIndex = _interpreter.CurrentInstructionIndex;
        _changingIndex = false;
    }

    public event EventHandler? ResetCodeExecution;

    private readonly IInterpreter _interpreter;
    private readonly IInstructionEncoder _instructionEncoder;
    private IInstructionEncoderResult _instructionEncoderResult;
    private bool _videoOutputInitialised = false;
    private Bitmap _bitmap;
    private ErrorsPopup _errorsPopup;
    private bool _changingIndex = false;
}
