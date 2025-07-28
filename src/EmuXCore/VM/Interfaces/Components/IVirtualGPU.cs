using System.Drawing;

namespace EmuXCore.VM.Interfaces.Components;

public interface IVirtualGPU : IVirtualComponent
{
    /// <summary>
    /// Executes the logic of the GPU and calls the inner DrawShape method. The parameters of the method are passed via IVirtualCPU registers in the IVirtualMachine, these are:
    /// Shape: AL
    /// StartX - EndX: ECX[0:15] - ECX[16:31] 
    /// StartY - EndY: EDX[0:15] - EDX[16:31]
    /// Red: CS[0:7]
    /// Green: SS[0:7]
    /// Blue: DS[0:7]
    /// </summary>
    void Execute();

    /// <summary>
    /// Gets the pixel colour specified as a strictly RGB only value.
    /// </summary>
    /// <param name="x">The pixel to retrive the colour from in the X coordinate</param>
    /// <param name="y">The pixel to retrive the colour from in the Y coordinate</param>
    /// <returns>The RGB value of the x:y pixel</returns>
    Color GetPixelColour(int x, int y);

    public ushort Height { get; }
    public ushort Width { get; }

    /// <summary>
    /// Used to access the internal buffer of the GPU, anything that writes inside that buffer should be done via the Execute() method
    /// </summary>
    public byte[] Data { get; }
}