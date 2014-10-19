using System;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdimagefilledpolygon0
{
    [Test]
    public void TestGdImageFilledPolygon0()
	{
        gdImageStruct im = gd.gdImageCreate(100, 100);
		if (im == null)
			Environment.Exit(1);
		int white = gd.gdImageColorAllocate(im, 0xff, 0xff, 0xff);
		int black = gd.gdImageColorAllocate(im, 0, 0, 0);
		gd.gdImageFilledRectangle(im, 0, 0, 99, 99, white);
		gd.gdImageFilledPolygon(im, null, 0, black); // no effect
		gd.gdImageFilledPolygon(im, null, -1, black); // no effect
		int r = GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR + "/gdimagefilledpolygon/gdimagefilledpolygon0.png", im);
		gd.gdImageDestroy(im);
		if (r == 0)
			Assert.Fail();
	}

    [Test]
    public void TestGdImageFilledPolygon0Cpp()
    {
        int r;
        using (var image = new Image(100, 100))
        {
            if (!image.good())
                Assert.Fail();
            int white = image.ColorAllocate(0xff, 0xff, 0xff);
            int black = image.ColorAllocate(0, 0, 0);
            image.FilledRectangle(0, 0, 99, 99, white);
            image.FilledPolygon((Point) null, 0, black); // no effect
            image.FilledPolygon((Point) null, -1, black); // no effect
            r = GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR + "/gdimagefilledpolygon/gdimagefilledpolygon0.png", image);
        }
        if (r == 0)
            Assert.Fail();
    }
}

