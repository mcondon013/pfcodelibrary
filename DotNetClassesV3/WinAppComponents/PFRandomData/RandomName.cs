//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2014
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
    /// Contains routines to generate random person and b usiness names.
    /// </summary>
    public class RandomName
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private enNameLocation _nameLocation = enNameLocation.enUnknown;
        private NameDataArrays _nameDataArrays = new NameDataArrays();
        private string[] _firstNamesFemale = null;
        private string[] _firstNamesMale = null;
        private string[] _lastNames = null;
        private string[] _businessNamesPrefix = null;
        private string[] _businessNamesSuffix = null;
        private string[] _business_Syllable1 = null;
        private string[] _business_Syllable2 = null;
        private RandomNumber _rn = new RandomNumber();
        private RandomString _rs = new RandomString();
        private int _maxFirstNamesFemaleArrayInx = 0;
        private int _maxFirstNamesMaleArrayInx = 0;
        private int _maxLastNamesArrayInx = 0;
        private int _maxBusinessNamesPrefixInx = 0;
        private int _maxBusinessNamesSuffixInx = 0;
        private int _maxBusiness_Syllable1Inx = 0;
        private int _maxBusiness_Syllable2Inx = 0;

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>Defaults to U.S. names if no location specified.</remarks>
        public RandomName()
        {
            _nameLocation = enNameLocation.enUS;
            SetNameArrays(enNameLocation.enUS);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nameLocation">Enum that specifies the location (country) for which name is to be generated.</param>
        /// <remarks>Formats names according to the nameLocation specified.</remarks>
        public RandomName(enNameLocation nameLocation)
        {
            _nameLocation = nameLocation;
            SetNameArrays(nameLocation);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstNamesFemaleFile">Path to file containing list of female first names. Supply empty string or null value if you do not intend to generate female first names with this instance.</param>
        /// <param name="firstNamesMaleFile">Path to file containing list of male first names. Supply empty string or null value if you do not intend to generate male first names with this instance.</param>
        /// <param name="lastNamesFile">Path to file containing list of last names. Supply empty string or null value if you do not intend to generate last names with this instance.</param>
        /// <remarks>Generates random names based on the names stored in the supplied files. If no file is supplied, then empty string will be returned by the Get random name routines.</remarks>
        public RandomName(string firstNamesFemaleFile, string firstNamesMaleFile, string lastNamesFile)
        {
            _nameLocation = enNameLocation.enCustom;
            LoadCustomArrays(firstNamesFemaleFile, firstNamesMaleFile,lastNamesFile);
            SetNameArrays(enNameLocation.enCustom);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstNamesFemaleFile">Path to file containing list of female first names. Supply empty string or null value if you do not intend to generate female first names with this instance.</param>
        /// <param name="firstNamesMaleFile">Path to file containing list of male first names. Supply empty string or null value if you do not intend to generate male first names with this instance.</param>
        /// <param name="lastNamesFile">Path to file containing list of last names. Supply empty string or null value if you do not intend to generate last names with this instance.</param>
        /// <param name="textEncoding">Supply a System.Text.Encoding enum value if .NET file routines have trouble reading your files. This will often be needed when incoming data has accents on individual letters.</param>
        /// <remarks>Generates random names based on the names stored in the supplied files. If no file is supplied, then empty string will be returned by the Get random name routines.</remarks>
        public RandomName(string firstNamesFemaleFile, string firstNamesMaleFile, string lastNamesFile, System.Text.Encoding textEncoding)
        {
            _nameLocation = enNameLocation.enCustom;
            LoadCustomArrays(firstNamesFemaleFile, firstNamesMaleFile, lastNamesFile, textEncoding);
            SetNameArrays(enNameLocation.enCustom);
        }


        private void LoadCustomArrays(string firstNamesFemaleFile, string firstNamesMaleFile, string lastNamesFile)
        {
            if (String.IsNullOrEmpty(firstNamesFemaleFile) == false)
            {
                _nameDataArrays.Custom_FirstNamesFemale = File.ReadAllLines(firstNamesFemaleFile);
            }
            if (String.IsNullOrEmpty(firstNamesMaleFile) == false)
            {
                _nameDataArrays.Custom_FirstNamesMale = File.ReadAllLines(firstNamesMaleFile);
            }
            if (String.IsNullOrEmpty(lastNamesFile) == false)
            {
                _nameDataArrays.Custom_LastNames = File.ReadAllLines(lastNamesFile);
            }
        }

        private void LoadCustomArrays(string firstNamesFemaleFile, string firstNamesMaleFile, string lastNamesFile, System.Text.Encoding textEncoding)
        {
            if (String.IsNullOrEmpty(firstNamesFemaleFile) == false)
            {
                _nameDataArrays.Custom_FirstNamesFemale = File.ReadAllLines(firstNamesFemaleFile, textEncoding);
            }
            if (String.IsNullOrEmpty(firstNamesMaleFile) == false)
            {
                _nameDataArrays.Custom_FirstNamesMale = File.ReadAllLines(firstNamesMaleFile, textEncoding);
            }
            if (String.IsNullOrEmpty(lastNamesFile) == false)
            {
                _nameDataArrays.Custom_LastNames = File.ReadAllLines(lastNamesFile, textEncoding);
            }
        }

        private void SetNameArrays(enNameLocation nameLocation)
        {
            switch (nameLocation)
            {
                case enNameLocation.enUS:
                    _firstNamesFemale = _nameDataArrays.US_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.US_FirstNamesMale;
                    _lastNames = _nameDataArrays.US_LastNames;
                    break;
                case enNameLocation.enCanada:
                    _firstNamesFemale = _nameDataArrays.Canada_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Canada_FirstNamesMale;
                    _lastNames = _nameDataArrays.Canada_LastNames;
                    break;
                case enNameLocation.enQuebec:
                    _firstNamesFemale = _nameDataArrays.Quebec_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Quebec_FirstNamesMale;
                    _lastNames = _nameDataArrays.Quebec_LastNames;
                    break;
                case enNameLocation.enMexico:
                    _firstNamesFemale = _nameDataArrays.Mexico_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Mexico_FirstNamesMale;
                    _lastNames = _nameDataArrays.Mexico_LastNames;
                    break;
                case enNameLocation.enEngland:
                    _firstNamesFemale = _nameDataArrays.England_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.England_FirstNamesMale;
                    _lastNames = _nameDataArrays.England_LastNames;
                    break;
                case enNameLocation.enScotland:
                    _firstNamesFemale = _nameDataArrays.Scotland_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Scotland_FirstNamesMale;
                    _lastNames = _nameDataArrays.Scotland_LastNames;
                    break;
                case enNameLocation.enIreland:
                    _firstNamesFemale = _nameDataArrays.Ireland_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Ireland_FirstNamesMale;
                    _lastNames = _nameDataArrays.Ireland_LastNames;
                    break;
                case enNameLocation.enFrance:
                    _firstNamesFemale = _nameDataArrays.France_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.France_FirstNamesMale;
                    _lastNames = _nameDataArrays.France_LastNames;
                    break;
                case enNameLocation.enItaly:
                    _firstNamesFemale = _nameDataArrays.Italy_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Italy_FirstNamesMale;
                    _lastNames = _nameDataArrays.Italy_LastNames;
                    break;
                case enNameLocation.enSpain:
                    _firstNamesFemale = _nameDataArrays.Spain_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Spain_FirstNamesMale;
                    _lastNames = _nameDataArrays.Spain_LastNames;
                    break;
                case enNameLocation.enPortugal:
                    _firstNamesFemale = _nameDataArrays.Portugal_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Portugal_FirstNamesMale;
                    _lastNames = _nameDataArrays.Portugal_LastNames;
                    break;
                case enNameLocation.enGermany:
                    _firstNamesFemale = _nameDataArrays.Germany_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Germany_FirstNamesMale;
                    _lastNames = _nameDataArrays.Germany_LastNames;
                    break;
                case enNameLocation.enRussia:
                    _firstNamesFemale = _nameDataArrays.Russia_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Russia_FirstNamesMale;
                    _lastNames = _nameDataArrays.Russia_LastNames;
                    break;
                case enNameLocation.enChina:
                    _firstNamesFemale = _nameDataArrays.China_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.China_FirstNamesMale;
                    _lastNames = _nameDataArrays.China_LastNames;
                    break;
                case enNameLocation.enJapan:
                    _firstNamesFemale = _nameDataArrays.Japan_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Japan_FirstNamesMale;
                    _lastNames = _nameDataArrays.Japan_LastNames;
                    break;
                case enNameLocation.enArabia:
                    _firstNamesFemale = _nameDataArrays.Arabia_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Arabia_FirstNamesMale;
                    _lastNames = _nameDataArrays.Arabia_LastNames;
                    break;
                case enNameLocation.enIsrael:
                    _firstNamesFemale = _nameDataArrays.Israel_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Israel_FirstNamesMale;
                    _lastNames = _nameDataArrays.Israel_LastNames;
                    break;
                case enNameLocation.enBrazil:
                    _firstNamesFemale = _nameDataArrays.Brazil_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Brazil_FirstNamesMale;
                    _lastNames = _nameDataArrays.Brazil_LastNames;
                    break;
                case enNameLocation.enArgentina:
                    _firstNamesFemale = _nameDataArrays.Argentina_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Argentina_FirstNamesMale;
                    _lastNames = _nameDataArrays.Argentina_LastNames;
                    break;
                case enNameLocation.enCustom:
                    _firstNamesFemale = _nameDataArrays.Custom_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.Custom_FirstNamesMale;
                    _lastNames = _nameDataArrays.Custom_LastNames;
                    break;
                case enNameLocation.enUnknown:
                    _firstNamesFemale = _nameDataArrays.US_FirstNamesFemale;
                    _firstNamesMale = _nameDataArrays.US_FirstNamesMale;
                    _lastNames = _nameDataArrays.US_LastNames;
                    break;
                default:
                    _msg.Length = 0;
                    _msg.Append("Invalid value for name location: ");
                    _msg.Append(Convert.ToInt32(nameLocation).ToString());
                    throw new System.Exception(_msg.ToString());
                    //break;
            }
            _businessNamesPrefix = _nameDataArrays.BusinessNamesPrefix;
            _businessNamesSuffix = _nameDataArrays.BusinessNamesSuffix;
            _business_Syllable1 = _nameDataArrays.Business_Syllable1;
            _business_Syllable2 = _nameDataArrays.Business_Syllable2;

            _maxFirstNamesFemaleArrayInx = _firstNamesFemale.Length - 1;
            _maxFirstNamesMaleArrayInx = _firstNamesMale.Length - 1;
            _maxLastNamesArrayInx = _lastNames.Length - 1;
            _maxBusinessNamesPrefixInx = _businessNamesPrefix.Length - 1;
            _maxBusinessNamesSuffixInx = _businessNamesSuffix.Length - 1;
            _maxBusiness_Syllable1Inx = _business_Syllable1.Length - 1;
            _maxBusiness_Syllable2Inx = _business_Syllable2.Length - 1;
        }


        //properties

        //methods

        /// <summary>
        /// Gets random female name using name location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetFirstNameFemale()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxFirstNamesFemaleArrayInx);
            return _firstNamesFemale[randInx].Trim();
        }

        /// <summary>
        /// Gets random male name using name location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetFirstNameMale()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxFirstNamesMaleArrayInx);
            return _firstNamesMale[randInx].Trim();
        }

        /// <summary>
        /// Gets random last name using name location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetLastName()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxLastNamesArrayInx);
            return _lastNames[randInx].Trim();
        }

        /// <summary>
        /// Gets random female name and random last name using name location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetFullNameFemale()
        {
            StringBuilder fullName = new StringBuilder();
            int randInx = 0;

            randInx = _rn.GenerateRandomInt((int)0, _maxFirstNamesFemaleArrayInx);
            fullName.Append(_firstNamesFemale[randInx].Trim());

            fullName.Append(" ");

            randInx = _rn.GenerateRandomInt((int)0, _maxLastNamesArrayInx);
            fullName.Append(_lastNames[randInx].Trim());

            return fullName.ToString();
        }

        /// <summary>
        /// Gets random male name and random last name using name location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetFullNameMale()
        {
            StringBuilder fullName = new StringBuilder();
            int randInx = 0;

            randInx = _rn.GenerateRandomInt((int)0, _maxFirstNamesMaleArrayInx);
            fullName.Append(_firstNamesMale[randInx].Trim());

            fullName.Append(" ");

            randInx = _rn.GenerateRandomInt((int)0, _maxLastNamesArrayInx);
            fullName.Append(_lastNames[randInx].Trim());

            return fullName.ToString();
        }

        /// <summary>
        /// Creates a random name that resembles a business name.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetBusinessName()
        {
            StringBuilder bizname = new StringBuilder();
            int randNumNameType = _rn.GenerateRandomInt(1, 100);
            int randNumPrefix = _rn.GenerateRandomInt(1, 100);
            int randNumSuffix = _rn.GenerateRandomInt(1, 100);

            if (randNumPrefix <= 20)
            {
                bizname.Append(_businessNamesPrefix[_rn.GenerateRandomInt(0,_maxBusinessNamesPrefixInx)]);
                bizname.Append(" ");
            }

            if (randNumNameType <= 33)
            {
                //use random syllables
                bizname.Append(_rs.GetRandomSyllablesUCLC(_rn.GenerateRandomInt(1, 3)));
            }
            else if (randNumNameType <= 66)
            {
                //use special list of syllables
                bizname.Append(_business_Syllable1[_rn.GenerateRandomInt(0, _maxBusiness_Syllable1Inx)]);
                bizname.Append(_business_Syllable2[_rn.GenerateRandomInt(0, _maxBusiness_Syllable2Inx)]);
            }
            else
            {
                //use last names
                bizname.Append(_lastNames[_rn.GenerateRandomInt(0, _maxLastNamesArrayInx)]);
            }

            if (randNumSuffix <= 20)
            {
                bizname.Append(" ");
                bizname.Append(_businessNamesSuffix[_rn.GenerateRandomInt(0,_maxBusinessNamesSuffixInx)]);
            }

            return bizname.ToString();
        }

        /// <summary>
        /// Retrieves name from custom names file supplied by caller.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetCustomName()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxLastNamesArrayInx);
            return _lastNames[randInx].Trim();
        }

    }//end class
}//end namespace
