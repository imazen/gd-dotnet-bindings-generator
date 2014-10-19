using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00010
{
    [Test]
    public void TestBug00010()
	{
        int error = 0;

        gdImageStruct im = gd.gdImageCreateTrueColor(100,100);
		gd.gdImageFilledEllipse(im, 50,50, 70, 90, 0x50FFFFFF);

		string path = string.Format("{0}/gdimagefilledellipse/bug00010_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
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
    public void TestBug00010Cpp()
    {
        int error = 0;

        using (var image = new Image(100, 100, true))
        {
            image.FilledEllipse(50, 50, 70, 90, 0x50FFFFFF);

            string path = string.Format("{0}/gdimagefilledellipse/bug00010_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
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

