using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_read
{
    [Test]
    public void TestJpeg_read()
	{
		gdImageStruct im;
        string path = new string(new char[1024]);

		path = string.Format("{0}/jpeg/conv_test.jpeg", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        im = gd.gdImageCreateFromJpeg(path);

		if (im == null)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "gdImageCreateFromJpeg failed.\n");
            Assert.Fail();
		}
		path = string.Format("{0}/jpeg/conv_test_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
		{
			GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "gdAssertImageEqualsToFile failed: <%s>.\n", path);
			gd.gdImageDestroy(im);
            Assert.Fail();
		}
	}
}

