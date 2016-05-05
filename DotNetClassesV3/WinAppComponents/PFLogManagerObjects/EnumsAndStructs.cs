namespace PFLogManagerObjects
{
    /// <summary>
    /// Enumeration of the log storage types supported by the pfLogManagerObjects class.
    /// </summary>
    public enum enLogFileStorageType
    {
#pragma warning disable 1591
        Unknown = 0,
        TextFile = 1,
        Database = 2
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration of the retry log storage types supported by the pfLogManagerObjects class.
    /// </summary>
    public enum enLogRetryQueueStorageType
    {
#pragma warning disable 1591
        Unknown = 0,
        XmlFile = 1,
        Database = 2
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration of the type of log messages supported by the pfLogManagerObjects class.
    /// </summary>
    public enum enLogMessageType
    {
#pragma warning disable 1591
        Error = 1,
        Warning = 2,
        Information = 4,
        Alert = 8
#pragma warning restore 1591
    }



}