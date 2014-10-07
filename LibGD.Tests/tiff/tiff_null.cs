using LibGD;
using NUnit.Framework;

#if !NO_TIFF

[TestFixture]
public class GlobalMembersTiff_null
{
    [Test]
    public void TestTiff_null()
	{
		gdImageStruct im;

		im = gd.gdImageCreateFromTiff((string) null);
		if (im != null)
		{
			gd.gdImageDestroy(im);
			Assert.Fail();
		}
		gd.gdImageTiff(im, null); // noop safely
	}
}

#endif
