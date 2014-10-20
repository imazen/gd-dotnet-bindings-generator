using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdimagepixelate
{
    public const int WIDTH = 12;
    public const int BLOCK_SIZE = 4;

    private static readonly int[,] expected_upperleft =
    {
        { 0x000000, 0x040404, 0x080808 },
        { 0x303030, 0x343434, 0x383838 },
        { 0x606060, 0x646464, 0x686868 }
    };

    private static readonly int[,] expected_average =
    {
        { 0x131313, 0x171717, 0x1b1b1b },
        { 0x434343, 0x474747, 0x4b4b4b },
        { 0x737373, 0x777777, 0x7b7b7b },
    };

    [Test]
    public void TestGdImagePixelate()
    {
        gdImageStruct im = gd.gdImageCreate(WIDTH, WIDTH);
        if (testPixelate(im) == 0)
        {
            Assert.Fail("Error: {0}", 1);
        }
        gd.gdImageDestroy(im);

        im = gd.gdImageCreateTrueColor(WIDTH, WIDTH);
        if (testPixelate(im) == 0)
        {
            Assert.Fail("Error: {0}", 2);
        }
        gd.gdImageDestroy(im);
    }

    [Test]
    public void TestGdImagePixelateCpp()
    {
        using (var image = new Image(WIDTH, WIDTH))
        {
            if (testPixelateCpp(image) == 0)
            {
                Assert.Fail("Error: {0}", 1);
            }

            image.CreateTrueColor(WIDTH, WIDTH);
            if (testPixelateCpp(image) == 0)
            {
                Assert.Fail("Error: {0}", 2);
            }
        }
    }

    private static int testPixelate(gdImageStruct im)
	{
		if (gd.gdImagePixelate(im, -1, (uint) gdPixelateMode.GD_PIXELATE_UPPERLEFT) != 0)
			return 0;
		if (gd.gdImagePixelate(im, 1, (uint) gdPixelateMode.GD_PIXELATE_UPPERLEFT) != 1)
			return 0;
		if (gd.gdImagePixelate(im, 2, uint.MaxValue) != 0)
			return 0;

		SetupPixels(im);
		if (gd.gdImagePixelate(im, BLOCK_SIZE, (uint) gdPixelateMode.GD_PIXELATE_UPPERLEFT) == 0)
			return 0;
	    CheckPixels(im, expected_upperleft);

        SetupPixels(im);
		if (gd.gdImagePixelate(im, BLOCK_SIZE, (uint) gdPixelateMode.GD_PIXELATE_AVERAGE) == 0)
			return 0;

	    CheckPixels(im, expected_average);

		return 1;
	}

    private static int testPixelateCpp(Image image)
    {
        if (image.Pixelate(-1, gdPixelateMode.GD_PIXELATE_UPPERLEFT))
            return 0;
        if (!image.Pixelate(1, gdPixelateMode.GD_PIXELATE_UPPERLEFT))
            return 0;
        if (image.Pixelate(2, (gdPixelateMode) int.MaxValue))
            return 0;

        SetupPixelsCpp(image);
        if (!image.Pixelate(BLOCK_SIZE, (uint) gdPixelateMode.GD_PIXELATE_UPPERLEFT))
            return 0;
        CheckPixelsCpp(image, expected_upperleft);

        SetupPixelsCpp(image);
        if (!image.Pixelate(BLOCK_SIZE, gdPixelateMode.GD_PIXELATE_AVERAGE))
            return 0;

        CheckPixelsCpp(image, expected_average);

        return 1;
    }

    private static void CheckPixels(gdImageStruct im, int[,] expected)
    {
        do
        {
            for (int y = 0; y < im.sy; y++)
            {
                for (int x = 0; x < im.sx; x++)
                {
                    int p = gd.gdImageGetPixel(im, x, y);
                    int r = (expected[y/BLOCK_SIZE, x/BLOCK_SIZE] >> 16) & 0xFF;
                    int g = (expected[y/BLOCK_SIZE, x/BLOCK_SIZE] >> 8) & 0xFF;
                    int b = (expected[y/BLOCK_SIZE, x/BLOCK_SIZE]) & 0xFF;
                    if (r != (im.trueColor != 0 ? (p & 0xFF0000) >> 16 : im.red[p]))
                    {
                        Assert.Fail("Red {0:x} is expected, but {1:x}\n", r, im.trueColor != 0 ? (p & 0xFF0000) >> 16 : im.red[p]);
                    }
                    if (g != (im.trueColor != 0 ? (p & 0x00FF00) >> 8 : im.green[p]))
                    {
                        Assert.Fail("Green {0:x} is expected, but {1:x}\n", g, im.trueColor != 0 ? (p & 0x00FF00) >> 8 : im.green[p]);
                    }
                    if (b != (im.trueColor != 0 ? p & 0x0000FF : im.blue[p]))
                    {
                        Assert.Fail("Blue {0:x} is expected, but {1:x}\n", b,
                            im.trueColor != 0 ? p & 0x0000FF : im.blue[p]);
                    }
                }
            }
        } while (false);
    }

    private static void CheckPixelsCpp(Image image, int[,] expected)
    {
        do
        {
            for (int y = 0; y < image.SY(); y++)
            {
                for (int x = 0; x < image.SX(); x++)
                {
                    int p = image.GetPixel(x, y);
                    int r = (expected[y / BLOCK_SIZE, x / BLOCK_SIZE] >> 16) & 0xFF;
                    int g = (expected[y / BLOCK_SIZE, x / BLOCK_SIZE] >> 8) & 0xFF;
                    int b = (expected[y / BLOCK_SIZE, x / BLOCK_SIZE]) & 0xFF;
                    if (r != (image.IsTrueColor() ? (p & 0xFF0000) >> 16 : image.Red(p)))
                    {
                        Assert.Fail("Red {0:x} is expected, but {1:x}\n", r, image.IsTrueColor() ? (p & 0xFF0000) >> 16 : image.Red(p));
                    }
                    if (g != (image.IsTrueColor() ? (p & 0x00FF00) >> 8 : image.Green(p)))
                    {
                        Assert.Fail("Green {0:x} is expected, but {1:x}\n", g, image.IsTrueColor() ? (p & 0x00FF00) >> 8 : image.Green(p));
                    }
                    if (b != (image.IsTrueColor() ? p & 0x0000FF : image.Blue(p)))
                    {
                        Assert.Fail("Blue {0:x} is expected, but {1:x}\n", b,
                            image.IsTrueColor() ? p & 0x0000FF : image.Blue(p));
                    }
                }
            }
        } while (false);
    }

    private static void SetupPixels(gdImageStruct im)
    {
        do
        {
            int i = 0;
            for (int y = 0; y < im.sy; y++)
            {
                for (int x = 0; x < im.sx; x++)
                {
                    int p = gd.gdImageColorResolve(im, i, i, i);
                    gd.gdImageSetPixel(im, x, y, p);
                    i++;
                }
            }
        } while (false);
    }

    private static void SetupPixelsCpp(Image image)
    {
        do
        {
            int x;
            int y;
            int i = 0;
            for (y = 0; y < image.SX(); y++)
            {
                for (x = 0; x < image.SX(); x++)
                {
                    int p = image.ColorResolve(i, i, i);
                    image.SetPixel(x, y, p);
                    i++;
                }
            }
        } while (false);
    }
}

