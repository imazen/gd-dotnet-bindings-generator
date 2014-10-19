using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00033
{
    [Test]
    public void TestBug00033()
	{
        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

		string path = string.Format("{0}/png/bug00033.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        gdImageStruct im = gd.gdImageCreateFromPng(path);

		if (im != null)
		{
			gd.gdImageDestroy(im);
            Assert.Fail();
		}
	}

    [Test]
    public void TestBug00033Cpp()
    {
        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);
        using (var image = new Image())
        {
            string path = string.Format("{0}/png/bug00033.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (image.CreateFromPng(path))
            {
                Assert.Fail();
            }
        }
    }
}

