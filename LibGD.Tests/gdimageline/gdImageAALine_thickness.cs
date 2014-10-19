using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdImageAALine_thickness
{
    [Test]
    public void TestGdImageAALine_thickness()
	{
        const string file_exp = "gdimageline/gdImageAALine_thickness_exp.png";

		gdImageStruct im = gd.gdImageCreateTrueColor(100, 100);
		gd.gdImageFilledRectangle(im, 0, 0, 99, 99, gd.gdImageColorExactAlpha(im, 255, 255, 255, 0));

		gd.gdImageSetThickness(im, 5);
		gd.gdImageSetAntiAliased(im, gd.gdImageColorExactAlpha(im, 0, 0, 0, 0));
		gd.gdImageLine(im, 0,0, 99, 99, GlobalMembersGdtest.DefineConstants.gdAntiAliased);

		string path = string.Format("{0}/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp);

        if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, im) == 0)
		{
            gd.gdImageDestroy(im);
			Assert.Fail("comparing rotated image to {0} failed.", path);
		}

		gd.gdImageDestroy(im);
	}

    [Test]
    public void TestGdImageAALine_thicknessCpp()
    {
        const string file_exp = "gdimageline/gdImageAALine_thickness_exp.png";

        using (var image = new Image(100, 100, true))
        {
            image.FilledRectangle(0, 0, 99, 99, image.ColorExact(255, 255, 255, 0));

            image.SetThickness(5);
            image.SetAntiAliased(image.ColorExact(0, 0, 0, 0));
            image.Line(0, 0, 99, 99, GlobalMembersGdtest.DefineConstants.gdAntiAliased);

            string path = string.Format("{0}/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp);

            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                Assert.Fail("comparing rotated image to {0} failed.", path);
            }
        }
    }
}

