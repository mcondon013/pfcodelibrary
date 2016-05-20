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
using System.Diagnostics;
using PFCollectionsObjects;
using PFDataAccessObjects;
using AppGlobals;

namespace PFTaskObjects
{
    /// <summary>
    /// Stores run history results for individual task.
    /// </summary>
    public class PFTaskHistoryEntry
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private Guid _taskEntryID = Guid.NewGuid();
        private Guid? _taskID = null;
        private string _taskName = string.Empty;
        private DateTime _scheduledStartTime = DateTime.MinValue;
        private DateTime _actualStartTime = DateTime.MinValue;
        private DateTime _actualEndTime = DateTime.MinValue;
        private enTaskRunResult _taskRunResult = enTaskRunResult.Unknown;
        private int _taskReturnCode = 0;
        private string _taskOutputMessages = string.Empty;
        private string _taskErrorMessages = string.Empty;

        private string _taskHistoryDefinitionsSelectAllSQL = "select TaskHistoryObject from TaskHistoryEntries order by TaskName, RunDate";
        internal string _taskHistoryDefinitionsSelectTaskSQL = "select TaskHistoryObject from TaskHistoryEntries where TaskName = '<taskname>' order by RunDate";
        internal string _taskHistoryDefinitionsSelectTaskEntrySQL = "select TaskHistoryObject from TaskHistoryEntries where TaskName = '<taskname>' and RunDate = '<rundate>'";
        private string _taskHistoryDefinitionsUpdateSQL = "update TaskHistoryEntries set TaskHistoryObject = '<taskobject>' where TaskName = '<taskname>' and RunDate = '<rundate>'";
        private string _taskHistoryDefinitionsInsertSQL = "insert TaskHistoryEntries (TaskName, RunDate, TaskHistoryObject) values ('<taskname>', '<rundate>', '<taskobject>')";
        private string _taskHistoryDefinitionsDeleteTaskSQL = "delete TaskHistoryEntries where TaskName = '<taskname>'";
        private string _taskHistoryDefinitionsDeleteTaskEntrySQL = "delete TaskHistoryEntries where TaskName = '<taskname>' and and RunDate = '<rundate>'";
        private string _taskHistoryDefinitionsIfTaskHistoryExistsSQL = "select count(*) as numRecsFound from TaskHistoryEntries  where TaskName = '<taskname>' and RunDate = '<rundate>'";

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFTaskHistoryEntry()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            string configValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryDefinitionsSelectAllSQL", string.Empty);
            if (configValue.Length > 0)
                _taskHistoryDefinitionsSelectAllSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryDefinitionsSelectTaskSQL", string.Empty);
            if (configValue.Length > 0)
                _taskHistoryDefinitionsSelectTaskSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryDefinitionsSelectTaskEntrySQL", string.Empty);
            if (configValue.Length > 0)
                _taskHistoryDefinitionsSelectTaskEntrySQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryDefinitionsInsertSQL", string.Empty);
            if (configValue.Length > 0)
                _taskHistoryDefinitionsInsertSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryDefinitionsUpdateSQL", string.Empty);
            if (configValue.Length > 0)
                _taskHistoryDefinitionsUpdateSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryDefinitionsDeleteTaskSQL", string.Empty);
            if (configValue.Length > 0)
                _taskHistoryDefinitionsDeleteTaskSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryDefinitionsDeleteTaskEntrySQL", string.Empty);
            if (configValue.Length > 0)
                _taskHistoryDefinitionsDeleteTaskEntrySQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskHistoryDefinitionsIfTasHistorykExistsSQL", string.Empty);
            if (configValue.Length > 0)
                _taskHistoryDefinitionsIfTaskHistoryExistsSQL = configValue;

        }



        //properties

        /// <summary>
        /// TaskEntryID Property.
        /// </summary>
        public Guid TaskEntryID
        {
            get
            {
                return _taskEntryID;
            }
        }

        /// <summary>
        /// TaskID Property.
        /// </summary>
        public Guid? TaskID
        {
            get
            {
                return _taskID;
            }
            set
            {
                _taskID = value;
            }
        }

        /// <summary>
        /// TaskName Property.
        /// </summary>
        public string TaskName
        {
            get
            {
                return _taskName;
            }
            set
            {
                _taskName = value;
            }
        }

        /// <summary>
        /// Date/Time at which the was scheduled to start.
        /// </summary>
        public DateTime ScheduledStartTime
        {
            get
            {
                return _scheduledStartTime;
            }
            set
            {
                _scheduledStartTime = value;
            }
        }

        /// <summary>
        /// Actual time at which the was started.
        /// </summary>
        public DateTime ActualStartTime
        {
            get
            {
                return _actualStartTime;
            }
            set
            {
                _actualStartTime = value;
            }
        }

        /// <summary>
        /// Actual time at which the ended.
        /// </summary>
        public DateTime ActualEndTime
        {
            get
            {
                return _actualEndTime;
            }
            set
            {
                _actualEndTime = value;
            }
        }

        /// <summary>
        /// Result for the the execution.
        /// </summary>
        /// <remarks>Result of NotRun means the the did not run within the scheduled start time window.</remarks>
        public enTaskRunResult TaskRunResult
        {
            get
            {
                return _taskRunResult;
            }
            set
            {
                _taskRunResult = value;
            }
        }

        /// <summary>
        /// Arbitrary return code for the the. 0 usually means success. Any other number probably means a failure of some kind.
        /// </summary>
        /// <remarks>Returns code meanings are dependent upon the application that is activated by the the.</remarks>
        public int TaskReturnCode
        {
            get
            {
                return _taskReturnCode;
            }
            set
            {
                _taskReturnCode = value;
            }
        }

        /// <summary>
        /// Information and warning essages that are normally output to the standard console output are stored in this field.
        /// </summary>
        public string TaskOutputMessages
        {
            get
            {
                return _taskOutputMessages;
            }
            set
            {
                _taskOutputMessages = value;
            }
        }

        /// <summary>
        /// Error messages that are normally output to the error output on the console are stored in this field.
        /// </summary>
        public string TaskErrorMessages
        {
            get
            {
                return _taskErrorMessages;
            }
            set
            {
                _taskErrorMessages = value;
            }
        }



        //methods

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFTaskHistoryEntry));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Saves the public property values contained in the current instance to the database specified by the connection string.
        /// </summary>
        /// <param name="connectionString">Contains information needed to open the database.</param>
        /// <remarks>Task name must be unique in the database. SQL Server CE 3.5 local file used for database storage.</remarks>
        public void SaveToDatabase(string connectionString)
        {
            string sqlStmt = string.Empty;
            PFDatabase db = new PFDatabase(DatabasePlatform.SQLServerCE35);
            DbDataReader rdr = null;
            int numRecsFound = 0;
            int numRecsAffected = 0;

            db.ConnectionString = connectionString;
            db.OpenConnection();


            //check if already exists
            sqlStmt = _taskHistoryDefinitionsIfTaskHistoryExistsSQL.Replace("<taskname>", this.TaskName);
            sqlStmt = sqlStmt.Replace("<rundate>",this.ActualStartTime.ToString("MM/dd/yyyy HH:mm:ss"));
            rdr = db.RunQueryDataReader(sqlStmt, CommandType.Text);
            numRecsFound = 0;
            while (rdr.Read())
            {
                numRecsFound = rdr.GetInt32(0);
                break;  //should be only one record
            }

            // if exists update it
            if (numRecsFound > 0)
            {
                //update the record
                sqlStmt = _taskHistoryDefinitionsUpdateSQL.Replace("<taskname>", this.TaskName);
                sqlStmt = sqlStmt.Replace("<taskobject>", this.ToXmlString());
                sqlStmt = sqlStmt.Replace("<rundate>", this.ActualStartTime.ToString("MM/dd/yyyy HH:mm:ss"));
                numRecsAffected = db.RunNonQuery(sqlStmt, CommandType.Text);
            }
            else
            {
                //insert the new record
                sqlStmt = _taskHistoryDefinitionsInsertSQL.Replace("<taskname>", this.TaskName);
                sqlStmt = sqlStmt.Replace("<rundate>", this.ActualStartTime.ToString("MM/dd/yyyy HH:mm:ss"));
                sqlStmt = sqlStmt.Replace("<taskobject>", this.ToXmlString());
                numRecsAffected = db.RunNonQuery(sqlStmt, CommandType.Text);
            }


            db.CloseConnection();


        }



        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFTaskHistoryEntry.</returns>
        public static PFTaskHistoryEntry LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFTaskHistoryEntry));
            TextReader textReader = new StreamReader(filePath);
            PFTaskHistoryEntry objectInstance;
            objectInstance = (PFTaskHistoryEntry)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a database record.
        /// </summary>
        /// <param name="connectionString">Connection parameters for the database.</param>
        /// <param name="taskName">Name of the the to retrieve.</param>
        /// <param name="actualStartTime">Actual start time for the history entry to be retrieved.</param>
        /// <returns>TaskHistoryEntry object.</returns>
        public static PFTaskHistoryEntry LoadFromDatabase(string connectionString, string taskName, DateTime actualStartTime)
        {
            string sqlStmt = string.Empty;
            PFTaskHistoryEntry objectInstance = null;
            PFTaskHistoryEntry tempObjectInstance = new PFTaskHistoryEntry();
            PFDatabase db = new PFDatabase(DatabasePlatform.SQLServerCE35);
            DbDataReader rdr = null;
            string taskHistoryDefXml = string.Empty;

            db.ConnectionString = connectionString;
            db.OpenConnection();

            sqlStmt = tempObjectInstance._taskHistoryDefinitionsSelectTaskEntrySQL.Replace("<taskname>", taskName);
            sqlStmt = sqlStmt.Replace("<rundate>", actualStartTime.ToString("MM/dd/yyyy HH:mm:ss"));
            rdr = db.RunQueryDataReader(sqlStmt, CommandType.Text);
            while (rdr.Read())
            {
                taskHistoryDefXml = rdr.GetString(0);
                objectInstance = PFTaskHistoryEntry.LoadFromXmlString(taskHistoryDefXml);
                break;  //should be only one record
            }


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
                StringBuilder tempObjectInstance = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    tempObjectInstance.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        tempObjectInstance.Append("Index ");
                        tempObjectInstance.Append(i.ToString());
                        tempObjectInstance.Append(" = ");
                        tempObjectInstance.Append(val = prop.GetValue(this, new object[] { i }));
                        tempObjectInstance.Append("  ");
                    }
                    val = tempObjectInstance.ToString();
                }
                else
                {
                    //this is an indexed property
                    tempObjectInstance.Length = 0;
                    tempObjectInstance.Append("Num indexes for property: ");
                    tempObjectInstance.Append(prop.GetIndexParameters().Length.ToString());
                    tempObjectInstance.Append("  ");
                    val = tempObjectInstance.ToString();
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
            XmlSerializer ser = new XmlSerializer(typeof(PFTaskHistoryEntry));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFTaskHistoryEntry.</returns>
        public static PFTaskHistoryEntry LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFTaskHistoryEntry));
            StringReader strReader = new StringReader(xmlString);
            PFTaskHistoryEntry objectInstance;
            objectInstance = (PFTaskHistoryEntry)deserializer.Deserialize(strReader);
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
