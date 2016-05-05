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
    /// Contains routines for generating random location names.
    /// </summary>
    public class RandomLocation
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private enLocation _location = enLocation.enUnknown;
        private string[] _cityNames = null;
        private string[] _stateProvinceCodes = null;
        private string[] _stateProvinceNames = null;
        private string[] _streetNames = null;
        private string[] _neighborhoodNames = null;    //this is only filled in when Mexico is chosen as the location
        private RandomNumber _rn = new RandomNumber();
        private int _maxCityNamesArrayInx = 0;
        private int _maxStateProvinceCodesArrayInx = 0;
        private int _maxStateProvinceNamesArrayInx = 0;
        private int _maxStreetNamesArrayInx = 0;
        private int _maxNeighborhoodNamesArrayInx = 0;


        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomLocation()
        {
            SetLocationArrays(enLocation.enUS);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loc">Enumeration value for the location to be represented by this instance.</param>
        public RandomLocation(enLocation loc)
        {
            SetLocationArrays(loc);
        }

        private void SetLocationArrays(enLocation loc)
        {
            _location = loc;
            LocationDataArrays lda = new LocationDataArrays();

            switch (loc)
            {
                case enLocation.enUS:
                    _cityNames = lda.US_CityNames;
                    _stateProvinceCodes = lda.US_StateCodes;
                    _stateProvinceNames = lda.US_StateNames;
                    _streetNames = lda.US_StreetNames;
                    _neighborhoodNames = new string[] {""}; 
                    break;
                case enLocation.enCanada:
                    _cityNames = lda.Canada_CityNames;
                    _stateProvinceCodes = lda.Canada_ProvinceCodes;
                    _stateProvinceNames = lda.Canada_ProvinceNames;
                    _streetNames = lda.Canada_StreetNames;
                    _neighborhoodNames = new string[] {""}; 
                    break;
                case enLocation.enQuebec:
                    _cityNames = lda.Quebec_CityNames;
                    _stateProvinceCodes = new string[] {"QC"};
                    _stateProvinceNames = new string[] { "Quebec" };
                    _streetNames = lda.Quebec_StreetNames;
                    _neighborhoodNames = new string[] {""};
                    break;
                case enLocation.enMexico:
                    _cityNames = lda.Mexico_CityNames;
                    _stateProvinceCodes = lda.Mexico_StateCodes;
                    _stateProvinceNames = lda.Mexico_StateNames;
                    _streetNames = lda.Mexico_StreetNames;
                    _neighborhoodNames = lda.Mexico_NeighborhoodNames; 
                    break;
                case enLocation.enCustom:
                    _cityNames = lda.US_CityNames;
                    _stateProvinceCodes = lda.US_StateCodes;
                    _stateProvinceNames = lda.US_StateNames;
                    _streetNames = lda.US_StreetNames;
                    _neighborhoodNames = new string[] {""}; 
                    break;
                default:
                    break;
            }

            _maxCityNamesArrayInx = _cityNames.Length - 1;
            _maxStateProvinceCodesArrayInx = _stateProvinceCodes.Length - 1;
            _maxStateProvinceNamesArrayInx = _stateProvinceNames.Length - 1;
            _maxStreetNamesArrayInx = _streetNames.Length - 1;
            _maxNeighborhoodNamesArrayInx = _neighborhoodNames.Length - 1;

        }

        //properties

        //methods
        /// <summary>
        /// Reads custom location names from a file into the array for the enLocationNameType specified.
        /// </summary>
        /// <param name="customLocationFile">Full path to file containing the custom names.</param>
        /// <param name="locNameType">Type of location name contained n the customLocationFile.</param>
        public void LoadCustomLocation(string customLocationFile, enLocationNameType locNameType)
        {
            LoadCustomArray(customLocationFile, locNameType, null);
        }

        private void LoadCustomArray(string customLocationFile, enLocationNameType locNameType, System.Text.Encoding textEncoding)
        {
            string[] locArray = null;

            if (String.IsNullOrEmpty(customLocationFile) == false)
            {
                if (textEncoding == null)
                {
                    locArray = File.ReadAllLines(customLocationFile);
                }
                else
                {
                    locArray = File.ReadAllLines(customLocationFile, textEncoding);
                }
            }

            switch (locNameType)
            {
                case enLocationNameType.enCityName:
                    _cityNames = locArray;
                    _maxCityNamesArrayInx = _cityNames.Length - 1;
                    break;
                case enLocationNameType.enStateProvinceCode:
                    _stateProvinceCodes = locArray;
                    _maxStateProvinceCodesArrayInx = _stateProvinceCodes.Length - 1;
                    break;
                case enLocationNameType.enStateProvinceName:
                    _stateProvinceNames = locArray;
                    _maxStateProvinceNamesArrayInx = _stateProvinceNames.Length - 1;
                    break;
                case enLocationNameType.enStreetName:
                    _streetNames = locArray;
                    _maxStreetNamesArrayInx = _streetNames.Length - 1;
                    break;
                case enLocationNameType.enNeighborhoodName:
                    _neighborhoodNames = locArray;
                    _maxNeighborhoodNamesArrayInx = _neighborhoodNames.Length - 1;
                    break;
                default:
                    _cityNames = locArray;
                    _maxCityNamesArrayInx = _cityNames.Length - 1;
                    break;
            }

        }

        /// <summary>
        /// Gets random city name using location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetCityName()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxCityNamesArrayInx);
            return _cityNames[randInx].Trim();
        }

        /// <summary>
        /// Gets random state/province code using location specified for this instance.
        /// </summary>
        /// <returns>String containing state/province code.</returns>
        public string GetStateProvinceCode()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxStateProvinceCodesArrayInx);
            return _stateProvinceCodes[randInx].Trim();
        }

        /// <summary>
        /// Gets random state/province name using location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetStateProvinceName()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxStateProvinceNamesArrayInx);
            return _stateProvinceNames[randInx].Trim();
        }

        /// <summary>
        /// Gets random street name using location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        public string GetStreetName()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxStreetNamesArrayInx);
            return _streetNames[randInx].Trim();
        }

        /// <summary>
        /// Gets random neighborhood name using location specified for this instance.
        /// </summary>
        /// <returns>String containing name.</returns>
        /// <remarks>This call will usually only return a value if the location for the class instance is Mexico.</remarks>
        public string GetNeighborhoodName()
        {
            int randInx = _rn.GenerateRandomInt((int)0, _maxNeighborhoodNamesArrayInx);
            return _neighborhoodNames[randInx].Trim();
        }



    }//end class
}//end namespace
