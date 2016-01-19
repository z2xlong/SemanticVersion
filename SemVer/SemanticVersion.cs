namespace SemVer
{
    using System;
    using System.Globalization;

    public partial class SemanticVersion : IFormattable
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

        public PreRelease PreRelease { get; private set; }

        public uint Build { get; private set; }

        #endregion

        #region Ctor
        public SemanticVersion(uint major, uint minor, uint patch)
            : this(major, minor, patch, null, 0)
        { }

        public SemanticVersion(uint major, uint minor, uint patch, PreRelease prelease)
            : this(major, minor, patch, prelease, 0)
        { }

        public SemanticVersion(uint major, uint minor, uint patch, uint build)
            : this(major, minor, patch, null, build)
        { }

        public SemanticVersion(uint major, uint minor, uint patch, PreRelease prelease, uint build)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            PreRelease = prelease;
            Build = build;

            _generalStr = string.Format("{0}.{1}.{2}", Major.ToString(), Minor.ToString(), Patch.ToString());

            if (PreRelease != null)
                _comparableStr = string.Format("{0}-{1}", _generalStr, PreRelease.ToString());
            else
                _comparableStr = _generalStr;

            if (Build == 0)
                _fullStr = _comparableStr;
            else
                _fullStr = string.Format("{0}+{1}", _comparableStr, Build.ToString());
        }
        #endregion

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