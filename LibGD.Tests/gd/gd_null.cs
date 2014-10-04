using System;
using LibGD;
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
        gd.gdImageGd(im, IntPtr.Zero); // noop safely
	}
}

