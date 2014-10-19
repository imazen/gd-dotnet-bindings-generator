using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGd_num_colors
{
    [Test]
    public void TestGdNumColors()
	{
        var path = string.Format("{0}/gd/crafted_num_colors.gd", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		var im = gd.gdImageCreateFromGd(path);
		if (im != null)
		{
			gd.gdImageDestroy(im);
            Assert.Fail();
		}
	}

    [Test]
    public void TestGdNumColorsCpp()
    {
        using (var image = new Image())
        {
            var path = string.Format("{0}/gd/crafted_num_colors.gd", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (image.CreateFromGd(path))
            {
                Assert.Fail();
            }
        }
    }
}

