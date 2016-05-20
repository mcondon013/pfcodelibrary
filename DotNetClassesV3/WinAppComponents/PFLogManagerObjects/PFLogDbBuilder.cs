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
using System.IO;
using PFTextObjects;
using PFSQLServerCE35Objects;

namespace PFLogManagerObjects
{
    /// <summary>
    /// Creates app log and app retry log database structures in a SQL Compact 3.5 database.
    /// </summary>
    public class PFLogDbBuilder
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private string _appLogTableDropSql = "drop table ApplicationLog";

        private string _appLogTableDeleteSql = "delete from ApplicationLog";

        private string _appLogTableCreateSql = "create table ApplicationLog "
                                              + "( "
                                              + "	LogEntryId int identity(1,1) not null "
                                              + ",LogEntryDateTime datetime "
                                              + ",ApplicationName nvarchar(100) "
                                              + ",MachineName nvarchar(100) "
                                              + ",Username nvarchar(100) "
                                              + ",MessageLevel nvarchar(25) "
                                              + ",MessageText ntext "
                                              + ",LogMessageObject ntext "
                                              + ") ";

        private string _appLogTableIdx1 = "create index idx_LogEntryDateTime on ApplicationLog(LogEntryDateTime, ApplicationName, MessageLevel)";

        private string _appLogTableIdx2 = "create index idx_ApplicationName on ApplicationLog(ApplicationName, MessageLevel)";

        private string _appLogTableIdx3 = "create index idx_MessageLevel on ApplicationLog(MessageLevel, ApplicationName)";

        private string _retryLogTableDropSql = "Drop table Lists ";

        private string _retryLogTableDeleteSql = "delete from Lists ";

        private string _retryLogTableCreateSql = "Create table Lists "
                                               + "( ListName nvarchar(100) "
                                               + " ,ListType nvarchar(100) "
                                               + " ,ID nvarchar(40) "
                                               + " ,ListObject ntext "
                                               + "); ";

        private string _retryLogTableIdx1 = "create index idx_ListName on Lists (ListName)";


        //private variables for properties

        private string _logConnectionString = @"data source='" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ApplicationLogs.sdf") + "';";
        private string _retryLogConnectionString = @"data source='" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ApplicationLogRetryQueues.sdf") + "';";

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFLogDbBuilder()
        {
            ;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        public PFLogDbBuilder(string logConnectionString, string retryLogConnectionString)
        {
            _logConnectionString = logConnectionString;
            _retryLogConnectionString = retryLogConnectionString;
        }


        //properties

        /// <summary>
        /// ConnectionString for log database.
        /// </summary>
        public string LogConnectionString
        {
            get
            {
                return _logConnectionString;
            }
            set
            {
                _logConnectionString = value;
            }
        }

        /// <summary>
        /// ConnectionString for RetryLog database.
        /// </summary>
        public string RetryLogConnectionString
        {
            get
            {
                return _retryLogConnectionString;
            }
            set
            {
                _retryLogConnectionString = value;
            }
        }



        //methods

        /// <summary>
        /// Creates SQLCE database file and populates it with required data table.
        /// </summary>
        public void CreateAppLogDatabase()
        {
            string sqlStmt = string.Empty;
            PFSQLServerCE35 db = null;

            try
            {
                db = new PFSQLServerCE35();

                db.CreateDatabase(_logConnectionString);

                db.ConnectionString = _logConnectionString;
                db.OpenConnection();

                sqlStmt = _appLogTableCreateSql;
                db.RunNonQuery(sqlStmt);
                sqlStmt = _appLogTableIdx1;
                db.RunNonQuery(sqlStmt);
                sqlStmt = _appLogTableIdx2;
                db.RunNonQuery(sqlStmt);
                sqlStmt = _appLogTableIdx3;
                db.RunNonQuery(sqlStmt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to create database: ");
                _msg.Append(_logConnectionString);
                if (sqlStmt.Length > 0)
                {
                    _msg.Append(" SQL statement: ");
                    _msg.Append(sqlStmt);
                }
                _msg.Append(" Error message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }


        /// <summary>
        /// Creates a SQLCE database to contain a retry log table.
        /// </summary>
        public void CreateRetryLogDatabase()
        {
            string sqlStmt = string.Empty;
            PFSQLServerCE35 db = null;

            try
            {
                db = new PFSQLServerCE35();

                db.CreateDatabase(_retryLogConnectionString);

                db.ConnectionString = _retryLogConnectionString;
                db.OpenConnection();

                sqlStmt = _retryLogTableCreateSql;
                db.RunNonQuery(sqlStmt);
                sqlStmt = _retryLogTableIdx1;
                db.RunNonQuery(sqlStmt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to create database: ");
                _msg.Append(_retryLogConnectionString);
                if (sqlStmt.Length > 0)
                {
                    _msg.Append(" SQL statement: ");
                    _msg.Append(sqlStmt);
                }
                _msg.Append(" Error message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }

        /// <summary>
        /// Creates an application log table.
        /// </summary>
        public void CreateAppLogTable()
        {
            string sqlStmt = string.Empty;
            PFSQLServerCE35 db = null;

            try
            {
                db = new PFSQLServerCE35();

                db.ConnectionString = _logConnectionString;
                db.OpenConnection();

                sqlStmt = _appLogTableCreateSql;
                db.RunNonQuery(sqlStmt);
                sqlStmt = _appLogTableIdx1;
                db.RunNonQuery(sqlStmt);
                sqlStmt = _appLogTableIdx2;
                db.RunNonQuery(sqlStmt);
                sqlStmt = _appLogTableIdx3;
                db.RunNonQuery(sqlStmt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to create table: ");
                _msg.Append(_logConnectionString);
                if (sqlStmt.Length > 0)
                {
                    _msg.Append(" SQL statement: ");
                    _msg.Append(sqlStmt);
                }
                _msg.Append(" Error message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }

        /// <summary>
        /// Creates a retry log table.
        /// </summary>
        public void CreateRetryLogTable()
        {
            string sqlStmt = string.Empty;
            PFSQLServerCE35 db = null;

            try
            {
                db = new PFSQLServerCE35();

                db.ConnectionString = _retryLogConnectionString;
                db.OpenConnection();

                sqlStmt = _retryLogTableCreateSql;
                db.RunNonQuery(sqlStmt);
                sqlStmt = _retryLogTableIdx1;
                db.RunNonQuery(sqlStmt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to create table: ");
                _msg.Append(_retryLogConnectionString);
                if (sqlStmt.Length > 0)
                {
                    _msg.Append(" SQL statement: ");
                    _msg.Append(sqlStmt);
                }
                _msg.Append(" Error message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }

        /// <summary>
        /// Drops the current app log table.
        /// </summary>
        public void DropAppLogTable()
        {
            string sqlStmt = string.Empty;
            PFSQLServerCE35 db = null;

            try
            {
                db = new PFSQLServerCE35(_logConnectionString);

                db.OpenConnection();

                sqlStmt = _appLogTableDropSql;
                db.RunNonQuery(sqlStmt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to drop table: ");
                _msg.Append(_logConnectionString);
                if (sqlStmt.Length > 0)
                {
                    _msg.Append(" SQL statement: ");
                    _msg.Append(sqlStmt);
                }
                _msg.Append(" Error message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }

        /// <summary>
        /// Drops the current retry log table.
        /// </summary>
        public void DropRetryLogTable()
        {
            string sqlStmt = string.Empty;
            PFSQLServerCE35 db = null;

            try
            {
                db = new PFSQLServerCE35(_retryLogConnectionString);

                db.OpenConnection();

                sqlStmt = _retryLogTableDropSql;
                db.RunNonQuery(sqlStmt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to drop table: ");
                _msg.Append(_retryLogConnectionString);
                if (sqlStmt.Length > 0)
                {
                    _msg.Append(" SQL statement: ");
                    _msg.Append(sqlStmt);
                }
                _msg.Append(" Error message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }

        /// <summary>
        /// Deletes all records from app log table.
        /// </summary>
        public void DeleteAppLogTable()
        {
            string sqlStmt = string.Empty;
            PFSQLServerCE35 db = null;

            try
            {
                db = new PFSQLServerCE35(_logConnectionString);

                db.OpenConnection();

                sqlStmt = _appLogTableDeleteSql;
                db.RunNonQuery(sqlStmt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to delete table: ");
                _msg.Append(_logConnectionString);
                if (sqlStmt.Length > 0)
                {
                    _msg.Append(" SQL statement: ");
                    _msg.Append(sqlStmt);
                }
                _msg.Append(" Error message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }

        /// <summary>
        /// Deletes all records from Retry Log table. 
        /// </summary>
        public void DeleteRetryLogTable()
        {
            string sqlStmt = string.Empty;
            PFSQLServerCE35 db = null;

            try
            {
                db = new PFSQLServerCE35(_retryLogConnectionString);

                db.OpenConnection();

                sqlStmt = _retryLogTableDeleteSql;
                db.RunNonQuery(sqlStmt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to delete table: ");
                _msg.Append(_retryLogConnectionString);
                if (sqlStmt.Length > 0)
                {
                    _msg.Append(" SQL statement: ");
                    _msg.Append(sqlStmt);
                }
                _msg.Append(" Error message: ");
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }

        }

        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String containing result.</returns>
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
        /// <returns>String containing result.</returns>
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
        /// <returns>String containing result.</returns>
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



    }//end class
}//end namespace
