using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00066
{
    [Test]
    public void TestBug00066()
	{
        string path = string.Format("{0}/gif/bug00066.gif", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

		gdImageStruct im = gd.gdImageCreateFromGif(path);

		path = string.Format("{0}/gif/bug00066_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
		{
            Assert.Fail();
		}
		gd.gdImageDestroy(im);
	}

    [Test]
    public void TestBug00066Cpp()
    {
        string path = string.Format("{0}/gif/bug00066.gif", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        using (var image = new Image())
        {
            image.CreateFromGif(path);

            path = string.Format("{0}/gif/bug00066_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                Assert.Fail();
            }
        }
    }
}

