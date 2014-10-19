using System;
using System.IO;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00002_1
{
    private const string TMP_FN = "bug00002_1.png";

    [Test]
    public void TestBug00002_1()
	{
        gdImageStruct im = gd.gdImageCreateTrueColor(100, 100);

		if (im == null)
		{
            GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image.\n");
			Assert.Fail();
		}

		gd.gdImageFill(im, 0, 0, 0xffffff);
		gd.gdImageFill(im, 0, 0, 0xffffff);

        gd.gdImagePng(im, TMP_FN);

		string path = string.Format("{0}/gdimagefill/bug00002_1_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
		{
			gd.gdImageDestroy(im);
            Assert.Fail();
		}

		gd.gdImageDestroy(im);

        File.Delete(TMP_FN);
	}

    [Test]
    public void TestBug00002_1Cpp()
    {
        using (var image = new Image(100, 100, true))
        {
            if (!image.good())
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image.");
                Assert.Fail();
            }

            image.Fill(0, 0, 0xffffff);
            image.Fill(0, 0, 0xffffff);

            image.Png(TMP_FN);

            string path = string.Format("{0}/gdimagefill/bug00002_1_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                Assert.Fail();
            }
        }

        File.Delete(TMP_FN);
    }
}

