//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using PFConnectionObjects;
using PFDataAccessObjects;
using PFListObjects;
using PFAppUtils;
using AppGlobals;

namespace PFConnectionStrings
{
    /// <summary>
    /// Basic prototype for a ProFast application or library class.
    /// </summary>
    public class ConnectionStringPrompt
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private IConnectionStringForm _frm = null;

        //private variables for properties
        private DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;
        private PFConnectionManager _connectionManager = null;
        private string _connectionName = string.Empty;
        private string _connectionString = string.Empty;
        private enConnectionAccessStatus _connectionAccessStatus = enConnectionAccessStatus.Unknown;
        private PFConnectionDefinition _connectionDefinition = null;
        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConnectionStringPrompt()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConnectionStringPrompt(DatabasePlatform dbPlatform, PFConnectionManager connMgr)
        {
            _dbPlatform = dbPlatform;
            _connectionManager = connMgr;
        }

        //properties

        /// <summary>
        /// DbPlatform Property contains one of the values in the DatabasePlatform enumeration.
        /// </summary>
        public DatabasePlatform DbPlatform
        {
            get
            {
                return _dbPlatform;
            }
            set
            {
                _dbPlatform = value;
            }
        }

        /// <summary>
        /// ConnectionManager property identifies object that is used for various operations on connection elements.
        /// </summary>
        public PFConnectionManager ConnectionManager
        {
            get
            {
                return _connectionManager;
            }
            set
            {
                _connectionManager = value;
            }
        }

        /// <summary>
        /// Name that will be used to identify the connection.
        /// </summary>
        public string ConnectionName
        {
            get
            {
                return _connectionName;
            }
            set
            {
                _connectionName = value;
            }
        }

        /// <summary>
        /// Connection string that can be used to connect to the database.
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

        /// <summary>
        /// Connection status returned when the connection string was verified: IsAccessible, NotAccessible or Unknown.
        /// </summary>
        /// <remarks>Verification involved attempting to logon using the connection string.</remarks>
        public enConnectionAccessStatus ConnectionAccessStatus
        {
            get
            {
                return _connectionAccessStatus;
            }
            set
            {
                _connectionAccessStatus = value;
            }
        }

        /// <summary>
        /// Object which represents several pieces of information associated with a particular connection.
        /// </summary>
        public PFConnectionDefinition ConnectionDefinition
        {
            get
            {
                return _connectionDefinition;
            }
            set
            {
                _connectionDefinition = value;
            }
        }

        //methods

        /// <summary>
        /// Displays the form dialog for inputting values for connection string keys.
        /// </summary>
        /// <returns>OK if input is to be accepted by the caller. Cancel if input should be ignored by the caller.</returns>
        public DialogResult ShowConnectionPrompt()
        {
            DialogResult res = DialogResult.None;
            _frm = GetFormToDisplay();
            if (_frm != null)
            {
                _frm.DbPlatform = _dbPlatform;
                _frm.ConnectionName = _connectionName;
                _frm.ConnectionString = _connectionString;
                _frm.CSP = this;
                res = _frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    _connectionName = _frm.ConnectionName;
                    _connectionString = _frm.ConnectionString;
                    _connectionAccessStatus = _frm.ConnectionAccessStatus;
                    _connectionDefinition = BuildConnectionDefinition();
                }
                else
                {
                    ;
                }
            }
            else
            {
                res = DialogResult.Cancel;
                _msg.Length = 0;
                _msg.Append("Unable to find connection string form for ");
                _msg.Append(this.DbPlatform.ToString());
                _msg.Append(".");
                throw new System.Exception(_msg.ToString());
            }
            return res;
        }

        private IConnectionStringForm GetFormToDisplay()
        {
            IConnectionStringForm frm = null;

            switch (this.DbPlatform)
            {
                case DatabasePlatform.MSSQLServer:
                    frm = new SQLServerConnectionStringForm();
                    break;
                case DatabasePlatform.SQLServerCE35:
                    frm = new SQLServerCE35ConnectionStringForm();
                    break;
                case DatabasePlatform.SQLServerCE40:
                    frm = new SQLServerCE40ConnectionStringForm();
                    break;
                case DatabasePlatform.MSAccess:
                    frm = new MSAccessConnectionStringForm();
                    break;
                case DatabasePlatform.MSOracle:
                    frm = new MSOracleConnectionStringForm();
                    break;
                case DatabasePlatform.ODBC:
                    frm = new OdbcConnectionStringForm();
                    break;
                case DatabasePlatform.OLEDB:
                    frm = new OleDbConnectionStringForm();
                    break;
                case DatabasePlatform.DB2:
                    frm = new DB2ConnectionStringForm();
                    break;
                case DatabasePlatform.Informix:
                    frm = new InformixConnectionStringForm();
                    break;
                case DatabasePlatform.MySQL:
                    frm = new MySQLConnectionStringForm();
                    break;
                case DatabasePlatform.OracleNative:
                    frm = new OracleConnectionStringForm();
                    break;
                case DatabasePlatform.SQLAnywhere:
                    frm = new SQLAnywhereConnectionStringForm();
                    break;
                case DatabasePlatform.SQLAnywhereUltraLite:
                    frm = new SQLAnywhereULConnectionStringForm();
                    break;
                case DatabasePlatform.Sybase:
                    frm = new SybaseConnectionStringForm();
                    break;
                default:
                    frm = new PFConnectionStringForm();
                    break;
            }

            return frm;
        }

        private PFConnectionDefinition BuildConnectionDefinition()
        {
            _connectionDefinition = new PFConnectionDefinition();
            _connectionDefinition.DbPlatform = _dbPlatform;
            _connectionDefinition.ConnectionName = _connectionName;
            _connectionDefinition.ConnectionString = _connectionString;
            _connectionDefinition.ProviderDefinition = _connectionManager.FindProvider(_dbPlatform.ToString());
            _connectionDefinition.ConnectionAccessStatus = _connectionAccessStatus;
            GetPlatformPropertiesAndKeys();
            return _connectionDefinition;
        }

        /// <summary>
        /// Routine to create a connection definition object.
        /// </summary>
        /// <param name="connectionName">Name for the connection definition.</param>
        /// <param name="connectionString">Connection string associated with the connection name.</param>
        /// <param name="connectionAccessStatus">Last connection access status: e.g. open, closed, unknown.</param>
        /// <returns>Connection definition object.</returns>
        public PFConnectionDefinition BuildConnectionDefinition(string connectionName, string connectionString, enConnectionAccessStatus connectionAccessStatus)
        {
            _connectionName = connectionName;
            _connectionString = connectionString;
            _connectionAccessStatus = connectionAccessStatus;
            return BuildConnectionDefinition();
        }


        private void GetPlatformPropertiesAndKeys()
        {
            DatabasePlatform dbPlat = _frm.DbPlatform;
            PFDatabase db = InitDatabaseObject(dbPlat);
            db.ConnectionString = _frm.ConnectionString;
            _connectionDefinition.DbPlatformConnectionStringProperties = db.GetPropertiesForPlatform();
            _connectionDefinition.ConnectionKeyElements = db.ConnectionStringKeyVals;
        }

        private PFDatabase InitDatabaseObject(DatabasePlatform dbPlat)
        {
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            PFDatabase db = null;

            string configValue = AppConfig.GetStringValueFromConfigFile(dbPlat.ToString(), string.Empty);
            if (configValue.Length > 0)
            {
                string[] parsedConfig = configValue.Split('|');
                if (parsedConfig.Length != 3)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid config entry items for ");
                    _msg.Append(_dbPlatform.ToString());
                    _msg.Append(". Number of items after parse: ");
                    _msg.Append(parsedConfig.Length.ToString());
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }

                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlat.ToString(), dllPath, nmSpace + "." + clsName);

            }
            else
            {
                db = new PFDatabase(dbPlat);
            }
            return db;
        }//end method


        /// <summary>
        /// Retrieves all connection definitions defined and saved by current user.
        /// </summary>
        /// <returns>PFConnectionDefinition object.</returns>
        public PFConnectionDefinition GetConnectionDefinition()
        {
            PFConnectionDefinition conndef = default(PFConnectionDefinition);

            string[] conndefFiles = Directory.GetFiles(_connectionManager.ConnectionDefintionsLocation, "*.xml", SearchOption.AllDirectories);

            return conndef;
        }

        /// <summary>
        /// Retrieves connection definitions defined and saved by current user for the specified database platform.
        /// </summary>
        /// <param name="dbPlat">DatabasePlatform used in the connection strings to be retrieved.</param>
        /// <returns>PFConnectionDefinition object.</returns>
        public PFConnectionDefinition GetConnectionDefinition(DatabasePlatform dbPlat)
        {
            PFConnectionDefinition conndef = default(PFConnectionDefinition);
            string pathToDefFiles = Path.Combine(_connectionManager.ConnectionDefintionsLocation, dbPlat.ToString());
            PFNameListPrompt nlp = new PFNameListPrompt();

            string[] conndefFiles = Directory.GetFiles(pathToDefFiles, "*.xml", SearchOption.AllDirectories);

            if (conndefFiles.Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("Unabled to find any saved connection definitions in ");
                _msg.Append(pathToDefFiles);
                _msg.Append(".");
                AppMessages.DisplayAlertMessage(_msg.ToString());
                return conndef;
            }

            nlp.Text = "Connection Definition Chooser ...";
            nlp.lblSelect.Text = "Select connection definition to open from list below:";
            foreach (string condefFile in conndefFiles)
            {
                string conndefName = Path.GetFileNameWithoutExtension(condefFile);
                nlp.lstNames.Items.Add(conndefName);
            }

            DialogResult res = nlp.ShowDialog();
            if (res == DialogResult.OK)
            {
                string selectedConnDefName = nlp.lstNames.SelectedItem.ToString();
                conndef = PFConnectionDefinition.LoadFromXmlFile(Path.Combine(pathToDefFiles,selectedConnDefName + ".xml")); 
            }

            return conndef;
        }

        /// <summary>
        /// Saves connection definition to external storage.
        /// </summary>
        /// <param name="connDef">Connection definition object to be saved.</param>
        /// <returns>Path to saved XML file containing connection definition.</returns>
        public string SaveConnectionDefinition(PFConnectionDefinition connDef)
        {
            string retval = string.Empty;
            string pathToDefFile = Path.Combine(_connectionManager.ConnectionDefintionsLocation, connDef.DbPlatform.ToString(), connDef.ConnectionName + ".xml");
            if(File.Exists(pathToDefFile))
            {
                _msg.Length = 0;
                _msg.Append("Connection definition already exists at ");
                _msg.Append(pathToDefFile);
                _msg.Append(".\r\nDo you wish to overwrite the file?");
                DialogResult res = AppMessages.DisplayMessage(_msg.ToString(), "Save Connection ...",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
                if (res != DialogResult.Yes)
                    return retval;
            }
            retval = _connectionManager.SaveConnection(connDef);
            return retval;
        }

        /// <summary>
        /// Allows renaming of connection definitions from list of saved definitions.
        /// </summary>
        /// <param name="dbPlat">Database platform for which the connections are defined.</param>
        public void RenameConnectionDefinition(DatabasePlatform dbPlat)
        {
            PFNameListRenamePrompt frm = null;

            try
            {
                frm = new PFNameListRenamePrompt();
                frm.Caption = "Rename Connection String Definition";
                frm.SourceFolder = Path.Combine(_connectionManager.ConnectionDefintionsLocation, dbPlat.ToString());
                frm.ShowDialog();
                frm.Close();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("FileRename on RandomBooleansForm failed.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                frm = null;
            }


        }

        /// <summary>
        /// Allows deleting of connection definitions from list of saved definitions.
        /// </summary>
        /// <param name="dbPlat">Database platform for which the connections are defined.</param>
        public void DeleteConnectionDefinition(DatabasePlatform dbPlat)
        {
            PFNameListDeleteListPrompt frm = null;

            try
            {
                frm = new PFNameListDeleteListPrompt();
                frm.Caption = "Delete Connection String Definition";
                frm.ListBoxLabel = "Select one or more items to delete:";
                frm.SourceFolder = Path.Combine(_connectionManager.ConnectionDefintionsLocation, dbPlat.ToString());
                frm.ShowDialog();
                frm.Close();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                frm = null;
            }
        }




    }//end class
}//end namespace
