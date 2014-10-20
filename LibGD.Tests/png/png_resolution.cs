using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersPng_resolution
{
	[Test]
	public unsafe void TestPng_resolution()
	{
	    int size;

	    gdImageStruct im = gd.gdImageCreate(100, 100);
		gd.gdImageSetResolution(im, 72, 300);
		int red = gd.gdImageColorAllocate(im, 0xFF, 0x00, 0x00);
		gd.gdImageFilledRectangle(im, 0, 0, 99, 99, red);
		IntPtr data = gd.gdImagePngPtr(im, &size);
		gd.gdImageDestroy(im);

		im = gd.gdImageCreateFromPngPtr(size, data);
		gd.gdFree(data);

		if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", im.res_x == 72 ? 1 : 0) == 0 ||
			GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", im.res_y == 300 ? 1 : 0) == 0)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "failed image resolution X (%d != 72) or Y (%d != 300)\n", im.res_x, im.res_y);
			gd.gdImageDestroy(im);
			Assert.Fail();
		}

		gd.gdImageDestroy(im);
	}

    [Test]
    public unsafe void TestPng_resolutionCpp()
    {
        int size;

        IntPtr data;
        using (var image = new Image(100, 100))
        {
            image.SetResolution(72, 300);
            int red = image.ColorAllocate(0xFF, 0x00, 0x00);
            image.FilledRectangle(0, 0, 99, 99, red);
            data = image.Png(&size);
        }

        using (var image = new Image())
        {
            image.CreateFromPng(size, data);
            gd.gdFree(data);

            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", image.ResX() == 72 ? 1 : 0) == 0 ||
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", image.ResY() == 300 ? 1 : 0) == 0)
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "failed image resolution X (%d != 72) or Y (%d != 300)\n", image.ResX(), image.ResY());
                Assert.Fail();
            }
        }
    }
}

