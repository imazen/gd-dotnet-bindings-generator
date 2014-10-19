using LibGD;
using LibGD.GD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersBug00007
{
    [Test]
    public void TestBug00007()
	{
        gdImageStruct src = gd.gdImageCreate(5,5);
		gd.gdImageAlphaBlending(src, 0);

		gd.gdImageColorAllocate(src, 255,255,255); // allocate white for background color
		int c1 = gd.gdImageColorAllocateAlpha(src, 255,0,0,70);

		gd.gdImageFilledRectangle(src, 0,0, 4,4, c1);

		gdImageStruct dst_tc = gd.gdImageCreateTrueColor(5,5);
		gd.gdImageAlphaBlending(dst_tc, 0);
		gd.gdImageCopy(dst_tc, src, 0,0, 0,0, src.sx, src.sy);

		/* CuAssertImageEquals(tc, src, dst_tc); */

		/* Destroy it */
		gd.gdImageDestroy(dst_tc);
		gd.gdImageDestroy(src);
	}

    public void TestBug00007Cpp()
    {
        using (var src = new Image(5, 5))
        {
            src.AlphaBlending(false);

            src.ColorAllocate(255, 255, 255); // allocate white for background color
            int c1 = src.ColorAllocate(255, 0, 0, 70);

            src.FilledRectangle(0, 0, 4, 4, c1);

            using (var dst_tc = new Image(5, 5, true))
            {
                dst_tc.AlphaBlending(false);
                dst_tc.Copy(src, 0, 0, 0, 0, src.SX(), src.SY());
            }
        }

        /* CuAssertImageEquals(tc, src, dst_tc); */
    }
}

