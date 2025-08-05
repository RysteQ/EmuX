using System.Drawing;

namespace EmuXCore.VM.Interfaces.Components;

/// <summary>
/// The IVirtualGPU is meant to emulate a real GPU
/// </summary>
public interface IVirtualGPU : IVirtualComponent
{
    /// <summary>
    /// Executes the logic of the GPU and calls the inner DrawShape method. The parameters of the method are passed via IVirtualCPU registers in the IVirtualMachine, these are: <br/>
    /// Shape: AH (Enum: VideoInterrupt) <br/>
    /// StartX - EndX: ECX[0:15] - ECX[16:31] <br/>
    /// StartY - EndY: EDX[0:15] - EDX[16:31] <br/>
    /// Red: CS[0:7] <br/>
    /// Green: SS[0:7] <br/>
    /// Blue: DS[0:7] <br/>
    /// </summary>
    /// <exception cref="IndexOutOfRangeException">Thrown if the coordinates are out of range / invalid</exception>
    void Execute();

    /// <summary>
    /// Gets the pixel colour specified as a strictly RGB only value.
    /// </summary>
    /// <param name="x">The pixel to retrive the colour from in the X coordinate</param>
    /// <param name="y">The pixel to retrive the colour from in the Y coordinate</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if the coordinates are out of range / invalid</exception>
    /// <returns>The RGB value of the x:y pixel</returns>
    Color GetPixelColour(int x, int y);

    public ushort Height { get; }
    public ushort Width { get; }

    /// <summary>
    /// Used to access the internal buffer of the GPU, anything that writes inside that buffer should be done via the Execute() method
    /// </summary>
    public byte[] Data { get; }
}