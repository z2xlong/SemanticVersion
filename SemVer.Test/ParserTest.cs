using Xunit;

namespace SemVer.Test
{
    public class ParserTest
    {
        [FactAttribute]
        public void GeneralParseTest()
        {
            SemanticVersion v;
            Assert.True(SemanticVersion.TryParse("0.2.1", out v));
        }

        [FactAttribute]
        public void EmptyStrTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse(string.Empty, out v));
        }

        [FactAttribute]
        public void ShortStrTest()
        {
            SemanticVersion v;
            Assert.False(SemanticVersion.TryParse("0.1.", out v));
        }
    }
}