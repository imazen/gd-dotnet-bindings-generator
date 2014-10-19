using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00020
{
    private const int WIDTH = 50;

    [Test]
    public void TestBug00020()
	{
        int error = 0;

        string path = string.Format("{0}/gdimagecopyrotated/bug00020_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        gdImageStruct im = gd.gdImageCreateTrueColor(WIDTH, WIDTH);
		gd.gdImageFilledRectangle(im, 0,0, WIDTH, WIDTH, 0xFF0000);
		gd.gdImageColorTransparent(im, 0xFF0000);
		gd.gdImageFilledEllipse(im, WIDTH / 2, WIDTH / 2, WIDTH - 20, WIDTH - 10, 0x50FFFFFF);

		gdImageStruct im2 = gd.gdImageCreateTrueColor(WIDTH, WIDTH);

		gd.gdImageCopyRotated(im2, im, WIDTH / 2, WIDTH / 2, 0,0, WIDTH, WIDTH, 60);

        if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im2)) == 0)
		{
			error = 1;
		}

		gd.gdImageDestroy(im2);
        gd.gdImageDestroy(im);
        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
	}

    [Test]
    public void TestBug00020Cpp()
    {
        int error = 0;

        string path = string.Format("{0}/gdimagecopyrotated/bug00020_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        using (var image = new Image(WIDTH, WIDTH, true))
        {
            image.FilledRectangle(0, 0, WIDTH, WIDTH, 0xFF0000);
            image.ColorTransparent(0xFF0000);
            image.FilledEllipse(WIDTH / 2, WIDTH / 2, WIDTH - 20, WIDTH - 10, 0x50FFFFFF);

            using (var image2 = new Image(WIDTH, WIDTH, true))
            {
                image2.CopyRotated(image, WIDTH / 2, WIDTH / 2, 0, 0, WIDTH, WIDTH, 60);

                if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image2) == 0)
                {
                    error = 1;
                }
            }
        }

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }
}

