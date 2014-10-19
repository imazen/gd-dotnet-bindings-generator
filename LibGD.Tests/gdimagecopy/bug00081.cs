using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00081
{
    [Test]
    public void TestBug00081()
	{
        const string file_exp = "bug00081_exp.png";

		gdImageStruct im = gd.gdImageCreateTrueColor(5, 5);
		if (im == null)
		{
            Assert.Fail("can't create the src truecolor image\n");
		}

		gd.gdImageFilledRectangle(im, 0, 0, 49, 49, 0x00FFFFFF);
		gd.gdImageColorTransparent(im, 0xFFFFFF);
		gd.gdImageFilledRectangle(im, 1, 1, 4, 4, 0xFF00FF);

		gdImageStruct im2 = gd.gdImageCreateTrueColor(20, 20);
		if (im2 == null)
		{
            gd.gdImageDestroy(im);
            Assert.Fail("can't create the dst truecolor image\n");
		}

		gd.gdImageCopy(im2, im, 2, 2, 0, 0, ((im).sx), ((im).sy));

		string path = string.Format("{0}/gdimagecopy/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im2)) == 0)
        {
            gd.gdImageDestroy(im);
            gd.gdImageDestroy(im2);
            Assert.Fail("Reference image and destination differ\n");
        }

		gd.gdImageDestroy(im);
		gd.gdImageDestroy(im2);
	}

    [Test]
    public void TestBug00081Cpp()
    {
        const string file_exp = "bug00081_exp.png";

        using (var image = new Image(5, 5, true))
        {
            if (!image.good())
            {
                Assert.Fail("can't create the src truecolor image\n");
            }

            image.FilledRectangle(0, 0, 49, 49, 0x00FFFFFF);
            image.ColorTransparent(0xFFFFFF);
            image.FilledRectangle(1, 1, 4, 4, 0xFF00FF);

            using (var image2 = new Image(20, 20, true))
            {
                if (!image2.good())
                {
                    Assert.Fail("can't create the dst truecolor image");
                }

                image2.Copy(image, 2, 2, 0, 0, image.SX(), image.SX());

                string path = string.Format("{0}/gdimagecopy/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp);
                if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image2) == 0)
                {
                    Assert.Fail("Reference image and destination differ\n");
                }
            }
        }
    }
}

