#pragma warning disable 1591
        
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SqlServerCe;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFFileSystemObjects;
using ActiveDatabaseSoftware.ActiveQueryBuilder;
using PFTextObjects;
using PFDataAccessObjects;

namespace PFSQLBuilderObjects
{
    public partial class QueryBuilderForm : Form
    {
        //private variables
        StringBuilder _msg = new StringBuilder();
        StringBuilder _str = new StringBuilder();

        //fields for properties
        private string _queryText = string.Empty;
        private string _connectionString = string.Empty;
        private QueryBuilderDatabasePlatform _databasePlatform = QueryBuilderDatabasePlatform.Universal;
        private AnsiSQLLevel _ansiSQLVersion = AnsiSQLLevel.SQL92;
        private string _helpFilePath = string.Empty;

        //properties

        public string QueryText
        {
            get
            {
                return _queryText;
            }
            set
            {
                _queryText = value;
            }
        }

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

        public QueryBuilderDatabasePlatform DatabasePlatform
        {
            get
            {
                return _databasePlatform;
            }
            set
            {
                _databasePlatform = value;
            }
        }

        public AnsiSQLLevel AnsiSQLVersion
        {
            get
            {
                return _ansiSQLVersion;
            }
            set
            {
                _ansiSQLVersion = value;
            }
        }

        public string HelpFilePath
        {
            get
            {
                return _helpFilePath;
            }
            set
            {
                _helpFilePath = value;
            }
        }
        
        //button clicks
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            QbfAccept();
        }

        private void qbfMenuQBFAccept_Click(object sender, EventArgs e)
        {
            QbfAccept();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            QbfCancel();
        }

        private void qbfMenuQBFCancel_Click(object sender, EventArgs e)
        {
            QbfCancel();
        }

        private void qbfMenuHelpTutorial_Click(object sender, EventArgs e)
        {
            ShowQbfTutorial();
        }

        
        //form processing routines
        public QueryBuilderForm()
        {
            InitializeComponent();
        }

        private void QbfAccept()
        {
            this.QueryText = queryBuilder.SQL;
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void QbfCancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void ShowQbfTutorial()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.TableOfContents);
        }



        //event handlers

        private void QueryBuilderForm_Load(object sender, EventArgs e)
        {
            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("QBFHelpFileName", "QbfUsersGuide.chm");
            //string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "InitWinFormsHelpFile.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.qbfHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            // Update the query builder with new manually edited query text:

            if (textBox1.Modified)
            {
                try
                {
                    queryBuilder.SQL = textBox1.Text;
                }
                catch (Exception ex)
                {
                    _msg.Length = 0;
                    _msg.Append("Parsing error: \r\n");
                    _msg.Append(AppMessages.FormatErrorMessage(ex));
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                }

                textBox1.Modified = false;
            }
        }

        private void queryBuilder_SQLUpdated(object sender, EventArgs e)
        {
            textBox1.Text = queryBuilder.SQL;
        }

        private void plainTextSQLBuilder1_SQLUpdated(object sender, EventArgs e)
        {
            textBox1.Text = plainTextSQLBuilder1.SQL;
            
        }


        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            			// Move the input focus to the query builder.
			// This will fire Leave event in the text box and update the query builder
			// with modified query text.
			queryBuilder.Focus();
			Application.DoEvents();

			// Try to execute the query using current database connection:

            if (e.TabPage == tabPageData)
            {
                DisplayDataForQuery();
            }
        }


        //methods

        public void InitForTest()
        {
            ResetQueryBuilder();
            MSSQLMetadataProvider metaprov = new MSSQLMetadataProvider();
            //metaprov.Connection = new SqlConnection("Server=PROFASTWS1;Database=Namelists;Trusted_Connection=True;");
            metaprov.Connection = new SqlConnection("Server=PROFASTWS1;Database=AdventureWorks2008R2;Trusted_Connection=True;");
            queryBuilder.MetadataProvider = metaprov;
            MSSQLSyntaxProvider sqlsyn = new MSSQLSyntaxProvider();
            queryBuilder.SyntaxProvider = sqlsyn;

            queryBuilder.SQL = this.QueryText;

            // kick the query builder to fill metadata tree
            queryBuilder.InitializeDatabaseSchemaTree();
        }

        public void ResetQueryBuilder()
        {
            queryBuilder.MetadataContainer.Items.Clear();
            queryBuilder.MetadataProvider = null;
            queryBuilder.SyntaxProvider = null;
            queryBuilder.OfflineMode = false;
        }

        public void InitQueryBuilder()
        {
            ResetQueryBuilder();
            queryBuilder.MetadataProvider = GetMetadataProvider();
            queryBuilder.SyntaxProvider = GetSyntaxProvider();
            //queryBuilder.SQL = this.QueryText;


            try
            {
                queryBuilder.InitializeDatabaseSchemaTree();

                if (queryBuilder.SyntaxProvider is MSSQLCESyntaxProvider)
                {
                    //workaround due to null reference exception being raised by mssqlcesyntax provider
                    // when trying to set queryBuilder.SQL value
                    queryBuilder.SQL = this.QueryText;
                    queryBuilder.SyntaxProvider = new GenericSyntaxProvider();
                }
                else
                {
                    queryBuilder.SQL = this.QueryText;
                }

                //workaround to load SQLCE 3.5 schema tree
                //if (this.DatabasePlatform == QueryBuilderDatabasePlatform.SQLServerCE)
                //{
                //    LoadSQLServerCE35SchemaTree();
                //    //queryBuilder.DatabaseSchemaTreeOptions.DefaultExpandLevel = 0;
                //}

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("(InitQueryBuilder) Unable to initialize database schema and syntax provider for this database: ");
                _msg.Append(this.DatabasePlatform);
                _msg.Append(Environment.NewLine);
                _msg.Append("Error Message: ");
                //_msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }

        }

        private BaseMetadataProvider GetMetadataProvider()
        {
            BaseMetadataProvider metaprov = null;

            switch (this.DatabasePlatform)
            {
                case QueryBuilderDatabasePlatform.MSSQLServer:
                    metaprov = new MSSQLMetadataProvider();
                    metaprov.Connection = new SqlConnection(this.ConnectionString);
                    break;
                case QueryBuilderDatabasePlatform.SQLServerCE:
                    //metaprov = new MSSQLCEMetadataProvider();  //does not work
                    //metaprov = new UniversalMetadataProvider();  //does not work
                    //metaprov.Connection = new SqlCeConnection(this.ConnectionString);

                    string oledbConnectionString = ConvertSQLCE35ConnectionStringToOLEDB(this.ConnectionString);
                    metaprov = new OLEDBMetadataProvider();
                    metaprov.Connection = new OleDbConnection(oledbConnectionString);
                    queryBuilder.DatabaseSchemaTreeOptions.DefaultExpandLevel = 0;
                    break;
                case QueryBuilderDatabasePlatform.OLEDB:
                    metaprov = new OLEDBMetadataProvider();
                    metaprov.Connection = new OleDbConnection(this.ConnectionString);
                    break;
                case QueryBuilderDatabasePlatform.ODBC:
                    metaprov = new ODBCMetadataProvider();
                    metaprov.Connection = new OdbcConnection(this.ConnectionString);
                    break;
                case QueryBuilderDatabasePlatform.MSAccess:
                    metaprov = new OLEDBMetadataProvider();
                    metaprov.Connection = new OleDbConnection(this.ConnectionString);
                    queryBuilder.DatabaseSchemaTreeOptions.DefaultExpandLevel = 0;
                    break;
                case QueryBuilderDatabasePlatform.Oracle:
                    metaprov = new OracleMetadataProvider();
                    metaprov.Connection = new System.Data.OracleClient.OracleConnection(this.ConnectionString);
                    queryBuilder.DatabaseSchemaTreeOptions.DefaultExpandLevel = 0;
                    break;
                default:
                    metaprov = new UniversalMetadataProvider();
                    metaprov.Connection = new OdbcConnection(this.ConnectionString);
                    break;
            }

            return metaprov;
        }

        private string ConvertSQLCE35ConnectionStringToOLEDB(string ce35ConnectionString)
        {
            StringBuilder oledbConnectionString = new StringBuilder();
            PFDataAccessObjects.PFDatabase db = null;
            string databasePath = string.Empty;
            string databasePassword = string.Empty;


            try
            {
                db = new PFDataAccessObjects.PFDatabase(PFDataAccessObjects.DatabasePlatform.SQLServerCE35);
                db.ConnectionString = ce35ConnectionString;

                databasePath = db.GetPropertyValue("DatabasePath").ToString();
                databasePassword = db.GetPropertyValue("DatabasePassword").ToString();

                oledbConnectionString.Length = 0;
                oledbConnectionString.Append("Provider=Microsoft.SQLSERVER.CE.OLEDB.3.5;");
                oledbConnectionString.Append("Data Source=");
                oledbConnectionString.Append(databasePath);
                oledbConnectionString.Append(";");
                if (databasePassword.Trim().Length > 0)
                {
                    oledbConnectionString.Append("SSCE:Database Password='");
                    oledbConnectionString.Append(databasePassword);
                    oledbConnectionString.Append("';");
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }
                 
        


            return oledbConnectionString.ToString();
        }

        private BaseSyntaxProvider GetSyntaxProvider()
        {
            BaseSyntaxProvider sqlsyn = null;

            switch (this.DatabasePlatform)
            {
                case QueryBuilderDatabasePlatform.MSSQLServer:
                    sqlsyn = new MSSQLSyntaxProvider();
                    break;
                case QueryBuilderDatabasePlatform.SQLServerCE:
                    //sqlsyn = new MSSQLCESyntaxProvider();   //does not work
                    sqlsyn = new GenericSyntaxProvider(); //does not work
                    break;
                case QueryBuilderDatabasePlatform.OLEDB:
                    if (queryBuilder.MetadataProvider.Connection.ConnectionString.Contains("Provider=Microsoft.ACE.OLEDB.12.0")
                        || queryBuilder.MetadataProvider.Connection.ConnectionString.Contains("Provider=Microsoft.Jet.OLEDB.4.0"))
                    {
                        sqlsyn = new MSAccessSyntaxProvider();
                    }
                    else if (queryBuilder.MetadataProvider.Connection.ConnectionString.Contains("Provider=OraOLEDB.Oracle")
                            || queryBuilder.MetadataProvider.Connection.ConnectionString.Contains("Provider=msdaora"))
                    {
                        sqlsyn = new OracleSyntaxProvider();
                    }
                    else
                    {
                        switch (this.AnsiSQLVersion)
                        {
                            case AnsiSQLLevel.SQL89:
                                sqlsyn = new SQL89SyntaxProvider();
                                break;
                            case AnsiSQLLevel.SQL92:
                                sqlsyn = new SQL92SyntaxProvider();
                                break;
                            case AnsiSQLLevel.SQL2003:
                                sqlsyn = new SQL2003SyntaxProvider();
                                break;
                        }
                    }
                    break;
                case QueryBuilderDatabasePlatform.ODBC:
                    if (queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={microsoft access driver"))
                    {
                        sqlsyn = new MSAccessSyntaxProvider();
                    }
                    else if (queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={oracle")
                             || queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={microsoft odbc for oracle"))
                    {
                        sqlsyn = new OracleSyntaxProvider();
                    }
                    else if (queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={sql server")
                             || queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("sql server"))
                    {
                        sqlsyn = new MSSQLSyntaxProvider();
                    }
                    else if (queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={ibm db2")
                            || queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("ibm db2"))
                    {
                        sqlsyn = new DB2SyntaxProvider();
                    }
                    else if (queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={ibm informix")
                            || queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("informix"))
                    {
                        sqlsyn = new InformixSyntaxProvider();
                    }
                    else if (queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={mysql"))
                    {
                        sqlsyn = new MySQLSyntaxProvider();
                    }
                    else if (queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={adaptive server")
                             || queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={sybase ase"))
                    {
                        sqlsyn = new SybaseSyntaxProvider();
                    }
                    else if (queryBuilder.MetadataProvider.Connection.ConnectionString.ToLower().Contains("driver={sql anywhere"))
                    {
                        sqlsyn = new GenericSyntaxProvider();
                    }
                    else
                    {
                        switch (this.AnsiSQLVersion)
                        {
                            case AnsiSQLLevel.SQL89:
                                sqlsyn = new SQL89SyntaxProvider();
                                break;
                            case AnsiSQLLevel.SQL92:
                                sqlsyn = new SQL92SyntaxProvider();
                                break;
                            case AnsiSQLLevel.SQL2003:
                                sqlsyn = new SQL2003SyntaxProvider();
                                break;
                        }
                    }
                    break;
                case QueryBuilderDatabasePlatform.MSAccess:
                    sqlsyn = new MSAccessSyntaxProvider();
                    break;
                case QueryBuilderDatabasePlatform.Oracle:
                    sqlsyn = new OracleSyntaxProvider();
                    break;
                default:
                    sqlsyn = new GenericSyntaxProvider();
                    break;
            }


            return sqlsyn;
        }

        private void DisplayDataForQuery()
        {
            //AppGlobals.AppMessages.DisplayAlertMessage("DisplayDataForQuery");
            dataGridView1.DataSource = null;

            if (queryBuilder.MetadataProvider != null && queryBuilder.MetadataProvider.Connected)
            {
                if (queryBuilder.MetadataProvider is MSSQLMetadataProvider)
                {
                    SqlCommand command = (SqlCommand)queryBuilder.MetadataProvider.Connection.CreateCommand();
                    command.CommandText = queryBuilder.SQL;

                    // handle the query parameters
                    if (queryBuilder.Parameters.Count > 0)
                    {
                        for (int i = 0; i < queryBuilder.Parameters.Count; i++)
                        {
                            if (!command.Parameters.Contains(queryBuilder.Parameters[i].FullName))
                            {
                                SqlParameter parameter = new SqlParameter();
                                parameter.ParameterName = queryBuilder.Parameters[i].FullName;
                                parameter.DbType = queryBuilder.Parameters[i].DataType;
                                command.Parameters.Add(parameter);
                            }
                        }

                        using (QueryParametersForm qpf = new QueryParametersForm(command))
                        {
                            qpf.ShowDialog();
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataset = new DataSet();

                    try
                    {
                        adapter.Fill(dataset, "QueryResult");
                        dataGridView1.DataSource = dataset.Tables["QueryResult"];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "SQL query error");
                    }
                }//end sql server

                //if (queryBuilder.MetadataProvider is MSSQLCEMetadataProvider)
                //{
                //    SqlCeCommand command = (SqlCeCommand)queryBuilder.MetadataProvider.Connection.CreateCommand();
                //    command.CommandText = queryBuilder.SQL;

                //    // handle the query parameters
                //    if (queryBuilder.Parameters.Count > 0)
                //    {
                //        for (int i = 0; i < queryBuilder.Parameters.Count; i++)
                //        {
                //            if (!command.Parameters.Contains(queryBuilder.Parameters[i].FullName))
                //            {
                //                SqlCeParameter parameter = new SqlCeParameter();
                //                parameter.ParameterName = queryBuilder.Parameters[i].FullName;
                //                parameter.DbType = queryBuilder.Parameters[i].DataType;
                //                command.Parameters.Add(parameter);
                //            }
                //        }

                //        using (QueryParametersForm qpf = new QueryParametersForm(command))
                //        {
                //            qpf.ShowDialog();
                //        }
                //    }

                //    SqlCeDataAdapter adapter = new SqlCeDataAdapter(command);
                //    DataSet dataset = new DataSet();

                //    try
                //    {
                //        adapter.Fill(dataset, "QueryResult");
                //        dataGridView1.DataSource = dataset.Tables["QueryResult"];
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message, "SQL query error");
                //    }
                //}//end sql server ce

                if (queryBuilder.MetadataProvider is OLEDBMetadataProvider)
                {
                    //AppGlobals.AppMessages.DisplayAlertMessage("for OLEDBMetadataProvider");
                    OleDbCommand command = (OleDbCommand)queryBuilder.MetadataProvider.Connection.CreateCommand();
                    command.CommandText = queryBuilder.SQL;

                    // handle the query parameters
                    if (queryBuilder.Parameters.Count > 0)
                    {
                        for (int i = 0; i < queryBuilder.Parameters.Count; i++)
                        {
                            if (!command.Parameters.Contains(queryBuilder.Parameters[i].FullName))
                            {
                                OleDbParameter parameter = new OleDbParameter();
                                parameter.ParameterName = queryBuilder.Parameters[i].FullName;
                                parameter.DbType = queryBuilder.Parameters[i].DataType;
                                command.Parameters.Add(parameter);
                            }
                        }

                        using (QueryParametersForm qpf = new QueryParametersForm(command))
                        {
                            qpf.ShowDialog();
                        }
                    }

                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    DataSet dataset = new DataSet();

                    try
                    {
                        adapter.Fill(dataset, "QueryResult");
                        dataGridView1.DataSource = dataset.Tables["QueryResult"];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "SQL query error");
                    }
                    //AppGlobals.AppMessages.DisplayAlertMessage("end for OLEDBMetadataProvider");
                }//end ole schemaRoot

                if (queryBuilder.MetadataProvider is ODBCMetadataProvider)
                {
                    OdbcCommand command = (OdbcCommand)queryBuilder.MetadataProvider.Connection.CreateCommand();
                    command.CommandText = queryBuilder.SQL;

                    // handle the query parameters
                    if (queryBuilder.Parameters.Count > 0)
                    {
                        for (int i = 0; i < queryBuilder.Parameters.Count; i++)
                        {
                            if (!command.Parameters.Contains(queryBuilder.Parameters[i].FullName))
                            {
                                OdbcParameter parameter = new OdbcParameter();
                                parameter.ParameterName = queryBuilder.Parameters[i].FullName;
                                parameter.DbType = queryBuilder.Parameters[i].DataType;
                                command.Parameters.Add(parameter);
                            }
                        }

                        using (QueryParametersForm qpf = new QueryParametersForm(command))
                        {
                            qpf.ShowDialog();
                        }
                    }

                    OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                    DataSet dataset = new DataSet();

                    try
                    {
                        adapter.Fill(dataset, "QueryResult");
                        dataGridView1.DataSource = dataset.Tables["QueryResult"];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "SQL query error");
                    }
                }//end odbc


                if (queryBuilder.MetadataProvider is OracleMetadataProvider)
                {
                    System.Data.OracleClient.OracleCommand command = (System.Data.OracleClient.OracleCommand)queryBuilder.MetadataProvider.Connection.CreateCommand();
                    command.CommandText = queryBuilder.SQL;

                    // handle the query parameters
                    if (queryBuilder.Parameters.Count > 0)
                    {
                        for (int i = 0; i < queryBuilder.Parameters.Count; i++)
                        {
                            if (!command.Parameters.Contains(queryBuilder.Parameters[i].FullName))
                            {
                                System.Data.OracleClient.OracleParameter parameter = new System.Data.OracleClient.OracleParameter();
                                parameter.ParameterName = queryBuilder.Parameters[i].FullName;
                                parameter.DbType = queryBuilder.Parameters[i].DataType;
                                command.Parameters.Add(parameter);
                            }
                        }

                        using (QueryParametersForm qpf = new QueryParametersForm(command))
                        {
                            qpf.ShowDialog();
                        }
                    }

                    System.Data.OracleClient.OracleDataAdapter adapter = new System.Data.OracleClient.OracleDataAdapter(command);
                    DataSet dataset = new DataSet();

                    try
                    {
                        adapter.Fill(dataset, "QueryResult");
                        dataGridView1.DataSource = dataset.Tables["QueryResult"];
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "SQL query error");
                    }
                }//end oracle (ms)



            }//end check if connected

        }//end method

        private void LoadSQLServerCE35SchemaTree()
        {
            queryBuilder.MetadataContainer.BeginUpdate();
            PFDataAccessObjects.PFDatabase db = null;
            string sql = string.Empty;

            try
            {
                queryBuilder.MetadataContainer.Items.Clear();

                //get list of tables and add them to the schema tree
                db = new PFDataAccessObjects.PFDatabase(PFDataAccessObjects.DatabasePlatform.SQLServerCE35);
                db.ConnectionString = this.ConnectionString;
                db.OpenConnection();
                sql = "select TABLE_NAME from INFORMATION_SCHEMA.TABLES";
                DataTable datatableList = db.RunQueryDataTable(sql, CommandType.Text);
                foreach (DataRow row in datatableList.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    MetadataObject table = queryBuilder.MetadataContainer.AddTable(tableName);
                    sql = "select * from " + tableName + " where 0=1";
                    Console.WriteLine(sql);
                    DataTable columnList = db.RunQueryDataTable(sql, CommandType.Text);
                    foreach (DataColumn col in columnList.Columns)
                    {
                        MetadataField metadataField = table.AddField(col.ColumnName);
                        // setup field
                        metadataField.FieldType = TypeToDbType(col.DataType);
                        metadataField.Nullable = col.AllowDBNull;
                        metadataField.ReadOnly = col.ReadOnly;

                        if (col.MaxLength != -1)
                        {
                            metadataField.Size = col.MaxLength;
                        }


                        // detect the field is primary key
                        foreach (DataColumn pkColumn in columnList.PrimaryKey)
                        {
                            if (col == pkColumn)
                            {
                                metadataField.PrimaryKey = true;
                            }
                        }
                    }
                    //close and reopen connection to free up space used by temp tables in the .NET provider
                    db.CloseConnection();
                    db.OpenConnection();
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to load SQLCE 3.5 Schema Tree failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (db != null)
                {
                    db.Connection.Close();
                    if (db.IsConnected)
                    {
                        db.CloseConnection();
                    }
                }
                db = null;
                queryBuilder.MetadataContainer.EndUpdate();
            }

            queryBuilder.DatabaseSchemaTreeOptions.DefaultExpandLevel = 0;
            queryBuilder.InitializeDatabaseSchemaTree();

        }


        private DbType TypeToDbType(Type type)
        {
            if (type == typeof(System.String)) return DbType.String;
            else if (type == typeof(System.Int16)) return DbType.Int16;
            else if (type == typeof(System.Int32)) return DbType.Int32;
            else if (type == typeof(System.Int64)) return DbType.Int64;
            else if (type == typeof(System.UInt16)) return DbType.UInt16;
            else if (type == typeof(System.UInt32)) return DbType.UInt32;
            else if (type == typeof(System.UInt64)) return DbType.UInt64;
            else if (type == typeof(System.Boolean)) return DbType.Boolean;
            else if (type == typeof(System.Single)) return DbType.Single;
            else if (type == typeof(System.Double)) return DbType.Double;
            else if (type == typeof(System.Decimal)) return DbType.Decimal;
            else if (type == typeof(System.DateTime)) return DbType.DateTime;
            else if (type == typeof(System.TimeSpan)) return DbType.Time;
            else if (type == typeof(System.Byte)) return DbType.Byte;
            else if (type == typeof(System.SByte)) return DbType.SByte;
            else if (type == typeof(System.Char)) return DbType.String;
            else if (type == typeof(System.Byte[])) return DbType.Binary;
            else if (type == typeof(System.Guid)) return DbType.Guid;
            else return DbType.Object;
        }


     }//end class
}//end namespace

#pragma warning restore 1591
