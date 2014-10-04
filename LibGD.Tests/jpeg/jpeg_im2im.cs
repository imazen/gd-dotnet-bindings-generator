using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_im2im
{
    [Test]
    public unsafe void TestJpeg_im2im()
	{
		gdImageStruct src;
		gdImageStruct dst;
		int r;
		int g;
		int b;
		void* p;
		int size = 0;
#if false
	//	CuTestImageResult result = {0, 0};
	#endif

		src = gd.gdImageCreateTrueColor(100, 100);
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
        OutputJpeg(src, "src");
		p = gd.gdImageJpegPtr(src, &size, 100);
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

		dst = gd.gdImageCreateFromJpegPtr(size, p);
		if (dst == null)
		{
            gd.gdFree(p);
		    gd.gdImageDestroy(src);
			Assert.Fail("could not create dst\n");
		}
        OutputJpeg(dst, "dst");
	#if false
	//	gd.gdTestImageDiff(src, dst, NULL, &result);
	//	if (result.pixels_changed > 0) {
	//		status = 1;
	//		printf("pixels changed: %d\n", result.pixels_changed);
	//	}
	#endif
		gd.gdImageDestroy(dst);
        gd.gdFree(p);
        gd.gdImageDestroy(src);
	}

    private static void OutputJpeg(gdImageStruct input, string name)
    {
        gd.gdImageJpeg(input, string.Format("jpeg_im2im_{0}.jpeg", name), 1);
    }
}

