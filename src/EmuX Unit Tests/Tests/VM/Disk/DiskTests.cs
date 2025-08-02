using EmuXCore.VM.Interfaces.Components;
using EmuXCoreUnitTests.Tests.Common;

namespace EmuXCoreUnitTests.Tests.VM.Disk;

[TestClass]
public sealed class DiskTests : TestWideInternalConstants
{
    [TestMethod]
    public void TestReadWriteRandomSector_Success()
    {
        IVirtualDisk virtualDisk = GenerateVirtualDisk(1, 4, 64, 255);
        byte[] randomBytesBuffer = new byte[virtualDisk.BytesPerSector];
        byte randomPlatter = (byte)Random.Shared.Next(4);
        ushort randomTrack = (ushort)Random.Shared.Next(64);
        byte randomSector = (byte)Random.Shared.Next(255);

        Random.Shared.NextBytes(randomBytesBuffer);

        try
        {
            virtualDisk.WriteSector(randomPlatter, randomTrack, randomSector, randomBytesBuffer);

            CollectionAssert.AreEqual(randomBytesBuffer, virtualDisk.ReadSector(randomPlatter, randomTrack, randomSector));
        }
        catch (IndexOutOfRangeException ex)
        {
            Assert.IsTrue(true);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }


    [TestMethod]
    public void TestWriteOutsideOfBounds_Success()
    {
        IVirtualDisk virtualDisk = GenerateVirtualDisk(1);
        byte[] randomBytesBuffer = new byte[virtualDisk.BytesPerSector];
        byte randomPlatter = byte.MaxValue;
        ushort randomTrack = byte.MaxValue;
        byte randomSector = byte.MaxValue;

        Random.Shared.NextBytes(randomBytesBuffer);

        try
        {
            virtualDisk.WriteSector(randomPlatter, randomTrack, randomSector, randomBytesBuffer);

            CollectionAssert.AreEqual(randomBytesBuffer, virtualDisk.ReadSector(randomPlatter, randomTrack, randomSector));
        }
        catch (IndexOutOfRangeException ex)
        {
            Assert.IsTrue(true);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}