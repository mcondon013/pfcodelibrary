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
using PFDataAccessObjects;
using PFCollectionsObjects;
using PFListObjects;
using AppGlobals;

namespace PFConnectionObjects
{
    /// <summary>
    /// Class to determine if a .NET data provider is installed on the current system.
    /// Class is also used to collect and store information on all installed .NET data providers. 
    /// </summary>
    public class PFConnectionManager
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        //private string _providerListName = "ProviderDefinitionsList";
        //private string _connectionListName = "ConnectionDefinitionsList";
        List<string> _installedProviders = default(List<string>);

        private PFList<KeyValuePair<string, string>> _dbProviderInvariantNames = new PFList<KeyValuePair<string, string>>()
               {new KeyValuePair<string,string>(DatabasePlatform.Unknown.ToString(),"System.Data.Odbc"),
                new KeyValuePair<string,string>(DatabasePlatform.MSSQLServer.ToString(),"System.Data.SqlClient"), 
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE35.ToString(),"System.Data.SQLServerCe.3.5"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE40.ToString(),"System.Data.SQLServerCe.4.0"),
                new KeyValuePair<string,string>(DatabasePlatform.MSOracle.ToString(),"System.Data.OracleClient"),
                new KeyValuePair<string,string>(DatabasePlatform.OracleNative.ToString(),"Oracle.DataAccess.Client"),
                new KeyValuePair<string,string>(DatabasePlatform.MySQL.ToString(),"MySql.Data.MySqlClient"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhere.ToString(),"iAnywhere.Data.SQLAnywhere"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhereUltraLite.ToString(),"iAnywhere.Data.UltraLite"),
                new KeyValuePair<string,string>(DatabasePlatform.Sybase.ToString(),"Sybase.Data.AseClient"),
                new KeyValuePair<string,string>(DatabasePlatform.DB2.ToString(),"IBM.Data.DB2"),
                new KeyValuePair<string,string>(DatabasePlatform.Informix.ToString(),"IBM.Data.Informix"),
                new KeyValuePair<string,string>(DatabasePlatform.ODBC.ToString(),"System.Data.Odbc"),
                new KeyValuePair<string,string>(DatabasePlatform.OLEDB.ToString(),"System.Data.OleDb"),
                new KeyValuePair<string,string>(DatabasePlatform.MSAccess.ToString(),"System.Data.OleDb") 
               };

        private PFList<KeyValuePair<string, string>> _dbAssemblyNamespaces = new PFList<KeyValuePair<string, string>>()
               {new KeyValuePair<string,string>(DatabasePlatform.Unknown.ToString(),"PFDataAccessObjects.PFOdbc"),
                new KeyValuePair<string,string>(DatabasePlatform.MSSQLServer.ToString(),"PFDataAccessObjects.PFSQLServer"), 
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE35.ToString(),"PFSQLServerCE35Objects.PFSQLServerCE35"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE40.ToString(),"PFSQLServerCE40Objects.PFSQLServerCE40"),
                new KeyValuePair<string,string>(DatabasePlatform.MSOracle.ToString(),"PFDataAccessObjects.PFMsOracle"),
                new KeyValuePair<string,string>(DatabasePlatform.OracleNative.ToString(),"PFOracleObjects.PFOracle"),
                new KeyValuePair<string,string>(DatabasePlatform.MySQL.ToString(),"PFMySQLObjects.PFMySQL"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhere.ToString(),"PFSQLAnywhereObjects.PFSQLAnywhere"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhereUltraLite.ToString(),"PFSQLAnywhereULObjects.PFSQLAnywhereUL"),
                new KeyValuePair<string,string>(DatabasePlatform.Sybase.ToString(),"PFSybaseObjects.PFSybase"),
                new KeyValuePair<string,string>(DatabasePlatform.DB2.ToString(),"PFDB2Objects.PFDB2"),
                new KeyValuePair<string,string>(DatabasePlatform.Informix.ToString(),"PFInformixObjects.PFInformix"),
                new KeyValuePair<string,string>(DatabasePlatform.ODBC.ToString(),"PFDataAccessObjects.PFOdbc"),
                new KeyValuePair<string,string>(DatabasePlatform.OLEDB.ToString(),"PFDataAccessObjects.PFOleDb"),
                new KeyValuePair<string,string>(DatabasePlatform.MSAccess.ToString(),"PFDataAccessObjects.PFMsAccess") 
               };

        private PFList<KeyValuePair<string, string>> _dbAssemblyNames = new PFList<KeyValuePair<string, string>>()
               {new KeyValuePair<string,string>(DatabasePlatform.Unknown.ToString(),"PFDataAccessObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.MSSQLServer.ToString(),"PFDataAccessObjects.dll"), 
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE35.ToString(),"PFSQLServerCE35Objects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE40.ToString(),"PFSQLServerCE40Objects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.MSOracle.ToString(),"PFDataAccessObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.OracleNative.ToString(),"PFOracleObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.MySQL.ToString(),"PFMySQLObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhere.ToString(),"PFSQLAnywhereObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhereUltraLite.ToString(),"PFSQLAnywhereULObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.Sybase.ToString(),"PFSybaseObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.DB2.ToString(),"PFDB2Objects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.Informix.ToString(),"PFInformixObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.ODBC.ToString(),"PFDataAccessObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.OLEDB.ToString(),"PFDataAccessObjects.dll"),
                new KeyValuePair<string,string>(DatabasePlatform.MSAccess.ToString(),"PFDataAccessObjects.dll") 
               };


        private PFList<KeyValuePair<string, string>> _connectionStrings = new PFList<KeyValuePair<string, string>>()
               { 
                new KeyValuePair<string,string>(DatabasePlatform.MSSQLServer.ToString(),@"Data Source=PROFASTSV2; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogGetSchemas; Workstation ID=PROFASTWS5;"), 
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE35.ToString(),@"data source='c:\SQLData\nametestV4_35.sdf';"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLServerCE40.ToString(),@"data source='c:\SQLData\nametestV4.sdf';"),
                new KeyValuePair<string,string>(DatabasePlatform.MSOracle.ToString(),@"Data Source=PROFASTSV4ORA;User ID=SYSTEM;Password=SYS1992;"),
                new KeyValuePair<string,string>(DatabasePlatform.OracleNative.ToString(),@"Data Source=PROFASTSV4ORA;User ID=SYSTEM;Password=SYS1992;"),
                new KeyValuePair<string,string>(DatabasePlatform.MySQL.ToString(),@"server=PROFASTSV4MYSQL; port=3306; database=SAKILA;User Id=Mike; password=MIKE92;"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhere.ToString(),@"UserID=DBA;Password=sql;DatabaseName=AdventureWorks;DatabasePath=C:\Testdata\SQLAnywhere\AdventureWorks.Db;ServerName=AdventureWorks"),
                new KeyValuePair<string,string>(DatabasePlatform.SQLAnywhereUltraLite.ToString(),@"nt_file=C:\Testdata\SQLAnywhere\Test1.udb;dbn=Test1;uid=DBA;pwd=sql"),
                new KeyValuePair<string,string>(DatabasePlatform.Sybase.ToString(),@"Data Source=PROFASTSV2SYB;Port=5000;Database=AdventureWorks;Uid=SA;Pwd=SA1992;"),
                new KeyValuePair<string,string>(DatabasePlatform.DB2.ToString(),@"Database=SAMPLE;User ID=DB2ADMIN;Password=DB21992;Server=PROFASTSV4DB2:50000;"),
                new KeyValuePair<string,string>(DatabasePlatform.Informix.ToString(),@"Database=miketest;User ID=informix;Password=IMX1992;Server=profastsv4imx:9089;"),
                new KeyValuePair<string,string>(DatabasePlatform.ODBC.ToString(),@"Driver={Oracle in OraClient11g_home1};Dbq=ORASV4;Uid=SYSTEM;Pwd=SYS1992;"),
                new KeyValuePair<string,string>(DatabasePlatform.OLEDB.ToString(),@"Provider=IBMDADB2;Database=sample;Hostname=profastsv4db2;Port=50000;Protocol=TCPIP;Uid=Mike;Pwd=MIKE92;"),
                new KeyValuePair<string,string>(DatabasePlatform.MSAccess.ToString(),@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Testdata\Access\AdventureWorksDW.accdb;User Id=admin;Password=;Jet OLEDB:Engine Type=6") 
               };
        

        
        
        //private variables for properties
        List<string> _errorMessages = new List<string>();
        private PFKeyValueList<string, PFProviderDefinition> _providerDefinitions = new PFKeyValueList<string, PFProviderDefinition>();
        private PFKeyValueList<string, PFConnectionDefinition> _connectionDefinitions = new PFKeyValueList<string, PFConnectionDefinition>();
        private string _providerDefinitionsLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\ConnectionManager\Providers\";
        private string _providerDefinitionsListLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\ConnectionManager\Providers\List";
        private string _connectionDefinitionsLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\ConnectionManager\Connections\";
        private string _connectionDefinitionsListLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\ConnectionManager\Connections\List";
        private string _expectedSqlAnywhereUltraLiteLocation = @"C:\Program Files\SQL Anywhere 12\UltraLite\UltraLite.NET\Assembly\V2\iAnywhere.Data.UltraLite.dll";


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFConnectionManager()
        {
            InitInstance();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="providerDefsLocation">File path or connection string for provider definition external storage.</param>
        /// <param name="providerDefsListLocation">File path for folder containing file with list of all providers.</param>
        /// <param name="connectionDefsLocation">File path or connection string for connections definition external storage.</param>
        /// <param name="connectionDefsListLocation">File path for folder containing file with list of all connection strings.</param>
        /// <remarks>Specify a file path (e.g. C:\DataFiles\providers.xml) if external storage is XML files. 
        /// Specify a connection string (e.g. data source='C:\DataFiles\providers.sdf') if external storage is a SQLCE 3.5 database.</remarks>
        public PFConnectionManager(string providerDefsLocation, string providerDefsListLocation, string connectionDefsLocation, string connectionDefsListLocation)
        {
            _providerDefinitionsLocation = providerDefsLocation;
            _providerDefinitionsListLocation = providerDefsLocation;
            _connectionDefinitionsLocation = connectionDefsLocation;
            _connectionDefinitionsListLocation = connectionDefsLocation;
            InitInstance();
        }

        private void InitInstance()
        {
            string configValue = string.Empty;


            if (Directory.Exists(_providerDefinitionsLocation) == false)
                Directory.CreateDirectory(_providerDefinitionsLocation);

            if (Directory.Exists(_connectionDefinitionsLocation) == false)
                Directory.CreateDirectory(_connectionDefinitionsLocation);

            if (Directory.Exists(_providerDefinitionsListLocation) == false)
                Directory.CreateDirectory(_providerDefinitionsListLocation);

            if (Directory.Exists(_connectionDefinitionsListLocation) == false)
                Directory.CreateDirectory(_connectionDefinitionsListLocation);

            //load provider definitions

            _providerDefinitions = GetListOfProviderDefinitions();

            //connection definitions are only loaded on request that includes database plaform for the connections.

            _installedProviders = GetListOfInstalledProviders();
        }

        /// <summary>
        /// Returns list provider definition objects for all supported providers (both installed and not).
        /// </summary>
        /// <returns>PFKeyValueList containing provider names and provider definitions. </returns>
        public PFKeyValueList<string, PFProviderDefinition> GetListOfProviderDefinitions()
        {
            PFKeyValueList<string, PFProviderDefinition> provDefs = new PFKeyValueList<string, PFProviderDefinition>();

            string[] provdefFiles = Directory.GetFiles(_providerDefinitionsLocation,"*.xml");

            foreach (string provdefFile in provdefFiles)
            {
                try
                {
                    PFProviderDefinition def = PFProviderDefinition.LoadFromXmlFile(provdefFile);
                    provDefs.Add(new stKeyValuePair<string, PFProviderDefinition>(def.DbPlatform.ToString(), def));
                }
                catch
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to load provider definition for ");
                    _msg.Append(provdefFile);
                    _msg.Append(" file. File may not be in correct XML format.");
                    _errorMessages.Add(_msg.ToString());
                }
            }


            return provDefs;
        }

        /// <summary>
        /// Gets list of invariant names for each provider supported by ADO.NET DBProviderFactories class.
        /// </summary>
        /// <returns>List of invariant names.</returns>
        /// <remarks>Only returns providers that register with .NET. (For example, SQLAnywhere Ultra Lite does not register with .NET.)</remarks>
        private List<string> GetListOfInstalledProviders()
        {
            List<string> installedProviders = new List<string>();
            DataTable dt = null;

            dt = DbProviderFactories.GetFactoryClasses();

            foreach (DataRow dr in dt.Rows)
            {
                //AppMessages.DisplayInfoMessage(dr["InvariantName"].ToString());
                installedProviders.Add(dr["InvariantName"].ToString());
            }

            return installedProviders;
        }


        //properties

        /// <summary>
        /// List of error messages reported during lifetime of current instance.
        /// </summary>
        public List<string> ErrorMessages
        {
            get
            {
                return _errorMessages;
            }
            set
            {
                _errorMessages = value;
            }
        }

        /// <summary>
        /// List of provider definition objects.
        /// </summary>
        /// <remarks>Some providers may not be installed on the local machine.</remarks>
        public PFKeyValueList<string, PFProviderDefinition> ProviderDefinitions
        {
            get
            {
                return _providerDefinitions;
            }
         }

        /// <summary>
        /// List of connection definition objects.
        /// </summary>
        public PFKeyValueList<string, PFConnectionDefinition> ConnectionDefinitions
        {
            get
            {
                return _connectionDefinitions;
            }
        }

        /// <summary>
        /// ProviderDefinitionsLocation Property. This will be either a file path if storage type is XML files or a SQL Compact 3.5 connection string if storage type is database.
        /// </summary>
        public string ProviderDefinitionsLocation
        {
            get
            {
                return _providerDefinitionsLocation;
            }
            set
            {
                _providerDefinitionsLocation = value;
            }
        }

        /// <summary>
        /// ConnectionDefintionsLocation Property. This will be either a file path if storage type is XML files or a database connection string if storage type is database.
        /// </summary>
        public string ConnectionDefintionsLocation
        {
            get
            {
                return _connectionDefinitionsLocation;
            }
            set
            {
                _connectionDefinitionsLocation = value;
            }
        }


        internal string ExpectedSqlAnywhereUltraLiteLocation
        {
            get
            {
                return _expectedSqlAnywhereUltraLiteLocation;
            }
            set
            {
                _expectedSqlAnywhereUltraLiteLocation = value;
            }
        }

        //Methods

        /// <summary>
        /// Routine to create provider definitions for each of the supported database platforms. 
        /// </summary>
        public void CreateProviderDefinitions()
        {
            foreach (DatabasePlatform dbp in (DatabasePlatform[]) Enum.GetValues(typeof(DatabasePlatform)))
            {
                string filename = Path.Combine(_providerDefinitionsLocation, dbp.ToString() + ".xml");
                if (File.Exists(filename) == false)
                {
                    if (dbp != DatabasePlatform.Unknown)
                    {
                        PFProviderDefinition provDef = new PFProviderDefinition(dbp);
                        provDef.ProviderName = dbp.ToString();
                        provDef.InstallationStatus = enProviderInstallationStatus.Unknown;
                        //test code removed Nov. 2, 2014
                        //switch (dbp)
                        //{
                        //    case DatabasePlatform.MSSQLServer:
                        //        provDef.AvailableForSelection = true;
                        //        break;
                        //    case DatabasePlatform.SQLServerCE35:
                        //        provDef.AvailableForSelection = true;
                        //        break;
                        //    case DatabasePlatform.ODBC:
                        //        provDef.AvailableForSelection = true;
                        //        break;
                        //    case DatabasePlatform.OLEDB:
                        //        provDef.AvailableForSelection = true;
                        //        break;
                        //    case DatabasePlatform.MSAccess:
                        //        provDef.AvailableForSelection = true;
                        //        break;
                        //    case DatabasePlatform.OracleNative:
                        //        provDef.AvailableForSelection = true;
                        //        break;
                        //    case DatabasePlatform.DB2:
                        //        provDef.AvailableForSelection = true;
                        //        break;
                        //    default:
                        //        provDef.AvailableForSelection = false;
                        //        break;
                        //}
                        provDef.InvariantName = GetInvariantName(dbp);
                        provDef.AssemblyName = GetAssemblyName(dbp);
                        provDef.AssemblyNamespace = GetAssemblyNamespace(dbp);
                        provDef.InstallationStatus = GetProviderInstallationStatus(provDef);
                        if (provDef.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                            provDef.AvailableForSelection = true;
                        else
                            provDef.AvailableForSelection = false;

                        _providerDefinitions.Add(new stKeyValuePair<string, PFProviderDefinition>(dbp.ToString(), provDef));

                        provDef.SaveToXmlFile(filename);
                    }
                }
            }
            string filepath = Path.Combine(_providerDefinitionsListLocation, "AllProviders.xml");
            _providerDefinitions.SaveToXmlFile(filepath);
        }

        private string GetInvariantName(DatabasePlatform dbp)
        {
            string invName = "Unknown";

            foreach (KeyValuePair<string, string> kvp in _dbProviderInvariantNames)
            {
                if (dbp.ToString().ToUpper() == kvp.Key.ToUpper())
                {
                    invName = kvp.Value;
                    break;
                }
            }

            return invName;
        }

        private string GetAssemblyName(DatabasePlatform dbp)
        {
            string asmName = "Unknown";

            foreach (KeyValuePair<string, string> kvp in _dbAssemblyNames)
            {
                if (dbp.ToString().ToUpper() == kvp.Key.ToUpper())
                {
                    asmName = kvp.Value;
                    break;
                }
            }

            return asmName;
        }

        private string GetAssemblyNamespace(DatabasePlatform dbp)
        {
            string asmNamespace = "Unknown";

            foreach (KeyValuePair<string, string> kvp in _dbAssemblyNamespaces)
            {
                if (dbp.ToString().ToUpper() == kvp.Key.ToUpper())
                {
                    asmNamespace = kvp.Value;
                    break;
                }
            }

            return asmNamespace;
        }

        /// <summary>
        /// Routine will create a set of test connection definitions. This is used for unit testing only.
        /// </summary>
        public void CreateTestConnectionDefinitions()
        {
            foreach (DatabasePlatform dbp in Enum.GetValues(typeof(DatabasePlatform)))
            {
                if (dbp != DatabasePlatform.Unknown)
                {
                    PFDatabase db = new PFDatabase(dbp);
                    PFConnectionDefinition connDef = new PFConnectionDefinition();
                    connDef.ProviderDefinition = GetProviderDefinition(dbp, _providerDefinitions);
                    if (connDef.ProviderDefinition == null)
                        connDef.ProviderDefinition = new PFProviderDefinition();
                    connDef.DbPlatform = dbp;
                    connDef.ConnectionAccessStatus = enConnectionAccessStatus.Unknown;
                    connDef.ConnectionName = "Test_" + dbp.ToString();
                    connDef.ConnectionString = GetConnectionString(dbp);
                    connDef.DbPlatformConnectionStringProperties = db.GetPropertiesForPlatform();
                    connDef.DbPlatformConnectionStringProperties = db.GetPropertiesForPlatform();
                    _connectionDefinitions.Add(new stKeyValuePair<string, PFConnectionDefinition>(connDef.ConnectionName, connDef));

                    string filename = Path.Combine(_connectionDefinitionsLocation, "Test", dbp.ToString(), connDef.ConnectionName + ".xml");
                    string dir = Path.GetDirectoryName(filename);
                    if (Directory.Exists(dir) == false)
                        Directory.CreateDirectory(dir);
                    connDef.SaveToXmlFile(filename);
                    db = null;
                }
            }

        }

        private PFProviderDefinition GetProviderDefinition(DatabasePlatform dbp, PFKeyValueList<string, PFProviderDefinition> provList)
        {
            PFProviderDefinition provDef = null;

            foreach (stKeyValuePair<string, PFProviderDefinition> kvp in provList)
            {
                if (kvp.Value.DbPlatform == dbp)
                {
                    provDef = kvp.Value;
                    break;
                }
            }

            return provDef;
        }

        private string GetConnectionString(DatabasePlatform dbp)
        {
            string connStr = "Unknown";

            foreach (KeyValuePair<string, string> kvp in _connectionStrings)
            {
                if (dbp.ToString().ToUpper() == kvp.Key.ToUpper())
                {
                    connStr = kvp.Value;
                    break;
                }
            }

            return connStr;
        }


        /// <summary>
        /// Determines if a particular provider definition exists in the provider database.
        /// </summary>
        /// <param name="providerName">Name of provider.</param>
        /// <returns>True if provider name found; otherwise false.</returns>
        public bool ProviderExists(string providerName)
        {
            bool provExists = false;

            stKeyValuePair<string, PFProviderDefinition> tmp = default(stKeyValuePair<string, PFProviderDefinition>);

            _providerDefinitions.SetToBOF();
            tmp = _providerDefinitions.FirstItem;
            while (!_providerDefinitions.EOF)
            {
                if (tmp.Key == providerName)
                {
                    provExists = true;
                    break;
                }
                tmp = _providerDefinitions.NextItem;
            }


            return provExists;
        }

        /// <summary>
        /// Determines if a particular connection definition exists in the connection database.
        /// </summary>
        /// <param name="connectionName">Name of connection.</param>
        /// <returns>True if connection name found; otherwise false.</returns>
        public bool ConnectionExists(string connectionName)
        {
            bool connExists = false;

            stKeyValuePair<string, PFConnectionDefinition> tmp = default(stKeyValuePair<string, PFConnectionDefinition>);

            _connectionDefinitions.SetToBOF();
            tmp = _connectionDefinitions.FirstItem;
            while (!_connectionDefinitions.EOF)
            {
                if (tmp.Key == connectionName)
                {
                    connExists = true;
                    break;
                }
                tmp = _connectionDefinitions.NextItem;
            }

            return connExists;
        }

        /// <summary>
        /// Adds specified provider definition object to provider definitions list.
        /// </summary>
        /// <param name="provDef">Object containing provider definition.</param>
        /// <remarks>If definition already exists for the provider name, an exception will be thrown.</remarks>
        public void AddProvider(PFProviderDefinition provDef)
        {
            AddProvider(provDef, false);
        }

        /// <summary>
        /// Adds specified provider definition object to provider definitions list.
        /// </summary>
        /// <param name="provDef">Object containing provider definition.</param>
        /// <param name="overwriteExistingDefinition">Set to true to overwrite an existing definition with same provider name.</param>
        public void AddProvider(PFProviderDefinition provDef, bool overwriteExistingDefinition)
        {
            string key = provDef.ProviderName;
            if(key.Trim().Length==0)
                key = provDef.DbPlatform.ToString();
            _providerDefinitions.Add(new stKeyValuePair<string, PFProviderDefinition>(key, provDef));
        }

        /// <summary>
        /// Adds specified connection definition object to connection definitions list.
        /// </summary>
        /// <param name="connDef">Object containing connection definition.</param>
        /// <remarks>If definition already exists for the connection name, an exception will be thrown.</remarks>
        public void AddConnection(PFConnectionDefinition connDef)
        {
            AddConnection(connDef, false);
        }

        /// <summary>
        /// Adds specified connection definition object to connection definitions list.
        /// </summary>
        /// <param name="connDef">Object containing connection definition.</param>
        /// <param name="overwriteExistingDefinition">Set to true to overwrite an existing definition with same connection name.</param>
        public void AddConnection(PFConnectionDefinition connDef, bool overwriteExistingDefinition)
        {
            bool connectionNameAlreadyExists = false;
            string key = connDef.ConnectionName;
            if (key.Trim().Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a name for the connection.");
                throw new System.Exception(_msg.ToString());
            }
            connectionNameAlreadyExists = _connectionDefinitions.Exists(connDef.ConnectionName);
            if (overwriteExistingDefinition)
            {
                if (connectionNameAlreadyExists)
                {
                    
                }
            }
            _connectionDefinitions.Add(new stKeyValuePair<string, PFConnectionDefinition>(key, connDef));
        }

        /// <summary>
        /// Finds and returns the provider definition object associated with the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns>PFProviderDefinition object if name found on provider list; otherwise returns null.</returns>
        public PFProviderDefinition FindProvider(string providerName)
        {
            PFProviderDefinition provDef = null;
            stKeyValuePair<string, PFProviderDefinition> tmp = default(stKeyValuePair<string, PFProviderDefinition>);

            _providerDefinitions.SetToBOF();
            tmp = _providerDefinitions.FirstItem;
            while (!_providerDefinitions.EOF)
            {
                if (tmp.Key  == providerName)
                {
                    provDef = tmp.Value;
                    break;
                }
                tmp = _providerDefinitions.NextItem;
            }


            return provDef;
        }

        /// <summary>
        /// Finds and returns the connection definition object associated with the specified connection name.
        /// </summary>
        /// <param name="connectionName">Name that was given to the connection.</param>
        /// <returns>PFConnectionDefinition object if name found on connection list; otherwise returns null.</returns>
        public PFConnectionDefinition FindConnection(string connectionName)
        {
            PFConnectionDefinition connDef = null;
            stKeyValuePair<string, PFConnectionDefinition> tmp = default(stKeyValuePair<string, PFConnectionDefinition>);

            _connectionDefinitions.SetToBOF();
            tmp = _connectionDefinitions.FirstItem;
            while (!_connectionDefinitions.EOF)
            {
                if (tmp.Key == connectionName)
                {
                    connDef = tmp.Value;
                    break;
                }
                tmp = _connectionDefinitions.NextItem;
            }


            return connDef;
        }

        /// <summary>
        /// Deletes the provider object associated with the specified provider name.
        /// </summary>
        /// <param name="providerName">Name of provider object to delete.</param>
        /// <returns>True if object deleted. Returns false if name not found or if delete operation failed.</returns>
        public bool DeleteProvider(string providerName)
        {
            bool itemDeleted = false;
            PFProviderDefinition provDef = null;

            provDef = this.FindProvider(providerName);
            if (provDef != null)
            {
                stKeyValuePair<string, PFProviderDefinition> kvp = new stKeyValuePair<string, PFProviderDefinition>(providerName, provDef);
                _providerDefinitions.Remove(kvp);
                itemDeleted = true;
            }


            return itemDeleted;
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void DeleteAllProviders()
        {

            _providerDefinitions.Clear();

        }

        /// <summary>
        /// Deletes the connection object associated with the specified connection name.
        /// </summary>
        /// <param name="connectionName">Name of connection object to delete.</param>
        /// <returns>True if object deleted. Returns false if name not found or if delete operation failed.</returns>
        public bool DeleteConnection(string connectionName)
        {
            bool itemDeleted = false;
            PFConnectionDefinition connDef = null;

            connDef = this.FindConnection(connectionName);
            if (connDef != null)
            {
                stKeyValuePair<string, PFConnectionDefinition> kvp = new stKeyValuePair<string, PFConnectionDefinition>(connectionName, connDef);
                _connectionDefinitions.Remove(kvp);
                itemDeleted = true;
            }


            return itemDeleted;
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void DeleteAllConnections()
        {
            _connectionDefinitions.Clear();
        }

        /// <summary>
        /// Saves items in provider list to external storage.
        /// </summary>
        public void SaveAllProviders()
        {
            foreach (stKeyValuePair<string, PFProviderDefinition> kvp in _providerDefinitions)
            {
                PFProviderDefinition provdef = kvp.Value;
                SaveProvider(provdef);
            }
        }

        /// <summary>
        /// Saves provider list to a single file on external storage.
        /// </summary>
        public void SaveProvidersListToFile()
        {
            _providerDefinitions.SaveToXmlFile(Path.Combine(_providerDefinitionsListLocation, "AllProviders.xml"));
        }

        /// <summary>
        /// Saves specified provider definition to external storage.
        /// </summary>
        /// <param name="provdef"></param>
        public void SaveProvider(PFProviderDefinition provdef)
        {
            string filename = Path.Combine(_providerDefinitionsLocation, provdef.DbPlatform.ToString() + ".xml");
            provdef.SaveToXmlFile(filename);
        }

        /// <summary>
        /// Saves connections list to external storage.
        /// </summary>
        public void SaveAllConnections()
        {
            foreach (stKeyValuePair<string, PFConnectionDefinition> kvp in _connectionDefinitions)
            {
                PFConnectionDefinition conndef = kvp.Value;
                SaveConnection(conndef);
            }
        }

        /// <summary>
        /// Saves specified connection definition to external storage.
        /// </summary>
        /// <param name="conndef"></param>
        /// <returns>Path to saved XML file containing connection definition.</returns>
        public string SaveConnection(PFConnectionDefinition conndef)
        {
            string filename = Path.Combine(_connectionDefinitionsLocation, conndef.DbPlatform.ToString(), conndef.ConnectionName + ".xml");
            string dir = Path.GetDirectoryName(filename);
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);
            conndef.SaveToXmlFile(filename);
            return filename;
        }

        /// <summary>
        /// Determines whether the .NET provider associated with the given providerName exists on the local machine.
        /// </summary>
        /// <param name="providerName">Name of the provider to verify.</param>
        /// <returns>Enumerated value that specifies whether provider is installed, not installed or installation status is unknown.</returns>
        public enProviderInstallationStatus VerifyProvider(string providerName)
        {
            enProviderInstallationStatus status = enProviderInstallationStatus.Unknown;

            PFProviderDefinition provDef = this.FindProvider(providerName);

            if (provDef != null)
                status = GetProviderInstallationStatus(provDef);

            return status;
        }

        /// <summary>
        /// Determines whether the .NET provider associated with the given provider object exists on the local machine.
        /// </summary>
        /// <param name="provDef">PFProviderDefinition object to be verified.</param>
        /// <returns>Enumerated value that specifies whether provider is installed, not installed or installation status is unknown.</returns>
        public enProviderInstallationStatus VerifyProvider(PFProviderDefinition provDef)
        {
            return GetProviderInstallationStatus(provDef);
        }

        /// <summary>
        /// Determines the installation status for all the providers contained in the providers list. Results of the verification process are stored in the InstallationStatus property of each object.
        /// </summary>
        public void VerifyAllProviders()
        {
            foreach (stKeyValuePair<string, PFProviderDefinition> provItem in _providerDefinitions)
            {
                provItem.Value.InstallationStatus = GetProviderInstallationStatus(provItem.Value);
            }
        }


        /// <summary>
        /// Determines and saves to disk whether the .NET provider associated with the given providerName exists on the local machine.
        /// </summary>
        /// <param name="providerName">Name of the provider to verify.</param>
        /// <returns>Enumerated value that specifies whether provider is installed, not installed or installation status is unknown.</returns>
        public enProviderInstallationStatus UpdateProviderInstallationStatus(string providerName)
        {
            enProviderInstallationStatus status = enProviderInstallationStatus.Unknown;

            PFProviderDefinition provDef = this.FindProvider(providerName);

            if (provDef != null)
            {
                status = GetProviderInstallationStatus(provDef);
                provDef.InstallationStatus = status;
                string filename = Path.Combine(_providerDefinitionsLocation, provDef.DbPlatform.ToString() + ".xml");
                provDef.SaveToXmlFile(filename);

                //refresh to the file containing data on all providers to include status just retrieved
                string filepath = Path.Combine(_providerDefinitionsListLocation, "AllProviders.xml");
                _providerDefinitions.SaveToXmlFile(filepath);
            }


            return status;
        }

        /// <summary>
        /// Updates the AvailableForSelection switch used by applications to determine if a provider can be used.
        /// </summary>
        /// <param name="providerName">Name of the provider to update.</param>
        /// <param name="availableForSelection">True or false value indicating whether or not the provider is available for use by applications.</param>
        /// <returns>True if provider is available. False if not.</returns>
        public void UpdateProviderAvailableForSelectionStatus(string providerName, bool availableForSelection)
        {
            PFProviderDefinition provDef = this.FindProvider(providerName);

            if (provDef != null)
            {
                provDef.AvailableForSelection = availableForSelection;
                string filename = Path.Combine(_providerDefinitionsLocation, provDef.DbPlatform.ToString() + ".xml");
                provDef.SaveToXmlFile(filename);

                //refresh to the file containing data on all providers to include status just retrieved
                string filepath = Path.Combine(_providerDefinitionsListLocation, "AllProviders.xml");
                _providerDefinitions.SaveToXmlFile(filepath);
            }

        }



        /// <summary>
        /// Determines the installation status for all the providers contained in the providers list. Results of the verification process are stored in the InstallationStatus property of each object and the object is saved to disk.
        /// </summary>
        public void UpdateAllProvidersInstallationStatus()
        {
            foreach (stKeyValuePair<string, PFProviderDefinition> provItem in _providerDefinitions)
            {
                provItem.Value.InstallationStatus = GetProviderInstallationStatus(provItem.Value);
                string filename = Path.Combine(_providerDefinitionsLocation, provItem.Value.DbPlatform.ToString() + ".xml");
                provItem.Value.SaveToXmlFile(filename);
                //if (provItem.Value.ProviderName.ToUpper() == "SQLSERVERCE35")
                //{
                //    _msg.Length = 0;
                //    _msg.Append(provItem.Value.ProviderName);
                //    _msg.Append(Environment.NewLine);
                //    _msg.Append(provItem.Value.InvariantName);
                //    _msg.Append(Environment.NewLine);
                //    _msg.Append(provItem.Value.DbPlatform.ToString());
                //    _msg.Append(Environment.NewLine);
                //    _msg.Append(filename);
                //    _msg.Append(Environment.NewLine);
                //    _msg.Append(provItem.Value.InstallationStatus);
                //    AppMessages.DisplayInfoMessage(_msg.ToString());
                //}
            }
            string filepath = Path.Combine(_providerDefinitionsListLocation, "AllProviders.xml");
            _providerDefinitions.SaveToXmlFile(filepath);
        }

        private enProviderInstallationStatus GetProviderInstallationStatus(PFProviderDefinition provDef)
        {
            enProviderInstallationStatus installationStatus = enProviderInstallationStatus.Unknown;

            foreach (string invariantName in _installedProviders)
            {
                if (provDef.InvariantName.ToUpper() == invariantName.ToUpper())
                {
                    installationStatus = enProviderInstallationStatus.IsInstalled;
                    provDef.InstallationStatus = enProviderInstallationStatus.IsInstalled;
                    break;
                }
            }

            if (installationStatus == enProviderInstallationStatus.Unknown)
            {
                if (provDef.InvariantName.ToUpper() == "iAnywhere.Data.UltraLite".ToUpper())
                {
                    //sql anywhere ultra lite will be listed as either installed or unknown
                    //driver used in testing was a .NET 2.0 driver and does not show up in the list of .NET 4 providers
                    //will be considered installed if DLL found at an expected location
                    if (File.Exists(@"C:\Program Files\SQL Anywhere 12\UltraLite\UltraLite.NET\Assembly\V2\iAnywhere.Data.UltraLite.dll"))
                    {
                        installationStatus = enProviderInstallationStatus.IsInstalled;
                        provDef.InstallationStatus = enProviderInstallationStatus.IsInstalled;
                    }
                    else
                    {
                        string configValue = AppConfig.GetStringValueFromConfigFile("SQLAnywhereUltraLiteInstallFolder", string.Empty);
                        if (configValue.Length > 0)
                        {
                            if (File.Exists(configValue))
                            {
                                installationStatus = enProviderInstallationStatus.IsInstalled;
                                provDef.InstallationStatus = enProviderInstallationStatus.IsInstalled;
                            }
                            else
                            {
                                installationStatus = enProviderInstallationStatus.Unknown;
                                provDef.InstallationStatus = enProviderInstallationStatus.Unknown;
                            }
                        }
                        installationStatus = enProviderInstallationStatus.Unknown;
                        provDef.InstallationStatus = enProviderInstallationStatus.Unknown;
                    }
                }
                else
                {
                    installationStatus = enProviderInstallationStatus.NotInstalled;
                    provDef.InstallationStatus = enProviderInstallationStatus.NotInstalled;
                }

            }

            return installationStatus;
        }

        /// <summary>
        /// Determines whether the database connection associated with the given connectionName can be opened.
        /// </summary>
        /// <param name="connectionName">Name of the connection to verify.</param>
        /// <returns>Enumerated value that specifies whether connection is accessible (can be opened), not accessible or connection accessibility is unknown.</returns>
        public enConnectionAccessStatus VerifyConnection(string connectionName)
        {
            enConnectionAccessStatus status = enConnectionAccessStatus.Unknown;

            PFConnectionDefinition connDef = this.FindConnection(connectionName);

            if (connDef != null)
                status = VerifyConnection(connDef);

            return status;
        }

        /// <summary>
        /// Determines whether the database connection associated with the given connection object can be opened.
        /// </summary>
        /// <param name="connDef">PFConnectionDefinition object to verify.</param>
        /// <returns>Enumerated value that specifies whether connection is accessible (can be opened), not accessible or connection accessibility is unknown.</returns>
        public enConnectionAccessStatus VerifyConnection(PFConnectionDefinition connDef)
        {
            return GetConnectionAccessStatus(connDef);
        }

        /// <summary>
        /// Determines the accessibility for all the connection definitions contained in the connections list. 
        ///  Results of the verification process are stored in the ConnectionAccessStatus property of each object.
        /// </summary>
        public void VerifyAllConnections()
        {
            foreach (stKeyValuePair<string, PFConnectionDefinition> connItem in _connectionDefinitions)
            {
                GetConnectionAccessStatus(connItem.Value);
            }
        }

        private enConnectionAccessStatus GetConnectionAccessStatus(PFConnectionDefinition connDef)
        {
            PFDatabase db = null;

            try
            {
                db = new PFDatabase(connDef.ProviderDefinition.DbPlatform);
                db.ConnectionString = connDef.ConnectionString;
                db.OpenConnection();
                if (db.IsConnected)
                {
                    connDef.ConnectionAccessStatus = enConnectionAccessStatus.IsAccessible;
                }
                else
                {
                    connDef.ConnectionAccessStatus = enConnectionAccessStatus.NotAccessible;
                }
            }
            catch
            {
                connDef.ConnectionAccessStatus = enConnectionAccessStatus.NotAccessible;
            }
            finally
            {
                if (db != null)
                    if (db.IsConnected)
                        db.CloseConnection();
                db = null;
            }


            return connDef.ConnectionAccessStatus;
        }



        //(updates should happen automatically when object is modified by caller)

        
        //Methods to save and load instances of this class follow:

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFConnectionManager));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFConnectionManager.</returns>
        public static PFConnectionManager LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFConnectionManager));
            TextReader textReader = new StreamReader(filePath);
            PFConnectionManager objectInstance;
            objectInstance = (PFConnectionManager)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(PFConnectionManager));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of PFConnectionManager.</returns>
        public static PFConnectionManager LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFConnectionManager));
            StringReader strReader = new StringReader(xmlString);
            PFConnectionManager objectInstance;
            objectInstance = (PFConnectionManager)deserializer.Deserialize(strReader);
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
