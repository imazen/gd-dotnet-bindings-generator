using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_empty_file
{
    [Test]
    public void TestJpeg_empty_file()
	{
		gdImageStruct im;
        string path = new string(new char[1024]);

        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

		path = string.Format("{0}/jpeg/empty.jpeg", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

		im = gd.gdImageCreateFromJpeg(path);

        if (im != null)
        {
            gd.gdImageDestroy(im);
            Assert.Fail();
        }
	}
}

