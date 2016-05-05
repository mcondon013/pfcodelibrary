//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
    /// <summary>
    /// Class contains methods for generation random boolean values in various formats.
    /// </summary>
    public class RandomBoolean
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private RandomNumber _rn = new RandomNumber();

        //private variables for properties

        private double _percentTrueValues = 0.50;
        private double _percentFalseValues = 0.50;

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>Random values will have an equal chance of being true or false.</remarks>
        public RandomBoolean()
        {
            CalculateTrueFalsePercentages((double)0.50, (double)0.50);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="trueValuesCount">Specify a number that will be used to calculate the chances that random boolean will be a true value.</param>
        /// <param name="falseValuesCount">Specify a number that will be used to calculate the chances that random boolean will be a false value.</param>
        /// <remarks> If trueValuesCount is .67 and falseValuesCount is .33, then the chances of getting a random true value are 67 percent.
        /// if percentTrueValue is 300 and falseValuesCount is 100, then the chances of getting a random true value are 75 percent. (300/400 = .75, 100/400 = .25</remarks>
        public RandomBoolean(double trueValuesCount, double falseValuesCount)
        {
            CalculateTrueFalsePercentages(trueValuesCount, falseValuesCount);
        }

        private void CalculateTrueFalsePercentages(double trueValuesCount, double falseValuesCount)
        {
            double trueVals = trueValuesCount;
            double falseVals = falseValuesCount;
            double tot = trueVals + falseVals;

            _percentTrueValues = trueVals / tot;
            _percentFalseValues = falseVals / tot;
        }

        //properties

        /// <summary>
        /// PercentTrueValues Property.
        /// </summary>
        public double PercentTrueValues
        {
            get
            {
                return _percentTrueValues;
            }
            set
            {
                _percentTrueValues = value;
            }
        }

        /// <summary>
        /// PercentFalseValues Property.
        /// </summary>
        public double PercentFalseValues
        {
            get
            {
                return _percentFalseValues;
            }
            set
            {
                _percentFalseValues = value;
            }
        }

        //methods

        /// <summary>
        /// Produces a random boolean value.
        /// </summary>
        /// <returns>Random boolean value.</returns>
        public bool GetRandomBoolean()
        {
            int trueVals = (int)(_percentTrueValues * 100.0);
            //int falseVals = (int)(_percentFalseValues * 100.0);

            return GenerateRandomBoolean(trueVals);

        }

        private bool GenerateRandomBoolean(int trueVals)
        {
            bool res = false;
            if(_rn.GenerateRandomInt(1, 100) < trueVals)
            {
                res = true;
            }
            return res;
        }

        /// <summary>
        /// Generate boolean represented by an integer for the true and false values (e.g. 1 or 0, -1 or 0).
        /// </summary>
        /// <param name="trueVal">Number to assign to true values.</param>
        /// <param name="falseVal">Number to assign to false values.</param>
        /// <returns>Integer representing a true/false value in integer format.</returns>
        public int GetRandomInt(int trueVal, int falseVal)
        {
            int res = 0;
            int trueVals = (int)(_percentTrueValues * 100.0);
            bool boolVal = false;

            boolVal = GenerateRandomBoolean(trueVals);

            if (boolVal == true)
                res = trueVal;
            else
                res = falseVal;


            return res;
        }

        /// <summary>
        /// Generate boolean represented by a string for the true and false value (e.g. "true" or "false", "yes" or "no").
        /// </summary>
        /// <param name="trueVal">String to assign to true values.</param>
        /// <param name="falseVal">String to assign to false values.</param>
        /// <returns>String representing a true/false value in string format.</returns>
        public string GetRandomString(string trueVal, string falseVal)
        {
            string res = "false";
            int trueVals = (int)(_percentTrueValues * 100.0);
            bool boolVal = false;

            boolVal = GenerateRandomBoolean(trueVals);

            if (boolVal == true)
                res = trueVal;
            else
                res = falseVal;


            return res;
        }

    }//end class
}//end namespace
