using EmuXCore.VM.Enums;
using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using System.Drawing;

namespace EmuXCore.VM.Internal.GPUs;

public class VirtualGPU : IVirtualGPU
{
    public VirtualGPU(IVirtualMachine? parentVirtualMachine = null)
    {
        ParentVirtualMachine = parentVirtualMachine;
        _data = new byte[Height * Width * 3];

        Random.Shared.NextBytes(_data);
    }

    public void Execute()
    {
        VideoInterrupt shape;
        (ushort StartX, ushort EndX) xCoordinates = (0, 0);
        (ushort StartY, ushort EndY) yCoordinates = (0, 0);
        byte red = 0;
        byte green = 0;
        byte blue = 0;

        if (ParentVirtualMachine == null)
        {
            throw new ArgumentNullException($"Cannot execute the {nameof(Execute)} method with a null value for the property {nameof(ParentVirtualMachine)}");
        }

        shape = (VideoInterrupt)(ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRAX>().AH);
        xCoordinates = ((ushort)ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRCX>().ECX, (ushort)(ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRCX>().ECX >> 16));
        yCoordinates = ((ushort)ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRDX>().EDX, (ushort)(ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRDX>().EDX >> 16));
        red = (byte)((ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_00_ff_00_00) >> 16);
        green = (byte)((ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_00_00_ff_00) >> 8);
        blue = (byte)(ParentVirtualMachine!.CPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_00_00_00_ff);

        if (xCoordinates.StartX < 0 || xCoordinates.StartX >= Width || yCoordinates.StartY < 0 || yCoordinates.StartY >= Width || xCoordinates.EndX < 0 || xCoordinates.EndX >= Width || yCoordinates.EndY < 0 || yCoordinates.EndY >= Width)
        {
            throw new IndexOutOfRangeException($"The X and Y coordinates must be width X[0:{Width - 1}] and Y[0:{Height - 1}");
        }

        DrawShape(shape, xCoordinates.StartX, yCoordinates.StartY, xCoordinates.EndX, yCoordinates.EndY, red, green, blue);
    }

    public Color GetPixelColour(int x, int y)
    {
        if ((x < 0 || x >= Width) || (y < 0 || y >= Width))
        {
            throw new IndexOutOfRangeException($"The X and Y coordinates must be width X[0:{Width - 1}] and Y[0:{Height - 1}");
        }

        return Color.FromArgb(_data[(x + y * Width) * 3], _data[(x + y * Width) * 3 + 1], _data[(x + y * Width) * 3 + 2]);
    }

    private void DrawShape(VideoInterrupt shape, ushort startX, ushort startY, ushort endX, ushort endY, byte r, byte g, byte b)
    {
        switch (shape)
        {
            case VideoInterrupt.DrawPixel: DrawPixel(startX, startY, r, g, b); break;
            case VideoInterrupt.DrawLine: DrawLine(startX, startY, endX, endY, r, g, b); break;
            case VideoInterrupt.DrawBox: DrawBox(startX, startY, endX, endY, r, g, b); break;
        }

        ParentVirtualMachine?.InvokeAccessEvent((EventArgs)DIFactory.GenerateIVideoCardAccess(shape, startX, startY, endX, endY, r, g, b));
    }

    private void DrawPixel(ushort x, ushort y, byte r, byte g, byte b)
    {
        ParentVirtualMachine?.RegisterAction(
            VmActionCategory.ModifiedGpu,
            Common.Enums.Size.Byte,
            [_data[(x + y * Width) * 3], _data[(x + y * Width) * 3 + 1], _data[(x + y * Width) * 3 + 2]],
            [r, g, b],
            memoryPointer: (x + y * Width) * 3);

        _data[(x + y * Width) * 3] = r;
        _data[(x + y * Width) * 3 + 1] = g;
        _data[(x + y * Width) * 3 + 2] = b;
    }

    private void DrawLine(ushort startX, ushort startY, ushort endX, ushort endY, byte r, byte g, byte b)
    {
        int x = startX;
        int y = startY;
        int dx = Math.Abs(endX - startX);
        int dy = -Math.Abs(endY - startY);
        int sx = startX < endX ? 1 : -1;
        int sy = startY < endY ? 1 : -1;
        int error = dx + dy;

        while (true)
        {
            DrawPixel((ushort)x, (ushort)y, r, g, b);

            if (2 * error >= dy)
            {
                if (x == endX)
                {
                    break;
                }

                error += dy;
                x += sx;
            }
            else if (2 * error <= dx)
            {
                if (y == endY)
                {
                    break;
                }

                error += dx;
                y += sy;
            }
        }
    }

    private void DrawBox(ushort startX, ushort startY, ushort endX, ushort endY, byte r, byte g, byte b)
    {
        DrawLine(startX, startY, endX, startY, r, g, b);
        DrawLine(startX, startY, startX, endY, r, g, b);
        DrawLine(endX, startY, endX, endY, r, g, b);
        DrawLine(startX, endY, endX, endY, r, g, b);
    }

    public ushort Height { get => 640; }
    public ushort Width { get => 480; }
    public byte[] Data { get => _data; }
    public IVirtualMachine? ParentVirtualMachine { get; set; }

    private byte[] _data;
}