using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_empty_file
{
    [Test]
    public void TestJpeg_empty_file()
	{
        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

		string path = string.Format("{0}/jpeg/empty.jpeg", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

		gdImageStruct im = gd.gdImageCreateFromJpeg(path);

        if (im != null)
        {
            gd.gdImageDestroy(im);
            Assert.Fail();
        }
	}

    [Test]
    public void TestJpeg_empty_fileCpp()
    {
        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

        string path = string.Format("{0}/jpeg/empty.jpeg", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        using (var image = new Image())
        {
            image.CreateFromJpeg(path);

            if (image.good())
            {
                Assert.Fail();
            }
        }
    }
}

