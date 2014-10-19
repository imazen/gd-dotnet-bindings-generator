using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGdimagecolortransparent
{
    [Test]
    public void TestGdImageColorTransparent()
	{
        int error = 0;

        int pos = GlobalMembersGdtest.DefineConstants.gdMaxColors;

		gdImageStruct im = gd.gdImageCreate(1,1);

		gd.gdImageColorTransparent(im, pos);

		if (im.transparent == pos)
		{
			error = -1;
		}

		pos = -2;

		gd.gdImageColorTransparent(im, pos);

		if (im.transparent == pos)
		{
			error = -1;
		}

		gd.gdImageDestroy(im);

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
	}

    [Test]
    public void TestGdImageColorTransparentCpp()
    {
        int error = 0;

        int pos = GlobalMembersGdtest.DefineConstants.gdMaxColors;

        using (var image = new Image(1, 1))
        {
            image.ColorTransparent(pos);

            if (image.GetTransparent() == pos)
            {
                error = -1;
            }

            pos = -2;

            image.ColorTransparent(pos);

            if (image.GetTransparent() == pos)
            {
                error = -1;
            }
        }

        if (error != 0)
        {
            Assert.Fail("Error: {0}", error);
        }
    }
}

