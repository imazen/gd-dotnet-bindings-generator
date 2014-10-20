using System;
using LibGD;
using NUnit.Framework;

#if !NO_TIFF

[TestFixture]
public class GlobalMembersTiff_im2im
{
    [Test]
	public unsafe void TestTiff_im2im()
	{
        int size = 0;
#if false
	//	CuTestImageResult result = {0, 0};
	#endif

		gdImageStruct src = gd.gdImageCreate(100, 100);
		if (src == null)
		{
			Assert.Fail("could not create src\n");
		}
		int r = gd.gdImageColorAllocate(src, 0xFF, 0, 0);
		int g = gd.gdImageColorAllocate(src, 0, 0xFF, 0);
		int b = gd.gdImageColorAllocate(src, 0, 0, 0xFF);
		gd.gdImageFilledRectangle(src, 0, 0, 99, 99, r);
		gd.gdImageRectangle(src, 20, 20, 79, 79, g);
		gd.gdImageEllipse(src, 70, 25, 30, 20, b);

		gd.gdImageTiff(src, "tiff_im2im_src.tiff");
		IntPtr p = gd.gdImageTiffPtr(src, &size);
		if (p == IntPtr.Zero)
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

		gdImageStruct dst = gd.gdImageCreateFromTiffPtr(size, p);
		if (dst == null)
		{
		    gd.gdFree(p);
            gd.gdImageDestroy(src);
			Assert.Fail("could not create dst\n");
		}
		gd.gdImageTiff(src, "tiff_im2im_dst.tiff");
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
}

#endif
