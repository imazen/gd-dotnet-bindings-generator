using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGd_im2im
{
    [Test]
    public unsafe void TestGd_im2im()
	{
		gdImageStruct src;
		gdImageStruct dst;
		int r;
		int g;
		int b;
		void* p;
		int size = 0;
        GlobalMembersGdtest.CuTestImageResult result = new GlobalMembersGdtest.CuTestImageResult(0, 0);

		src = gd.gdImageCreate(100, 100);
		if (src == null)
		{
            Assert.Fail("could not create src\n");
		}
		r = gd.gdImageColorAllocate(src, 0xFF, 0, 0);
		g = gd.gdImageColorAllocate(src, 0, 0xFF, 0);
		b = gd.gdImageColorAllocate(src, 0, 0, 0xFF);
		gd.gdImageFilledRectangle(src, 0, 0, 99, 99, r);
		gd.gdImageRectangle(src, 20, 20, 79, 79, g);
		gd.gdImageEllipse(src, 70, 25, 30, 20, b);

        OutputGd(src, "src");
		p = gd.gdImageGdPtr(src, &size);
		if (p == null)
		{
            gd.gdImageDestroy(src);
            Assert.Fail("p is null\n");
		}
		if (size <= 0)
		{
            gd.gdFree(p);
		    gd.gdImageDestroy(src);
			Assert.Fail("size is non-positive\n");
		}

		dst = gd.gdImageCreateFromGdPtr(size, p);
		if (dst == null)
		{
            gd.gdFree(p);
            gd.gdImageDestroy(src);
			Assert.Fail("could not create dst\n");
		}
        OutputGd(dst, "dst");
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

    private static void OutputGd(gdImageStruct input, string name)
    {
        gd.gdImageGd(input, string.Format("gd_im2im_{0}.gd", name));
    }
}

