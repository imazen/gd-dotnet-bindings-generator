using System.IO;
using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00032
{
    private const string exp_img = "bug00032_exp.png";

    [Test]
    public void TestBug00032()
	{

        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

		gdImageStruct tile = gd.gdImageCreateTrueColor(10, 10);
		gd.gdImageFill(tile, 0, 0, 0xFFFFFF);
		gd.gdImageLine(tile, 0,0, 9,9, 0xff0000);
		gd.gdImageColorTransparent(tile, 0xFFFFFF);

		gdImageStruct im = gd.gdImageCreateTrueColor(50, 50);
		gd.gdImageFilledRectangle(im, 0, 0, 25, 25, 0x00FF00);

		gd.gdImageSetTile(im, tile);
		gd.gdImageFilledRectangle(im, 10, 10, 49, 49, GlobalMembersGdtest.DefineConstants.gdTiled);

        string path = Path.Combine(GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, "gdtiled", exp_img);
        if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, im) == 0)
		{
			gd.gdImageDestroy(im);
			gd.gdImageDestroy(tile);
			Assert.Fail();
		}

		gd.gdImageDestroy(im);
		gd.gdImageDestroy(tile);
	}

    [Test]
    public void TestBug00032Cpp()
    {

        //gd.gdSetErrorMethod(GlobalMembersGdtest.gdSilence);

        using (var tile = new Image(10, 10, true))
        {
            tile.Fill(0, 0, 0xFFFFFF);
            tile.Line(0, 0, 9, 9, 0xff0000);
            tile.ColorTransparent(0xFFFFFF);

            using (var image = new Image(50, 50, true))
            {
                image.FilledRectangle(0, 0, 25, 25, 0x00FF00);

                image.SetTile(tile);
                image.FilledRectangle(10, 10, 49, 49, GlobalMembersGdtest.DefineConstants.gdTiled);

                string path = Path.Combine(GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, "gdtiled", exp_img);
                if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
                {
                    Assert.Fail();
                }
            }
        }
    }
}

