using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00033
{
    [Test]
    public void TestBug00033()
	{
		gdImageStruct im;
        string path = new string(new char[1024]);

        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

		path = string.Format("{0}/png/bug00033.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        im = gd.gdImageCreateFromPng(path);

		if (im != null)
		{
			gd.gdImageDestroy(im);
            Assert.Fail();
		}
	}
}

