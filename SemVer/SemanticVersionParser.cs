namespace SemVer
{
    public partial class SemanticVersion
    {
        static uint _maxTen = uint.MaxValue / 10;
        static uint _maxMod = uint.MaxValue % 10;

        // full version Pattern:  <Major>. <Minor>. <Patch> [- PreRelease [. PreReleaseNum]] [+ Build]
        // full verison Type:     uint . uint . uint - Enum . uint + uint
        public static bool TryParse(string str, out SemanticVersion version)
        {
            version = null;
            if (string.IsNullOrWhiteSpace(str))
                return false;
            if (str.Length < 5)
                return false;

            char brk = '.';
            uint stage = 0, idx = 0, num = 0;
            uint[] seg = new uint[5];
            PreRelease prelease = null;

            char[] chs = str.ToCharArray();

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

            if (idx < chs.Length || stage < 2)
                return false;

            version = new SemanticVersion(seg[0], seg[1], seg[2], prelease, seg[4]);
            return true;
        }

        private static bool TryParseToUint(char[] chs, char brk, ref uint idx, out uint num)
        {
            num = 0;
            uint n = 0;
            uint? result = 0;

            for (; idx < chs.Length; idx++)
            {
                if (chs[idx] >= '0' && chs[idx] <= '9')
                {
                    n = (uint)chs[idx] - 48;

                    if (num > _maxTen || (num == _maxTen && n > _maxMod))
                        return false;

                    result = result == null ? n : result * 10 + n;
                }
                else if (chs[idx] == brk)
                    break;
                else
                    return false;
            }

            if (result == null)
                return false;
            else
            {
                num = (uint)result;
                return true;
            }
        }

        private static bool TryParseToSemVerPreStage(char[] chs, ref uint idx, out PreRelease prelease)
        {
            prelease = null;

            char[] ename = new char[5];

            for (; idx < chs.Length; idx++)
            {

            }
            return true;
        }
    }
}