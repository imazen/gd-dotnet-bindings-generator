using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00079
{
    [Test]
    public void TestBug00079()
	{
        gdImageStruct im = gd.gdImageCreateTrueColor(300, 300);
		gd.gdImageFilledRectangle(im, 0,0, 299,299, 0xFFFFFF);

		gd.gdImageSetAntiAliased(im, 0x000000);
		gd.gdImageArc(im, 300, 300, 600,600, 0, 360, GlobalMembersGdtest.DefineConstants.gdAntiAliased);

		string path = string.Format("{0}/gdimagearc/bug00079_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
		{
            Assert.Fail("{0} failed\n", path);
		}

		gd.gdImageDestroy(im);
	}

    [Test]
    public void TestBug00079Cpp()
    {
        using (var image = new Image(300, 300, true))
        {
            image.FilledRectangle(0, 0, 299, 299, 0xFFFFFF);
            image.SetAntiAliased(0x000000);
            image.Arc(300, 300, 600, 600, 0, 360, GlobalMembersGdtest.DefineConstants.gdAntiAliased);

            string path = string.Format("{0}/gdimagearc/bug00079_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                Assert.Fail("{0} failed\n", path);
            }
        }
    }
}

