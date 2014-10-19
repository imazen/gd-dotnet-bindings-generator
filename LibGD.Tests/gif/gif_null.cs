using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGif_null
{
    [Test]
    public void TestGif_null()
	{
        gdImageStruct im = gd.gdImageCreateFromGif((string) null);
		if (im != null)
		{
			gd.gdImageDestroy(im);
			Assert.Fail();
		}
        gd.gdImageGif(im, null); // noop safely
	}

    [Test]
    public void TestGif_nullCpp()
    {
        using (var image = new Image())
        {
            image.CreateFromGif(null);
            if (image.good())
            {
                Assert.Fail();
            }
            image.Gif((string) null); // noop safely
        }
    }
}

