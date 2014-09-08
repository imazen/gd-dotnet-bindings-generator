using System;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBmp_null
{
    [Test]
	public void TestBmpNull()
	{
        gdImageStruct im = gd.gdImageCreateFromBmp(IntPtr.Zero);
		if (im != null)
		{
			gd.gdImageDestroy(im);
            Assert.Fail("gdImageCreateFromBmp returns non-null when passed null.");
		}
        gd.gdImageBmp(im, IntPtr.Zero, 0); // noop safely
	}
}

