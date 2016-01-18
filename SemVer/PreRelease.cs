using System;

namespace SemVer
{
    public class PreRelease : IComparable<PreRelease>
    {
        readonly string _str;

        public PreReleaseStage Stage { get; private set; }

        public uint Number { get; private set; }

        public PreRelease(PreReleaseStage stage) : this(stage, 0) { }

        public PreRelease(PreReleaseStage stage, uint num)
        {
            this.Stage = stage;
            this.Number = num;

            if (num > 0)
                _str = string.Format("{0}.{1}", stage.ToString(), num.ToString());
            else
                _str = stage.ToString();
        }

        public override string ToString()
        {
            return _str;
        }

        public int CompareTo(PreRelease other)
        {
            if (other == null)
                return 1;

            if (this.Stage != other.Stage)
                return this.Stage.CompareTo(other.Stage);
            if (this.Number != other.Number)
                return this.Number.CompareTo(other.Number);

            return 0;
        }

        public override int GetHashCode()
        {
            return this.Stage.GetHashCode() ^ this.Number.GetHashCode();
        }
    }
}