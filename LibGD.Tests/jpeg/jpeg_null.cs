using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_null
{
    [Test]
    public void TestJpeg_null()
	{
        gdImageStruct im = gd.gdImageCreateFromJpeg((string) null);
		if (im != null)
		{
			gd.gdImageDestroy(im);
			Assert.Fail();
		}
        gd.gdImageJpeg(im, null, 100); // noop safely
	}

    [Test]
    public void TestJpeg_nullCpp()
    {
        var image = new Image();
        image.CreateFromJpeg((string) null);
        if (image.good())
        {
            Assert.Fail();
        }
        image.Jpeg((string) null, 100); // noop safely
    }
}

