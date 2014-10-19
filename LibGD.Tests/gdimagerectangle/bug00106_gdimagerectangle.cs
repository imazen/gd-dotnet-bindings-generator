using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00106_gdimagerectangle
{
    [Test]
    public void TestBug00106_GdImageRectangle()
	{
        gdImageStruct im = gd.gdImageCreateTrueColor(10,10);

		if (im == null)
		{
			Assert.Fail();
		}

		gd.gdImageRectangle(im, 1,1, 1,1, 0x50FFFFFF);
		int c1 = gd.gdImageGetTrueColorPixel(im, 1, 1);
		int c2 = gd.gdImageGetTrueColorPixel(im, 2, 1);
		int c3 = gd.gdImageGetTrueColorPixel(im, 1, 2);
		int c4 = gd.gdImageGetTrueColorPixel(im, 2, 2);
		gd.gdImageDestroy(im);

        if (0x005e5e5e != c1 || 0x0 != c2 || 0x0 != c3 || 0x0 != c4)
        {
            Assert.Fail();
        }
	}

    [Test]
    public void TestBug00106_GdImageRectangleCpp()
    {
        using (var image = new Image(10, 10, true))
        {
            if (!image.good())
            {
                Assert.Fail();
            }

            image.Rectangle(1, 1, 1, 1, 0x50FFFFFF);
            int c1 = image.GetTrueColorPixel(1, 1);
            int c2 = image.GetTrueColorPixel(2, 1);
            int c3 = image.GetTrueColorPixel(1, 2);
            int c4 = image.GetTrueColorPixel(2, 2);

            if (0x005e5e5e != c1 || 0x0 != c2 || 0x0 != c3 || 0x0 != c4)
            {
                Assert.Fail();
            }
        }
    }
}

