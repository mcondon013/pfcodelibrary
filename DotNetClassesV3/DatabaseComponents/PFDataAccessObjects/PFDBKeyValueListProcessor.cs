//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using PFCollectionsObjects;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Basic prototype for a ProFast application or library class.
    /// </summary>
    public class PFDBKeyValueListProcessor<K, V>
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private string _listsSelectSQL = "select ListObject from Lists where ListName = '<listname>' and ListType = 'PFKeyValueList'";
        private string _listsInsertSQL = "insert Lists (ListName, ListType, ID, ListObject) values ('<listname>', 'PFKeyValueList', <id>, '<listobject>')";
        private string _listsDeleteOldSQL = "delete Lists where ListName = '<listname>' and ID <> <id> and ListType = 'PFKeyValueList'";


        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFDBKeyValueListProcessor()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Saves the public property values contained in the current instance to the database specified by the connection string.
        /// </summary>
        /// <param name="kvlist">Key/Value list object to be saved to a database.</param>
        /// <param name="connectionString">Contains information needed to open the database.</param>
        /// <param name="listName">Name of the list in the database.</param>
        public void SaveToDatabase(PFKeyValueList<K, V> kvlist, string connectionString, string listName)
        {
            string sqlStmt = string.Empty;
            PFDatabase db = new PFDatabase(DatabasePlatform.SQLServerCE35);
            int numRecsAffected = 0;
            DateTime currdate = DateTime.Now;
            string currBatchId = string.Empty;
            string listObject = string.Empty;

            db.ConnectionString = connectionString;
            db.OpenConnection();

            //create batch id for this list
            currBatchId = "'" + Guid.NewGuid().ToString().Trim() + "'";

            listObject = kvlist.ToXmlString().Replace("'", "");      //get rid of any single quotes in the object. they will mess up the sql syntax e.g. values(1, 'two' ,'this is the 'object'')

            //insert current list to the database
            sqlStmt = _listsInsertSQL.Replace("<listname>", listName).Replace("<id>", currBatchId).Replace("<listobject>", listObject);
            numRecsAffected = db.RunNonQuery(sqlStmt, CommandType.Text);

            //get rid of any previous PFListEx objects in the database
            sqlStmt = _listsDeleteOldSQL.Replace("<listname>", listName).Replace("<id>", currBatchId);
            numRecsAffected = db.RunNonQuery(sqlStmt, CommandType.Text);


            db.CloseConnection();
            db = null;


        }


        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a database record.
        /// </summary>
        /// <param name="connectionString">Connection parameters for the database.</param>
        /// <param name="listName">Name of the list in the database.</param>
        /// <returns>PFListEx object.</returns>
        public PFKeyValueList<K, V> LoadFromDatabase(string connectionString, string listName)
        {
            string sqlStmt = string.Empty;
            PFKeyValueList<K, V> objectInstance = null;
            PFDatabase db = new PFDatabase(DatabasePlatform.SQLServerCE35);
            DbDataReader rdr = null;
            string pfKeyValueListExXml = string.Empty;

            db.ConnectionString = connectionString;
            db.OpenConnection();

            sqlStmt = _listsSelectSQL.Replace("<listname>", listName);
            rdr = db.RunQueryDataReader(sqlStmt, CommandType.Text);
            while (rdr.Read())
            {
                pfKeyValueListExXml = rdr.GetString(0);
                objectInstance = PFKeyValueList<K, V>.LoadFromXmlString(pfKeyValueListExXml);
                break;  //should be only one record
            }

            db.CloseConnection();
            db = null;

            if (objectInstance == null)
                objectInstance = new PFKeyValueList<K, V>();

            return objectInstance;
        }





    }//end class
}//end namespace
