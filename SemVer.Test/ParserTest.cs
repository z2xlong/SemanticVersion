using Xunit;

namespace SemVer.Test
{
    public class ParserTest
    {
        [FactAttribute]
        public void GeneralParse()
        {
            SemanticVersion v;
            Assert.True(SemanticVersion.TryParse("0.2.1", out v));
        }
    }
}