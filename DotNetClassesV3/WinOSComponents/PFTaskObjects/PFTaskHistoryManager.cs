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
using PFCollectionsObjects;
using PFDataAccessObjects;
using AppGlobals;

namespace PFTaskObjects
{
    /// <summary>
    /// Manages task history database.
    /// </summary>
    public class PFTaskHistoryManager
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private PFList<PFTaskHistoryEntry> _taskHistoryList = new PFList<PFTaskHistoryEntry>();
        private int _defaultMaxHistoryEntriesPerTask = 25;

        private enTaskStorageType _taskStorageType = enTaskStorageType.XMLFiles;
        private string _connectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Tasks\TaskHistory");

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
        public PFTaskHistoryManager()
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
        /// List containing entries for each time the has been run.
        /// </summary>
        public PFList<PFTaskHistoryEntry> TaskHistoryList
        {
            get
            {
                return _taskHistoryList;
            }
            set
            {
                _taskHistoryList = value;
            }
        }

        /// <summary>
        /// Specifies maximum number of history entries a the can have. If maximum entries specified by the the is greater than default maximum, then default maximum will be used.
        ///  If maximum entries specified for the the is less than default maximum then the maximum entries specified for the the will be used.
        /// </summary>
        public int DefaultMaxHistoryEntriesPerTask
        {
            get
            {
                return _defaultMaxHistoryEntriesPerTask;
            }
            set
            {
                _defaultMaxHistoryEntriesPerTask = value;
            }
        }

        /// <summary>
        /// TaskStorageType Property (XMLFiles or Database).
        /// </summary>
        public enTaskStorageType TaskStorageType
        {
            get
            {
                return _taskStorageType;
            }
            set
            {
                _taskStorageType = value;
            }
        }

        /// <summary>
        /// ConnectionString Property: Specify path to folder containing the history entry definition files if TaskStorageType is XMLFiles; specify database connection string if TaskStorageType is Database.
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
        /// <param name="taskName">Name of task.</param>
        /// <returns>List of PFTask objects.</returns>
        public PFList<PFTaskHistoryEntry> GetTaskHistoryList(string taskName)
        {
            PFList<PFTaskHistoryEntry> taskHistoryList = null;

            if (this.TaskStorageType == enTaskStorageType.XMLFiles)
            {
                taskHistoryList = GetTaskListXML(taskName);
            }
            else if (this.TaskStorageType == enTaskStorageType.Database)
            {
                taskHistoryList = GetTaskListDatabase(taskName);
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Invalid or missing TaskStorageType: ");
                _msg.Append(this.TaskStorageType.ToString());
                throw new System.Exception(_msg.ToString());
            }

            _taskHistoryList = taskHistoryList;

            return taskHistoryList;
        }

        private PFList<PFTaskHistoryEntry> GetTaskListXML(string taskName)
        {
            PFList<PFTaskHistoryEntry> taskHistoryEntryList = new PFList<PFTaskHistoryEntry>();
            string[] taskHistoryEntryFiles = null;
            string searchPattern = taskName + "*.xml";


            taskHistoryEntryFiles = Directory.GetFiles(this.ConnectionString,searchPattern,SearchOption.TopDirectoryOnly);

            if (taskHistoryEntryFiles != null)
            {
                for (int i = 0; i < taskHistoryEntryFiles.Length; i++)
                {
                    PFTaskHistoryEntry the = PFTaskHistoryEntry.LoadFromXmlFile(taskHistoryEntryFiles[i]);
                    taskHistoryEntryList.Add(the);
                }
            }

            return taskHistoryEntryList;
        }

        private PFList<PFTaskHistoryEntry> GetTaskListDatabase(string taskName)
        {
            PFList<PFTaskHistoryEntry> taskHistoryEntryList = new PFList<PFTaskHistoryEntry>();
            PFDatabase db = null;
            string sqlStmt = string.Empty;

            try
            {
                db = new PFDatabase(DatabasePlatform.SQLServerCE35);
                db.ConnectionString = this.ConnectionString;
                db.OpenConnection();

                sqlStmt = _taskHistoryDefinitionsSelectTaskSQL.Replace("<taskname>",taskName);
                DbDataReader rdr = db.RunQueryDataReader(sqlStmt, System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    string str = rdr["TaskHistoryObject"].ToString();
                    PFTaskHistoryEntry the = PFTaskHistoryEntry.LoadFromXmlString(str);
                    taskHistoryEntryList.Add(the);
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




            return taskHistoryEntryList;
        }



        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFTaskHistoryManager));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFTaskHistoryManager.</returns>
        public static PFTaskHistoryManager LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFTaskHistoryManager));
            TextReader textReader = new StreamReader(filePath);
            PFTaskHistoryManager objectInstance;
            objectInstance = (PFTaskHistoryManager)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(PFTaskHistoryManager));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFTaskHistoryManager.</returns>
        public static PFTaskHistoryManager LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFTaskHistoryManager));
            StringReader strReader = new StringReader(xmlString);
            PFTaskHistoryManager objectInstance;
            objectInstance = (PFTaskHistoryManager)deserializer.Deserialize(strReader);
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
