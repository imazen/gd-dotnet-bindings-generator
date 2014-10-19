using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdimagecolorexact
{
    [Test]
    public void TestGdImageColorExact()
	{
		gdImageStruct im;
		int error = 0;
		int c;
		int c1;
		int c2;
		int c3;
		int c4;
		int color;

		im = gd.gdImageCreateTrueColor(5, 5);
		c = gd.gdImageColorExact(im, 255, 0, 255);
		c2 = gd.gdImageColorExactAlpha(im, 255, 0, 255, 100);
		gd.gdImageDestroy(im);

        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c == 0xFF00FF) ? 1 : 0) != 1)
		{
			error = -1;
		}
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c2 == 0x64FF00FF) ? 1 : 0) != 1)
		{
			error = -1;
		}

		im = gd.gdImageCreate(5, 5);
		c1 = gd.gdImageColorAllocate(im, 255, 0, 255);
		c2 = gd.gdImageColorAllocate(im, 255, 200, 0);
		c3 = gd.gdImageColorAllocateAlpha(im, 255, 0, 255, 100);

		c1 = gd.gdImageColorExact(im, 255, 0, 255);
		c2 = gd.gdImageColorExact(im, 255, 200, 0);
		c3 = gd.gdImageColorExactAlpha(im, 255, 0, 255, 100);
		c4 = gd.gdImageColorExactAlpha(im, 255, 34, 255, 100);

        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c1 == 0) ? 1 : 0) != 1)
		{
			error = -1;
		}
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c2 == 1) ? 1 : 0) != 1)
		{
			error = -1;
		}
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c3 == 2) ? 1 : 0) != 1)
		{
			error = -1;
		}
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c4 == -1) ? 1 : 0) != 1)
		{
			error = -1;
		}

		color = (((0) << 24) + ((((im).trueColor != 0 ? (((c1) & 0xFF0000) >> 16) : (im).red[(c1)])) << 16) + ((((im).trueColor != 0 ? (((c1) & 0x00FF00) >> 8) : (im).green[(c1)])) << 8) + (((im).trueColor != 0 ? ((c1) & 0x0000FF) : (im).blue[(c1)])));
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (color == 0xFF00FF) ? 1 : 0) != 1)
		{
			error = -1;
		}
		color = (((0) << 24) + ((((im).trueColor != 0 ? (((c2) & 0xFF0000) >> 16) : (im).red[(c2)])) << 16) + ((((im).trueColor != 0 ? (((c2) & 0x00FF00) >> 8) : (im).green[(c2)])) << 8) + (((im).trueColor != 0 ? ((c2) & 0x0000FF) : (im).blue[(c2)])));
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (color == 0xFFC800) ? 1 : 0) != 1)
		{
			error = -1;
		}
		color = (((0) << 24) + ((((im).trueColor != 0 ? (((c3) & 0xFF0000) >> 16) : (im).red[(c3)])) << 16) + ((((im).trueColor != 0 ? (((c3) & 0x00FF00) >> 8) : (im).green[(c3)])) << 8) + (((im).trueColor != 0 ? ((c3) & 0x0000FF) : (im).blue[(c3)])));
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (color == 0xFF00FF) ? 1 : 0) != 1)
		{
			error = -1;
		}
		gd.gdImageDestroy(im);

		Assert.AreEqual(0, error);
	}

    [Test]
    public void TestGdImageColorExactCpp()
    {
        int error = 0;
        using (var image = new Image(5, 5, true))
        {
            int c = image.ColorExact(255, 0, 255);
            int c2 = image.ColorExact(255, 0, 255, 100);

            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c == 0xFF00FF) ? 1 : 0) != 1)
            {
                error = -1;
            }
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c2 == 0x64FF00FF) ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        using (var image = new Image(5, 5))
        {
            image.ColorAllocate(255, 0, 255);
            image.ColorAllocate(255, 200, 0);
            image.ColorAllocate(255, 0, 255, 100);

            int c1 = image.ColorExact(255, 0, 255);
            int c2 = image.ColorExact(255, 200, 0);
            int c3 = image.ColorExact(255, 0, 255, 100);
            int c4 = image.ColorExact(255, 34, 255, 100);

            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c1 == 0 ? 1 : 0) != 1)
            {
                error = -1;
            }
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c2 == 1 ? 1 : 0) != 1)
            {
                error = -1;
            }
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c3 == 2 ? 1 : 0) != 1)
            {
                error = -1;
            }
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c4 == -1 ? 1 : 0) != 1)
            {
                error = -1;
            }

            int color = (0 << 24) + ((image.IsTrueColor() ? (c1 & 0xFF0000) >> 16 : image.Red(c1)) << 16) +
                        ((image.IsTrueColor() ? (c1 & 0x00FF00) >> 8 : image.Green(c1)) << 8) +
                        (image.IsTrueColor() ? c1 & 0x0000FF : image.Blue(c1));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", color == 0xFF00FF ? 1 : 0) != 1)
            {
                error = -1;
            }
            color = (0 << 24) + ((image.IsTrueColor() ? (c2 & 0xFF0000) >> 16 : image.Red(c2)) << 16) +
                    ((image.IsTrueColor() ? (c2 & 0x00FF00) >> 8 : image.Green(c2)) << 8) +
                    (image.IsTrueColor() ? (c2 & 0x0000FF) : image.Blue(c2));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", color == 0xFFC800 ? 1 : 0) != 1)
            {
                error = -1;
            }
            color = (0 << 24) + ((image.IsTrueColor() ? (c3 & 0xFF0000) >> 16 : image.Red(c3)) << 16) +
                    ((image.IsTrueColor() ? (c3 & 0x00FF00) >> 8 : image.Green(c3)) << 8) +
                    (image.IsTrueColor() ? (c3 & 0x0000FF) : image.Blue(c3));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", color == 0xFF00FF ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        Assert.AreEqual(0, error);
    }
}

