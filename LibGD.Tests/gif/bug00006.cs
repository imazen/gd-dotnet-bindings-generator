using System;
using System.IO;
using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00006
{
    public const string TMP_FN = "_tmp_bug0006.gif";

    [Test]
    public void TestBug00006()
	{
		gdImageStruct im;
		IntPtr fp;
		const int r = 255;
		const int g = 0;
		const int b = 0;
		int r_f;
		int g_f;
		int b_f;
		int trans_c_f;
		int trans_c = (((0) << 24) + ((r) << 16) + ((g) << 8) + (b));
		int error = 0;

		im = gd.gdImageCreateTrueColor(192, 36);
		if (im == null)
		{
            GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image\n");
			Assert.Fail();
		}

		gd.gdImageColorTransparent(im, trans_c);
		gd.gdImageFilledRectangle(im, 0,0, 192,36, trans_c);

		fp = C.fopen(TMP_FN, "wb");
		if (fp == IntPtr.Zero)
		{
            GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot open <%s> for writing\n", TMP_FN);
            Assert.Fail();
		}
		gd.gdImageGif(im,fp);
		C.fclose(fp);

		gd.gdImageDestroy(im);
		im = gd.gdImageCreateFromGif(TMP_FN);

		if (im == null)
		{
            GlobalMembersGdtest.gdTestErrorMsg(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "Cannot create image from <%s>\n", TMP_FN);
            Assert.Fail();
		}

		trans_c_f = ((im).transparent);
        if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (trans_c_f == 1) ? 1 : 0) != 0)
		{
			r_f = ((im).trueColor != 0 ? (((trans_c_f) & 0xFF0000) >> 16) : (im).red[(trans_c_f)]);
			g_f = ((im).trueColor != 0 ? (((trans_c_f) & 0x00FF00) >> 8) : (im).green[(trans_c_f)]);
			b_f = ((im).trueColor != 0 ? ((trans_c_f) & 0x0000FF) : (im).blue[(trans_c_f)]);

            if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (r_f == r) ? 1 : 0) == 0 ||
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (g_f == g) ? 1 : 0) == 0 ||
                GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (b_f == b) ? 1 : 0) == 0)
			{
				error = 1;
			}
		}
		else
		{
			error = 1;
		}

		/* Destroy it */
		gd.gdImageDestroy(im);
		File.Delete(TMP_FN);
        Assert.AreEqual(0, error);
	}
}

