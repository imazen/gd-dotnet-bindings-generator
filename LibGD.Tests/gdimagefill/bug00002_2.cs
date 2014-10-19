using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00002_2
{
    [Test]
    public void TestBug00002_2()
	{
        int error = 0;

        /*	fputs("flag 0\n", stdout); */
		gdImageStruct im = gd.gdImageCreate(150, 150);
		gdImageStruct tile = gd.gdImageCreate(36, 36);
		gd.gdImageColorAllocate(tile,255,255,255); // allocate white for background color
		int tile_black = gd.gdImageColorAllocate(tile,0,0,0);
		gd.gdImageColorAllocate(im,255,255,255); // allocate white for background color
		int im_black = gd.gdImageColorAllocate(im,0,0,0);


		/* create the dots pattern */
		for (int x = 0; x < 36; x += 2)
		{
		    int y;
		    for (y = 0; y < 36; y += 2)
			{
				gd.gdImageSetPixel(tile,x,y,tile_black);
			}
		}

        gd.gdImageSetTile(im,tile);
		gd.gdImageRectangle(im, 9,9,139,139, im_black);
		gd.gdImageLine(im, 9,9,139,139, im_black);
		gd.gdImageFill(im, 11,12, GlobalMembersGdtest.DefineConstants.gdTiled);


	/*	fputs("flag 1\n", stdout); */
		gd.gdImageFill(im, 0, 0, 0xffffff);
	/*	fputs("flag 2\n", stdout); */
		gd.gdImageFill(im, 0, 0, 0xffffff);
	/*	fputs("flag 2\n", stdout); */


		string path = string.Format("{0}/gdimagefill/bug00002_2_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
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
    public void TestBug00002_2Cpp()
    {
        int error = 0;

        /*	fputs("flag 0\n", stdout); */
        using (var image = new Image(150, 150))
        {
            int im_black;
            using (var tile = new Image(36, 36))
            {
                tile.ColorAllocate(255, 255, 255); // allocate white for background color
                int tile_black = tile.ColorAllocate(0, 0, 0);
                image.ColorAllocate(255, 255, 255); // allocate white for background color
                im_black = image.ColorAllocate(0, 0, 0);


                /* create the dots pattern */
                for (int x = 0; x < 36; x += 2)
                {
                    int y;
                    for (y = 0; y < 36; y += 2)
                    {
                        tile.SetPixel(x, y, tile_black);
                    }
                }

                image.SetTile(tile);
                image.Rectangle(9, 9, 139, 139, im_black);
                image.Line(9, 9, 139, 139, im_black);
                image.Fill(11, 12, GlobalMembersGdtest.DefineConstants.gdTiled);

                /*	fputs("flag 1\n", stdout); */
                image.Fill(0, 0, 0xffffff);
                /*	fputs("flag 2\n", stdout); */
                image.Fill(0, 0, 0xffffff);
                /*	fputs("flag 2\n", stdout); */

                string path = string.Format("{0}/gdimagefill/bug00002_2_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
                if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
                {
                    error = 1;
                }
            }
        }

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }
}

