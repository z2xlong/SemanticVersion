namespace SemVer
{
    public partial class SemanticVersion
    {
        // full version Pattern: Major. Minor. Patch - PreRelease . PreReleaseNum + Build
        // full verison Type:    uint . uint . uint  - Enum       . uint          + uint
        public static bool TryParse(string str, out SemanticVersion version)
        {
            version = null;
            if (string.IsNullOrWhiteSpace(str))
                return false;
            if (str.Length < 5)
                return false;

            char brk = '.';
            uint stage = 0, idx = 0, num = 0;
            uint[] seg = new uint[6];
            char[] chs = str.ToCharArray();
            SemVerPreStage prelease = SemVerPreStage.None;

            while (idx < chs.Length && stage < 6)
            {
                if (stage == 3 && !TryParseToSemVerPreStage(chs, ref idx, out prelease))
                    return false;

                if (stage == 2)
                    brk = '-';
                else if (stage == 4)
                    brk = '+';
                else
                    brk = '.';

                if (TryParseToUint(chs, brk, ref idx, out num))
                    seg[stage] = num;
                else
                    return false;

                idx += 1;
                stage += 1;
            }

            version = new SemanticVersion(seg[0], seg[1], seg[2], prelease, seg[4], seg[5]);
            return idx == chs.Length;
        }

        private static bool TryParseToUint(char[] chs, char brk, ref uint idx, out uint num)
        {
            num = 0;

            return true;
        }

        private static bool TryParseToSemVerPreStage(char[] chs, ref uint idx, out SemVerPreStage prelease)
        {
            prelease = SemVerPreStage.None;
            return true;
        }
    }
}