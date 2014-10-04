using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersColor_name
{
    [Test]
    public void TestColor_name()
	{
		gdImageStruct im;
		string path = new string(new char[1024]);
		int c;
		int result;

		path = string.Format("{0}/xpm/color_name.xpm", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		im = gd.gdImageCreateFromXpm(path);
		if (im == null)
		{
			return 2;
		}
		c = gd.gdImageGetPixel(im, 2, 2);
		if (((im).trueColor != 0 ? (((c) & 0xFF0000) >> 16) : (im).red[(c)]) == 0xFF && ((im).trueColor != 0 ? (((c) & 0x00FF00) >> 8) : (im).green[(c)]) == 0xFF && ((im).trueColor != 0 ? ((c) & 0x0000FF) : (im).blue[(c)]) == 0x0)
		{
			result = 0;
		}
		else
		{
			result = 1;
		}
		gd.gdImageDestroy(im);
		return result;
	}
}

