using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdModesAndPalettes
{
	public const int X = 100;
	public const int Y = 100;

	public const int NX = 20;
	public const int NY = 20;

	[Test]
	public void TestGdModesAndPalettes()
	{
	    for (var method = gdInterpolationMethod.GD_BELL; method <= gdInterpolationMethod.GD_TRIANGLE; method++) // GD_WEIGHTED4 is unsupported.
		{
			var im = new gdImageStruct[2];

			// printf("Method = %d\n", method);
			im[0] = gd.gdImageCreateTrueColor(X, Y);
			im[1] = gd.gdImageCreate(X, Y);

		    for (int i = 0; i < 2; i++)
			{
			    // printf("    %s\n", i == 0 ? "truecolor" : "palette");

				gd.gdImageFilledRectangle(im[i], 0, 0, X - 1, Y - 1, gd.gdImageColorExactAlpha(im[i], 255, 255, 255, 0));

				gd.gdImageSetInterpolationMethod(im[i], method);
				GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", im[i].interpolation_id == method ? 1: 0);

				gdImageStruct result = gd.gdImageScale(im[i], NX, NY);
				GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", result != null ? 1 : 0);
				GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", result != im[i] ? 1 : 0);
				GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", result != null && result.sx == NX && result.sy == NY ? 1 : 0);

				gd.gdImageDestroy(result);
				gd.gdImageDestroy(im[i]);
			} // for
		} // for


		Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
    } // main

    [Test]
    public void TestGdModesAndPalettesCpp()
    {
        for (var method = gdInterpolationMethod.GD_BELL; method <= gdInterpolationMethod.GD_TRIANGLE; method++) // GD_WEIGHTED4 is unsupported.
        {
            var images = new Image[2];

            // printf("Method = %d\n", method);
            images[0] = new Image(X, Y, true);
            images[1] = new Image(X, Y);

            for (int i = 0; i < 2; i++)
            {
                // printf("    %s\n", i == 0 ? "truecolor" : "palette");

                images[i].FilledRectangle(0, 0, X - 1, Y - 1, images[i].ColorExact(255, 255, 255, 0));

                // this function is not exposed in the C++ wrapper
                gd.gdImageSetInterpolationMethod(images[i].GetPtr(), method);
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", images[i].GetPtr().interpolation_id == method ? 1 : 0);

                using (var result = new Image(gd.gdImageScale(images[i].GetPtr(), NX, NY)))
                {
                    GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", result.good() ? 1 : 0);
                    GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", result.GetPtr().__Instance != images[i].GetPtr().__Instance ? 1 : 0);
                    GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", result.good() && result.SX() == NX && result.SY() == NY ? 1 : 0);
                }
                images[i].Dispose();
            } // for
        } // for


        Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
    } // main
}

