//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace PFSchedulerObjects
{
    /// <summary>
    /// Class for managing various elements of scheduling.
    /// </summary>
    public class PFScheduler
    {
        //private work variables
        private static StringBuilder _msg = new StringBuilder();


        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFScheduler()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Converts enumeration constant to its corresponding value.
        /// </summary>
        /// <param name="frequencyName">Constant name for the enum value.</param>
        /// <returns>An enScheduleFrequency value.</returns>
        public static enScheduleFrequency GetScheduleFrequency(string frequencyName)
        {
            return (enScheduleFrequency)Enum.Parse(typeof(enScheduleFrequency), frequencyName);
        }

        /// <summary>
        /// Converts enumeration constant to its corresponding value.
        /// </summary>
        /// <param name="frequencyName">Constant name for the enum value.</param>
        /// <returns>An enDailyFrequency value.</returns>
        public static enDailyFrequency GetDailyFrequency(string frequencyName)
        {
            return (enDailyFrequency)Enum.Parse(typeof(enDailyFrequency), frequencyName);
        }

        /// <summary>
        /// Converts enumeration constant to its corresponding value.
        /// </summary>
        /// <param name="occursIntervalName">Constant name for the enum value.</param>
        /// <returns>An enDailyFrequency value.</returns>
        public static enDailyOccursInterval GetDailyOccursInterval(string occursIntervalName)
        {
            return (enDailyOccursInterval)Enum.Parse(typeof(enDailyOccursInterval), occursIntervalName);
        }

        /// <summary>
        /// Converts enumeration constant to its corresponding value.
        /// </summary>
        /// <param name="montlyOrdinal">Constant name for the enum value.</param>
        /// <returns>An enMontlyScheduleOrdinal value.</returns>
        public static enMontlyScheduleOrdinal GetMonthlyScheduleOrdinal(string montlyOrdinal)
        {
            return (enMontlyScheduleOrdinal)Enum.Parse(typeof(enMontlyScheduleOrdinal), montlyOrdinal);
        }

        /// <summary>
        /// Converts enumeration constant to its corresponding value.
        /// </summary>
        /// <param name="scheduleDayName">Constant name for the enum value.</param>
        /// <returns>An enScheduleDay value.</returns>
        public static enScheduleDay GetScheduleDay(string scheduleDayName)
        {
            return (enScheduleDay)Enum.Parse(typeof(enScheduleDay), scheduleDayName);
        }

        /// <summary>
        /// Converts enumeration constant to its corresponding value.
        /// </summary>
        /// <param name="scheduleMonthName">Constant name for the enum value.</param>
        /// <returns>An enScheduleMonth value.</returns>
        public static enScheduleMonth GetScheduleMonth(string scheduleMonthName)
        {
            return (enScheduleMonth)Enum.Parse(typeof(enScheduleMonth), scheduleMonthName);
        }

        /// <summary>
        /// Converts enumeration constant to its corresponding value.
        /// </summary>
        /// <param name="lookupResultName">Constant name for the enum value.</param>
        /// <returns>An enScheduleLookupResult value.</returns>
        public static enScheduleLookupResult GetScheduleLookupResult(string lookupResultName)
        {
            return (enScheduleLookupResult)Enum.Parse(typeof(enScheduleLookupResult), lookupResultName);
        }


        // routines to manage saving and retrieving class property data in XML format

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFScheduler));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFScheduler.</returns>
        public static PFScheduler LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFScheduler));
            TextReader textReader = new StreamReader(filePath);
            PFScheduler objectInstance;
            objectInstance = (PFScheduler)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }

        /// <summary>
        /// Routine formats a TimeSpan object into a string with HH:mm:ss format.
        /// </summary>
        /// <param name="ts">TimeSpan object to format.</param>
        /// <returns>String with formatted time span.</returns>
        public static string FormatTimeSpan(TimeSpan ts)
        {
            int hrs = ts.Hours;
            int mins = ts.Minutes;
            int secs = ts.Seconds;
            string formattedTimeSpan = hrs.ToString("00") + ":" + mins.ToString("00") + ":" + secs.ToString("00");

            return formattedTimeSpan;
        }

        /// <summary>
        /// Routine converts a string in HH:mm:ss format to a TimeSpan value.
        /// </summary>
        /// <param name="timeString">String to be converted.</param>
        /// <returns>TimeSpan object.</returns>
        public static TimeSpan GetTimeSpan(string timeString)
        {
            int hrs = 0;
            int mins = 0;
            int secs = 0;
            string[] parsedTime;
            TimeSpan retval;

            parsedTime = timeString.Split(':');
            if (parsedTime.Length == 3)
            {
                hrs = Convert.ToInt32(parsedTime[0]);
                mins = Convert.ToInt32(parsedTime[1]);
                secs = Convert.ToInt32(parsedTime[2]);
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Time value not formatted correctly: ");
                _msg.Append(timeString);
                throw new System.Exception(_msg.ToString());
            }
            retval = new TimeSpan(hrs, mins, secs);
            return retval;
        }


        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns>String value.</returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns>String value.</returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFScheduler));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFScheduler.</returns>
        public static PFScheduler LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFScheduler));
            StringReader strReader = new StringReader(xmlString);
            PFScheduler objectInstance;
            objectInstance = (PFScheduler)deserializer.Deserialize(strReader);
            strReader.Close();
            return objectInstance;
        }


        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace
