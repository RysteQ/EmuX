using EmuXUI.Models.Internal;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace EmuXUI.Models.Observable;

public sealed class Bitmap : ObservableObject
{
    public Bitmap(int height, int width)
    {
        _image = new(width, height);
    }

    /// <summary>
    /// Writes a single byte into the image
    /// </summary>
    /// <param name="x">The X coordinate to write to</param>
    /// <param name="y">The Y coordinate to write to</param>
    /// <param name="colour">The colour data to write with the exception of the alpha channel</param>
    public async Task WritePixel(int x, int y, Color colour)
    {
        await _image.PixelBuffer.AsStream().WriteAsync([colour.B, colour.G, colour.R, 0], x + y * Image.PixelWidth * 4, 4);
    }

    /// <summary>
    /// Writes the entire image data to the bitmap
    /// </summary>
    /// <param name="imageData">The RGB data to write</param>
    public async Task WriteEntireImage(byte[] imageData)
    {
        List<byte> toWrite = imageData.ToList();
        int offset = 0;

        await Task.Run(async () =>
        {
            for (int i = 1; i * 3 + offset <= toWrite.Count; i++)
            {
                toWrite.Insert(i * 3 + offset, 0);
                offset++;
            }
        });

        await _image.PixelBuffer.AsStream().WriteAsync([.. toWrite], 0, toWrite.Count);
    }

    /// <summary>
    /// Invokes the update call to notify the UI to retrieve the new bitmap image
    /// </summary>
    public void Update()
    {
        OnPropertyChanged(nameof(Image));
    }

    public WriteableBitmap Image { get => _image; }
    public int Height { get => _image.PixelHeight; }
    public int Width { get => _image.PixelWidth; }

    private readonly WriteableBitmap _image;
}