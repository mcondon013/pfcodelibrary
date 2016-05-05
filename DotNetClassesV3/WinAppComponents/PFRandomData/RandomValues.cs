//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PFRandomData
{
    /// <summary>
    /// Contains routines for generating various types of random data values.
    /// </summary>
    public class RandomValues
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        RandomNumber _rn = new RandomNumber();
        RandomString _rs = new RandomString();
        private string[] _customRandomValues = { "" };
        private int _customRandomValuesMaxInx = 0;
        //private variables for properties

        //enumerations
        /// <summary>
        /// Enumerates the countries for which national ids can be generated.
        /// </summary>
        public enum enNationalIdCountry
        {
#pragma warning disable 1591
            NotSpecified,
            Canada,
            Mexico,
            UnitedStates
#pragma warning restore 1591
        }

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomValues()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="customRandomValuesFile">Path to file containing random values.</param>
        /// <remarks>File needs to be a text file with one random value stored on each line in the file.</remarks>
        public RandomValues(string customRandomValuesFile)
        {
            _customRandomValues = File.ReadAllLines(customRandomValuesFile);
            _customRandomValuesMaxInx = _customRandomValues.Length - 1;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="customRandomValuesFile">Path to file containing random values.</param>
        /// <param name="textEncoding">Supply a System.Text.Encoding enum value if .NET file routines have trouble reading your file. This will often be needed when incoming data has accents on individual letters.</param>
        /// <remarks>File needs to be a text file with one random value stored on each line in the file.</remarks>
        public RandomValues(string customRandomValuesFile, System.Text.Encoding textEncoding)
        {
            _customRandomValues = File.ReadAllLines(customRandomValuesFile, textEncoding);
            _customRandomValuesMaxInx = _customRandomValues.Length - 1;
        }


        //properties

        //methods

        /// <summary>
        /// Routine for generating the default national id of all zeros.
        /// </summary>
        /// <param name="country">Specifies the country for which the ID is to be generated.</param>
        /// <returns>Always returns string containing an all zeros national id.</returns>
        public string GetDefaultNationalId(enNationalIdCountry country)
        {
            string nationalId = "000000000";

            switch (country)
            {
                case enNationalIdCountry.UnitedStates:
                    nationalId = "000-00-0000";
                    break;
                case enNationalIdCountry.Canada:
                    nationalId = "000-000-000";
                    break;
                case enNationalIdCountry.Mexico:
                    nationalId = "XXXX000000XXXXXX00";
                    break;
                default:
                    nationalId = "000000000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <param name="country">Country for which the random id will be formatted.</param>
        /// <returns>String containing the random national id.</returns>
        public string GetNationalId(enNationalIdCountry country)
        {
            string nationalId = "000000000";

            switch (country)
            {
                case enNationalIdCountry.UnitedStates:
                    nationalId = GetNationalIdUS();
                    break;
                case enNationalIdCountry.Canada:
                    nationalId = GetNationalIdCAN();
                    break;
                case enNationalIdCountry.Mexico:
                    nationalId = GetNationalIdMEX();
                    break;
                default:
                    nationalId = "000000000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) SSN.
        /// </summary>
        /// <returns>String containing the random SSN.</returns>
        public string GetNationalIdUS()
        {
            string nationalId = "000-00-0000";
            int randomFormat = 0;

            randomFormat = _rn.GenerateRandomInt(1, 3);

            switch (randomFormat)
            {
                case 1:
                    nationalId = _rn.GenerateRandomInt(1, 999).ToString("000") + "-00-0000";
                    break;
                case 2:
                    nationalId = "000-" + _rn.GenerateRandomInt(1, 99).ToString("00") + "-0000";
                    break;
                case 3:
                    nationalId = "000-00-" + _rn.GenerateRandomInt(1, 9999).ToString("0000");
                    break;
                default:
                    nationalId = "000-00-0000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <returns>String containing the random national id.</returns>
        public string GetNationalIdCAN()
        {
            string nationalId = "000-000-000";
            int randomFormat = 0;

            randomFormat = _rn.GenerateRandomInt(1, 3);

            switch (randomFormat)
            {
                case 1:
                    nationalId = "0" + _rn.GenerateRandomInt(1, 99).ToString("00") + "-000-000";
                    break;
                case 2:
                    nationalId = "000-" + _rn.GenerateRandomInt(1, 999).ToString("000") + "-000";
                    break;
                case 3:
                    nationalId = "000-000-" + _rn.GenerateRandomInt(1, 999).ToString("000");
                    break;
                default:
                    nationalId = "000-000-000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <returns>String containing the random SSN.</returns>
        public string GetNationalIdMEX()
        {
            string nationalId = "XXXX000000XXXXXX00";
            int randomFormat = 0;

            randomFormat = _rn.GenerateRandomInt(1, 4);

            switch (randomFormat)
            {
                case 1:
                    nationalId = _rs.GetStringUC(4) + "000000" + _rs.GetStringUC(6) + "00";
                    break;
                case 2:
                    nationalId = "YYXX" + "000000" + _rs.GetStringUC(6) + "00";
                    break;
                case 3:
                    nationalId = "ZZXX" + "000000" + _rs.GetStringUC(6) + "00";
                    break;
                case 4:
                    nationalId = "XXXX" + "000000" + _rs.GetStringUC(6) + "00";
                    break;
                default:
                    nationalId = "XXXX000000XXXXXX00";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Country code of 1 and area code of 000 will be specified in the resulting random telephone number.</remarks>
        public string GetTelephoneNumber()
        {
            return GetTelephoneNumber("1", "000");
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <param name="areaCode">Specify the area code for the telephone number.</param>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Country code of 1 will be specified in the resulting random telephone number.</remarks>
        public string GetTelephoneNumber(string areaCode)
        {
            return GetTelephoneNumber("1", areaCode);
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <param name="countryCode">Specify the country code for the telephone number.</param>
        /// <param name="areaCode">Specify the area code for the telephone number.</param>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Leave countryCode and/or areaCode blank to omit those portions of the telephone number from the random result.</remarks>
        public string GetTelephoneNumber(string countryCode, string areaCode)
        {
            string telNo = "1-000-000-0000";
            StringBuilder sb = new StringBuilder();

            if (countryCode.Length > 0)
            {
                sb.Append(countryCode);
                sb.Append("-");
            }
            if (areaCode.Length > 0)
            {
                sb.Append(areaCode);
                sb.Append("-");
            }

            sb.Append("555-");
            sb.Append(_rn.GenerateRandomInt(100,199).ToString("0000"));

            telNo = sb.ToString();

            return telNo;
        }

        /// <summary>
        /// Routine to generate a random email address.
        /// </summary>
        /// <returns>String containing random email address.</returns>
        /// <remarks>All email addresses are for the example domain.</remarks>
        public string GetEmailAddress()
        {
            string topLevelDomain = "000";

            switch (_rn.GenerateRandomInt(1, 10))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    topLevelDomain = "com";
                    break;
                case 5:
                case 6:
                case 7:
                    topLevelDomain = "net";
                    break;
                case 8:
                case 9:
                    topLevelDomain = "org";
                    break;
                case 10:
                    topLevelDomain = "edu";
                    break;
                default:
                    topLevelDomain = "000";
                    break;
            }

            return GetEmailAddress(topLevelDomain);
        }

        /// <summary>
        /// Routine to generate a random email address.
        /// </summary>
        /// <param name="topLevelDomain">Top level domain to use for this address (e.g. com, net, org, edu).</param>
        /// <returns>String containing random email address.</returns>
        /// <remarks>All email addresses are for the example domain.</remarks>
        public string GetEmailAddress(string topLevelDomain)
        {
            StringBuilder sb = new StringBuilder();
            string emailAddress = string.Empty;


            sb.Append(_rs.GetRandomSyllablesLC(_rn.GenerateRandomInt(2, 4)));
            sb.Append(@"@example.");
            sb.Append(topLevelDomain.TrimStart('.'));

            emailAddress = sb.ToString();

            return emailAddress;
        }

        /// <summary>
        /// Retrieves random value from set of random values loaded in constructor for this class.
        /// </summary>
        /// <returns>String with random value. Empty string is returned if no custom random value file was specified when this instance was constructed.</returns>
        public string GetRandomValue()
        {
            return _customRandomValues[_rn.GenerateRandomInt(0, _customRandomValuesMaxInx)];
        }


    }//end class
}//end namespace
