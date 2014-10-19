using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00111
{
    [Test]
    public void TestBug00111()
	{
        const string file_exp = "bug00111_exp.png";

		gdImageStruct im = gd.gdImageCreateTrueColor(10, 10);
		if (im == null)
		{
            Assert.Fail("can't get truecolor image");
		}

		gd.gdImageLine(im, 2, 2, 2, 2, 0xFFFFFF);
		gd.gdImageLine(im, 5, 5, 5, 5, 0xFFFFFF);

		gd.gdImageLine(im, 0, 0, 0, 0, 0xFFFFFF);

		string path = string.Format("{0}/gdimageline/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
		{
			Assert.Fail("Reference image and destination differ");
		}

		gd.gdImageDestroy(im);
	}

    [Test]
    public void TestBug00111Cpp()
    {
        const string file_exp = "bug00111_exp.png";

        using (var image = new Image(10, 10, true))
        {
            if (!image.good())
            {
                Assert.Fail("can't get truecolor image");
            }

            image.Line(2, 2, 2, 2, 0xFFFFFF);
            image.Line(5, 5, 5, 5, 0xFFFFFF);

            image.Line(0, 0, 0, 0, 0xFFFFFF);

            string path = string.Format("{0}/gdimageline/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, file_exp);
            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                Assert.Fail("Reference image and destination differ");
            }
        }
    }
}

