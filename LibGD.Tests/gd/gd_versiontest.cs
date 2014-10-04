using LibGD;
using NUnit.Framework;

[TestFixture]
public class GlobalMembersGd_versiontest
{
	[Test]
	public void TestGd_versiontest()
	{
		string buffer = new string(new char[100]);

		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.DefineConstants.GD_MAJOR_VERSION == gd.gdMajorVersion()) ? 1 : 0);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.DefineConstants.GD_MINOR_VERSION == gd.gdMinorVersion()) ? 1 : 0);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (GlobalMembersGdtest.DefineConstants.GD_RELEASE_VERSION == gd.gdReleaseVersion()) ? 1 : 0);
		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n", (string.Compare(GlobalMembersGdtest.DefineConstants.GD_EXTRA_VERSION, gd.gdExtraVersion()) == 0) ? 1 : 0);

		GlobalMembersGdtest.gdTestAssert(GlobalMembersGdtest.__FILE__, GlobalMembersGdtest.__LINE__, "assert failed in <%s:%i>\n",
			(string.Compare(GlobalMembersGdtest.DefineConstants.GD_MAJOR_VERSION + "." + GlobalMembersGdtest.DefineConstants.GD_MINOR_VERSION + "." +
			GlobalMembersGdtest.DefineConstants.GD_RELEASE_VERSION + GlobalMembersGdtest.DefineConstants.GD_EXTRA_VERSION, gd.gdVersionString()) == 0) ? 1 : 0);

		Assert.AreEqual(0, GlobalMembersGdtest.gdNumFailures());
	}
}

