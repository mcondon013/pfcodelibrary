#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.Odbc;
using iAnywhere.Data.UltraLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFFileSystemObjects;
using ActiveDatabaseSoftware.ActiveQueryBuilder;
using PFTextObjects;
using PFDataAccessObjects;

namespace PFSQLAnywhereULSQLBuilderObjects
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

                //workaround to load UltraLite schema tree
                if (this.DatabasePlatform == QueryBuilderDatabasePlatform.SQLAnywhereUL)
                {
                    LoadUltraLiteSchemaTree();
                }

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
                case QueryBuilderDatabasePlatform.SQLAnywhereUL:
                    metaprov = new UniversalMetadataProvider();
                    metaprov.Connection = new ULConnection(this.ConnectionString);
                    break;
                default:
                    metaprov = new UniversalMetadataProvider();
                    metaprov.Connection = new OdbcConnection(this.ConnectionString);
                    break;
            }

            return metaprov;
        }

        private BaseSyntaxProvider GetSyntaxProvider()
        {
            BaseSyntaxProvider sqlsyn = null;

            switch (this.DatabasePlatform)
            {
                case QueryBuilderDatabasePlatform.SQLAnywhereUL:
                    sqlsyn = new GenericSyntaxProvider();
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
                if (queryBuilder.MetadataProvider is UniversalMetadataProvider)
                {
                    if (this.DatabasePlatform == QueryBuilderDatabasePlatform.SQLAnywhereUL)
                    {
                        ULCommand command = (ULCommand)queryBuilder.MetadataProvider.Connection.CreateCommand();
                        command.CommandText = queryBuilder.SQL;

                        // handle the query parameters
                        if (queryBuilder.Parameters.Count > 0)
                        {
                            for (int i = 0; i < queryBuilder.Parameters.Count; i++)
                            {
                                if (!command.Parameters.Contains(queryBuilder.Parameters[i].FullName))
                                {
                                    ULParameter parameter = new ULParameter();
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

                        ULDataAdapter adapter = new ULDataAdapter(command);
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
                    }//end sql anywhere UL

                }//end universal metadata provider



            }//end check if connected

        }//end method


        private void LoadUltraLiteSchemaTree()
        {
            queryBuilder.MetadataContainer.BeginUpdate();
            MetadataItem schemaRoot = null;
            PFDataAccessObjects.PFDatabase db = null;

            try
            {
                queryBuilder.MetadataContainer.Items.Clear();
                queryBuilder.InitializeDatabaseSchemaTree();


                if (queryBuilder.MetadataContainer.Items.Count == 1)
                {
                    schemaRoot = queryBuilder.MetadataContainer.Items[0];
                }
                else
                {
                    schemaRoot = queryBuilder.MetadataContainer.AddDatabase("UltraLiteDb");
                }


                //MetadataObject table = queryBuilder.MetadataContainer.AddTable("TestTable");
                //MetadataObject table = schemaRoot.AddTable("TestTable");

                //get list of tables and add them to the schema tree
                db = new PFDataAccessObjects.PFDatabase(PFDataAccessObjects.DatabasePlatform.SQLAnywhereUltraLite);
                db.ConnectionString = this.ConnectionString;
                db.OpenConnection();
                DataTable datatableList = db.Connection.GetSchema(ULMetaDataCollectionNames.Tables);
                foreach (DataRow row in datatableList.Rows)
                {
                    string tableName = row["table_name"].ToString();
                    MetadataObject table = schemaRoot.AddTable(tableName);
                    string sql = "select * from " + tableName + " where 0=1";
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
                    //close and reopen connection to free up space used by temp tables in the UL .NET provider
                    db.CloseConnection();
                    db.OpenConnection();
                }


                //MetadataField field = table.AddField("Fld1");
                //field.FieldTypeName = "nvarchar";
                //field.Size = 30;

                //field = table.AddField("Fld2");
                //field.FieldTypeName = "int";

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to load UltraLite Schema Tree failed.");
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

            queryBuilder.DatabaseSchemaTreeOptions.DefaultExpandLevel = 1;
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
