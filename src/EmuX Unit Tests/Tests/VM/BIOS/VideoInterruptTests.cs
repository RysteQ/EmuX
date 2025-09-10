using EmuXCore.VM.Interfaces;
using EmuXCore.VM.Interfaces.Components.BIOS.Enums.SubInterrupts;
using EmuXCore.VM.Internal.CPU.Registers.MainRegisters;
using EmuXCore.VM.Internal.CPU.Registers.SubRegisters;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.BIOS;

[TestClass]
public sealed class VideoInterruptTests : TestWideInternalConstants
{
    [TestMethod]
    public void DrawPixel_1_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawPixel;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 69;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawPixel);

        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

        Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
        Assert.AreEqual<byte>(69, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
        Assert.AreEqual<byte>(0, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
    }

    [TestMethod]
    public void DrawPixel_2_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawPixel;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 10;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 11;
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 69;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 10;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawPixel);

        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

        Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
        Assert.AreEqual<byte>(69, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
        Assert.AreEqual<byte>(10, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
    }

    [TestMethod]
    public void DrawPixel_3_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawPixel;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 200;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 200;
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawPixel);

        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

        Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
        Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
        Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
    }

    [TestMethod]
    public void DrawPixel_4_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawPixel;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 20;
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 13;
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 16;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 16;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 16;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawPixel);

        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

        Assert.AreEqual<byte>(16, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
        Assert.AreEqual<byte>(16, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
        Assert.AreEqual<byte>(16, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
    }

    [TestMethod]
    public void DrawLine_1_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        (int X, int Y)[] coordinatesToCheck = [
            (0, 0),
            (1, 1),
            (2, 2),
            (3, 3),
            (4, 4),
            (5, 5),
            (6, 6),
            (7, 7),
            (8, 8),
            (9, 9),
        ];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawLine;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 0 + (10 << 16); // X: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0 + (10 << 16); // Y: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawLine);

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
        }
    }

    [TestMethod]
    public void DrawLine_2_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        (int X, int Y)[] coordinatesToCheck = [
            (10, 0),
            (9, 1),
            (8, 2),
            (7, 3),
            (6, 4),
            (5, 5),
            (4, 6),
            (3, 7),
            (2, 8),
            (1, 9),
            (0, 10),
        ];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawLine;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 0 + (10 << 16); // X: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 10 + (0 << 16); // Y: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawLine);

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
        }
    }

    [TestMethod]
    public void DrawLine_3_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        (int X, int Y)[] coordinatesToCheck = [
            (5, 0),
            (5, 1),
            (5, 2),
            (5, 3),
            (5, 4),
            (5, 5),
            (5, 6),
            (5, 7),
            (5, 8),
            (5, 9),
            (5, 9),
        ];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawLine;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 5 + (5 << 16); // X: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0 + (10 << 16); // Y: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawLine);

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
        }
    }

    [TestMethod]
    public void DrawLine_4_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        (int X, int Y)[] coordinatesToCheck = [
            (5, 5),
            (6, 5),
            (7, 5),
            (8, 5),
            (9, 5),
            (10, 5),
            (11, 5),
            (12, 5),
            (13, 5),
            (14, 5),
            (15, 5),
        ];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawLine;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 5 + (15 << 16); // X: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 5 + (5 << 16); // Y: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawLine);

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
        }
    }

    [TestMethod]
    public void DrawBox_1_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        (int X, int Y)[] coordinatesToCheck = [
            (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0), (7, 0), (8, 0), (9, 0), (10, 0),
            (10, 1), (10, 2), (10, 3), (10, 4), (10, 5), (10, 6), (10, 7), (10, 8), (10, 9), (10, 10),
            (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), (0, 8), (0, 9), (0, 10),
            (1, 10), (2, 10), (3, 10), (4, 10), (5, 10), (6, 10), (7, 10), (8, 10), (9, 10), (10, 10),
        ];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawBox;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 0 + (10 << 16); // X: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0 + (10 << 16); // Y: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawBox);

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
        }
    }

    [TestMethod]
    public void DrawBox_2_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        (int X, int Y)[] coordinatesToCheck = [
            (0, 0), (1, 0), (2, 0), (3, 0), (4, 0), (5, 0),
            (5, 1), (5, 2), (5, 3), (5, 4), (5, 5),
            (0, 1), (0, 2), (0, 3), (0, 4), (0, 5),
            (1, 5), (2, 5), (3, 5), (4, 5), (5, 5),
        ];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawBox;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 0 + (5 << 16); // X: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0 + (5 << 16); // Y: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawBox);

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
        }
    }

    [TestMethod]
    public void DrawBox_3_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        (int X, int Y)[] coordinatesToCheck = [
            (5, 0), (6, 0), (7, 0), (8, 0), (9, 0), (10, 0), (11, 0), (12, 0), (13, 0), (14, 0), (15, 0),
            (15, 1), (15, 2), (15, 3), (15, 4), (15, 5), (15, 6), (15, 7), (15, 8), (15, 9), (15, 10),
            (5, 1), (5, 2), (5, 3), (5, 4), (5, 5), (5, 6), (5, 7), (5, 8), (5, 9), (5, 10),
            (5, 10), (6, 10), (7, 10), (8, 10), (9, 10), (10, 10), (11, 10), (12, 10), (13, 10), (14, 10),
        ];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawBox;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 5 + (15 << 16); // X: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0 + (10 << 16); // Y: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawBox);

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
        }
    }

    [TestMethod]
    public void DrawBox_4_Success()
    {
        IVirtualMachine virtualMachine = GenerateVirtualMachine();
        (int X, int Y)[] coordinatesToCheck = [
            (5, 0), (6, 0), (7, 0), (8, 0), (9, 0), (10, 0),
            (10, 1), (10, 2), (10, 3), (10, 4), (10, 5),
            (5, 1), (5, 2), (5, 3), (5, 4), (5, 5),
            (5, 10), (6, 10), (7, 10), (8, 10), (9, 10), (10, 10),
        ];

        virtualMachine.CPU.GetRegister<VirtualRegisterRAX>().AH = (byte)VideoInterrupt.DrawBox;
        virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = 5 + (10 << 16); // X: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = 0 + (10 << 16); // Y: start + end
        virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 255;
        virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 255;
        virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.DrawBox);

        foreach ((int X, int Y) coordinate in coordinatesToCheck)
        {
            virtualMachine.CPU.GetRegister<VirtualRegisterRCX>().ECX = (uint)coordinate.X;
            virtualMachine.CPU.GetRegister<VirtualRegisterRDX>().EDX = (uint)coordinate.Y;
            virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS = 0;
            virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS = 0;
            virtualMachine.BIOS.HandleVideoInterrupt(VideoInterrupt.ReadPixel);

            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterCS>().CS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterSS>().SS);
            Assert.AreEqual<byte>(255, (byte)virtualMachine.CPU.GetRegister<VirtualRegisterDS>().DS);
        }
    }
}