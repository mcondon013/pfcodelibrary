#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.Common;
using Sybase.Data.AseClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFFileSystemObjects;
using ActiveDatabaseSoftware.ActiveQueryBuilder;
using PFTextObjects;
using PFDataAccessObjects;

namespace PFSybaseSQLBuilderObjects
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

                queryBuilder.SQL = this.QueryText;
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
                case QueryBuilderDatabasePlatform.Sybase:
                    metaprov = new UniversalMetadataProvider();
                    AseConnection aseConn = new AseConnection(this.ConnectionString);
                    metaprov.Connection = (IDbConnection)aseConn;

                    //workaround for problem with using Sybase provider
                    //NOTE: has been fixed as of 11/26/2013 release: see above code (12/2/13 mc)
                    //metaprov = new ODBCMetadataProvider();
                    //string odbcConnectionString = ConvertSybaseConnectionStringToOdbc(this.ConnectionString);
                    //OdbcConnection aseConn = new OdbcConnection(odbcConnectionString);
                    //metaprov.Connection = (IDbConnection)aseConn;
                    break;
                default:
                    metaprov = new UniversalMetadataProvider();
                    metaprov.Connection = new OdbcConnection(this.ConnectionString);
                    break;
            }

            return metaprov;
        }

        //example:
        //Driver={Adaptive Server Enterprise};server=profastsv2syb;port=5000;schemaRoot=AdventureWorks;uid=SA;Pwd=SA1992;
        private string ConvertSybaseConnectionStringToOdbc(string sybaseConnectionString)
        {
            string odbcConnectionString = string.Empty;
            PFParseString sqlConnBuilder = new PFParseString();
            string val = string.Empty;
            if (_connectionString.Trim().Length > 0)
            {
                sqlConnBuilder.KeyType = PFParseString.PFArgumentKeyType.NamedKey;
                sqlConnBuilder.Delimiters = ";";
                sqlConnBuilder.StringToParse = sybaseConnectionString;

                _str.Length = 0;
                _str.Append("Driver={Adaptive Server Enterprise};");

                PFParseString.PFKeyValuePair kv = new PFParseString.PFKeyValuePair();
                kv = sqlConnBuilder.GetFirstKeyValue();
                while (kv.Key != string.Empty)
                {
                    if (kv.Key.ToLower() == "data source")
                    {
                        _str.Append("server=");
                        _str.Append(kv.Value);
                        _str.Append(";");
                    }
                    if (kv.Key.ToLower() == "port")
                    {
                        _str.Append("port=");
                        _str.Append(kv.Value);
                        _str.Append(";");
                    }
                    if (kv.Key.ToLower() == "database")
                    {
                        _str.Append("schemaRoot=");
                        _str.Append(kv.Value);
                        _str.Append(";");
                    }
                    if (kv.Key.ToLower() == "uid")
                    {
                        _str.Append("uid=");
                        _str.Append(kv.Value);
                        _str.Append(";");
                    }
                    if (kv.Key.ToLower() == "pwd")
                    {
                        _str.Append("pwd=");
                        _str.Append(kv.Value);
                        _str.Append(";");
                    }

                    kv = sqlConnBuilder.GetNextKeyValue();
                }

            }

            odbcConnectionString = _str.ToString();

            return odbcConnectionString;
        }

        private BaseSyntaxProvider GetSyntaxProvider()
        {
            BaseSyntaxProvider sqlsyn = null;

            switch (this.DatabasePlatform)
            {
                case QueryBuilderDatabasePlatform.Sybase:
                    sqlsyn = new SybaseSyntaxProvider();
                    break;
                default:
                    sqlsyn = new GenericSyntaxProvider();
                    break;
            }


            return sqlsyn;
        }

        private void DisplayDataForQuery()
        {
            dataGridView1.DataSource = null;

            if (queryBuilder.MetadataProvider != null && queryBuilder.MetadataProvider.Connected)
            {
                if (queryBuilder.MetadataProvider is OLEDBMetadataProvider)
                {
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

                if (queryBuilder.MetadataProvider is UniversalMetadataProvider)
                {
                    if (this.DatabasePlatform == QueryBuilderDatabasePlatform.Sybase)
                    {
                        AseCommand command = (AseCommand)queryBuilder.MetadataProvider.Connection.CreateCommand();
                        command.CommandText = queryBuilder.SQL;

                        // handle the query parameters
                        if (queryBuilder.Parameters.Count > 0)
                        {
                            for (int i = 0; i < queryBuilder.Parameters.Count; i++)
                            {
                                if (!command.Parameters.Contains(queryBuilder.Parameters[i].FullName))
                                {
                                    AseParameter parameter = new AseParameter();
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

                        AseDataAdapter adapter = new AseDataAdapter(command);
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
                    }//end sybase

                }//end universal metadata provider



            }//end check if connected

        }//end method


    }//end class
}//end namespace

#pragma warning restore 1591
