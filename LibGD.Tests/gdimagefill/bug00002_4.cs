using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00002_4
{
    [Test]
    public void TestBug00002_4()
	{
        int error = 0;

        gdImageStruct im = gd.gdImageCreate(50,100);
		int red = gd.gdImageColorAllocate(im, 255, 0, 0);
		int blue = gd.gdImageColorAllocate(im, 0,0,255);
		int white = gd.gdImageColorAllocate(im, 255,255,255);
		int black = gd.gdImageColorAllocate(im, 0,0,0);
		gd.gdImageFill(im, 0,0, black);

		gd.gdImageLine(im, 20,20,180,20, white);
		gd.gdImageLine(im, 20,20,20,70, blue);
		gd.gdImageLine(im, 20,70,180,70, red);
		gd.gdImageLine(im, 180,20,180,45, white);
		gd.gdImageLine(im, 180,70,180,45, red);
		gd.gdImageLine(im, 20,20,100,45, blue);
		gd.gdImageLine(im, 20,70,100,45, blue);
		gd.gdImageLine(im, 100,45,180,45, red);

		gd.gdImageFill(im, 21,45, blue);
		gd.gdImageFill(im, 100,69, red);
		gd.gdImageFill(im, 100,21, white);

		string path = string.Format("{0}/gdimagefill/bug00002_4_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
		{
			error = 1;
		}

		/* Destroy it */
        gd.gdImageDestroy(im);
        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
	}

    [Test]
    public void TestBug00002_4Cpp()
    {
        int error = 0;

        using (var image = new Image(50, 100))
        {
            int red = image.ColorAllocate(255, 0, 0);
            int blue = image.ColorAllocate(0, 0, 255);
            int white = image.ColorAllocate(255, 255, 255);
            int black = image.ColorAllocate(0, 0, 0);
            image.Fill(0, 0, black);

            image.Line(20, 20, 180, 20, white);
            image.Line(20, 20, 20, 70, blue);
            image.Line(20, 70, 180, 70, red);
            image.Line(180, 20, 180, 45, white);
            image.Line(180, 70, 180, 45, red);
            image.Line(20, 20, 100, 45, blue);
            image.Line(20, 70, 100, 45, blue);
            image.Line(100, 45, 180, 45, red);

            image.Fill(21, 45, blue);
            image.Fill(100, 69, red);
            image.Fill(100, 21, white);

            string path = string.Format("{0}/gdimagefill/bug00002_4_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                error = 1;
            }
        }

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }
}

