using System;

namespace SemVer
{
    public partial class SemanticVersion : IComparable<SemanticVersion>
    {
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

            return PreRelease.Compare(this.PreRelease, other.PreRelease);
        }

        public static bool operator ==(SemanticVersion a, SemanticVersion b)
        {
            if (ReferenceEquals(a, b))
                return true;

            return a.CompareTo(b) == 0;
        }

        public static bool operator !=(SemanticVersion a, SemanticVersion b)
        {
            return !(a == b);
        }

        public static bool operator <(SemanticVersion a, SemanticVersion b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator >(SemanticVersion a, SemanticVersion b)
        {
            return a.CompareTo(b) > 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            SemanticVersion semVer = obj as SemanticVersion;

            if (semVer == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return this.CompareTo(semVer) == 0;
        }

        public override int GetHashCode()
        {
            return this.Major.GetHashCode() ^ this.Minor.GetHashCode()
                ^ this.Patch.GetHashCode() ^ this.PreRelease.GetHashCode()
                ^ this.Build.GetHashCode();
        }
    }

}