using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersPhp_bug_64898
{
    [Test]
    public void TestPhp_bug_64898()
	{
        const string file_im = "gdimagerotate/php_bug_64898.png";
		const string file_exp = "gdimagerotate/php_bug_64898_exp.png";

        string path = string.Format("{0}/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_im);

        gdImageStruct im = gd.gdImageCreateTrueColor(141, 200);

		if (im == null)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "loading %s failed.", path);
			Assert.Fail();
		}

		gd.gdImageFilledRectangle(im, 0, 0, 140, 199, 0x00ffffff);

	/*	Try default interpolation method, but any non-optimized fails */
	/*	gd.gdImageSetInterpolationMethod(im, GD_BICUBIC_FIXED); */

		gdImageStruct exp = gd.gdImageRotateInterpolated(im, 45, 0x0);

		if (exp == null)
		{
            GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "rotating image failed.");
			gd.gdImageDestroy(im);
			Assert.Fail();
		}

		path = string.Format("{0}/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp);

        if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, exp) == 0)
		{
			Console.Write("comparing rotated image to {0} failed.\n", path);
			gd.gdImageDestroy(im);
			gd.gdImageDestroy(exp);
			Assert.Fail();
		}

		gd.gdImageDestroy(exp);
		gd.gdImageDestroy(im);
	}

    [Test]
    public void TestPhp_bug_64898Cpp()
    {
        const string file_im = "gdimagerotate/php_bug_64898.png";
        const string file_exp = "gdimagerotate/php_bug_64898_exp.png";

        string path = string.Format("{0}/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_im);

        using (var image = new Image(141, 200, true))
        {
            if (!image.good())
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "loading %s failed.", path);
                Assert.Fail();
            }

            image.FilledRectangle(0, 0, 140, 199, 0x00ffffff);

            /*	Try default interpolation method, but any non-optimized fails */
            /*	gd.gdImageSetInterpolationMethod(im, GD_BICUBIC_FIXED); */

            // this function is not exposed in the C++ wrapper
            using (var exp = new Image(gd.gdImageRotateInterpolated(image.GetPtr(), 45, 0x0)))
            {
                if (!exp.good())
                {
                    GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "rotating image failed.");
                    Assert.Fail();
                }

                path = string.Format("{0}/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp);

                if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, exp) == 0)
                {
                    Console.Write("comparing rotated image to {0} failed.\n", path);
                    Assert.Fail();
                }
            }
        }
    }
}

