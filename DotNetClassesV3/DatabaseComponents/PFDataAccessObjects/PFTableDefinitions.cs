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
using PFTextObjects;

namespace PFDataAccessObjects  
{
    /// <summary>
    /// Contains list of database tables and their schema definitions.
    /// </summary>
    public class PFTableDefinitions //: PFCollectionsObjects.PFList<PFTableDef>
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFTableDefinitions()
        {
            ;
        }

        //properties


        //methods

                /// <summary>
        /// Retrieves list of tables and their associated schema information contained in the database pointed to by the current connection.
        /// </summary>
        /// <param name="sourceDatabase">Database from which the table list will be retrieved.</param>
        /// <returns>Object containing the list of table definitions.</returns>
        public PFList<PFTableDef> GetTableList(IDatabaseProvider sourceDatabase)
        {
            return GetTableList(sourceDatabase, null, null);
        }

        /// <summary>
        /// Retrieves list of tables and their associated schema information contained in the database pointed to by the current connection.
        /// </summary>
        /// <param name="sourceDatabase">Database from which the table list will be retrieved.</param>
        /// <param name="includePatterns">Wildard pattern to use when selecting which tables to include. Specify * or null or empty string for pattern to include all tables.</param>
        /// <param name="excludePatterns">Wildard pattern to use when selecting which tables to exclude. Specify * for pattern to exclude all tables. Specify null or empty string to exclude no tables.</param>
        /// <returns>Object containing the list of table definitions.</returns>
        public PFList<PFTableDef> GetTableList(IDatabaseProvider sourceDatabase, string[] includePatterns, string[] excludePatterns)
        {
            PFList<PFTableDef> tableDefs = new PFList<PFTableDef>();
            DataTable dt = null;
            string sqlSelect = "select * from <tableName> where 1=0";
            PFSearchPattern[] regexInclude = null;
            PFSearchPattern[] regexExclude = null;

            regexInclude = GetSearchPatternRegexObjects(includePatterns, "*");
            regexExclude = GetSearchPatternRegexObjects(excludePatterns, string.Empty);

            if (sourceDatabase.IsConnected == false)
            {
                _msg.Length = 0;
                _msg.Append("You must be connected to a database to run GetTableList method.");
                throw new System.Exception(_msg.ToString());
            }

            dt = sourceDatabase.Connection.GetSchema("Tables");

            foreach (DataRow dr in dt.Rows)
            {
                if (sourceDatabase.TypeIsUserTable(dr))
                {
                    string tabName = sourceDatabase.GetFullTableName(dr);
                    if (IsMatchToPattern(regexInclude, tabName) && IsMatchToPattern(regexExclude, tabName)==false)
                    {
                        string sql = sqlSelect.Replace("<tableName>", tabName);
                        try
                        {
                            dt = sourceDatabase.RunQueryDataTable(sql, CommandType.Text);
                        }
                        catch
                        {
                            dt = new DataTable(tabName);
                        }
                        string tabCreateStatement = string.Empty;
                        dt.TableName = tabName;
                        try
                        {
                            tabCreateStatement = sourceDatabase.BuildTableCreateStatement(dt);
                        }
                        catch
                        {
                            tabCreateStatement = "Unable to build create statement. Error occurred in BuildTableCreateStatement.";
                        }
                        PFTableDef tabDef = new PFTableDef();
                        tabDef.DbPlatform = sourceDatabase.DbPlatform;
                        tabDef.DbConnectionString = sourceDatabase.ConnectionString;
                        tabDef.TableCreateStatement = tabCreateStatement;
                        tabDef.TableObject = dt;
                        tabDef.TableFullName = tabName;
                        TableNameQualifiers tnq = sourceDatabase.GetTableNameQualifiers(dr);
                        tabDef.TableCatalog = tnq.TableCatalog;
                        tabDef.TableOwner = tnq.TableSchema;
                        tabDef.TableName = tnq.TableName;
                        tableDefs.Add(tabDef);
                        
                    }
                }
            }

            return tableDefs;
        }


        /// <summary>
        /// Creates an array of regex objects. There will be one for each of the search patterns specified in the searchPatterns parameter.
        /// </summary>
        /// <param name="searchPatterns">An array of one or more search patterns. Set to null if no search patterns are specified.</param>
        /// <param name="ifNullDefaultPattern">Specifies the default search pattern to create when search patterns is null.</param>
        /// <returns>An array of PFSearchPattern objects that encapsulate regex processing.</returns>
        /// <remarks>Search patterns are composed of the Windows wildcard characters for file system searches.</remarks>
        public PFSearchPattern[] GetSearchPatternRegexObjects(string[] searchPatterns, string ifNullDefaultPattern)
        {
            PFSearchPattern[] regexPatterns = null;
            int numPatterns = 1;
            string searchPattern = string.Empty;

            if (searchPatterns != null)
            {
                numPatterns = searchPatterns.Length;
                regexPatterns = new PFSearchPattern[numPatterns];
                for (int i = 0; i < searchPatterns.Length; i++)
                {
                    searchPattern = searchPatterns[i];
                    regexPatterns[i] = new PFSearchPattern(String.IsNullOrEmpty(searchPattern) ? ifNullDefaultPattern : searchPattern);
                }
            }
            else
            {
                regexPatterns = new PFSearchPattern[1];
                regexPatterns[0] = new PFSearchPattern(ifNullDefaultPattern);
            }



            return regexPatterns;
        }

        /// <summary>
        /// Determines if table name matches search pattern.
        /// </summary>
        /// <param name="regexPatterns">Array of search patterns to match against.</param>
        /// <param name="tabName">Name of table.</param>
        /// <returns>True if table name matches at least one of the search patterns.</returns>
        public bool IsMatchToPattern(PFSearchPattern[] regexPatterns, string tabName)
        {
            bool isMatch = false;

            for (int i = 0; i < regexPatterns.Length; i++)
            {
                if (regexPatterns[i].IsMatch(tabName))
                {
                    isMatch = true;
                    break;
                }
            }

            return isMatch;
        }


        /// <summary>
        /// Method to convert table definitions from another database format to the data format supported by the destination database class.
        /// </summary>
        /// <param name="tableDefs">Object containing the list of table definitions to be converted.</param>
        /// <param name="destinationDatabase">Database object pointing to database that will define converted table definitions.</param>
        /// <param name="newSchemaName">Specify a new schema (owner) name for the tables when they are recreated in the database managed by the current instance.</param>
        /// <returns>Object containing the list of table definitions after they have been converted to match the data formats of the current instance.</returns>
        public PFList<PFTableDef> ConvertTableDefs(PFList<PFTableDef> tableDefs, IDatabaseProvider destinationDatabase, string newSchemaName)
        {
            PFList<PFTableDef> newTableDefs = new PFList<PFTableDef>();
            PFTableDef tabDef = null;
            string tabName = string.Empty;
            string schemaName = string.Empty;
            string tabCreateStatement = string.Empty;

            tableDefs.SetToBOF();

            while ((tabDef = tableDefs.NextItem) != null)
            {
                string tabDefXmlString = tabDef.ToXmlString();
                PFTableDef newTabDef = PFTableDef.LoadFromXmlString(tabDefXmlString);

                tabName = destinationDatabase.RebuildFullTableName(tabDef, newSchemaName);
                schemaName = String.IsNullOrEmpty(newSchemaName) == false ? newSchemaName : tabDef.TableOwner;
                newTabDef.TableObject.TableName = tabName;
                tabCreateStatement = destinationDatabase.BuildTableCreateStatement(newTabDef.TableObject);
                newTabDef.DbPlatform = destinationDatabase.DbPlatform;
                newTabDef.DbConnectionString = destinationDatabase.ConnectionString;
                newTabDef.TableOwner = schemaName;
                newTabDef.TableFullName = tabName;
                newTabDef.TableCreateStatement = tabCreateStatement;
                newTableDefs.Add(newTabDef);
            }

            return newTableDefs;
        }

        /// <summary>
        /// Runs the table create statements contained in the provided tableDefs object.
        /// </summary>
        /// <param name="destinationDatabase">Database object pointing to database where tables will be created.</param>
        /// <param name="tableDefs">Object containing list of table definitions.</param>
        /// <returns>Number of tables created.</returns>
        /// <remarks>Will not create table if table already exists.</remarks>
        public int CreateTablesFromTableDefs(IDatabaseProvider destinationDatabase, PFList<PFTableDef> tableDefs)
        {
            return CreateTablesFromTableDefs(destinationDatabase, tableDefs, false);
        }

        /// <summary>
        /// Runs the table create statements contained in the provided tableDefs object.
        /// </summary>
        /// <param name="destinationDatabase">Database object pointing to database where tables will be created.</param>
        /// <param name="tableDefs">Object containing list of table definitions.</param>
        /// <param name="dropBeforeCreate">If true and table already exists, table will be dropped and then recreated using the table definition in the supplied PFTableDefs list. If false, table create step is bypassed if table already exists.</param>
        /// <returns>Number of tables created.</returns>
        public int CreateTablesFromTableDefs(IDatabaseProvider destinationDatabase, PFList<PFTableDef> tableDefs, bool dropBeforeCreate)
        {
            int numTablesCreated = 0;
            PFTableDef td = null;
            string sqlStatement = string.Empty;

            tableDefs.SetToBOF();

            while ((td = tableDefs.NextItem) != null)
            {
                if (destinationDatabase.TableExists(td) && dropBeforeCreate)
                {
                    destinationDatabase.DropTable(td);
                }

                if (destinationDatabase.TableExists(td) == false)
                {
                    sqlStatement = td.TableCreateStatement;
                    destinationDatabase.RunNonQuery(sqlStatement, CommandType.Text);
                    numTablesCreated++;
                }

            }

            return numTablesCreated;
        }


        /// <summary>
        /// Runs the table create statements contained in the provided tableDefs object.
        /// </summary>
        /// <param name="sourceDatabase">Database containing source tables..</param>
        /// <param name="tableIncludePatterns">Wildard pattern to use when selecting which tables to include. Specify * or null or empty string for pattern to include all tables.</param>
        /// <param name="tableExcludePatterns">Wildard pattern to use when selecting which tables to exclude. Specify * for pattern to exclude all tables. Specify null or empty string to exclude no tables.</param>
        /// <param name="destinationDatabase">Database were data will be copied to..</param>
        /// <param name="newSchemaName">Schema to use for identifying the destination tables.</param>
        /// <param name="dropBeforeCreate">If true and table already exists, table will be dropped and then recreated using the table definition in the supplied PFTableDefs list. If false, table create step is bypassed if table already exists.</param>
        /// <returns>Number of tables created.</returns>
        /// <remarks>Include and exclude patterns are the same as Windows File wildcards. * specifies zero or more characters. ? specifies one character.</remarks>
        public PFList<TableCopyDetails> CopyTableDataFromTableDefs(IDatabaseProvider sourceDatabase, string[] tableIncludePatterns, string[] tableExcludePatterns,
                                                                   IDatabaseProvider destinationDatabase, string newSchemaName, bool dropBeforeCreate)
        {
            PFList<TableCopyDetails> tableCopyLog = new PFList<TableCopyDetails>();
            PFTableDef td = null;
            string sqlStatement = string.Empty;
            string selectStatement = "select * from <TableName>";
            string newTableName = string.Empty;
            string tdToXml = string.Empty;
            PFTableDef newTd = null;

            PFList<PFTableDef> tableDefs = sourceDatabase.GetTableList(tableIncludePatterns, tableExcludePatterns);
            DataTable sourceData = null;

            tableDefs.SetToBOF();

            while ((td = tableDefs.NextItem) != null)
            {
                newTableName = td.TableName;
                tdToXml = td.ToXmlString();
                newTd = PFTableDef.LoadFromXmlString(tdToXml);

                if(newSchemaName.Trim().Length > 0)
                    newTd.TableObject.TableName = newSchemaName + "." + newTableName;
                else
                    newTd.TableObject.TableName = newTableName;
                newTd.TableFullName = newTd.TableObject.TableName;
                newTd.TableOwner = newSchemaName;
                newTd.TableName = newTableName;

                if (destinationDatabase.TableExists(newSchemaName, newTableName) && dropBeforeCreate)
                {
                    destinationDatabase.DropTable(newSchemaName, newTableName);
                }

                if (destinationDatabase.TableExists(newSchemaName, newTableName) == false)
                {
                    sqlStatement = destinationDatabase.BuildTableCreateStatement(newTd.TableObject);
                    destinationDatabase.RunNonQuery(sqlStatement, CommandType.Text);
                }

                TableCopyDetails tcdetails = new TableCopyDetails();
                tcdetails.sourceTableName = td.TableFullName;
                tcdetails.destinationTableName = newTd.TableFullName;
                tcdetails.numSourceRows = -1;
                tcdetails.numRowsCopied = -1;
                try
                {
                    sqlStatement = selectStatement.Replace("<TableName>", td.TableObject.TableName);
                    sourceData = sourceDatabase.RunQueryDataTable(sqlStatement, CommandType.Text);
                    sourceData.TableName = newTd.TableObject.TableName;
                    tcdetails.numSourceRows = sourceData.Rows.Count;
                    if (sourceData.Rows.Count > 0)
                    {
                        destinationDatabase.ImportDataFromDataTable(sourceData);
                        tcdetails.numRowsCopied = sourceData.Rows.Count;
                        tcdetails.result = TableCopyResult.Success;
                        tcdetails.messages = string.Empty;
                    }
                    else
                    {
                        tcdetails.numRowsCopied = 0;
                        tcdetails.result = TableCopyResult.Alert;
                        tcdetails.messages = "The were no rows in the source table.";
                    }
                }
                catch (System.Exception ex)
                {
                    _msg.Length = 0;
                    _msg.Append("Attempt to copy ");
                    _msg.Append(td.TableFullName);
                    _msg.Append(" to ");
                    _msg.Append(newTd.TableFullName);
                    _msg.Append(" failed. Error message: ");
                    _msg.Append(PFTextObjects.PFTextProcessor.FormatErrorMessage(ex));
                    tcdetails.messages = _msg.ToString();
                    tcdetails.numRowsCopied = -1;
                    tcdetails.result = TableCopyResult.Failure;
                }

                tableCopyLog.Add(tcdetails);


                newTd = null;
                sourceData = null;
            }

            return tableCopyLog;
        }

        

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFTableDefinitions));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFTableDefinitions.</returns>
        public static PFTableDefinitions LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFTableDefinitions));
            TextReader textReader = new StreamReader(filePath);
            PFTableDefinitions objectInstance;
            objectInstance = (PFTableDefinitions)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(PFTableDefinitions));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFTableDefinitions.</returns>
        public static PFTableDefinitions LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFTableDefinitions));
            StringReader strReader = new StringReader(xmlString);
            PFTableDefinitions objectInstance;
            objectInstance = (PFTableDefinitions)deserializer.Deserialize(strReader);
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
