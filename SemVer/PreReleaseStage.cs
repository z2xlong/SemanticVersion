namespace SemVer
{
    public enum PreReleaseStage
    {
        /// <summary>
        /// This typically means that work on major features is still
        /// ongoing.
        /// </summary>
        ALPHA = 1,
        /// <summary>
        /// This typically means that major features are complete, though
        /// not necessarily bug-free or tested, and may or may not mean
        /// that minor features are done or tested.
        /// </summary>
        BETA = 2,
        /// <summary>
        /// This typically means that all planned features or changes are
        /// either done or cut, as well as tested and mostly ready.  Code
        /// should be mainly stable.
        /// </summary>
        RC = 4,
    }
}