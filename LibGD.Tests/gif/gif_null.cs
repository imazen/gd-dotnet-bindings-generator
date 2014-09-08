using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGif_null
{
    [Test]
    public void TestGif_null()
	{
		gdImageStruct im;

        im = gd.gdImageCreateFromGif(IntPtr.Zero);
		if (im != null)
		{
			gd.gdImageDestroy(im);
			Assert.Fail();
		}
        gd.gdImageGif(im, IntPtr.Zero); // noop safely
	}
}

