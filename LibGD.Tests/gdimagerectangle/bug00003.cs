using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00003
{
    [Test]
    public void TestBug00003()
	{
        gdImageStruct im = gd.gdImageCreateTrueColor(100,100);
		gd.gdImageRectangle(im, 2,2, 80,95, 0x50FFFFFF);
		int c1 = gd.gdImageGetTrueColorPixel(im, 2, 2);
		int c2 = gd.gdImageGetTrueColorPixel(im, 80, 95);
		int c3 = gd.gdImageGetTrueColorPixel(im, 80, 2);
		int c4 = gd.gdImageGetTrueColorPixel(im, 2, 95);

		gd.gdImageDestroy(im);
        if (0x005e5e5e != c1 || 0x005e5e5e != c2 || 0x005e5e5e != c3 || 0x005e5e5e != c4)
        {
            Assert.Fail();
        }
	}

    [Test]
    public void TestBug00003Cpp()
    {
        using (var image = new Image(100, 100, true))
        {
            image.Rectangle(2, 2, 80, 95, 0x50FFFFFF);
            int c1 = image.GetTrueColorPixel(2, 2);
            int c2 = image.GetTrueColorPixel(80, 95);
            int c3 = image.GetTrueColorPixel(80, 2);
            int c4 = image.GetTrueColorPixel(2, 95);

            if (0x005e5e5e != c1 || 0x005e5e5e != c2 || 0x005e5e5e != c3 || 0x005e5e5e != c4)
            {
                Assert.Fail();
            }
        }
    }
}

