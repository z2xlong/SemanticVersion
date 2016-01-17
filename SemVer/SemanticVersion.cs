namespace SemVer
{
    using System;
    using System.Globalization;

    public partial class SemanticVersion : IComparable<SemanticVersion>, IFormattable
    {
        #region Field & Property

        // general version string: Major.Minor.Patch
        readonly string _generalStr;
        // Comparable version string: Major.Minor.Patch-PreRelease.PreReleaseNum
        readonly string _comparableStr;
        // full version string: Major.Minor.Patch-PreRelease.PreReleaseNum+Build
        readonly string _fullStr;

        public uint Major { get; private set; }

        public uint Minor { get; private set; }

        public uint Patch { get; private set; }

        public SemVerPreStage PreRelease { get; private set; }

        public uint PreReleaseNum { get; private set; }

        public uint Build { get; private set; }

        #endregion

        #region Ctor
        public SemanticVersion(uint major, uint minor, uint patch)
            : this(major, minor, patch, SemVerPreStage.None, 0, 0)
        { }

        public SemanticVersion(uint major, uint minor, uint patch, SemVerPreStage prelease)
            : this(major, minor, patch, prelease, 0, 0)
        { }

        public SemanticVersion(uint major, uint minor, uint patch, SemVerPreStage prelease, uint preleaseNum)
            : this(major, minor, patch, prelease, preleaseNum, 0)
        { }

        public SemanticVersion(uint major, uint minor, uint patch, SemVerPreStage prelease, uint preleaseNum, uint build)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            PreRelease = prelease;
            PreReleaseNum = preleaseNum;
            Build = build;

            _generalStr = string.Format("{0}.{1}.{2}", Major.ToString(), Minor.ToString(), Patch.ToString());

            if (PreRelease != SemVerPreStage.None && PreReleaseNum > 0)
                _comparableStr = string.Format("{0}-{1}.{2}", _generalStr, PreRelease.ToString(), PreReleaseNum.ToString());
            else if (PreRelease != SemVerPreStage.None)
                _comparableStr = string.Format("{0}-{1}", _generalStr, PreRelease.ToString());
            else
                _comparableStr = _generalStr;

            if (Build == 0)
                _fullStr = _comparableStr;
            else
                _fullStr = string.Format("{0}+{1}", _comparableStr, Build.ToString());
        }
        #endregion

        public int CompareTo(SemanticVersion other)
        {
            if (other == null)
                return 1;

            if (this.Major != other.Major)
                return this.Major.CompareTo(other.Major);
            if (this.Minor != other.Minor)
                return this.Minor.CompareTo(other.Minor);
            if (this.Patch != other.Patch)
                return this.Patch.CompareTo(other.Patch);
            if (this.PreRelease != other.PreRelease)
                return this.PreRelease.CompareTo(other.PreRelease);
            if (this.PreReleaseNum != other.PreReleaseNum)
                return this.PreReleaseNum.CompareTo(other.PreReleaseNum);

            return 0;
        }

        #region IFormattable
        public override string ToString()
        {
            return ToString("F");
        }

        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;
            if (String.IsNullOrWhiteSpace(format))
                format = "F";

            switch (format.ToUpperInvariant())
            {
                case "F":
                    return _fullStr;
                case "C":
                    return _comparableStr;
                case "G":
                default:
                    return _generalStr;
            }
        }

        #endregion
    }
}