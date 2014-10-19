using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00181
{
    [Test]
    public void TestBug00181()
	{
        /* GIFEncode */
		gdImageStruct im = gd.gdImageCreate(100, 100);
		if (im == null)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image.\n");
			Assert.Fail();
		}
		im.interlace = 1;
        gd.gdImageGif(im, "bug00181.gif");
		gd.gdImageDestroy(im);

        im = gd.gdImageCreateFromGif("bug00181.gif");
		if (im == null)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image from <%s>\n", "bug00181.gif");
			Assert.Fail();
		}
		int error = im.interlace == 0 ? 1 : 0;
		gd.gdImageDestroy(im);

		if (error != 0)
			Assert.Fail();

		/* GIFAnimEncode */
		im = gd.gdImageCreate(100, 100);
		im.interlace = 1;
		gd.gdImageColorAllocate(im, 255, 255, 255); // allocate white for background color
		int black = gd.gdImageColorAllocate(im, 0, 0, 0);
		int trans = gd.gdImageColorAllocate(im, 1, 1, 1);
		gd.gdImageRectangle(im, 0, 0, 10, 10, black);
		IntPtr fp = C.fopen("bug00181a.gif", "wb");
		if (fp == IntPtr.Zero)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot open <%s> for writing.\n", "bug00181a.gif");
			Assert.Fail();
		}
		gd.gdImageGifAnimBegin(im, fp, 1, 3);
		gd.gdImageGifAnimAdd(im, fp, 0, 0, 0, 100, 1, null);
		gdImageStruct im2 = gd.gdImageCreate(100, 100);
		im2.interlace = 1;
		gd.gdImageColorAllocate(im2, 255, 255, 255);
		gd.gdImagePaletteCopy(im2, im);
		gd.gdImageRectangle(im2, 0, 0, 15, 15, black);
		gd.gdImageColorTransparent(im2, trans);
		gd.gdImageGifAnimAdd(im2, fp, 0, 0, 0, 100, 1, im);
		gdImageStruct im3 = gd.gdImageCreate(100, 100);
		im3.interlace = 1;
		gd.gdImageColorAllocate(im3, 255, 255, 255);
		gd.gdImagePaletteCopy(im3, im);
		gd.gdImageRectangle(im3, 0, 0, 15, 20, black);
		gd.gdImageColorTransparent(im3, trans);
		gd.gdImageGifAnimAdd(im3, fp, 0, 0, 0, 100, 1, im2);
		gd.gdImageGifAnimEnd(fp);
		C.fclose(fp);
		gd.gdImageDestroy(im);
		gd.gdImageDestroy(im2);
		gd.gdImageDestroy(im3);

        im = gd.gdImageCreateFromGif("bug00181a.gif");
		if (im == null)
		{
            GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image from <%s>\n", "bug00181a.gif");
			Assert.Fail();
		}
		error = im.interlace == 0 ? 1 : 0;
		gd.gdImageDestroy(im);

        if (error != 0)
        {
            Assert.Fail();
        }
	}

    [Test]
    public void TestBug00181Cpp()
    {
        /* GIFEncode */
        int error;
        using (var image = new Image(100, 100))
        {
            if (!image.good())
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image.");
                Assert.Fail();
            }
            image.Interlace(true);
            image.Gif("bug00181.gif");

            image.CreateFromGif("bug00181.gif");
            if (!image.good())
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image from <%s>", "bug00181.gif");
                Assert.Fail();
            }
            error = image.GetInterlaced() == 0 ? 1 : 0;

            if (error != 0)
                Assert.Fail();

            /* GIFAnimEncode */
            image.Create(100, 100);
            image.Interlace(true);
            image.ColorAllocate(255, 255, 255); // allocate white for background color
            int black = image.ColorAllocate(0, 0, 0);
            int trans = image.ColorAllocate(1, 1, 1);
            image.Rectangle(0, 0, 10, 10, black);
            IntPtr fp = C.fopen("bug00181a.gif", "wb");
            if (fp == IntPtr.Zero)
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot open <%s> for writing.", "bug00181a.gif");
                Assert.Fail();
            }
            image.GifAnimBegin(fp, 1, 3);
            image.GifAnimAdd(fp, 0, 0, 0, 100, 1, (gdImageStruct) null);
            using (var image2 = new Image(100, 100))
            {
                image2.Interlace(true);
                image2.ColorAllocate(255, 255, 255);
                image2.PaletteCopy(image);
                image2.Rectangle(0, 0, 15, 15, black);
                image2.ColorTransparent(trans);
                image2.GifAnimAdd(fp, 0, 0, 0, 100, 1, image);
                using (var image3 = new Image(100, 100))
                {
                    image3.Interlace(true);
                    image3.ColorAllocate(255, 255, 255);
                    image3.PaletteCopy(image);
                    image3.Rectangle(0, 0, 15, 20, black);
                    image3.ColorTransparent(trans);
                    image3.GifAnimAdd(fp, 0, 0, 0, 100, 1, image2);
                }
                gd.gdImageGifAnimEnd(fp);
                C.fclose(fp);
            }

            image.CreateFromGif("bug00181a.gif");
            if (!image.good())
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image from <%s>\n", "bug00181a.gif");
                Assert.Fail();
            }
            error = image.GetInterlaced() == 0 ? 1 : 0;
        }

        if (error != 0)
        {
            Assert.Fail();
        }
    }
}

