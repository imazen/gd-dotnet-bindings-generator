using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGd2_read
{
    [Test]
    public void TestGd2Read()
	{
        string path = string.Format("{0}/gd2/conv_test.gd2", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		gdImageStruct im = gd.gdImageCreateFromGd2(path);

		path = string.Format("{0}/gd2/conv_test_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
		if (GlobalMembersGdtest.gdTestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, im) == 0)
		{
			gd.gdImageDestroy(im);
            Assert.Fail();
		}
		else
		{
			if (im != null)
			{
				gd.gdImageDestroy(im);
			}
			else
			{
				Assert.Fail();
			}
		}
	}

    [Test]
    public void TestGd2ReadCpp()
    {
        string path = string.Format("{0}/gd2/conv_test.gd2", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
        using (var image = new Image())
        {
            image.CreateFromGd2(path);

            path = string.Format("{0}/gd2/conv_test_exp.png", GlobalMembersGdtest.DefineConstants.GDTEST_TOP_DIR);
            if (GlobalMembersGdtest.TestImageCompareToFile(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, null, path, image) == 0)
            {
                Assert.Fail();
            }
            else
            {
                if (!image.good())
                {
                    Assert.Fail();
                }
            }
        }
    }
}

