using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersPng_null
{
	[Test]
	public void TestPng_null()
	{
		gdImageStruct im;

		//gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

		im = gd.gdImageCreateFromPng((string) null);
		if (im != null)
		{
			gd.gdImageDestroy(im);
			Assert.Fail();
		}
		gd.gdImagePng(im, null); // noop safely
	}

    [Test]
    public void TestPng_nullCpp()
    {
        using (var image = new Image())
        {
            if (image.CreateFromPng(null))
            {
                Assert.Fail();
            }
            image.Png((string) null); // noop safely
        }
    }
}

