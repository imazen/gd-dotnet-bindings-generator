using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdimagecolorresolve
{
    [Test]
    public void TestGdImageColorResolve()
	{
        int error = 0;

        gdImageStruct im = gd.gdImageCreateTrueColor(5, 5);
		int c = gd.gdImageColorResolve(im, 255, 0, 255);
		int c2 = gd.gdImageColorResolveAlpha(im, 255, 0, 255, 100);
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

		int c1 = gd.gdImageColorResolve(im, 255, 0, 255);
		c2 = gd.gdImageColorResolve(im, 255, 200, 0);
		int c3 = gd.gdImageColorResolveAlpha(im, 255, 0, 255, 100);
		int c4 = gd.gdImageColorResolveAlpha(im, 255, 34, 255, 100);

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
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (c4 == 3) ? 1 : 0) != 1)
		{
			error = -1;
		}

		int color = (((0) << 24) + ((((im).trueColor != 0 ? (((c1) & 0xFF0000) >> 16) : (im).red[(c1)])) << 16) + ((((im).trueColor != 0 ? (((c1) & 0x00FF00) >> 8) : (im).green[(c1)])) << 8) + (((im).trueColor != 0 ? ((c1) & 0x0000FF) : (im).blue[(c1)])));
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
		color = (((0) << 24) + ((((im).trueColor != 0 ? (((c4) & 0xFF0000) >> 16) : (im).red[(c4)])) << 16) + ((((im).trueColor != 0 ? (((c4) & 0x00FF00) >> 8) : (im).green[(c4)])) << 8) + (((im).trueColor != 0 ? ((c4) & 0x0000FF) : (im).blue[(c4)])));
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (color == 0xFF22FF) ? 1 : 0) != 1)
		{
			error = -1;
		}
		gd.gdImageDestroy(im);

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
	}

    [Test]
    public void TestGdImageColorResolveCpp()
    {
        int error = 0;

        using (var image = new Image(5, 5, true))
        {
            int c = image.ColorResolve(255, 0, 255);
            int c2 = image.ColorResolve(255, 0, 255, 100);

            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c == 0xFF00FF ? 1 : 0) != 1)
            {
                error = -1;
            }
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c2 == 0x64FF00FF ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        using (var image = new Image(5, 5))
        {
            int c1 = image.ColorResolve(255, 0, 255);
            int c2 = image.ColorResolve(255, 200, 0);
            int c3 = image.ColorResolve(255, 0, 255, 100);
            int c4 = image.ColorResolve(255, 34, 255, 100);

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
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", c4 == 3 ? 1 : 0) != 1)
            {
                error = -1;
            }

            int color = (((0) << 24) + ((((image).IsTrueColor() ? (((c1) & 0xFF0000) >> 16) : (image).Red(c1))) << 16) +
                ((((image).IsTrueColor() ? (((c1) & 0x00FF00) >> 8) : (image).Green(c1))) << 8) +
                (((image).IsTrueColor() ? ((c1) & 0x0000FF) : (image).Blue(c1))));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", color == 0xFF00FF ? 1 : 0) != 1)
            {
                error = -1;
            }
            color = (0 << 24) + ((image.IsTrueColor() ? ((c2 & 0xFF0000) >> 16) : image.Red(c2)) << 16) +
                    ((image.IsTrueColor() ? (c2 & 0x00FF00) >> 8 : image.Green(c2)) << 8) +
                    (image.IsTrueColor() ? c2 & 0x0000FF : (image).Blue(c2));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", color == 0xFFC800 ? 1 : 0) != 1)
            {
                error = -1;
            }
            color = (0 << 24) + ((image.IsTrueColor() ? (c3 & 0xFF0000) >> 16 : image.Red(c3)) << 16) +
                    ((image.IsTrueColor() ? (c3 & 0x00FF00) >> 8 : image.Green(c3)) << 8) +
                    (image.IsTrueColor() ? c3 & 0x0000FF : image.Blue(c3));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", color == 0xFF00FF ? 1 : 0) != 1)
            {
                error = -1;
            }
            color = ((0) << 24) + ((image.IsTrueColor() ? (c4 & 0xFF0000) >> 16 : image.Red(c4)) << 16) +
                    ((image.IsTrueColor() ? (c4 & 0x00FF00) >> 8 : image.Green(c4)) << 8) +
                    (image.IsTrueColor() ? c4 & 0x0000FF : image.Blue(c4));
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", color == 0xFF22FF ? 1 : 0) != 1)
            {
                error = -1;
            }
        }

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }

}

