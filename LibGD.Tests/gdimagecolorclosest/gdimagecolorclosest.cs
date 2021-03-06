using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdimagecolorclosest
{
    [Test]
    public void TestGdImageColorClosest()
	{
		gdImageStruct im;
		int error = 0;
		int c;
		int i;

		im = gd.gdImageCreateTrueColor(5, 5);
		c = gd.gdImageColorClosest(im, 255, 0, 255);
		gd.gdImageDestroy(im);

        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c == 0xFF00FF) ? 1 : 0) != 1)
		{
			error = -1;
		}

		im = gd.gdImageCreate(5, 5);
		c = gd.gdImageColorClosest(im, 255, 0, 255);
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c == -1) ? 1 : 0) != 1)
		{
			error = -1;
		}
		gd.gdImageDestroy(im);

		im = gd.gdImageCreate(5, 5);
		gd.gdImageColorAllocate(im, 255, 0, 255);
		c = gd.gdImageColorClosest(im, 255, 0, 255);
		c = (((0) << 24) + ((((im).trueColor != 0 ? (((c) & 0xFF0000) >> 16) : (im).red[(c)])) << 16) + ((((im).trueColor != 0 ? (((c) & 0x00FF00) >> 8) : (im).green[(c)])) << 8) + (((im).trueColor != 0 ? ((c) & 0x0000FF) : (im).blue[(c)])));
		gd.gdImageDestroy(im);
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c == 0xFF00FF) ? 1 : 0) != 1)
		{
			error = -1;
		}


		im = gd.gdImageCreate(5, 5);
		for (i = 0; i < 255; i++)
		{
			c = gd.gdImageColorAllocate(im, 255, 0, 0);
		}
		c = gd.gdImageColorClosest(im, 255, 0, 0);
		c = (((0) << 24) + ((((im).trueColor != 0 ? (((c) & 0xFF0000) >> 16) : (im).red[(c)])) << 16) + ((((im).trueColor != 0 ? (((c) & 0x00FF00) >> 8) : (im).green[(c)])) << 8) + (((im).trueColor != 0 ? ((c) & 0x0000FF) : (im).blue[(c)])));
		gd.gdImageDestroy(im);
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c == 0xFF0000) ? 1 : 0) != 1)
		{
			error = -1;
		}

		im = gd.gdImageCreate(5, 5);
		for (i = 0; i < 256; i++)
		{
			c = gd.gdImageColorAllocate(im, 255, 0, 0);
		}
		c = gd.gdImageColorClosest(im, 255, 0, 0);
		c = (((0) << 24) + ((((im).trueColor != 0 ? (((c) & 0xFF0000) >> 16) : (im).red[(c)])) << 16) + ((((im).trueColor != 0 ? (((c) & 0x00FF00) >> 8) : (im).green[(c)])) << 8) + (((im).trueColor != 0 ? ((c) & 0x0000FF) : (im).blue[(c)])));
		gd.gdImageDestroy(im);
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c == 0xFF0000) ? 1 : 0) != 1)
		{
			error = -1;
		}

		Assert.AreEqual(0, error);
	}
    
    [Test]
    public void TestGdImageColorClosestCpp()
    {
        int error = 0;
        int c;
        using (var image = new Image(5, 5, true))
        {
            c = image.ColorClosest(255, 0, 255);
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c == 0xFF00FF ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        using (var image = new Image(5, 5))
        {
            c = image.ColorClosest(255, 0, 255);
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c == -1 ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        using (var image = new Image(5, 5))
        {
            image.ColorAllocate(255, 0, 255);
            c = image.ColorClosest(255, 0, 255);
            c = (0 << 24) + ((image.IsTrueColor() ? ((c & 0xFF0000) >> 16) : image.Red(c)) << 16) +
                ((image.IsTrueColor() ? (c & 0x00FF00) >> 8 : image.Green(c)) << 8) +
                (image.IsTrueColor() ? (c & 0x0000FF) : image.Blue(c));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c == 0xFF00FF ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        using (var image = new Image(5, 5))
        {
            for (int i = 0; i < 255; i++)
            {
                image.ColorAllocate(255, 0, 0);
            }
            c = image.ColorClosest(255, 0, 0);
            c = (0 << 24) + ((image.IsTrueColor() ? ((c & 0xFF0000) >> 16) : image.Red(c)) << 16) +
                ((image.IsTrueColor() ? ((c & 0x00FF00) >> 8) : image.Green(c)) << 8) +
                (image.IsTrueColor() ? ((c) & 0x0000FF) : image.Blue(c));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c == 0xFF0000 ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        using (var image = new Image(5, 5))
        {
            for (int i = 0; i < 256; i++)
            {
                image.ColorAllocate(255, 0, 0);
            }
            c = image.ColorClosest(255, 0, 0);
            c = (0 << 24) + ((image.IsTrueColor() ? ((c & 0xFF0000) >> 16) : image.Red(c)) << 16) +
                ((image.IsTrueColor() ? ((c & 0x00FF00) >> 8) : image.Green(c)) << 8) +
                (image.IsTrueColor() ? ((c) & 0x0000FF) : image.Blue(c));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c == 0xFF0000 ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        Assert.AreEqual(0, error);
    }
}

