using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using System.Drawing;

namespace EmuXCore.VM.Internal.BIOS.InterruptHandlers;

public class VideoInterruptHandler : IVideoInterruptHandler
{
    public void GetResolution(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU)
    {
        virtualCPU.GetRegister<VirtualRegisterRAX>().AX = virtualGPU.Height;
        virtualCPU.GetRegister<VirtualRegisterRCX>().CX = virtualGPU.Width;
    }

    public void DrawBox(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU)
    {
        virtualGPU.Execute();
    }

    public void DrawLine(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU)
    {
        virtualGPU.Execute();
    }

    public void ReadPixel(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU)
    {
        Color pixelColour = virtualGPU.GetPixelColour(virtualCPU.GetRegister<VirtualRegisterRCX>().CX, virtualCPU.GetRegister<VirtualRegisterRDX>().DX);

        virtualCPU.GetRegister<VirtualRegisterRBX>().EBX = (uint)((virtualCPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_ff_00_ff_ff) + (pixelColour.R << 16));
        virtualCPU.GetRegister<VirtualRegisterRBX>().EBX = (uint)((virtualCPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_ff_ff_00_ff) + (pixelColour.G << 8));
        virtualCPU.GetRegister<VirtualRegisterRBX>().EBX = (uint)((virtualCPU.GetRegister<VirtualRegisterRBX>().EBX & 0x_ff_ff_ff_00) + (pixelColour.B));
    }

    public void DrawPixel(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU)
    {
        virtualGPU.Execute();
    }
}