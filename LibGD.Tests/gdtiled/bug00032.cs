using System.IO;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00032
{
    private const string exp_img = "bug00032_exp.png";

    [Test]
    public void TestBug00032()
	{
		gdImageStruct im;
		gdImageStruct tile;
        string path = Path.Combine(GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, "gdtiled", exp_img);

        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

		tile = gd.gdImageCreateTrueColor(10, 10);
		gd.gdImageFill(tile, 0, 0, 0xFFFFFF);
		gd.gdImageLine(tile, 0,0, 9,9, 0xff0000);
		gd.gdImageColorTransparent(tile, 0xFFFFFF);

		im = gd.gdImageCreateTrueColor(50, 50);
		gd.gdImageFilledRectangle(im, 0, 0, 25, 25, 0x00FF00);

		gd.gdImageSetTile(im, tile);
		gd.gdImageFilledRectangle(im, 10, 10, 49, 49, GlobalMembersGdtest.DefineConstants.gdTiled);

        if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
		{
			gd.gdImageDestroy(im);
			gd.gdImageDestroy(tile);
			Assert.Fail();
		}

		gd.gdImageDestroy(im);
		gd.gdImageDestroy(tile);
	}
}

