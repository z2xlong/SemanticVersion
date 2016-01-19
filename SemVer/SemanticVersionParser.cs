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
            uint s = 0, i = 0, num = 0;
            uint[] seg = new uint[6];
            char[] chs = str.ToCharArray();

            while (i < chs.Length && s < seg.Length)
            {
                switch (brk)
                {
                    case '.':
                        if (!TryParseToUint(chs, ref i, out num))
                            return false;
                        break;
                    case '-':
                        if (s != 3 || !TryParseToSemVerPreStage(chs, ref i, out num))
                            return false;
                        break;
                    case '+':
                        if (s < 3 || s > 5 || !TryParseToUint(chs, ref i, out num))
                            return false;
                        break;
                    default:
                        return false;
                }
                seg[s] = num;
                brk = chs[i];
                i += 1;
                s += 1;
            }

            if (i < chs.Length || s < 2)
                return false;

            
            // version = new SemanticVersion(seg[0], seg[1], seg[2], , seg[5]);
            return true;
        }

        private static bool TryParseToUint(char[] chs, ref uint idx, out uint num)
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
                else
                    break;
            }

            if (result == null)
                return false;

            num = (uint)result;
            return true;
        }

        private static bool TryParseToSemVerPreStage(char[] chs, ref uint idx, out uint num)
        {
            num = 0;

            char[] ename = new char[5];

            for (; idx < chs.Length; idx++)
            {

            }
            return true;
        }
    }
}