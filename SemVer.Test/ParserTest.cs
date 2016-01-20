using Xunit;

namespace SemVer.Test
{
    public class ParserTest
    {
        [Fact]
        public void SimpleParseTest()
        {
            SemanticVersion v;
            Assert.True(SemanticVersion.TryParse("0.2.1", out v));
        }

        [Fact]
        public void EmptyStrTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse(string.Empty, out v));
        }

        [Fact]
        public void ShortStrTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse("0.1.", out v));
        }

        [Fact]
        public void FullPatternTest()
        {
            SemanticVersion v;
            Assert.True(SemanticVersion.TryParse("0.2.1-ALPHA.5+234", out v));

            Assert.Equal(PreReleaseStage.ALPHA, v.PreRelease.Stage);
            Assert.Equal((uint)5, v.PreRelease.Number);
            Assert.Equal((uint)234, v.Build);
        }

        [Fact]
        public void ComparableParseTest()
        {
            SemanticVersion v;
            Assert.True(SemanticVersion.TryParse("0.1111.5-rc.6", out v));

            SemanticVersion o = new SemanticVersion(0, 1111, 5, new PreRelease(PreReleaseStage.RC, 7), 32);
            Assert.Equal(1, o.CompareTo(v));
        }

        [Fact]
        public void BuildParseTest()
        {
            SemanticVersion v;
            Assert.True(SemanticVersion.TryParse("32.567.11+9700", out v));

            Assert.Equal((uint)9700, v.Build);
        }

        [Fact]
        public void TanglyBreakCharTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse("0.2.1+BETA.5-234", out v));
        }

        [Fact]
        public void InvalidPreReleaseStageTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse("1.5.7-PREALPHA", out v));
            Assert.False(SemanticVersion.TryParse("1.5.7-PREAL", out v));
        }

        [Fact]
        public void WhiteSpaceTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse("1.1.0 -RC. 5", out v));
        }

        [Fact]
        public void GetHashCodeTest()
        {
            PreRelease p1 = new PreRelease(PreReleaseStage.RC, 1);
            PreRelease p2 = new PreRelease(PreReleaseStage.RC, 1);

            Assert.Equal(p1.GetHashCode(), p2.GetHashCode());
        }

        [Fact]
        public void InvalidVersionNumberTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse("2.a.1", out v));
        }

        [Fact]
        public void LongStrParseTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse("2.3.1.", out v));
            Assert.False(SemanticVersion.TryParse("2.3.1/abc", out v));
            Assert.False(SemanticVersion.TryParse("1.0", out v));
        }

        [Fact]
        public void UintOverflowTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse("2.3.4294967296", out v));
        }
    }
}