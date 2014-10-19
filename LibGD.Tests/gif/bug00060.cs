using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00060
{
    [Test]
    public void TestBug00060()
	{
        string path = string.Format("{0}/gif/bug00060.gif", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

		gdImageStruct im = gd.gdImageCreateFromGif(path);
		gd.gdImageDestroy(im);
	}

    [Test]
    public void TestBug00060Cpp()
    {
        string path = string.Format("{0}/gif/bug00060.gif", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);

        using (var image = new Image())
        {
            image.CreateFromGif(path);
        }
    }
}

