using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersPng_im2im
{
	[Test]
	public unsafe void TestPng_im2im()
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
        gd.gdImagePng(src, "png_im2im_src.png");
	    void* p = gd.gdImagePngPtr(src, &size);
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

		gdImageStruct dst = gd.gdImageCreateFromPngPtr(size, p);
		if (dst == null)
		{
            gd.gdFree(p);
            gd.gdImageDestroy(src);
			Assert.Fail("could not create dst");
		}
        gd.gdImagePng(dst, "png_im2im_dst.png");
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

    [Test]
    public unsafe void TestPng_im2imCpp()
    {
        int size = 0;
        var result = new GlobalMembersGdtest.CuTestImageResult(0, 0);

        void* p;
        using (var src = new Image(100, 100))
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
            src.Png("png_im2im_src.png");
            p = src.Png(&size);
            if (p == null)
            {
                Assert.Fail("p is null");
            }
            if (size <= 0)
            {
                gd.gdFree(p);
                Assert.Fail("size is non-positive");
            }

            using (var dst = new Image(size, p, new Png_tag()))
            {
                if (!dst.good())
                {
                    gd.gdFree(p);
                    Assert.Fail("could not create dst");
                }
                dst.Png("png_im2im_dst.png");
                GlobalMembersGdtest.TestImageDiff(src, dst, null, result);

                if (result.pixels_changed > 0)
                {
                    gd.gdFree(p);
                    Assert.Fail("pixels changed: {0:D}\n", result.pixels_changed);
                }
            }
            gd.gdFree(p);
        }
    }
}

