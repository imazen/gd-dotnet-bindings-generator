using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00077
{
    [Test]
    public void TestBug00077()
	{
        const string exp = "bug00077_exp.png";
		int error = 0;

        gdImageStruct im = gd.gdImageCreateTrueColor(11, 11);
		gd.gdImageFilledRectangle(im, 0, 0, 10, 10, 0xFFFFFF);
		gd.gdImageSetThickness(im, 1);

		gd.gdImageLine(im, 0, 10, 0, 0, 0x0);
		gd.gdImageLine(im, 5, 10, 5, 0, 0x0);
		gd.gdImageLine(im, 10, 5, 0, 5, 0x0);
		gd.gdImageLine(im, 10, 10, 0, 10, 0x0);

		string path = string.Format("{0}/gdimageline/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, exp);

		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
		{
			error = 1;
		}

		gd.gdImageDestroy(im);

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
	}

    [Test]
    public void TestBug00077Cpp()
    {
        const string exp = "bug00077_exp.png";
        int error = 0;

        using (var image = new Image(11, 11, true))
        {
            image.FilledRectangle(0, 0, 10, 10, 0xFFFFFF);
            image.SetThickness(1);

            image.Line(0, 10, 0, 0, 0x0);
            image.Line(5, 10, 5, 0, 0x0);
            image.Line(10, 5, 0, 5, 0x0);
            image.Line(10, 10, 0, 10, 0x0);

            string path = string.Format("{0}/gdimageline/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, exp);

            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                error = 1;
            }
        }

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }
}

