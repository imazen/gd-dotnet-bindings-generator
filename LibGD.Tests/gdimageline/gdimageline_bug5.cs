using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdimageline_bug5
{
    [Test]
    public void TestGdImageLine_bug5()
	{
		/* Declare the image */

        /* Declare output files */
		/* FILE *pngout; */

        gdImageStruct im = gd.gdImageCreateTrueColor(63318, 771);

		/* Allocate the color white (red, green and blue all maximum). */
		int white = gd.gdImageColorAllocate(im, 255, 255, 255);
		/* Allocate the color white (red, green and blue all maximum). */
		int black = gd.gdImageColorAllocate(im, 0, 0, 0);

		/* white background */
		gd.gdImageFill(im, 1, 1, white);

		/* Make a reference copy. */
		gdImageStruct @ref = gd.gdImageClone(im);

		gd.gdImageSetAntiAliased(im, black);

		/* This line used to fail. */
		gd.gdImageLine(im, 28562, 631, 34266, 750, GlobalMembersGdtest.DefineConstants.gdAntiAliased);

		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.gdMaxPixelDiff(im, @ref) > 0) ? 1 : 0);

	#if false
	//    {
	//        FILE *pngout;
	//
	// /* Open a file for writing. "wb" means "write binary",
	//  * important under MSDOS, harmless under Unix. */
	//        pngout = fopen("test.png", "wb");
	//
	// /* Output the image to the disk file in PNG format. */
	//        gd.gdImagePng(im, pngout);
	//
	// /* Close the files. */
	//        fclose(pngout);
	//    }
	#endif

		/* Destroy the image in memory. */
		gd.gdImageDestroy(im);
		gd.gdImageDestroy(@ref);

		Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
	}

    [Test]
    public void TestGdImageLine_bug5Cpp()
    {
        /* Declare the image */

        /* Declare output files */
        /* FILE *pngout; */

        using (var image = new Image(63318, 771, true))
        {
            int white = image.ColorAllocate(255, 255, 255);
            /* Allocate the color white (red, green and blue all maximum). */
            int black = image.ColorAllocate(0, 0, 0);

            /* white background */
            image.Fill(1, 1, white);

            /* Make a reference copy. */
            // this function is not exposed in the C++ wrapper
            using (var @ref = new Image(gd.gdImageClone(image.GetPtr())))
            {
                image.SetAntiAliased(black);

                /* This line used to fail. */
                image.Line(28562, 631, 34266, 750, GlobalMembersGdtest.DefineConstants.gdAntiAliased);

                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", GlobalMembersGdtest.gdMaxPixelDiff(image, @ref) > 0 ? 1 : 0);
            }
        }

#if false
	//    {
	//        FILE *pngout;
	//
	// /* Open a file for writing. "wb" means "write binary",
	//  * important under MSDOS, harmless under Unix. */
	//        pngout = fopen("test.png", "wb");
	//
	// /* Output the image to the disk file in PNG format. */
	//        gd.gdImagePng(im, pngout);
	//
	// /* Close the files. */
	//        fclose(pngout);
	//    }
#endif

        /* Destroy the image in memory. */

        Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
    }
}

