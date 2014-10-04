using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00011
{
    [Test]
    public void TestBug00011()
	{
		gdImageStruct im;
        string path = new string(new char[2048]);

		path = string.Format("{0}/png/emptyfile", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
        im = gd.gdImageCreateFromPng(path);

	    if (im != null)
	    {
	        Assert.Fail();
	    }
	}
}

