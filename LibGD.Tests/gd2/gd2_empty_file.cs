using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGd2_empty_file
{
    [Test]
    public void TestGd2EmptyFile()
	{
        var path = string.Format("{0}/gd2/empty.gd2", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		var im = gd.gdImageCreateFromGd2(path);
        if (im != null)
        {
            gd.gdImageDestroy(im);
            Assert.Fail();
        }
	}

    [Test]
    public void TestGd2EmptyFileCpp()
    {
        using (var image = new Image())
        {
            var path = string.Format("{0}/gd2/empty.gd2", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (image.CreateFromGd2(path))
            {
                Assert.Fail();
            }
        }
    }
}

