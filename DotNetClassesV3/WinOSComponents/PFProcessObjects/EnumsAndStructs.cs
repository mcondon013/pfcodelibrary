using System.Diagnostics;

namespace PFProcessObjects
{
    /// <summary>
    /// Enumeration of Windows styles.
    /// </summary>
    public enum PFProcessWindowStyle
    {
#pragma warning disable 1591
        Hidden = ProcessWindowStyle.Hidden,
        Maximized = ProcessWindowStyle.Maximized,
        Minimized = ProcessWindowStyle.Minimized,
        Normal = ProcessWindowStyle.Normal,
#pragma warning restore 1591
    }

}//end namespace