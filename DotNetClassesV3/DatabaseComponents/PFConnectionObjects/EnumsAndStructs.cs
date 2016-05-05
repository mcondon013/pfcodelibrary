namespace PFConnectionObjects
{
#pragma warning disable 1591

    public enum enDatabaseDefinitionsStorageType
    {
        Unknown = 0,
        XMLFiles = 1,
        Database = 2
    }

    public enum enProviderInstallationStatus
    {
        Unknown = 0,
        IsInstalled = 1,
        NotInstalled = 2
    }

    public enum enConnectionAccessStatus
    {
        Unknown = 0,
        IsAccessible = 1,
        NotAccessible = 2
    }

    public enum enConnectionStringValuePrompt
    {
        Unknown = 0,
        TextBox = 1,
        ComboBox = 2,
        CheckBox = 3
    }
#pragma warning restore 1591

}//end namespace