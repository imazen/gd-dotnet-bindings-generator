using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdTrivialResize
{
    [Test]
    public void TestGdTrivialResize()
    {

        do_test(300, 300, 600, 600);
        do_test(3200, 2133, 640, 427);

        Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
    }

    [Test]
    public void TestGdTrivialResizeCpp()
    {
        do_testCpp(300, 300, 600, 600);
        do_testCpp(3200, 2133, 640, 427);

        Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
    }

    private static gdImageStruct mkwhite(int x, int y)
	{
	    gdImageStruct im = gd.gdImageCreateTrueColor(x, y);
		gd.gdImageFilledRectangle(im, 0, 0, x - 1, y - 1, gd.gdImageColorExactAlpha(im, 255, 255, 255, 0));

		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (im != null) ? 1 : 0);

		gd.gdImageSetInterpolationMethod(im, gdInterpolationMethod.GD_BICUBIC); // FP interp'n

		return im;
    } // mkwhite

    private static Image mkwhiteCpp(int x, int y)
    {
        var image = new Image(x, y, true);
        image.FilledRectangle(0, 0, x - 1, y - 1, image.ColorExact(255, 255, 255, 0));

        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", image.good() ? 1 : 0);

        image.SetInterpolationMethod(gdInterpolationMethod.GD_BICUBIC); // FP interp'n

        return image;
    } // mkwhite


	/* Fill with almost-black. */
    private static void mkblack(gdImageStruct ptr)
	{
		gd.gdImageFilledRectangle(ptr, 0, 0, ptr.sx - 1, ptr.sy - 1, gd.gdImageColorExactAlpha(ptr, 2, 2, 2, 0));
    } // mkblack

    /* Fill with almost-black. */
    private static void mkblack(Image ptr)
    {
        ptr.FilledRectangle(0, 0, ptr.SX() - 1, ptr.SX() - 1, ptr.ColorExact(2, 2, 2, 0));
    } // mkblack


    private const int CLOSE_ENOUGH = 15;

    private static void scaletest(int x, int y, int nx, int ny)
	{
	    gdImageStruct imref = mkwhite(x, y);
		gdImageStruct im = mkwhite(x, y);
		gdImageStruct tmp = gd.gdImageScale(im, (uint) nx, (uint) ny);
		gdImageStruct same = gd.gdImageScale(tmp, (uint) x, (uint) y);

		/* Test the result to insure that it's close enough to the
		 * original. */
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(im, same) < CLOSE_ENOUGH) ? 1 : 0);

		/* Modify the original and test for a change again.  (I.e. test
		 * for accidentally shared memory.) */
        mkblack(tmp);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imref, same) < CLOSE_ENOUGH) ? 1 : 0);

		gd.gdImageDestroy(im);
		gd.gdImageDestroy(tmp);
		gd.gdImageDestroy(same);
        gd.gdImageDestroy(imref);
    } // scaletest

    private static void scaletestCpp(int x, int y, int nx, int ny)
    {
        using (Image imageref = mkwhiteCpp(x, y))
        {
            using (Image image = mkwhiteCpp(x, y))
            {
                using (var tmp = new Image(gd.gdImageScale(image.GetPtr(), (uint) nx, (uint) ny)))
                {
                    using (var same = new Image(gd.gdImageScale(tmp.GetPtr(), (uint) x, (uint) y)))
                    {
                        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(image, same) < CLOSE_ENOUGH) ? 1 : 0);

                        /* Modify the original and test for a change again.  (I.e. test
         * for accidentally shared memory.) */
                        mkblack(tmp);
                        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imageref, same) < CLOSE_ENOUGH) ? 1 : 0);
                    }
                }
            }
        }
    } // scaletest

    private static void do_test(int x, int y, int nx, int ny)
	{
	    gdImageStruct im = mkwhite(x, y);
		gdImageStruct imref = mkwhite(x, y);

		gdImageStruct same = gd.gdImageScale(im, (uint) x, (uint) y);

		/* Trivial resize should be a straight copy. */
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (im != same) ? 1 : 0);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(im, same) == 0) ? 1 : 0);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imref, same) == 0) ? 1 : 0);

		/* Ensure that modifying im doesn't modify same (i.e. see if we
		 * can catch them accidentally sharing the same pixel buffer.) */
		mkblack(im);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imref, same) == 0) ? 1 : 0);

		gd.gdImageDestroy(same);
		gd.gdImageDestroy(im);

		/* Scale horizontally, vertically and both. */
		scaletest(x, y, nx, y);
		scaletest(x, y, x, ny);
		scaletest(x, y, nx, ny);
	}

    private static void do_testCpp(int x, int y, int nx, int ny)
    {
        using (Image image = mkwhiteCpp(x, y))
        {
            using (Image imageref = mkwhiteCpp(x, y))
            {
                using (var same = image.Scale(x, y))
                {
                    GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (image.GetPtr().__Instance != same.GetPtr().__Instance) ? 1 : 0);
                    GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(image, same) == 0) ? 1 : 0);
                    GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imageref, same) == 0) ? 1 : 0);

                    /* Ensure that modifying im doesn't modify same (i.e. see if we
         * can catch them accidentally sharing the same pixel buffer.) */
                    mkblack(image);
                    GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imageref, same) == 0) ? 1 : 0);
                }
            }
        }

        /* Scale horizontally, vertically and both. */
        scaletestCpp(x, y, nx, y);
        scaletestCpp(x, y, x, ny);
        scaletestCpp(x, y, nx, ny);
    }
}

