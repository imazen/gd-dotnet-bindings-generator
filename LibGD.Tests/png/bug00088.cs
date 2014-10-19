using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00088
{
    [Test]
    public void TestBug00088()
	{
        string[] files = {"bug00088_1.png", "bug00088_2.png"};
		string[] files_exp = {"bug00088_1_exp.png", "bug00088_2_exp.png"};

		int i;
		const int cnt = 2;
		int error = 0;

		for (i = 0; i < cnt; i++)
		{
			string path = string.Format("{0}/png/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, files[i]);

		    gdImageStruct im = gd.gdImageCreateFromPng(path);

			if (im == null)
			{
				error |= 1;
				continue;
			}

			path = string.Format("{0}/png/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, files_exp[i]);
			if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, im) == 0)
			{
				error |= 1;
			}
			gd.gdImageDestroy(im);
		}
        if (error != 0)
        {
            Assert.Fail("Error: {0}.", error);
        }
	}

    [Test]
    public void TestBug00088Cpp()
    {
        string[] files = { "bug00088_1.png", "bug00088_2.png" };
        string[] files_exp = { "bug00088_1_exp.png", "bug00088_2_exp.png" };

        int i;
        const int cnt = 2;
        int error = 0;

        for (i = 0; i < cnt; i++)
        {
            string path = string.Format("{0}/png/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, files[i]);

            using (var image = new Image())
            {
                if (!image.CreateFromPng(path))
                {
                    error |= 1;
                    continue;
                }

                path = string.Format("{0}/png/{1}", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR, files_exp[i]);
                if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
                {
                    error |= 1;
                }
            }
        }
        if (error != 0)
        {
            Assert.Fail("Error: {0}.", error);
        }
    }
}

