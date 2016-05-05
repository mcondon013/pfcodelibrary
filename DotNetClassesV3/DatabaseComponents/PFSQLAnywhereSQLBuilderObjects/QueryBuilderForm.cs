#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.Common;
using iAnywhere.Data.SQLAnywhere;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFFileSystemObjects;
using ActiveDatabaseSoftware.ActiveQueryBuilder;
using PFTextObjects;
using PFDataAccessObjects;

namespace PFSQLAnywhereSQLBuilderObjects
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
                case QueryBuilderDatabasePlatform.SQLAnywhere:
                    metaprov = new UniversalMetadataProvider();
                    metaprov.Connection = new SAConnection(this.ConnectionString);
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
                case QueryBuilderDatabasePlatform.SQLAnywhere:
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
                    if (this.DatabasePlatform == QueryBuilderDatabasePlatform.SQLAnywhere)
                    {
                        SACommand command = (SACommand)queryBuilder.MetadataProvider.Connection.CreateCommand();
                        command.CommandText = queryBuilder.SQL;

                        // handle the query parameters
                        if (queryBuilder.Parameters.Count > 0)
                        {
                            for (int i = 0; i < queryBuilder.Parameters.Count; i++)
                            {
                                if (!command.Parameters.Contains(queryBuilder.Parameters[i].FullName))
                                {
                                    SAParameter parameter = new SAParameter();
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

                        SADataAdapter adapter = new SADataAdapter(command);
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
                    }//end sql anywhere

                }//end universal metadata provider



            }//end check if connected

        }//end method


    }//end class
}//end namespace

#pragma warning restore 1591
