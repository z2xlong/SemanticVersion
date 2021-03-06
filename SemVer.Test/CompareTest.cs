using Xunit;

namespace SemVer.Test
{
    // see example explanation on xUnit.net website:
    // https://xunit.github.io/docs/getting-started-dnx.html
    public class CompareTest
    {
        [Fact]
        public void GeneralCtorTest()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1);
            Assert.Equal("0.2.1", v.ToString());
            Assert.Equal("0.2.1", v.ToString("C"));
            Assert.Equal("0.2.1", v.ToString("G"));
        }

        [Fact]
        public void ComparableCtorTest()
        {
            PreRelease pre = new PreRelease(PreReleaseStage.ALPHA);
            SemanticVersion v = new SemanticVersion(0, 2, 1, pre);
            Assert.Equal("0.2.1-ALPHA", v.ToString(""));
            Assert.Equal("0.2.1-ALPHA", v.ToString("C"));
            Assert.Equal("0.2.1", v.ToString("G"));
        }

        [Fact]
        public void BuildCtorTest()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, 1000);
            Assert.Equal("0.2.1+1000", v.ToString());
        }

        [Fact]
        public void FullCtorTest()
        {
            PreRelease pre = new PreRelease(PreReleaseStage.BETA, 5);
            SemanticVersion v = new SemanticVersion(0, 2, 1, pre, 1000);
            Assert.Equal("0.2.1-BETA.5+1000", v.ToString());
            Assert.Equal("0.2.1-BETA.5", v.ToString("C"));
            Assert.Equal("0.2.1", v.ToString("G"));
        }

        [Fact]
        public void GeneralEqualGeneral()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1);
            SemanticVersion o = new SemanticVersion(0, 2, 1);

            Assert.True(v.CompareTo(o) == 0);
        }

        [Fact]
        public void GeneralBiggerThanGeneral()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1);
            SemanticVersion o = new SemanticVersion(0, 2, 0);

            Assert.True(v.CompareTo(o) == 1);
        }

        [Fact]
        public void GeneralBiggerThanPreRelease()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1);
            SemanticVersion o = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.ALPHA));

            Assert.True(v.CompareTo(o) == 1);
        }

        [Fact]
        public void PreReleaseBiggerPreRelease()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.BETA));
            SemanticVersion o = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.ALPHA));

            Assert.True(v.CompareTo(o) == 1);
        }

        [Fact]
        public void PreReleaseLessThanPreRelease()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.ALPHA));
            SemanticVersion o = new SemanticVersion(0, 2, 1, new PreRelease(PreReleaseStage.ALPHA, 1));

            Assert.True(v.CompareTo(o) < 0);
        }

        [Fact]
        public void CompareWithBuild()
        {
            SemanticVersion v = new SemanticVersion(1, 2, 5, new PreRelease(PreReleaseStage.RC, 2), 1000);
            SemanticVersion o = new SemanticVersion(1, 2, 5, new PreRelease(PreReleaseStage.RC, 2), 10000);

            Assert.True(v.CompareTo(o) == 0);
        }

        [Fact]
        public void CompareMajorVersion()
        {
            SemanticVersion o = new SemanticVersion(4, 2, 1);
            SemanticVersion v = new SemanticVersion(5, 2, 1);

            Assert.Equal(1, v.CompareTo(o));
        }

        [Fact]
        public void CompareMinorVersion()
        {
            SemanticVersion o = new SemanticVersion(4, 3, 1);
            SemanticVersion v = new SemanticVersion(4, 2, 1);

            Assert.Equal(-1, v.CompareTo(o));
        }

        [Fact]
        public void OperatorNotEqualTest()
        {
            SemanticVersion o = new SemanticVersion(4, 3, 1);
            SemanticVersion v = new SemanticVersion(4, 2, 1);

            Assert.True(v != o);
        }

        [Fact]
        public void OperatorLessTest()
        {
            SemanticVersion o = new SemanticVersion(4, 3, 1);
            SemanticVersion v = new SemanticVersion(4, 2, 1);
            Assert.True(v < o);
        }

        [Fact]
        public void OperatorAboveTest()
        {
            SemanticVersion o = new SemanticVersion(4, 3, 1);
            SemanticVersion v = new SemanticVersion(4, 2, 1);
            Assert.True(o > v);
        }

        [Fact]
        public void OverridedEqualsTest()
        {
            SemanticVersion o = null;
            SemanticVersion v = new SemanticVersion(4, 2, 1);

            Assert.False(v.Equals(o));
            Assert.False(v.Equals(new PreRelease(PreReleaseStage.ALPHA)));

            o = v;
            Assert.True(v.Equals(o));

            o = new SemanticVersion(4, 2, 1);
            Assert.True(v.Equals(o));
        }

        [Fact]
        public void GetHashCodeTest()
        {
            SemanticVersion o = new SemanticVersion(4, 2, 1, new PreRelease(PreReleaseStage.ALPHA, 3), 45);
            SemanticVersion v = o;

            Assert.Equal(o.GetHashCode(), v.GetHashCode());

            v = new SemanticVersion(4, 2, 1, new PreRelease(PreReleaseStage.ALPHA, 3), 45);
            Assert.Equal(o.GetHashCode(), v.GetHashCode());
        }

    }
}
