//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFCollectionsObjects;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Basic prototype for a ProFast application or library class.
    /// </summary>
    public class PFDBKeyValueListSortedProcessor<K, V>
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFDBKeyValueListSortedProcessor()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Converts PFKeyValueListSorted object to PFKeyValueList object.
        /// </summary>
        /// <returns>PFKeyValueList object.</returns>
        public PFKeyValueList<K, V> ConvertPFKeyValueListSortedToPFKeyValueList(PFKeyValueListSorted<K, V> sortedKvList)
        {
            PFKeyValueList<K, V> kvlist = new PFKeyValueList<K, V>();

            IEnumerator<KeyValuePair<K, V>> enumerator = sortedKvList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // Get current key value pair 
                stKeyValuePair<K, V> keyValuePair = new stKeyValuePair<K, V>(enumerator.Current.Key, enumerator.Current.Value);
                kvlist.Add(keyValuePair);
            }

            return kvlist;
        }

        /// <summary>
        /// Converts PFKeyValueList object to PFKeyValueListSorted object.
        /// </summary>
        /// <param name="kvlist"></param>
        /// <returns>PFKeyValueListSorted object.</returns>
        public static PFKeyValueListSorted<K, V> ConvertPFKeyValueListToSortedList(PFKeyValueList<K, V> kvlist)
        {
            PFKeyValueListSorted<K, V> kvlistSorted = new PFKeyValueListSorted<K, V>();
            kvlist.SetToBOF();
            stKeyValuePair<K, V> pfKeyValuePair = kvlist.FirstItem;
            while (!kvlist.EOF)
            {
                kvlistSorted.Add(pfKeyValuePair.Key, pfKeyValuePair.Value);
                pfKeyValuePair = kvlist.NextItem;
            }
            return kvlistSorted;
        }


        /// <summary>
        /// Saves the public property values contained in the current instance to the database specified by the connection string.
        /// </summary>
        /// <param name="kvlistSorted">Sorted key value list to be saved to database.</param>
        /// <param name="connectionString">Contains information needed to open the database.</param>
        /// <param name="listName">Name to give list in the database.</param>
        public void SaveToDatabase(PFKeyValueListSorted<K, V> kvlistSorted, string connectionString, string listName)
        {
            PFKeyValueList<K, V> kvlist = ConvertPFKeyValueListSortedToPFKeyValueList(kvlistSorted);

            PFDBKeyValueListProcessor<K, V> listProcessor = new PFDBKeyValueListProcessor<K, V>();
            listProcessor.SaveToDatabase(kvlist, connectionString, listName);
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a database record.
        /// </summary>
        /// <param name="connectionString">Connection parameters for the database.</param>
        /// <param name="listName">Name of the list in the database.</param>
        /// <returns>PFListEx object.</returns>
        public PFKeyValueListSorted<K, V> LoadFromDatabase(string connectionString, string listName)
        {
            PFDBKeyValueListProcessor<K, V> listProcessor = new PFDBKeyValueListProcessor<K, V>();
            PFKeyValueList<K, V> kvlist = listProcessor.LoadFromDatabase(connectionString, listName);
            return ConvertPFKeyValueListToSortedList(kvlist);
        }




    }//end class
}//end namespace
