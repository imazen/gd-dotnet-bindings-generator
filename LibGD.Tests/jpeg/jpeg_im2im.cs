using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_im2im
{
    [Test]
    public unsafe void TestJpeg_im2im()
	{
        int size = 0;
#if false
	//	CuTestImageResult result = {0, 0};
	#endif

		gdImageStruct src = gd.gdImageCreateTrueColor(100, 100);
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
        gd.gdImageJpeg(src, "jpeg_im2im_src.jpeg", 1);
        void* p = gd.gdImageJpegPtr(src, &size, 100);
		if (p == null)
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

		gdImageStruct dst = gd.gdImageCreateFromJpegPtr(size, p);
		if (dst == null)
		{
            gd.gdFree(p);
		    gd.gdImageDestroy(src);
			Assert.Fail("could not create dst");
		}
        gd.gdImageJpeg(dst, "jpeg_im2im_dst.jpeg", 1);
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

    [Test]
    public unsafe void TestJpeg_im2imCpp()
    {
        int size = 0;
#if false
	//	CuTestImageResult result = {0, 0};
#endif

        void* p;
        using (var src = new Image(100, 100, true))
        {
            if (!src.good())
            {
                Assert.Fail("could not create src");
            }
            int r = src.ColorAllocate(0xFF, 0, 0);
            int g = src.ColorAllocate(0, 0xFF, 0);
            int b = src.ColorAllocate(0, 0, 0xFF);
            src.FilledRectangle(0, 0, 99, 99, r);
            src.Rectangle(20, 20, 79, 79, g);
            src.Ellipse(70, 25, 30, 20, b);
            src.Jpeg("jpeg_im2im_src.jpeg", 1);
            p = src.Jpeg(&size, 100);
            if (p == null)
            {
                Assert.Fail("p is null\n");
            }
            if (size <= 0)
            {
                gd.gdFree(p);
                Assert.Fail("size is non-positive");
            }

            using (var dst = new Image(size, p))
            {
                if (!dst.good())
                {
                    gd.gdFree(p);
                    Assert.Fail("could not create dst");
                }
                dst.Jpeg("jpeg_im2im_dst.jpeg", 1);
            }
#if false
    //	gd.gdTestImageDiff(src, dst, NULL, &result);
    //	if (result.pixels_changed > 0) {
    //		status = 1;
    //		printf("pixels changed: %d\n", result.pixels_changed);
    //	}
#endif
            gd.gdFree(p);
        }
    }
}

