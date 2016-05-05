//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2014
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
#pragma warning disable 1591
    /// <summary>
    /// Enumeration used to specify country for which name is to be generated.
    /// The enCustom enumeration is automatically assigned when loading a file of custom names.
    /// </summary>
    public enum enNameLocation
    {
#pragma warning disable 1591
        enUS = 0,
        enCanada = 1,
        enQuebec = 2,
        enMexico = 3,
        enEngland = 4,
        enScotland = 5,
        enIreland = 6,
        enFrance = 7,
        enItaly = 8,
        enSpain = 9,
        enPortugal = 10,
        enGermany = 11,
        enRussia = 12,
        enChina = 13,
        enJapan = 14,
        enArabia = 15,
        enIsrael = 16,
        enBrazil = 17,
        enArgentina = 18,
        enCustom = 19,
        enUnknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration used to specify country for which location is to be generated.
    /// Location names are for city, street, state/province.
    /// The enCustom enumeration is automatically assigned when loading a file of custom location names.
    /// </summary>
    public enum enLocation
    {
#pragma warning disable 1591
        enUS = 0,
        enCanada = 1,
        enQuebec = 2,
        enMexico = 3,
        enCustom = 9,
        enUnknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration used to specify what type of location is being referred to by a name.
    /// The enCustom enumerated value is used when custom names refer to a non-standard location type.
    /// </summary>
    public enum enLocationNameType
    {
#pragma warning disable 1591
        enCityName = 0,
        enStateProvinceCode = 1,
        enStateProvinceName = 2,
        enStreetName = 3,
        enNeighborhoodName = 4,
        enCustom = 9,
        enUnknown = 99
#pragma warning restore 1591
    }
    /// <summary>
    /// Enumeration used to specify type of random number to generate.
    /// </summary>
    public enum enRandomNumberType
    {
#pragma warning disable 1591
        enInt = 0,
        enUInt = 1,
        enLong = 2,
        enULong = 3,
        enShort = 4,
        enUShort = 5,
        enSByte = 6,
        enByte = 7,
        enDouble = 8,
        enFloat = 9,
        enDecimal = 10,
        enUnknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration used to specify the time span to offset a date/time in order to produce a random date.
    /// </summary>
    public enum enRandomOffsetType
    {
#pragma warning disable 1591
        enYears = 0,
        enMonths = 1,
        enDays = 2,
        enHours = 3,
        enMinutes = 4,
        enSeconds = 5,
        enUnknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration that specifies data type to use for random boolean values.
    /// </summary>
    public enum enBooleanOutputType
    {
#pragma warning disable 1591

        enBoolean = 0,
        enInteger = 1,
        enString = 2,
        enUnknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumerates the output formats for data warehouse style integer date and time values
    /// </summary>
    public enum enDwDateTimeFormat
    {
#pragma warning disable 1591
        enDateAndTime = 0,
        enDateOnly = 1,
        enTimeOnly = 2,
        enUnknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration used specify the type and format of a random string.
    /// </summary>
    public enum enRandomStringType
    {
#pragma warning disable 1591
        enAN = 0,
        enANUC = 1,
        enANLC = 2,
        enANX = 3,
        enAL = 4,
        enLC = 5,
        enUC = 6,
        enDEC = 7,
        enHEX = 8,
        enUnknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumeration used to specify the upper and lower case formatting for random syllables.
    /// </summary>
    public enum enRandomSyllableStringType
    {
#pragma warning disable 1591
        enUCLC = 0,
        enLC = 1,
        enUC = 2,
        enUnknown = 99
#pragma warning restore 1591
    }

    /// <summary>
    /// Dscribes types of words and parts of words that can be generated.
    /// </summary>
    public enum enWordType
    {
#pragma warning disable 1591
        NotSpecified = 0,
        Noun = 1,
        Verb = 2,
        Adjective = 3,
        Adverb = 4,
        Pronoun = 5,
        Determiner = 6,
        DeterminerPronoun = 7,
        Preposition = 8,
        Conjunction = 9,
        Interjection = 10,
        Interrogative = 11,
        SubordinateConjunction = 12,
        CityName = 13,
        StateName = 14,
        FirstName = 15,
        FirstNameMale = 16,
        FirstNameFemale = 17,
        LastName = 18,
        BusinessName = 19,
        BizName3Con_1 = 20,
        BizName3Con_2 = 21,
        BizNameSyllable_1 = 22,
        BizNameSyllable_2 = 23,
        BizNameSuffix = 24,
        BizNamePrefix = 25,
        SubjectPronoun = 26,
        ObjectPronoun = 27
#pragma warning restore 1591
    }

    /// <summary>
    /// Enumerates the sentence parts that can be constructed by the random sentence routines.
    /// </summary>
    public enum enSentenceSyntaxCategory
    {
#pragma warning disable 1591
        Sentence = 0,
        SubordinateClause = 1,
        NounPhrase = 2,
        Subject = 3,
        VerbPhrase = 4,
        Object = 5,
        SentenceTerminator = 6,
        Unknown = 99,
#pragma warning restore 1591
    }


}//end namespace