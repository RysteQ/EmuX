namespace EmuXCore.VM.Interfaces.Components.BIOS.Interfaces;

/// <summary>
/// The video interrupt handler is used to handle all the video sub-interrupt code function calls
/// </summary>
public interface IVideoInterruptHandler
{
    /// <summary>
    /// Get the resolution from the IVirtualGPU module and stores the height in the AX register and width in the CX register
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU module implementation to store the height and width to</param>
    /// <param name="virtualGPU">The IVIrtualGPU module implementation to get the height and width from</param>
    void GetResolution(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU);

    /// <summary>
    /// Draw a single pixel in the display buffer <br/>
    /// X: CX <br/>
    /// Y: DX <br/>
    /// Red: CS[0:7] <br/>
    /// Green: SS[0:7] <br/>
    /// Blue: DS[0:7] <br/>
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation module to get the parameters from</param>
    /// <param name="virtualGPU">The IVirtualGPU implementation module to draw to</param>
    void DrawPixel(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU);

    /// <summary>
    /// Reads a single pixel RGB values from the display buffer
    /// X: CX  <br/>
    /// Y: DX  <br/>
    /// Red: CS[0:7]  <br/>
    /// Green: SS[0:7] <br/>
    /// Blue: DS[0:7] <br/>
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation module to get the parameters from and save the RGB colour data to</param>
    /// <param name="virtualGPU">The IVirtualGPU implementation module get the pixel data from</param>
    void ReadPixel(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU);

    /// <summary>
    /// Draws a line in the display buffer
    /// StartX - EndX: ECX[0:15] - ECX[16:31] <br/>
    /// StartY - EndY: EDX[0:15] - EDX[16:31] <br/>
    /// Red: CS[0:7] <br/>
    /// Green: SS[0:7] <br/>
    /// Blue: DS[0:7] <br/>
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation module to get the parameters from</param>
    /// <param name="virtualGPU">The IVirtualGPU implementation module to draw to</param>
    void DrawLine(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU);

    /// <summary>
    /// Draws a box in the display buffer
    /// StartX - EndX: ECX[0:15] - ECX[16:31] <br/>
    /// StartY - EndY: EDX[0:15] - EDX[16:31] <br/>
    /// Red: CS[0:7] <br/>
    /// Green: SS[0:7] <br/>
    /// Blue: DS[0:7] <br/>
    /// </summary>
    /// <param name="virtualCPU">The IVirtualCPU implementation module to get the parameters from</param>
    /// <param name="virtualGPU">The IVirtualGPU implementation module to draw to</param>
    void DrawBox(IVirtualCPU virtualCPU, IVirtualGPU virtualGPU);
}