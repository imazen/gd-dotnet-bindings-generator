using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdimagecolorreplace
{
    [Test]
    public void TestGdImageColorReplace()
    {
        gdImageStruct im;
        int error = 0;

        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

        /* true color */
        im = gd.gdImageCreateTrueColor(5, 5);
        run_tests(im, ref error);
        gd.gdImageDestroy(im);

        /* palette */
        im = gd.gdImageCreate(5, 5);
        run_tests(im, ref error);
        gd.gdImageDestroy(im);

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }

    [Test]
    public void TestGdImageColorReplaceCpp()
    {
        int error = 0;

        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

        /* true color */
        using (var image = new Image(5, 5, true))
        {
            run_testsCpp(image, ref error);
        }

        using (var image = new Image(5, 5))
        {
            run_testsCpp(image, ref error);
        }

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }

    private static unsafe int callback(IntPtr imPtr, int src)
	{
		int r;
		int g;
		int b;

        var im = gdImageStruct.__CreateInstance(imPtr);
		r = ((im).trueColor != 0 ? (((src) & 0xFF0000) >> 16) : (im).red[(src)]);
		g = ((im).trueColor != 0 ? (((src) & 0x00FF00) >> 8) : (im).green[(src)]);
		b = ((im).trueColor != 0 ? ((src) & 0x0000FF) : (im).blue[(src)]);
		if ((b & 0xFF) != 0)
		{
			return gd.gdImageColorResolve(im, 0x0F & r, 0x0F & g, 0);
		}
        return -1;
	}

    private static unsafe int callbackCpp(IntPtr imPtr, int src)
    {
        var image = new Image(gdImageStruct.__CreateInstance(imPtr));
        int r = image.IsTrueColor() ? (src & 0xFF0000) >> 16 : image.Red(src);
        int g = image.IsTrueColor() ? (src & 0x00FF00) >> 8 : image.Green(src);
        int b = image.IsTrueColor() ? (src & 0x0000FF) : image.Blue(src);
        if ((b & 0xFF) != 0)
        {
            return image.ColorResolve(0x0F & r, 0x0F & g, 0);
        }
        return -1;
    }

    private static unsafe void run_tests(gdImageStruct im, ref int error)
	{
        int[] src = new int[2];
		int[] dst = new int[2];

        int black = gd.gdImageColorAllocateAlpha(im, 0, 0, 0, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
		int white = gd.gdImageColorAllocateAlpha(im, 0xFF, 0xFF, 0xFF, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
		int cosmic_latte = gd.gdImageColorAllocateAlpha(im, 0xFF, 0xF8, 0xE7, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
		int cream = gd.gdImageColorAllocateAlpha(im, 0xFF, 0xFD, 0xD0, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
		int ivory = gd.gdImageColorAllocateAlpha(im, 0xFF, 0xFF, 0xF0, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
		int magnolia = gd.gdImageColorAllocateAlpha(im, 0xF8, 0xF4, 0xFF, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
		int old_lace = gd.gdImageColorAllocateAlpha(im, 0xFD, 0xF5, 0xE6, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
		int seashell = gd.gdImageColorAllocateAlpha(im, 0xFF, 0xF5, 0xEE, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
		int yellow = gd.gdImageColorAllocateAlpha(im, 0xFF, 0xFF, 0, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);

		int c = gd.gdImageColorAllocate(im, 0xFF, 0, 0xFF);
		gd.gdImageFilledRectangle(im, 0, 0, 4, 4, white);
		gd.gdImageFilledRectangle(im, 0, 0, 3, 3, black);
		int n = gd.gdImageColorReplace(im, white, c);

	    CheckValue(n, 9);
	    CheckPixel(im, 0, 0, black);
	    CheckPixel(im, 2, 3, black);
	    CheckPixel(im, 4, 4, c);

		gd.gdImageSetClip(im, 1, 1, 3, 3);
		n = gd.gdImageColorReplace(im, black, c);
        CheckValue(n, 9);
	    CheckPixel(im, 0, 0, black);
	    CheckPixel(im, 2, 3, c);

		src[0] = black;
		src[1] = c;
		dst[0] = c;
		dst[1] = white;
        gd.gdImageSetClip(im, 0, 0, 4, 4);
        fixed (int* srcPtr = src)
        {
            fixed (int* dstPtr = dst)
            {
                n = gd.gdImageColorReplaceArray(im, 2, srcPtr, dstPtr);
            }
        }
	    CheckValue(n, 25);
	    CheckPixel(im, 0, 0, c);
	    CheckPixel(im, 2, 3, white);
	    CheckPixel(im, 4, 4, white);

	    fixed (int* srcPtr = src)
	    {
	        fixed (int* dstPtr = dst)
	        {
                n = gd.gdImageColorReplaceArray(im, 0, srcPtr, dstPtr);	            
	        }
	    }
        CheckValue(n, 0);
        fixed (int* srcPtr = src)
        {
            fixed (int* dstPtr = dst)
            {
                n = gd.gdImageColorReplaceArray(im, -1, srcPtr, dstPtr);
            }
        }
        CheckValue(n, 0);
        fixed (int* srcPtr = src)
        {
            fixed (int* dstPtr = dst)
            {
                n = gd.gdImageColorReplaceArray(im, int.MaxValue, srcPtr, dstPtr);
            }
        }
	    CheckValue(n, -1);

		gd.gdImageSetClip(im, 1, 1, 4, 4);
		n = gd.gdImageColorReplaceCallback(im, callback);
	    CheckValue(n, 16);
	    CheckPixel(im, 0, 0, c);
	    CheckPixel(im, 0, 4, white);
		int d = gd.gdImageColorExact(im, 0x0F, 0x0F, 0);
		if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (d > 0) ? 1 : 0) != 1)
		{
			error = -1;
		}
	    CheckPixel(im, 2, 3, d);
	    CheckPixel(im, 4, 4, d);

		gd.gdImageSetClip(im, 0, 0, 4, 4);
        gd.gdImageFilledRectangle(im, 0, 0, 4, 4, black);
        gd.gdImageFilledRectangle(im, 1, 1, 3, 3, white);
        gd.gdImageSetPixel(im, 1, 1, cosmic_latte);
        gd.gdImageSetPixel(im, 1, 2, cream);
        gd.gdImageSetPixel(im, 2, 1, ivory);
        gd.gdImageSetPixel(im, 2, 2, magnolia);
        gd.gdImageSetPixel(im, 3, 1, old_lace);
        gd.gdImageSetPixel(im, 3, 2, seashell);
		n = gd.gdImageColorReplaceThreshold(im, white, yellow, 2);
	    CheckValue(n, 9);
	    CheckPixel(im, 0, 0, black);
	    CheckPixel(im, 1, 1, yellow);
	    CheckPixel(im, 2, 2, yellow);
	    CheckPixel(im, 3, 3, yellow);
	}

    private static unsafe void run_testsCpp(Image image, ref int error)
    {
        int[] src = new int[2];
        int[] dst = new int[2];
        int n;

        int black = image.ColorAllocate(0, 0, 0, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
        int white = image.ColorAllocate(0xFF, 0xFF, 0xFF, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
        int cosmic_latte = image.ColorAllocate(0xFF, 0xF8, 0xE7, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
        int cream = image.ColorAllocate(0xFF, 0xFD, 0xD0, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
        int ivory = image.ColorAllocate(0xFF, 0xFF, 0xF0, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
        int magnolia = image.ColorAllocate(0xF8, 0xF4, 0xFF, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
        int old_lace = image.ColorAllocate(0xFD, 0xF5, 0xE6, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
        int seashell = image.ColorAllocate(0xFF, 0xF5, 0xEE, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);
        int yellow = image.ColorAllocate(0xFF, 0xFF, 0, GlobalMembersGdtest.DefineConstants.gdAlphaOpaque);

        int c = image.ColorAllocate(0xFF, 0, 0xFF);
        image.FilledRectangle(0, 0, 4, 4, white);
        image.FilledRectangle(0, 0, 3, 3, black);
        n = image.ColorReplace(white, c);

        CheckValue(n, 9);
        CheckPixel(image, 0, 0, black);
        CheckPixel(image, 2, 3, black);
        CheckPixel(image, 4, 4, c);

        image.SetClip(1, 1, 3, 3);
        n = image.ColorReplace(black, c);
        CheckValue(n, 9);
        CheckPixel(image, 0, 0, black);
        CheckPixel(image, 2, 3, c);

        src[0] = black;
        src[1] = c;
        dst[0] = c;
        dst[1] = white;
        image.SetClip(0, 0, 4, 4);
        fixed (int* srcPtr = src)
        {
            fixed (int* dstPtr = dst)
            {
                n = image.ColorReplaceArray(2, srcPtr, dstPtr);
            }
        }
        CheckValue(n, 25);
        CheckPixel(image, 0, 0, c);
        CheckPixel(image, 2, 3, white);
        CheckPixel(image, 4, 4, white);

        fixed (int* srcPtr = src)
        {
            fixed (int* dstPtr = dst)
            {
                n = image.ColorReplaceArray(0, srcPtr, dstPtr);
            }
        }
        CheckValue(n, 0);
        fixed (int* srcPtr = src)
        {
            fixed (int* dstPtr = dst)
            {
                n = image.ColorReplaceArray(-1, srcPtr, dstPtr);
            }
        }
        CheckValue(n, 0);
        fixed (int* srcPtr = src)
        {
            fixed (int* dstPtr = dst)
            {
                n = image.ColorReplaceArray(int.MaxValue, srcPtr, dstPtr);
            }
        }
        CheckValue(n, -1);

        image.SetClip(1, 1, 4, 4);
        n = image.ColorReplaceCallback(callbackCpp);
        CheckValue(n, 16);
        CheckPixel(image, 0, 0, c);
        CheckPixel(image, 0, 4, white);
        int d = image.ColorExact(0x0F, 0x0F, 0);
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (d > 0) ? 1 : 0) != 1)
        {
            error = -1;
        }
        CheckPixel(image, 2, 3, d);
        CheckPixel(image, 4, 4, d);

        image.SetClip(0, 0, 4, 4);
        image.FilledRectangle(0, 0, 4, 4, black);
        image.FilledRectangle(1, 1, 3, 3, white);
        image.SetPixel(1, 1, cosmic_latte);
        image.SetPixel(1, 2, cream);
        image.SetPixel(2, 1, ivory);
        image.SetPixel(2, 2, magnolia);
        image.SetPixel(3, 1, old_lace);
        image.SetPixel(3, 2, seashell);
        n = image.ColorReplaceThreshold(white, yellow, 2);
        CheckValue(n, 9);
        CheckPixel(image, 0, 0, black);
        CheckPixel(image, 1, 1, yellow);
        CheckPixel(image, 2, 2, yellow);
        CheckPixel(image, 3, 3, yellow);
    }

    private static void CheckValue(int n, int expected)
    {
        do
        {
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__,
                                                 "assert failed in <%s:%i>\n", ((n) == (expected)) ? 1 : 0) != 1)
            {
                Assert.Fail("{0:D} is expected, but {1:D}\n", expected, n);
            }
        } while (false);
    }

    private static void CheckPixel(gdImageStruct im, int x, int y, int expected)
    {
        do
        {
            gd.gdImageSetClip(im, 0, 0, 4, 4);
            var c = gd.gdImageGetPixel(im, x, y);
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__,
                                                 "assert failed in <%s:%i>\n", (c == (expected)) ? 1 : 0) != 1)
            {
                Assert.Fail("{0:D} is expected, but {1:D}\n", expected, c);
            }
        } while (false);
    }

    private static void CheckPixel(Image image, int x, int y, int expected)
    {
        do
        {
            image.SetClip(0, 0, 4, 4);
            var c = image.GetPixel(x, y);
            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__,
                                                 "assert failed in <%s:%i>\n", (c == (expected)) ? 1 : 0) != 1)
            {
                Assert.Fail("{0:D} is expected, but {1:D}\n", expected, c);
            }
        } while (false);
    }
}

