using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00005
{
    [Test]
    public void TestBug00005()
	{
        string[] giffiles = {"bug00005_0.gif", "bug00005_1.gif", "bug00005_2.gif", "bug00005_3.gif"};
		int[] valid = {0, 0, 0, 0};
		string[] exp = {null, null, "bug00005_2_exp.png", null};
		const int files_cnt = 4;
        int i;
		int error = 0;

        for (i = 0; i < files_cnt; i++)
		{
			string path = string.Format("{0}/gif/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, giffiles[i]);

			gdImageStruct im = gd.gdImageCreateFromGif(path);

			if (valid[i] != 0)
			{
				if (im == null)
				{
					error = 1;
				}
				else
				{
					path = string.Format("{0}/gif/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, exp[i]);
					if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, (path), (im)) == 0)
					{
						error = 1;
					}
					gd.gdImageDestroy(im);
				}
			}
			else
			{
				if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (im == null) ? 1 : 0) == 0)
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

    [Test]
    public void TestBug00005Cpp()
    {
        string[] giffiles = { "bug00005_0.gif", "bug00005_1.gif", "bug00005_2.gif", "bug00005_3.gif" };
        int[] valid = { 0, 0, 0, 0 };
        string[] exp = { null, null, "bug00005_2_exp.png", null };
        const int files_cnt = 4;
        int i;
        int error = 0;

        for (i = 0; i < files_cnt; i++)
        {
            string path = string.Format("{0}/gif/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, giffiles[i]);

            var image = new Image();
            image.CreateFromGif(path);

            if (valid[i] != 0)
            {
                if (!image.good())
                {
                    error = 1;
                }
                else
                {
                    path = string.Format("{0}/gif/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, exp[i]);
                    if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
                    {
                        error = 1;
                    }
                }
            }
            else
            {
                if (GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", !image.good() ? 1 : 0) == 0)
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

