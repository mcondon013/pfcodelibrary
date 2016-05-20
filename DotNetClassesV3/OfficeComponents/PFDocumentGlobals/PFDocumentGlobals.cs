//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFDocumentGlobals
{
    /// <summary>
    /// Enum for Excel formats.
    /// </summary>
    public enum enExcelOutputFormat
    {
#pragma warning disable 1591
        NotSpecified,
        Excel2003,
        Excel2007,
        CSV
#pragma warning restore 1591
    }

    /// <summary>
    /// Enum for Word formats.
    /// </summary>
    public enum enWordOutputFormat
    {
#pragma warning disable 1591
        NotSpecified,
        Word2003,
        Word2007,
        RTF,
        PDF
#pragma warning restore 1591
    }

}//end namespace
