using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00078
{
    [Test]
    public void TestBug00078()
	{
        gdImageStruct im = gd.gdImageCreateTrueColor(100,100);
		gd.gdImageFilledRectangle(im, 2,2, 80,95, 0x50FFFFFF);
		int c1 = gd.gdImageGetTrueColorPixel(im, 2, 2);
		int c2 = gd.gdImageGetTrueColorPixel(im, 80, 95);
		int c3 = gd.gdImageGetTrueColorPixel(im, 80, 2);
		int c4 = gd.gdImageGetTrueColorPixel(im, 2, 95);
		int c5 = gd.gdImageGetTrueColorPixel(im, 49, 49);

		if (!(0x005e5e5e == c1 && 0x005e5e5e == c2 && 0x005e5e5e == c3 && 0x005e5e5e == c4))
		{
			Assert.Fail();
		}
		gd.gdImageFilledRectangle(im, 0, 0, 99, 99, 0x0);

		gd.gdImageFilledRectangle(im, 80,95, 2,2, 0x50FFFFFF);
		c1 = gd.gdImageGetTrueColorPixel(im, 2, 2);
		c2 = gd.gdImageGetTrueColorPixel(im, 80, 95);
		c3 = gd.gdImageGetTrueColorPixel(im, 80, 2);
		c4 = gd.gdImageGetTrueColorPixel(im, 2, 95);
		c5 = gd.gdImageGetTrueColorPixel(im, 49, 49);

		if (!(0x005e5e5e == c1 && 0x005e5e5e == c2 && 0x005e5e5e == c3 && 0x005e5e5e == c4 && 0x005e5e5e == c5))
		{
			Assert.Fail();
		}

		gd.gdImageDestroy(im);
	}

    [Test]
    public void TestBug00078Cpp()
    {
        using (var image = new Image(100, 100, true))
        {
            image.FilledRectangle(2, 2, 80, 95, 0x50FFFFFF);
            int c1 = image.GetTrueColorPixel(2, 2);
            int c2 = image.GetTrueColorPixel(80, 95);
            int c3 = image.GetTrueColorPixel(80, 2);
            int c4 = image.GetTrueColorPixel(2, 95);
            int c5 = image.GetTrueColorPixel(49, 49);

            if (!(0x005e5e5e == c1 && 0x005e5e5e == c2 && 0x005e5e5e == c3 && 0x005e5e5e == c4))
            {
                Assert.Fail();
            }
            image.FilledRectangle(0, 0, 99, 99, 0x0);

            image.FilledRectangle(80, 95, 2, 2, 0x50FFFFFF);
            c1 = image.GetTrueColorPixel(2, 2);
            c2 = image.GetTrueColorPixel(80, 95);
            c3 = image.GetTrueColorPixel(80, 2);
            c4 = image.GetTrueColorPixel(2, 95);
            c5 = image.GetTrueColorPixel(49, 49);

            if (!(0x005e5e5e == c1 && 0x005e5e5e == c2 && 0x005e5e5e == c3 && 0x005e5e5e == c4 && 0x005e5e5e == c5))
            {
                Assert.Fail();
            }
        }
    }
}

