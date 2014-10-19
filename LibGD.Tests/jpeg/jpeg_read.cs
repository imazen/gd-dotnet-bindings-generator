using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_read
{
    [Test]
    public void TestJpeg_read()
	{
        string path = string.Format("{0}/jpeg/conv_test.jpeg", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        gdImageStruct im = gd.gdImageCreateFromJpeg(path);

		if (im == null)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "gdImageCreateFromJpeg failed.");
            Assert.Fail();
		}
		path = string.Format("{0}/jpeg/conv_test_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, im) == 0)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "gdAssertImageEqualsToFile failed: <%s>.", path);
			gd.gdImageDestroy(im);
            Assert.Fail();
		}
	}

    [Test]
    public void TestJpeg_readCpp()
    {
        string path = string.Format("{0}/jpeg/conv_test.jpeg", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        using (var image = new Image())
        {
            image.CreateFromJpeg(path);

            if (!image.good())
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "gdImageCreateFromJpeg failed.");
                Assert.Fail();
            }
            path = string.Format("{0}/jpeg/conv_test_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "gdAssertImageEqualsToFile failed: <%s>.", path);
                Assert.Fail();
            }
        }
    }
}

