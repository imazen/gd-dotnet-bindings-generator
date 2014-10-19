using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00011
{
    [Test]
    public void TestBug00011()
	{
        string path = string.Format("{0}/png/emptyfile", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
        gdImageStruct im = gd.gdImageCreateFromPng(path);

	    if (im != null)
	    {
	        Assert.Fail();
	    }
	}

    [Test]
    public void TestBug00011Cpp()
    {
        using (var image = new Image())
        {
            string path = string.Format("{0}/png/emptyfile", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (image.CreateFromPng(path))
            {
                Assert.Fail();
            }
        }
    }
}

