using LibGD;
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
		gdInterpolationMethod method;
		int i;

		for (method = gdInterpolationMethod.GD_BELL; method <= gdInterpolationMethod.GD_TRIANGLE; method++) // GD_WEIGHTED4 is unsupported.
		{
			gdImageStruct[] im = new gdImageStruct[2];

			// printf("Method = %d\n", method);
			im[0] = gd.gdImageCreateTrueColor(X, Y);
			im[1] = gd.gdImageCreate(X, Y);

			for (i = 0; i < 2; i++)
			{
				gdImageStruct result;

				// printf("    %s\n", i == 0 ? "truecolor" : "palette");

				gd.gdImageFilledRectangle(im[i], 0, 0, X - 1, Y - 1, gd.gdImageColorExactAlpha(im[i], 255, 255, 255, 0));

				gd.gdImageSetInterpolationMethod(im[i], method);
				GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (im[i].interpolation_id == method) ? 1: 0);

				result = gd.gdImageScale(im[i], NX, NY);
				GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (result != null) ? 1 : 0);
				GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (result != im[i]) ? 1 : 0);
				GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (result != null && result.sx == NX && result.sy == NY) ? 1 : 0);

				gd.gdImageDestroy(result);
				gd.gdImageDestroy(im[i]);
			} // for
		} // for


		Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
	} // main
}

