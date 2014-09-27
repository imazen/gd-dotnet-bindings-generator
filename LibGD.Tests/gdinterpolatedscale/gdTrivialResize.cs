using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdTrivialResize
{
	public static gdImageStruct mkwhite(int x, int y)
	{
		gdImageStruct im;

		im = gd.gdImageCreateTrueColor(x, y);
		gd.gdImageFilledRectangle(im, 0, 0, x - 1, y - 1, gd.gdImageColorExactAlpha(im, 255, 255, 255, 0));

		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (im != null) ? 1 : 0);

		gd.gdImageSetInterpolationMethod(im, gdInterpolationMethod.GD_BICUBIC); // FP interp'n

		return im;
	} // mkwhite


	/* Fill with almost-black. */
	public static void mkblack(gdImageStruct ptr)
	{
		gd.gdImageFilledRectangle(ptr, 0, 0, ptr.sx - 1, ptr.sy - 1, gd.gdImageColorExactAlpha(ptr, 2, 2, 2, 0));
	} // mkblack


	public const int CLOSE_ENOUGH = 15;

	public static void scaletest(int x, int y, int nx, int ny)
	{
		gdImageStruct im;
		gdImageStruct imref;
		gdImageStruct tmp;
		gdImageStruct same;

		imref = mkwhite(x, y);
		im = mkwhite(x, y);
		tmp = gd.gdImageScale(im, (uint) nx, (uint) ny);
		same = gd.gdImageScale(tmp, (uint) x, (uint) y);

		/* Test the result to insure that it's close enough to the
		 * original. */
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(im, same) < CLOSE_ENOUGH) ? 1 : 0);

		/* Modify the original and test for a change again.  (I.e. test
		 * for accidentally shared memory.) */
		GlobalMembersGdCopyBlurred.mkblack(tmp);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imref, same) < CLOSE_ENOUGH) ? 1 : 0);

		gd.gdImageDestroy(im);
		gd.gdImageDestroy(tmp);
		gd.gdImageDestroy(same);
	} // scaletest

	public static void do_test(int x, int y, int nx, int ny)
	{
		gdImageStruct im;
		gdImageStruct imref;
		gdImageStruct tmp;
		gdImageStruct same;
		gdImageStruct same2;

		im = mkwhite(x, y);
		imref = mkwhite(x, y);

		same = gd.gdImageScale(im, (uint) x, (uint) y);

		/* Trivial resize should be a straight copy. */
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (im != same) ? 1 : 0);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(im, same) == 0) ? 1 : 0);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imref, same) == 0) ? 1 : 0);

		/* Ensure that modifying im doesn't modify same (i.e. see if we
		 * can catch them accidentally sharing the same pixel buffer.) */
		GlobalMembersGdCopyBlurred.mkblack(im);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(imref, same) == 0) ? 1 : 0);

		gd.gdImageDestroy(same);
		gd.gdImageDestroy(im);

		/* Scale horizontally, vertically and both. */
		GlobalMembersGdTrivialResize.scaletest(x, y, nx, y);
		GlobalMembersGdTrivialResize.scaletest(x, y, x, ny);
		GlobalMembersGdTrivialResize.scaletest(x, y, nx, ny);
	}

	[Test]
	public void TestGdTrivialResize()
	{

		do_test(300, 300, 600, 600);
		do_test(3200, 2133, 640, 427);

		Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
	}
}

