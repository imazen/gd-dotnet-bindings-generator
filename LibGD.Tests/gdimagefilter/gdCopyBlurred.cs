using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdCopyBlurred
{
    public const int WIDTH = 300;
    public const int HEIGHT = 200;
    public const int LX = (WIDTH/2);    // Line X
    public const int LY = (HEIGHT/2);   // Line Y
    public const int HT = 3;            // Half of line-thickness

    public const int CLOSE_ENOUGH = 0;
    public const int PIXEL_CLOSE_ENOUGH = 0;

    [Test]
    public void TestGdCopyBlurred()
    {
        do_test();
        do_crosstest();

        Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
    }

    [Test]
    public void TestGdCopyBlurredCpp()
    {
        do_testCpp();
        do_crosstestCpp();

        Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
    }

    private static void save(gdImageStruct im, string filename)
	{
	#if false
	//    FILE *out;
	//
	//    out = fopen(filename, "wb");
	//    gd.gdImagePng(im, out);
	//    fclose(out);
	#else
        //im, filename;
	#endif
    } // save

    private static void saveCpp(Image image, string filename)
    {
#if false
	//    FILE *out;
	//
	//    out = fopen(filename, "wb");
	//    gd.gdImagePng(im, out);
	//    fclose(out);
#else
        //im, filename;
#endif
    } // save


	/* Test gd.gdImageScale() with bicubic interpolation on a simple
	 * all-white image. */

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

        // this function is not exposed in the C++ wrapper
        gd.gdImageSetInterpolationMethod(image.GetPtr(), gdInterpolationMethod.GD_BICUBIC); // FP interp'n

        return image;
    } // mkwhite


	/* Fill with almost-black. */
    private static void mkblack(gdImageStruct ptr)
	{
		gd.gdImageFilledRectangle(ptr, 0, 0, ptr.sx - 1, ptr.sy - 1, gd.gdImageColorExactAlpha(ptr, 2, 2, 2, 0));
    } // mkblack

    /* Fill with almost-black. */
    private static void mkblackCpp(Image ptr)
    {
        ptr.FilledRectangle(0, 0, ptr.SX() - 1, ptr.SY() - 1, ptr.ColorExact(2, 2, 2, 0));
    } // mkblack


    private static gdImageStruct mkcross()
	{
        int n;

		gdImageStruct im = mkwhite(WIDTH, HEIGHT);
		int fg = gd.gdImageColorAllocate(im, 0, 0, 0);

		for (n = -HT; n < HT; n++)
		{
			gd.gdImageLine(im, WIDTH / 2 - n, 0, WIDTH / 2 - n, HEIGHT - 1, fg);
			gd.gdImageLine(im, 0, HEIGHT / 2 - n, WIDTH - 1, HEIGHT / 2 - n, fg);
		} // for

		return im;
    } // mkcross

    private static Image mkcrossCpp()
    {
        int n;

        Image image = mkwhiteCpp(WIDTH, HEIGHT);
        int fg = image.ColorAllocate(0, 0, 0);

        for (n = -HT; n < HT; n++)
        {
            image.Line(WIDTH / 2 - n, 0, WIDTH / 2 - n, HEIGHT - 1, fg);
            image.Line(0, HEIGHT / 2 - n, WIDTH - 1, HEIGHT / 2 - n, fg);
        } // for

        return image;
    } // mkcross


    private static void blurblank(gdImageStruct im, int radius, double sigma)
	{
	    gdImageStruct result = gd.gdImageCopyGaussianBlurred(im, radius, sigma);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (result != null) ? 1 : 0);
		if (result == null)
			return;

        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(im, result) <= CLOSE_ENOUGH) ? 1 : 0);

		gd.gdImageDestroy(result);
    } // blurblank

    private static void blurblankCpp(Image image, int radius, double sigma)
    {
        // this function is not exposed in the C++ wrapper
        using (var result = new Image(gd.gdImageCopyGaussianBlurred(image.GetPtr(), radius, sigma)))
        {
            GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", result.good() ? 1 : 0);
            if (!result.good())
                return;

            GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", GlobalMembersGdtest.gdMaxPixelDiff(image, result) <= CLOSE_ENOUGH ? 1 : 0);
        }
    } // blurblank


    private static void do_test()
	{
	    gdImageStruct im = mkwhite(WIDTH, HEIGHT);
		gdImageStruct imref = mkwhite(WIDTH, HEIGHT);

		/* Blur a plain white image to various radii and ensure they're
		 * still similar enough. */
		blurblank(im, 1, 0.0);
		blurblank(im, 2, 0.0);
		blurblank(im, 4, 0.0);
		blurblank(im, 8, 0.0);
		blurblank(im, 16, 0.0);

		/* Ditto a black image. */
		mkblack(im);
	//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent in C# to the following C++ macro:
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(im, imref) >= 240) ? 1 : 0);

		blurblank(im, 1, 0.0);
		blurblank(im, 2, 0.0);
		blurblank(im, 4, 0.0);
		blurblank(im, 8, 0.0);
		blurblank(im, 16, 0.0);
    } // do_test

    private static void do_testCpp()
    {
        Image image = mkwhiteCpp(WIDTH, HEIGHT);
        Image imref = mkwhiteCpp(WIDTH, HEIGHT);

        /* Blur a plain white image to various radii and ensure they're
         * still similar enough. */
        blurblankCpp(image, 1, 0.0);
        blurblankCpp(image, 2, 0.0);
        blurblankCpp(image, 4, 0.0);
        blurblankCpp(image, 8, 0.0);
        blurblankCpp(image, 16, 0.0);

        /* Ditto a black image. */
        mkblackCpp(image);
        //C++ TO C# CONVERTER TODO TASK: There is no direct equivalent in C# to the following C++ macro:
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(image, imref) >= 240) ? 1 : 0);

        blurblankCpp(image, 1, 0.0);
        blurblankCpp(image, 2, 0.0);
        blurblankCpp(image, 4, 0.0);
        blurblankCpp(image, 8, 0.0);
        blurblankCpp(image, 16, 0.0);
    } // do_test

	/* Ensure that RGB values are equal, then return r (which is therefore
	 * the whiteness.) */
    private static int getwhite(gdImageStruct im, int x, int y)
	{
		int px;
		int r;
		int g;
		int b;

		px = gd.gdImageGetPixel(im, x, y);
		r = ((im).trueColor != 0 ? (((px) & 0xFF0000) >> 16) : (im).red[(px)]);
		g = ((im).trueColor != 0 ? (((px) & 0x00FF00) >> 8) : (im).green[(px)]);
		b = ((im).trueColor != 0 ? ((px) & 0x0000FF) : (im).blue[(px)]);

        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (r == g && r == b) ? 1 : 0);

		return r;
    } // getrgb

    /* Ensure that RGB values are equal, then return r (which is therefore
     * the whiteness.) */
    private static int getwhiteCpp(Image image, int x, int y)
    {
        int px;

        px = image.GetPixel(x, y);
        int r = image.IsTrueColor() ? (px & 0xFF0000) >> 16 : image.Red(px);
        int g = image.IsTrueColor() ? (px & 0x00FF00) >> 8 : image.Green(px);
        int b = image.IsTrueColor() ? px & 0x0000FF : image.Blue(px);

        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", r == g && r == b ? 1 : 0);

        return r;
    } // getrgb

    private static int whitecmp(gdImageStruct im1, gdImageStruct im2, int x, int y)
	{
		int w1;
		int w2;

		w1 = getwhite(im1, x, y);
		w2 = getwhite(im2, x, y);

		return Math.Abs(w1 - w2);
    } // whitediff

    private static int whitecmpCpp(Image image1, Image image2, int x, int y)
    {
        int w1;
        int w2;

        w1 = getwhiteCpp(image1, x, y);
        w2 = getwhiteCpp(image2, x, y);

        return Math.Abs(w1 - w2);
    } // whitediff

    private static void do_crosstest()
	{
		gdImageStruct im = mkcross();
        const int RADIUS = 16;

        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (im != null) ? 1 : 0);

		save(im, "cross.png");

		gdImageStruct blurred = gd.gdImageCopyGaussianBlurred(im, RADIUS, 0.0);
		save(blurred, "blurredcross.png");

		/* These spots shouldn't be affected. */
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmp(im, blurred, 5, 5) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmp(im, blurred, WIDTH - 5, 5) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmp(im, blurred, 5, HEIGHT - 5) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmp(im, blurred, WIDTH - 5, HEIGHT - 5) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);

	//    printf("%d %d %d %d\n", whitecmp(im, blurred, 0, 0), whitecmp(im, blurred, WIDTH-1, 0),
	//         whitecmp(im, blurred, 0, HEIGHT-1), whitecmp(im, blurred, WIDTH-1, HEIGHT-1));

		/* Ditto these, right in the corners */
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmp(im, blurred, 0, 0) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmp(im, blurred, WIDTH - 1, 0) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmp(im, blurred, 0, HEIGHT - 1) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmp(im, blurred, WIDTH - 1, HEIGHT - 1) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);

		/* Now, poking let's poke around the blurred lines. */

		/* Vertical line gets darker when approached from the left. */
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, 1, 1) > getwhite(blurred, WIDTH / 2 - (HT - 1), 1) ? 1 : 0));
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, WIDTH / 2 - 2, 1) > getwhite(blurred, WIDTH / 2 - 1, 1) ? 1 : 0));

		/* ...and lighter when moving away to the right. */
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, WIDTH / 2 + 2, 1) >= getwhite(blurred, WIDTH / 2 + 1, 1) ? 1 : 0));
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, WIDTH / 2 + 3, 1) >= getwhite(blurred, WIDTH / 2 + 2, 1) ? 1 : 0));
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, WIDTH - 1, 1) > getwhite(blurred, WIDTH / 2 + 1, 1) ? 1 : 0));

		/* And the same way, vertically */
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, 1, 1) > getwhite(blurred, 1, HEIGHT / 2 - (HT - 1))) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, 1, HEIGHT / 2 - (HT - 1)) > getwhite(blurred, 1, HEIGHT / 2 - (HT - 2))) ? 1 : 0);

        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, 1, HEIGHT / 2) <= getwhite(blurred, 1, HEIGHT / 2 + 1)) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, 1, HEIGHT / 2 + 1) < getwhite(blurred, 1, HEIGHT / 2 + 3)) ? 1 : 0);
        GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (getwhite(blurred, 1, HEIGHT / 2 + 3) < getwhite(blurred, 1, HEIGHT - 1)) ? 1 : 0);

    } // do_crosstest

    private static void do_crosstestCpp()
    {
        using (Image image = mkcrossCpp())
        {
            const int RADIUS = 16;

            GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", image.good() ? 1 : 0);

            saveCpp(image, "cross.png");

            // this function is not exposed in the C++ wrapper
            using (var blurred = new Image(gd.gdImageCopyGaussianBlurred(image.GetPtr(), RADIUS, 0.0)))
            {
                saveCpp(blurred, "blurredcross.png");

                /* These spots shouldn't be affected. */
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmpCpp(image, blurred, 5, 5) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmpCpp(image, blurred, WIDTH - 5, 5) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmpCpp(image, blurred, 5, HEIGHT - 5) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmpCpp(image, blurred, WIDTH - 5, HEIGHT - 5) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);

                //    printf("%d %d %d %d\n", whitecmp(im, blurred, 0, 0), whitecmp(im, blurred, WIDTH-1, 0),
                //         whitecmp(im, blurred, 0, HEIGHT-1), whitecmp(im, blurred, WIDTH-1, HEIGHT-1));

                /* Ditto these, right in the corners */
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmpCpp(image, blurred, 0, 0) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmpCpp(image, blurred, WIDTH - 1, 0) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmpCpp(image, blurred, 0, HEIGHT - 1) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (whitecmpCpp(image, blurred, WIDTH - 1, HEIGHT - 1) <= PIXEL_CLOSE_ENOUGH) ? 1 : 0);

                /* Now, poking let's poke around the blurred lines. */

                /* Vertical line gets darker when approached from the left. */
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, 1, 1) > getwhiteCpp(blurred, WIDTH / 2 - (HT - 1), 1) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, WIDTH / 2 - 2, 1) > getwhiteCpp(blurred, WIDTH / 2 - 1, 1) ? 1 : 0);

                /* ...and lighter when moving away to the right. */
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, WIDTH / 2 + 2, 1) >= getwhiteCpp(blurred, WIDTH / 2 + 1, 1) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, WIDTH / 2 + 3, 1) >= getwhiteCpp(blurred, WIDTH / 2 + 2, 1) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, WIDTH - 1, 1) > getwhiteCpp(blurred, WIDTH / 2 + 1, 1) ? 1 : 0);

                /* And the same way, vertically */
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, 1, 1) > getwhiteCpp(blurred, 1, HEIGHT / 2 - (HT - 1)) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, 1, HEIGHT / 2 - (HT - 1)) > getwhiteCpp(blurred, 1, HEIGHT / 2 - (HT - 2)) ? 1 : 0);

                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, 1, HEIGHT / 2) <= getwhiteCpp(blurred, 1, HEIGHT / 2 + 1) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, 1, HEIGHT / 2 + 1) < getwhiteCpp(blurred, 1, HEIGHT / 2 + 3) ? 1 : 0);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", getwhiteCpp(blurred, 1, HEIGHT / 2 + 3) < getwhiteCpp(blurred, 1, HEIGHT - 1) ? 1 : 0);
            }
        }
    } // do_crosstest
}

