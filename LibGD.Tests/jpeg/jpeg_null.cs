using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersJpeg_null
{
    [Test]
    public void TestJpeg_null()
	{
		gdImageStruct im;

        im = gd.gdImageCreateFromJpeg(IntPtr.Zero);
		if (im != null)
		{
			gd.gdImageDestroy(im);
			Assert.Fail();
		}
        gd.gdImageJpeg(im, IntPtr.Zero, 100); // noop safely
	}
}

