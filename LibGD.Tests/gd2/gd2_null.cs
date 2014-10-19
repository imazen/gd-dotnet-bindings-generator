using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGd2_null
{
    [Test]
    public void TestGd2Null()
	{
		gdImageStruct im;

		im = gd.gdImageCreateFromGd2((string) null);
		if (im != null)
		{
			gd.gdImageDestroy(im);
			Assert.Fail();
		}
		gd.gdImageGd2(im, null, 0, GlobalMembersGdtest.DefineConstants.GD2_FMT_RAW); // noop safely
	}

    [Test]
    public void TestGd2NullCpp()
    {
        using (var image = new Image())
        {
            if (image.CreateFromGd2(null))
            {
                Assert.Fail();            
            }
            image.Gd2(null, 0, GlobalMembersGdtest.DefineConstants.GD2_FMT_RAW); // noop safely
        }
    }
}

