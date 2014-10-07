using LibGD;
using NUnit.Framework;

#if !NO_TIFF

[TestFixture]
public class GlobalMembersTiff_im2im
{
    [Test]
	public unsafe void TestTiff_im2im()
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

		gd.gdImageTiff(src, "tiff_im2im_src.tiff");
		p = gd.gdImageTiffPtr(src, &size);
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

		dst = gd.gdImageCreateFromTiffPtr(size, p);
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
