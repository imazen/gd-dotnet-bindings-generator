using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00067
{
    [Test]
    public void TestBug00067()
	{
        const string file_im = "gdimagerotate/remirh128.jpg";
		const string file_exp = "gdimagerotate/bug00067";
        int error = 0;

        string path = string.Format("{0}/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_im);
		gdImageStruct im = gd.gdImageCreateFromJpeg(path);

		if (im == null)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "loading %s failed.", path);
			Assert.Fail();
		}

		int color = gd.gdImageColorAllocate(im, 0, 0, 0);

		if (color < 0)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "allocation color from image failed.");
			gd.gdImageDestroy(im);
			Assert.Fail();
		}

		for (int angle = 0; angle <= 180; angle += 15)
		{

			gdImageStruct exp = gd.gdImageRotateInterpolated(im, angle, color);

			if (exp == null)
			{
				GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "rotating image failed for %03d.", angle);
				gd.gdImageDestroy(im);
				Assert.Fail();
			}

			path = string.Format("{0}/{1}_{2:D3}_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp, angle);

			if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, exp) == 0)
			{
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "comparing rotated image to %s failed.", path);
				error += 1;
			}

			gd.gdImageDestroy(exp);
		}

		gd.gdImageDestroy(im);

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
	}

    [Test]
    public void TestBug00067Cpp()
    {
        const string file_im = "gdimagerotate/remirh128.jpg";
        const string file_exp = "gdimagerotate/bug00067";
        int error = 0;

        string path = string.Format("{0}/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_im);
        using (var image = new Image())
        {
            image.CreateFromJpeg(path);

            if (!image.good())
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "loading %s failed.", path);
                Assert.Fail();
            }

            int color = image.ColorAllocate(0, 0, 0);

            if (color < 0)
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "allocation color from image failed.");
                Assert.Fail();
            }

            for (int angle = 0; angle <= 180; angle += 15)
            {
                var exp = image.RotateInterpolated(angle, color);

                if (!exp.good())
                {
                    GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "rotating image failed for %03d.", angle);
                    Assert.Fail();
                }

                path = string.Format("{0}/{1}_{2:D3}_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp, angle);

                if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, exp) == 0)
                {
                    GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "comparing rotated image to %s failed.", path);
                    error += 1;
                }
            }
        }

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }
}

