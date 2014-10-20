using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBmp_im2im
{
    [Test]
    public unsafe void TestBmp_im2im()
	{
        int size = 0;
        var result = new GlobalMembersGdtest.CuTestImageResult(0, 0);

		gdImageStruct src = gd.gdImageCreate(100, 100);
		if (src == null)
		{
			Assert.Fail("could not create src");
		}
		int r = gd.gdImageColorAllocate(src, 0xFF, 0, 0);
		int g = gd.gdImageColorAllocate(src, 0, 0xFF, 0);
		int b = gd.gdImageColorAllocate(src, 0, 0, 0xFF);
		gd.gdImageFilledRectangle(src, 0, 0, 99, 99, r);
		gd.gdImageRectangle(src, 20, 20, 79, 79, g);
		gd.gdImageEllipse(src, 70, 25, 30, 20, b);

        gd.gdImageBmp(src, "bmp_im2im_src.bmp", 1);
        IntPtr p = gd.gdImageBmpPtr(src, &size, 1);
		if (p == IntPtr.Zero)
		{
            gd.gdImageDestroy(src);
			Assert.Fail("p is null\n");
		}
		if (size <= 0)
		{
            gd.gdFree(p);
		    gd.gdImageDestroy(src);
			Assert.Fail("size is non-positive");
		}

		gdImageStruct dst = gd.gdImageCreateFromBmpPtr(size, p);
		if (dst == null)
		{
            gd.gdFree(p);
		    gd.gdImageDestroy(src);
			Assert.Fail("could not create dst");
		}
        gd.gdImageBmp(dst, "bmp_im2im_dst.bmp", 1);
        GlobalMembersGdtest.gdTestImageDiff(src, dst, null, result);
		if (result.pixels_changed > 0)
		{
            gd.gdImageDestroy(dst);
		    gd.gdFree(p);
		    gd.gdImageDestroy(src);
			Assert.Fail("pixels changed: {0:D}\n", result.pixels_changed);
		}
		gd.gdImageDestroy(dst);
        gd.gdFree(p);
        gd.gdImageDestroy(src);
	}
}