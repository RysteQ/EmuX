using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;
using EmuXCore.VM.Internal.GPUs.Enums;
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
        Shape shape = (Shape)(ParentVirtualMachine?.CPU.GetRegister<VirtualRegisterRAX>().AL);
        (ushort StartX, ushort EndX) xCoordinates = ((ushort)ParentVirtualMachine?.CPU.GetRegister<VirtualRegisterRCX>().ECX, (ushort)(ParentVirtualMachine?.CPU.GetRegister<VirtualRegisterRCX>().ECX >> 16));
        (ushort StartY, ushort EndY) yCoordinates = ((ushort)ParentVirtualMachine?.CPU.GetRegister<VirtualRegisterRDX>().EDX, (ushort)(ParentVirtualMachine?.CPU.GetRegister<VirtualRegisterRDX>().EDX >> 16));
        byte red = (byte)ParentVirtualMachine?.CPU.GetRegister<VirtualRegisterCS>().CS;
        byte green = (byte)ParentVirtualMachine?.CPU.GetRegister<VirtualRegisterSS>().SS;
        byte blue = (byte)ParentVirtualMachine?.CPU.GetRegister<VirtualRegisterDS>().DS;

        DrawShape(shape, xCoordinates.StartX, yCoordinates.StartY, xCoordinates.EndX, yCoordinates.EndY, red, green, blue);
    }

    public Color GetPixelColour(int x, int y)
    {
        return Color.FromArgb(_data[(x + y * Width) * 3], _data[(x + y * Width) * 3 + 1], _data[(x + y * Width) * 3 + 2]);
    }

    private void DrawShape(Shape shape, ushort startX, ushort startY, ushort endX, ushort endY, byte r, byte g, byte b)
    {
        switch (shape)
        {
            case Shape.Pixel: DrawPixel(startX, startY, r, g, b); break;
            case Shape.Line: DrawLine(startX, startY, endX, endY, r, g, b); break;
            case Shape.Box: DrawBox(startX, startY, endX, endY, r, g, b); break;
        }
    }

    private void DrawPixel(ushort x, ushort y, byte r, byte g, byte b)
    {
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