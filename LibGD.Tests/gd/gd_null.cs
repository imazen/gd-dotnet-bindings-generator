using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGd_null
{
    [Test]
    public void TestGdNull()
	{
		gdImageStruct im;

        im = gd.gdImageCreateFromGd((string) null);
		if (im != null)
		{
			gd.gdImageDestroy(im);
		    Assert.Fail();
		}
        gd.gdImageGd(im, null); // noop safely
	}

    [Test]
    public void TestGdNullCpp()
    {
        using (var image = new Image())
        {
            if (image.CreateFromGd(null))
            {
                Assert.Fail();
            }
        }
    }
}

