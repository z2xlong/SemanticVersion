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
            SemanticVersion v = new SemanticVersion(0, 2, 1, SemVerPreStage.Alpha);
            Assert.Equal("0.2.1-Alpha", v.ToString(""));
            Assert.Equal("0.2.1-Alpha", v.ToString("C"));
            Assert.Equal("0.2.1", v.ToString("G"));
        }

        [FactAttribute]
        public void ComparableCtorTest2()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, SemVerPreStage.None, 5);
            Assert.Equal("0.2.1", v.ToString());
        }

        [FactAttribute]
        public void FullCtorTest()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, SemVerPreStage.Beta, 5, 1000);
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
            SemanticVersion o = new SemanticVersion(0, 2, 1, SemVerPreStage.Alpha);

            Assert.True(v.CompareTo(o) == 1);
        }

        [FactAttribute]
        public void PreReleaseBiggerPreRelease()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, SemVerPreStage.Beta);
            SemanticVersion o = new SemanticVersion(0, 2, 1, SemVerPreStage.Alpha);

            Assert.True(v.CompareTo(o) == 1);
        }

        [FactAttribute]
        public void PreReleaseLessThanPreRelease()
        {
            SemanticVersion v = new SemanticVersion(0, 2, 1, SemVerPreStage.Alpha);
            SemanticVersion o = new SemanticVersion(0, 2, 1, SemVerPreStage.Alpha, 1);

            Assert.True(v.CompareTo(o) < 0);
        }

        [FactAttribute]
        public void CompareWithBuild()
        {
            SemanticVersion v = new SemanticVersion(1, 2, 5, SemVerPreStage.RC, 2, 1000);
            SemanticVersion o = new SemanticVersion(1, 2, 5, SemVerPreStage.RC, 2, 10000);

            Assert.True(v.CompareTo(o) == 0);
        }
    }
}
