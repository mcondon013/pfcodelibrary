//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************

namespace PFTaskObjects
{
#pragma warning disable 1591

    public enum enTaskType
    {
        Unknown = 0,
        WindowsExecutable = 1,
        WindowsBatchFile = 2,
        DatabaseCommand = 3,
    }

    public enum enTaskRunResult
    {
        Unknown = 0,
        Success = 1,
        Failure = 2,
        NotRun = 3,
    }

    public enum enWindowStyle
    {
        Normal = 0,
        Hidden = 1,
        Minimized = 2,
        Maximized = 3,
    }

    public enum enTaskStorageType
    {
        Unknown = 0,
        XMLFiles = 1,
        Database = 2
    }

#pragma warning restore 1591

 }//end namespace