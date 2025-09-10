﻿using EmuXCore.VM.Interfaces.Components;
using EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;
using System.Drawing;

namespace EmuXCore.VM.Internal.BIOS.Internal;

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

        virtualCPU.GetRegister<VirtualRegisterCS>().CS = pixelColour.R;
        virtualCPU.GetRegister<VirtualRegisterSS>().SS = pixelColour.G;
        virtualCPU.GetRegister<VirtualRegisterDS>().DS = pixelColour.B;
    }

    public void DrawPixel(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU)
    {
        virtualGPU.Execute();
    }
}