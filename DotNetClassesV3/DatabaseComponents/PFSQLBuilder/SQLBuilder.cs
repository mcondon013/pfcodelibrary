//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using PFDataAccessObjects;
using PFSystemObjects;

namespace PFSQLBuilder
{
    /// <summary>
    /// Basic prototype for a ProFast application or library class.
    /// </summary>
    public class SQLBuilder
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SQLBuilder()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Routine to create an instance of the PFQueryBuilder class.
        /// </summary>
        /// <param name="dbPlat">Database platform to use.</param>
        /// <param name="connStr">Connection string for the query.</param>
        /// <returns>PFQueryBuilder object.</returns>
        /// <remarks>Default AnsiSQLLevel SQL92 will be used. </remarks>
        public static ISQLBuilder CreateQueryBuilderObject(DatabasePlatform dbPlat, string connStr)
        {

            return CreateQueryBuilderObject(dbPlat, connStr, AnsiSQLLevel.SQL92);

        }

        /// <summary>
        /// Routine to create an instance of the PFQueryBuilder class.
        /// </summary>
        /// <param name="dbPlat">Database platform to use.</param>
        /// <param name="connStr">Connection string for the query.</param>
        /// <param name="ansiLevel">AnsiSQLLevel for the query builder to use.</param>
        /// <returns>PFQueryBuilder object.</returns>
        public static ISQLBuilder CreateQueryBuilderObject(DatabasePlatform dbPlat, string connStr, AnsiSQLLevel ansiLevel)
        {
            ISQLBuilder  qbf = null;

            qbf = LoadObjectFromAssembly(dbPlat);

            qbf.DatabasePlatform = qbf.ConvertDbPlatformToQueryBuilderPlatform(dbPlat);
            qbf.ConnectionString = connStr;
            qbf.AnsiSQLVersion = ansiLevel;

            return qbf;
        }

        private static ISQLBuilder LoadObjectFromAssembly(DatabasePlatform dbPlat)
        {
            ISQLBuilder qbf = null;
            string currentEntryAssemblyPath = System.IO.Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetEntryAssembly().CodeBase).LocalPath);
            string assemblyPath = string.Empty;
            string assemblyNamespace = string.Empty;

            switch (dbPlat)
            {
                case DatabasePlatform.MSSQLServer:
                case DatabasePlatform.SQLServerCE35:
                case DatabasePlatform.MSAccess:
                case DatabasePlatform.ODBC:
                case DatabasePlatform.OLEDB:
                case DatabasePlatform.MSOracle:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath,"PFSQLBuilderObjects.dll");
                    assemblyNamespace = "PFSQLBuilderObjects.PFQueryBuilder";
                    break;
                case DatabasePlatform.SQLServerCE40:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath, "PFSQLServerCE40SQLBuilderObjects.dll");
                    assemblyNamespace = "PFSQLServerCE40SQLBuilderObjects.PFQueryBuilder";
                    break;
                case DatabasePlatform.OracleNative:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath, "PFOracleSQLBuilderObjects.dll");
                    assemblyNamespace = "PFOracleSQLBuilderObjects.PFQueryBuilder";
                    break;
                case DatabasePlatform.MySQL:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath, "PFMySQLSQLBuilderObjects.dll");
                    assemblyNamespace = "PFMySQLSQLBuilderObjects.PFQueryBuilder";
                    break;
                case DatabasePlatform.DB2:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath, "PFDB2SQLBuilderObjects.dll");
                    assemblyNamespace = "PFDB2SQLBuilderObjects.PFQueryBuilder";
                    break;
                case DatabasePlatform.Informix:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath, "PFInformixSQLBuilderObjects.dll");
                    assemblyNamespace = "PFInformixSQLBuilderObjects.PFQueryBuilder";
                    break;
                case DatabasePlatform.Sybase:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath, "PFSybaseSQLBuilderObjects.dll");
                    assemblyNamespace = "PFSybaseSQLBuilderObjects.PFQueryBuilder";
                    break;
                case DatabasePlatform.SQLAnywhere:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath, "PFSQLAnywhereSQLBuilderObjects.dll");
                    assemblyNamespace = "PFSQLAnywhereSQLBuilderObjects.PFQueryBuilder";
                    break;
                case DatabasePlatform.SQLAnywhereUltraLite:
                    assemblyPath = Path.Combine(currentEntryAssemblyPath, "PFSQLAnywhereULSQLBuilderObjects.dll");
                    assemblyNamespace = "PFSQLAnywhereULSQLBuilderObjects.PFQueryBuilder";
                    break;
                default:
                    break;
            }

            if (assemblyPath.Trim().Length > 0 && assemblyNamespace.Trim().Length > 0)
            {
                qbf = (ISQLBuilder)WindowsAssembly.LoadAndInstantiateType(assemblyPath, assemblyNamespace);
            }

            return qbf;
        }


    }//end class
}//end namespace
