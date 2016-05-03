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
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Data;
using System.Data.Common;
using PFCollectionsObjects;
using PFDataAccessObjects;
//using PFSQLServerCE35Objects;
using AppGlobals;

namespace PFTaskObjects
{
    /// <summary>
    /// Manages task definition database.
    /// </summary>
    public class PFTaskManager
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private PFList<PFTask> _taskList = new PFList<PFTask>();
        private string _taskManagerName = "TaskManager";
        private int _defaultMaxTaskThreads = 5;

        private enTaskStorageType _taskStorageType = enTaskStorageType.XMLFiles;
        private string _connectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"TaskManager\Tasks\");

        private string _taskDefinitionsSelectAllSQL = "select TaskObject from Tasks order by TaskName";
        private string _taskDefinitionsSelectTaskSQL = "select TaskObject from Tasks where TaskName = '<taskname>'";
        private string _taskDefinitionsUpdateSQL = "update Tasks set TaskObject = '<taskobject>' where TaskName = '<taskname>'";
        private string _taskDefinitionsInsertSQL = "insert Tasks (TaskName, TaskObject) values ('<taskname>', '<taskobject>'";
        private string _taskDefinitionsDeleteSQL = "delete Tasks where TaskName = '<taskname>'";
        private string _taskDefinitionsIfTaskExistsSQL = "select count(*) as numRecsFound from Tasks  where TaskName = '<taskname>'";


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFTaskManager()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="taskStorageType">Type of storage for task.</param>
        /// <param name="connectionString">Connection information for task storage location.</param>
        public PFTaskManager(enTaskStorageType taskStorageType, string connectionString)
        {
            InitInstance(taskStorageType, connectionString, "TaskManager");
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="taskStorageType">Type of storage for task.</param>
        /// <param name="connectionString">Connection information for task storage location.</param>
        /// <param name="taskManagerName">Name of task manager.</param>
        public PFTaskManager(enTaskStorageType taskStorageType, string connectionString, string taskManagerName)
        {
            InitInstance(taskStorageType, connectionString, taskManagerName);
        }

        private void InitInstance(enTaskStorageType taskStorageType, string connectionString, string taskManagerName)
        {
            string configValue = string.Empty;

            _taskStorageType = taskStorageType;
            _connectionString = connectionString;
            _taskManagerName = taskManagerName;

            configValue = AppConfig.GetStringValueFromConfigFile("TaskDefinitionsSelectAllSQL", string.Empty);
            if (configValue.Length > 0)
                _taskDefinitionsSelectAllSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskDefinitionsSelectTaskSQL", string.Empty);
            if (configValue.Length > 0)
                _taskDefinitionsSelectTaskSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskDefinitionsInsertSQL", string.Empty);
            if (configValue.Length > 0)
                _taskDefinitionsInsertSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskDefinitionsUpdateSQL", string.Empty);
            if (configValue.Length > 0)
                _taskDefinitionsUpdateSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskDefinitionsDeleteSQL", string.Empty);
            if (configValue.Length > 0)
                _taskDefinitionsDeleteSQL = configValue;
            configValue = AppConfig.GetStringValueFromConfigFile("TaskDefinitionsIfTaskExistsSQL", string.Empty);
            if (configValue.Length > 0)
                _taskDefinitionsIfTaskExistsSQL = configValue;

        }

        //properties

        /// <summary>
        /// Object that stores list of tasks to be managed.
        /// </summary>
        public PFList<PFTask> TaskList
        {
            get
            {
                return _taskList;
            }
            set
            {
                _taskList = value;
            }
        }

        /// <summary>
        /// Name given to the the manager.
        /// </summary>
        public string TaskManagerName
        {
            get
            {
                return _taskManagerName;
            }
            set
            {
                _taskManagerName = value;
            }
        }

        /// <summary>
        /// Maximum number of separate threads to use for running tasks.
        /// </summary>
        public int MaxTaskThreads
        {
            get
            {
                return _defaultMaxTaskThreads;
            }
            set
            {
                _defaultMaxTaskThreads = value;
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
        /// ConnectionString Property: Specify path to folder containing the definition files if TaskStorageType is XMLFiles; specify database connection string if TaskStorageType is Database.
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
        /// <returns>List of PFTask objects.</returns>
        public PFList<PFTask> GetTaskList()
        {
            PFList<PFTask> taskList = null;

            if(this.TaskStorageType == enTaskStorageType.XMLFiles)
            {
                taskList = GetTaskListXML();
            }
            else if (this.TaskStorageType == enTaskStorageType.Database)
            {
                taskList = GetTaskListDatabase();
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Invalid or missing TaskStorageType: ");
                _msg.Append(this.TaskStorageType.ToString());
                throw new System.Exception(_msg.ToString());
            }

            _taskList = taskList;

            return taskList;
        }

        private PFList<PFTask> GetTaskListXML()
        {
            PFList<PFTask> taskList = new PFList<PFTask>();
            string[] taskFiles = null;


            taskFiles = Directory.GetFiles(this.ConnectionString,"*.xml",SearchOption.TopDirectoryOnly);

            if(taskFiles != null)
            {
                for(int i = 0; i < taskFiles.Length; i++)
                {
                    PFTask task = PFTask.LoadFromXmlFile(taskFiles[i]);
                    taskList.Add(task);
                }
            }

            return taskList;
        }

        private PFList<PFTask> GetTaskListDatabase()
        {
            PFList<PFTask> taskList = new PFList<PFTask>();
            PFDatabase db = null;

            try
            {
                db = new PFDatabase(DatabasePlatform.SQLServerCE35);              
                db.ConnectionString = this.ConnectionString;
                db.OpenConnection();

                DbDataReader rdr = db.RunQueryDataReader(_taskDefinitionsSelectAllSQL, System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    string str = rdr["TaskObject"].ToString();
                    PFTask task = PFTask.LoadFromXmlString(str);
                    taskList.Add(task);
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
                 
        


            return taskList;
        }

        /// <summary>
        /// Retrieves the object for a specific task.
        /// </summary>
        /// <param name="taskName">Name of task the to get.</param>
        /// <returns>Task object.</returns>
        public PFTask GetTaskByName(string taskName)
        {
            PFList<PFTask> taskList = null;
            PFTask task = null;

            if(this.TaskStorageType == enTaskStorageType.XMLFiles)
            {
                taskList = GetTaskListXML();
            }
            else if (this.TaskStorageType == enTaskStorageType.Database)
            {
                taskList = GetTaskListDatabase();
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Invalid or missing TaskStorageType: ");
                _msg.Append(this.TaskStorageType.ToString());
                throw new System.Exception(_msg.ToString());
            }

            _taskList = taskList;

            for (int i = 0; i < taskList.Count; i++)
            {
                if (taskName.ToUpper() == taskList[i].TaskName.ToUpper())
                {
                    task = taskList[i];
                    break;
                }
            }

            return task;
        }



        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFTaskManager));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFTaskManager.</returns>
        public static PFTaskManager LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFTaskManager));
            TextReader textReader = new StreamReader(filePath);
            PFTaskManager objectInstance;
            objectInstance = (PFTaskManager)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(PFTaskManager));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFTaskManager.</returns>
        public static PFTaskManager LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFTaskManager));
            StringReader strReader = new StringReader(xmlString);
            PFTaskManager objectInstance;
            objectInstance = (PFTaskManager)deserializer.Deserialize(strReader);
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
