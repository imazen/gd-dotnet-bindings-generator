using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGd2_empty_file
{
    [Test]
    public void TestGd2EmptyFile()
	{
		gdImageStruct im;
        string path = new string(new char[1024]);

		path = string.Format("{0}/gd2/empty.gd2", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		im = gd.gdImageCreateFromGd2(path);

		if (im == null)
		{
			return;
		}
	    gd.gdImageDestroy(im);
	    Assert.Fail();
	}
}

