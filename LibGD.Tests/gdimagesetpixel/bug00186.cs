using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00186
{
    [Test]
    public void TestBug00186()
	{
        int i;
		int r = 0;

		gdImageStruct im = gd.gdImageCreateTrueColor(100, 100);
		gdImageStruct tile = gd.gdImageCreate(10, 10);
		int red = gd.gdImageColorAllocate(tile, 0xFF, 0, 0);
		int green = gd.gdImageColorAllocate(tile, 0, 0xFF, 0);
		int blue = gd.gdImageColorAllocate(tile, 0, 0, 0xFF);
		int other = gd.gdImageColorAllocate(tile, 0, 0, 0x2);
		gd.gdImageFilledRectangle(tile, 0, 0, 2, 10, red);
		gd.gdImageFilledRectangle(tile, 3, 0, 4, 10, green);
		gd.gdImageFilledRectangle(tile, 5, 0, 7, 10, blue);
		gd.gdImageFilledRectangle(tile, 8, 0, 9, 10, other);
		gd.gdImageColorTransparent(tile, blue);
		gd.gdImageSetTile(im, tile);
		for (i = 0; i < 100; i++)
		{
			gd.gdImageSetPixel(im, i, i, GlobalMembersGdtest.DefineConstants.gdTiled);
		}
		if (((gd.gdImageGetPixel(im, 9, 9)) & 0x0000FF) != 0x2)
		{
			r = 1;
		}
		gd.gdImageDestroy(tile);
		gd.gdImageDestroy(im);
        if (r != 0)
        {
            Assert.Fail("Error: {0}", r);
        }
	}

    [Test]
    public void TestBug00186Cpp()
    {
        int i;
        int r = 0;

        using (var image = new Image(100, 100, true))
        {
            using (var tile = new Image(10, 10))
            {
                int red = tile.ColorAllocate(0xFF, 0, 0);
                int green = tile.ColorAllocate(0, 0xFF, 0);
                int blue = tile.ColorAllocate(0, 0, 0xFF);
                int other = tile.ColorAllocate(0, 0, 0x2);
                tile.FilledRectangle(0, 0, 2, 10, red);
                tile.FilledRectangle(3, 0, 4, 10, green);
                tile.FilledRectangle(5, 0, 7, 10, blue);
                tile.FilledRectangle(8, 0, 9, 10, other);
                tile.ColorTransparent(blue);
                image.SetTile(tile);
                for (i = 0; i < 100; i++)
                {
                    image.SetPixel(i, i, GlobalMembersGdtest.DefineConstants.gdTiled);
                }
                if ((image.GetPixel(9, 9) & 0x0000FF) != 0x2)
                {
                    r = 1;
                }
            }
        }
        if (r != 0)
        {
            Assert.Fail("Error: {0}", r);
        }
    }
}

