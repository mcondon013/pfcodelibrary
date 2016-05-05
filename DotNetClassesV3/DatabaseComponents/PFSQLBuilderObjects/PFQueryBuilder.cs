//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PFDataAccessObjects;

namespace PFSQLBuilderObjects
{
    //***********************************************************************************
    //
    // PFSQLBuilderObjects requires Active Quary Builder Winforms procduct
    // Product can be obtained at following link:
    // http://www.activequerybuilder.com/product_net.html
    //
    //***********************************************************************************

    /// <summary>
    /// Class containing routines for manaing a query builder session.
    /// </summary>
    public class PFQueryBuilder : ISQLBuilder
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private varialbles for properties
        private string _connectionString = string.Empty;
        private QueryBuilderDatabasePlatform _databasePlatform = QueryBuilderDatabasePlatform.Universal;
        private AnsiSQLLevel _ansiSQLVersion = AnsiSQLLevel.SQL92;


        //constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public PFQueryBuilder()
        {
            ;
        }

        //properties
        /// <summary>
        /// Connection string to database containing metadata for the query.
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
        /// Used to determine which metadata provider and syntax checker the query builder will use.
        /// </summary>
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

        /// <summary>
        /// Used to determine what type of SQL syntax checking to use for OLEDB and ODBC connections. 
        /// </summary>
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

        //methods

        /// <summary>
        /// Routine to create an instance of the PFQueryBuilder class.
        /// </summary>
        /// <param name="dbPlat">Database platform to use.</param>
        /// <param name="connStr">Connection string for the query.</param>
        /// <returns>Object that implements ISQLBuilder interface.</returns>
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
        /// <returns>Object that implements ISQLBuilder interface.</returns>
        public static ISQLBuilder CreateQueryBuilderObject(DatabasePlatform dbPlat, string connStr, AnsiSQLLevel ansiLevel)
        {
            PFQueryBuilder qbf = new PFQueryBuilder();
            
            qbf.DatabasePlatform = qbf.ConvertDbPlatformToQueryBuilderPlatform(dbPlat);
            qbf.ConnectionString = connStr;
            qbf.AnsiSQLVersion = ansiLevel;

            return qbf;
        }

        /// <summary>
        /// Converts a DatabasePlatform enum value to its equivalent QueryBuilderDatabasePlatform enum value.
        /// </summary>
        /// <param name="dbPlat">DatabasePlatform enum value to convert.</param>
        /// <returns>QueryBuilderDatabasePlatform enum value.</returns>
        public QueryBuilderDatabasePlatform ConvertDbPlatformToQueryBuilderPlatform(DatabasePlatform dbPlat)
        {
            QueryBuilderDatabasePlatform qbPlat = QueryBuilderDatabasePlatform.Unknown;

            switch (dbPlat)
            {
                case PFDataAccessObjects.DatabasePlatform.MSSQLServer:
                    qbPlat = QueryBuilderDatabasePlatform.MSSQLServer;
                    break;
                case PFDataAccessObjects.DatabasePlatform.MSAccess:
                    qbPlat = QueryBuilderDatabasePlatform.MSAccess;
                    break;
                case PFDataAccessObjects.DatabasePlatform.SQLServerCE35:
                    qbPlat = QueryBuilderDatabasePlatform.SQLServerCE;     //does not work
                    break;
                case PFDataAccessObjects.DatabasePlatform.ODBC:
                    qbPlat = QueryBuilderDatabasePlatform.ODBC;
                    break;
                case PFDataAccessObjects.DatabasePlatform.OLEDB:
                    qbPlat = QueryBuilderDatabasePlatform.OLEDB;
                    break;
                case PFDataAccessObjects.DatabasePlatform.MSOracle:
                    qbPlat = QueryBuilderDatabasePlatform.Oracle;
                    break;
                default:
                    qbPlat = QueryBuilderDatabasePlatform.Universal;
                    break;
            }


            return qbPlat;
        }

        /// <summary>
        /// Displays an instance of QueryBuilderForm.
        /// </summary>
        /// <param name="queryText">(optional)SQL query text from calling application. Leave blank if defining a query from scratch..</param>
        /// <returns>SQL query text defined through the query builder form.</returns>
        public string ShowQueryBuilder(string queryText)
        {
            string modifiedQueryText = queryText;
            QueryBuilderForm qbf = new QueryBuilderForm();

            qbf.QueryText = queryText;
            qbf.ConnectionString = this.ConnectionString;
            qbf.DatabasePlatform = this.DatabasePlatform;
            qbf.AnsiSQLVersion = this.AnsiSQLVersion;
            qbf.InitQueryBuilder();
            
            DialogResult res = qbf.ShowDialog();
            if (res == DialogResult.OK)
            {
                modifiedQueryText = qbf.QueryText;
            }
            qbf = null;



            return modifiedQueryText;
        }

        /// <summary>
        /// Unit testing routine.
        /// </summary>
        /// <param name="queryText"></param>
        /// <returns></returns>
        public static string RunQueryBuilderTest(string queryText)
        {
            string modifiedQueryText = queryText;
            QueryBuilderForm qbf = new QueryBuilderForm();
            qbf.QueryText = queryText;
            qbf.InitForTest();
            DialogResult res = qbf.ShowDialog();
            if (res == DialogResult.OK)
            {
                modifiedQueryText = qbf.QueryText;
            }
            qbf = null;
            return modifiedQueryText;
        }

    }//end class
}//end namespace
