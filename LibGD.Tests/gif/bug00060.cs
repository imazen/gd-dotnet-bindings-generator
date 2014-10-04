using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00060
{
    [Test]
    public void TestBug00060()
	{
		gdImageStruct im;
        string path = new string(new char[1024]);

		path = string.Format("{0}/gif/bug00060.gif", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

		im = gd.gdImageCreateFromGif(path);
		gd.gdImageDestroy(im);
	}
}

