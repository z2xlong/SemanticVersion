using Xunit;

namespace SemVer.Test
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dnx.html
    public class SampleTest
    {
        [Fact]
        public void GeneralCtorTest()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1);
            Assert.Equal("0.2.1", v.ToString());
            Assert.Equal("0.2.1", v.ToString("C"));
            Assert.Equal("0.2.1", v.ToString("G"));
        }

        [FactAttribute]
        public void ComparableCtorTest()
        {
            PreRelease pre = new PreRelease(PreReleaseStage.Alpha);
            SemanticVersion v = new SemanticVersion(0, 2, 1, pre);
            Assert.Equal("0.2.1-Alpha", v.ToString(""));
            Assert.Equal("0.2.1-Alpha", v.ToString("C"));
            Assert.Equal("0.2.1", v.ToString("G"));
        }

        [FactAttribute]
        public void FullCtorTest()
        {
            PreRelease pre = new PreRelease(PreReleaseStage.Beta, 5);
            SemanticVersion v = new SemanticVersion(0, 2, 1, pre, 1000);
            Assert.Equal("0.2.1-Beta.5+1000", v.ToString());
            Assert.Equal("0.2.1-Beta.5", v.ToString("C"));
            Assert.Equal("0.2.1", v.ToString("G"));
        }

        [FactAttribute]
        public void GeneralEqualGeneral()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1);
            SemanticVersion o = new SemanticVersion(0, 2, 1);

            Assert.True(v.CompareTo(o) == 0);
        }

        [FactAttribute]
        public void GeneralBiggerThanGeneral()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1);
            SemanticVersion o = new SemanticVersion(0, 2, 0);

            Assert.True(v.CompareTo(o) == 1);
        }

        [FactAttribute]
        public void GeneralBiggerThanPreRelease()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1);
            SemanticVersion o = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.Alpha));

            Assert.True(v.CompareTo(o) == 1);
        }

        [FactAttribute]
        public void PreReleaseBiggerPreRelease()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.Beta));
            SemanticVersion o = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.Alpha));

            Assert.True(v.CompareTo(o) == 1);
        }

        [FactAttribute]
        public void PreReleaseLessThanPreRelease()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.Alpha));
            SemanticVersion o = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.Alpha, 1));

            Assert.True(v.CompareTo(o) < 0);
        }

        [FactAttribute]
        public void CompareWithBuild()
        {
            SemanticVersion v = new SemanticVersion(1, 2, 5, new PreRelease(PreReleaseStage.RC, 2), 1000);
            SemanticVersion o = new SemanticVersion(1, 2, 5, new PreRelease(PreReleaseStage.RC, 2), 10000);

            Assert.True(v.CompareTo(o) == 0);
        }
    }
}
