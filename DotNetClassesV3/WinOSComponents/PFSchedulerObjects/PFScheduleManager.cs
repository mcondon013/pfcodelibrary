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
using System.Data;
using System.Data.Common;
using AppGlobals;
using PFCollectionsObjects;
using PFDataAccessObjects;

namespace PFSchedulerObjects
{
    /// <summary>
    /// Contains routines for storing and retrieving task schedule objects from a database.
    /// </summary>
    public class PFScheduleManager
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private variables for properties
        private PFList<PFSchedule> _scheduleList = new PFList<PFSchedule>();
        private string _scheduleManagerName = "ScheduleManager";

        private enScheduleStorageType _scheduleStorageType = enScheduleStorageType.XMLFiles;
        private string _connectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"ScheduleManager\Schedules\");

        private string _scheduleDefinitionsSelectAllSQL = "select ScheduleObject from Schedules order by ScheduleName";
        private string _scheduleDefinitionsSelectScheduleSQL = "select ScheduleObject from Schedules where ScheduleName = '<schedulename>'";
        private string _scheduleDefinitionsUpdateSQL = "update Schedules set ScheduleObject = '<scheduleobject>' where ScheduleName = '<schedulename>'";
        private string _scheduleDefinitionsInsertSQL = "insert Schedules (ScheduleName, ScheduleObject) values ('<schedulename>', '<scheduleobject>')";
        private string _scheduleDefinitionsDeleteSQL = "delete Schedules where ScheduleName = '<schedulename>'";
        private string _scheduleDefinitionsIfScheduleExistsSQL = "select count(*) as numRecsFound from Schedules  where ScheduleName = '<schedulename>'";


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFScheduleManager()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="scheduleStorageType">Type of storage.</param>
        /// <param name="connectionString">Connection string for storage.</param>
        public PFScheduleManager(enScheduleStorageType scheduleStorageType, string connectionString)
        {
            InitInstance(scheduleStorageType, connectionString, "ScheduleManager");
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="scheduleStorageType">Type of storage.</param>
        /// <param name="connectionString">Connection string for storage.</param>
        /// <param name="scheduleManagerName">Name of schedule manager to use.</param>
        public PFScheduleManager(enScheduleStorageType scheduleStorageType, string connectionString, string scheduleManagerName)
        {
            InitInstance(scheduleStorageType, connectionString, scheduleManagerName);
        }

        private void InitInstance(enScheduleStorageType scheduleStorageType, string connectionString, string scheduleManagerName)
        {
            string configValue = string.Empty;

            _scheduleStorageType = scheduleStorageType;
            _connectionString = connectionString;
            _scheduleManagerName = scheduleManagerName;

            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsSelectAllSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsSelectAllSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsSelectScheduleSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsSelectScheduleSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsInsertSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsInsertSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsUpdateSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsUpdateSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsDeleteSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsDeleteSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("ScheduleDefinitionsIfScheduleExistsSQL", string.Empty);
            if (configValue.Length > 0)
                _scheduleDefinitionsIfScheduleExistsSQL = configValue;

        }

       //properties

        /// <summary>
        /// Object that stores list of schedules to be managed.
        /// </summary>
        public PFList<PFSchedule> ScheduleList
        {
            get
            {
                return _scheduleList;
            }
            set
            {
                _scheduleList = value;
            }
        }

        /// <summary>
        /// Name given to the the manager.
        /// </summary>
        public string ScheduleManagerName
        {
            get
            {
                return _scheduleManagerName;
            }
            set
            {
                _scheduleManagerName = value;
            }
        }

        /// <summary>
        /// ScheduleStorageType Property (XMLFiles or Database).
        /// </summary>
        public enScheduleStorageType ScheduleStorageType
        {
            get
            {
                return _scheduleStorageType;
            }
            set
            {
                _scheduleStorageType = value;
            }
        }

        /// <summary>
        /// ConnectionString Property: Specify path to folder containing the definition files if ScheduleStorageType is XMLFiles; specify database connection string if SheduleStorageType is Database.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }



        //methods

        /// <summary>
        /// Retrieves list of stored the objects.
        /// </summary>
        /// <returns>List of PFSchedule objects.</returns>
        public PFList<PFSchedule> GetScheduleList()
        {
            PFList<PFSchedule> scheduleList = null;

            if (this.ScheduleStorageType == enScheduleStorageType.XMLFiles)
            {
                scheduleList = GetScheduleListXML();
            }
            else if (this.ScheduleStorageType == enScheduleStorageType.Database)
            {
                scheduleList = GetScheduleListDatabase();
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Invalid or missing ScheduleStorageType: ");
                _msg.Append(this.ScheduleStorageType.ToString());
                throw new System.Exception(_msg.ToString());
            }

            _scheduleList = scheduleList;

            return scheduleList;
        }

        private PFList<PFSchedule> GetScheduleListXML()
        {
            PFList<PFSchedule> scheduleList = new PFList<PFSchedule>();
            string[] scheduleFiles = null;


            scheduleFiles = Directory.GetFiles(this.ConnectionString, "*.xml", SearchOption.TopDirectoryOnly);

            if (scheduleFiles != null)
            {
                for (int i = 0; i < scheduleFiles.Length; i++)
                {
                    PFSchedule schedule = PFSchedule.LoadFromXmlFile(scheduleFiles[i]);
                    scheduleList.Add(schedule);
                }
            }

            return scheduleList;
        }

        private PFList<PFSchedule> GetScheduleListDatabase()
        {
            PFList<PFSchedule> scheduleList = new PFList<PFSchedule>();
            PFDatabase db = null;

            try
            {
                db = new PFDatabase(DatabasePlatform.SQLServerCE35);
                db.ConnectionString = this.ConnectionString;
                db.OpenConnection();

                DbDataReader rdr = db.RunQueryDataReader(_scheduleDefinitionsSelectAllSQL, System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    string str = rdr["ScheduleObject"].ToString();
                    PFSchedule schedule = PFSchedule.LoadFromXmlString(str);
                    scheduleList.Add(schedule);
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
            }




            return scheduleList;
        }

        /// <summary>
        /// Retrieves the object for a specific schedule.
        /// </summary>
        /// <param name="scheduleName">Name of the schedule to get.</param>
        /// <returns>Schedule object.</returns>
        public PFSchedule GetScheduleByName(string scheduleName)
        {
            PFList<PFSchedule> scheduleList = null;
            PFSchedule schedule = null;

            if (this.ScheduleStorageType == enScheduleStorageType.XMLFiles)
            {
                scheduleList = GetScheduleListXML();
            }
            else if (this.ScheduleStorageType == enScheduleStorageType.Database)
            {
                scheduleList = GetScheduleListDatabase();
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Invalid or missing ScheduleStorageType: ");
                _msg.Append(this.ScheduleStorageType.ToString());
                throw new System.Exception(_msg.ToString());
            }

            _scheduleList = scheduleList;

            for (int i = 0; i < scheduleList.Count; i++)
            {
                if (scheduleName.ToUpper() == scheduleList[i].Name.ToUpper())
                {
                    schedule = scheduleList[i];
                    break;
                }
            }

            return schedule;
        }




        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFScheduleManager));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFScheduleManager.</returns>
        public static PFScheduleManager LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFScheduleManager));
            TextReader textReader = new StreamReader(filePath);
            PFScheduleManager objectInstance;
            objectInstance = (PFScheduleManager)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
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
            XmlSerializer ser = new XmlSerializer(typeof(PFScheduleManager));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFScheduleManager.</returns>
        public static PFScheduleManager LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFScheduleManager));
            StringReader strReader = new StringReader(xmlString);
            PFScheduleManager objectInstance;
            objectInstance = (PFScheduleManager)deserializer.Deserialize(strReader);
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
