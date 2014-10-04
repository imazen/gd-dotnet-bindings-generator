using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersTga_null
{
    [Test]
    public void TestTga_null()
	{
		gdImageStruct im;

		im = gd.gdImageCreateFromTga((string) null);
		if (im != null)
		{
			gd.gdImageDestroy(im);
			Assert.Fail();
		}
	}
}

