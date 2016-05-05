//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestprogSystemObjectsDLL
{
    /// <summary>
    /// Initial class prototype for ProFast application or library code that includes ToString override.
    /// </summary>
    public class PFInitClassWithToStringOverride
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private varialbles for properties

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFInitClassWithToStringOverride()
        {
            ;
        }

        //properties

        //methods

        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name and value for all public properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties();
            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);
                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");
            }

            return data.ToString();
        }


    }//end class
}//end namespace
