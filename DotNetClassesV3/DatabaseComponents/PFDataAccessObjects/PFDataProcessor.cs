//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Xml;

namespace PFDataAccessObjects
{
    /// <summary>
    /// Class to manage common data operations on data stored in ADO.NET DataSet and DataTable objects.
    /// </summary>
    public class PFDataProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        //private varialbles for properties

        //events
#pragma warning disable 1591
        public delegate void ResultDelegate(DataColumnCollection columns, DataRow data, int tableNumber);
        public event ResultDelegate returnResult;
#pragma warning restore 1591


        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFDataProcessor()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Returns results for one or more tables stored in the specified DataSet.
        /// </summary>
        /// <param name="ds">DataSet object containing the data to return.</param>
        /// <remarks>Calling code must retrieve data by handling the returnResult event. </remarks>
        public void ProcessDataSet(DataSet ds)
        {
            int tabInx = 0;
            int maxTabInx = ds.Tables.Count - 1;

            for (tabInx = 0; tabInx <= maxTabInx; tabInx++)
            {
                ProcessDataTable(ds.Tables[tabInx], tabInx);
            }
        }

        /// <summary>
        /// Returns results via a callback function defined as a ResultDelegate.
        /// </summary>
        /// <param name="tab">DataTable object containing the data to return.</param>
        /// <remarks>Calling code must retrieve data by handling the returnResult event. </remarks>
        public void ProcessDataTable(DataTable tab)
        {
            ProcessDataTable(tab, (int)1);
        }

        private void ProcessDataTable(DataTable tab, int tableNumber)
        {
            int rowInx = 0;
            int maxRowInx = -1;
            DataColumnCollection cols = tab.Columns;

            maxRowInx = tab.Rows.Count - 1;
            for (rowInx = 0; rowInx <= maxRowInx; rowInx++)
            {
                DataRow row = tab.Rows[rowInx];
                if (returnResult != null)
                {
                    returnResult(cols, row, tableNumber);
                }
            }
        }

        /// <summary>
        /// Creates a DataTable object from the contents of an xml file.
        /// </summary>
        /// <param name="filePath">Location of xml file.</param>
        /// <returns>DataTable containing the Xml data.</returns>
        public DataTable LoadXmlFileToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            if (File.Exists(filePath))
            {
                dt.ReadXml(filePath);
            }
            return dt;
        }

        /// <summary>
        /// Creates a DataSet object from the contents of an xml file.
        /// </summary>
        /// <param name="filePath">Location of xml file.</param>
        /// <returns>Dataset containing the Xml data.</returns>
        public DataSet LoadXmlFileToDataSet(string filePath)
        {
            DataSet ds = new DataSet();
            if (File.Exists(filePath))
            {
                ds.ReadXml(filePath);
            }
            return ds;
        }

        /// <summary>
        /// Method to copy a DataTable object to a XmlDocument object.
        /// </summary>
        /// <param name="tab">DataTable object containing data to copy.</param>
        /// <returns>XmlDocument object containing the copied data.</returns>
        public XmlDocument CopyDataTableToXmlDocument(DataTable tab)
        {
            StringWriter writer = new StringWriter();
            if (String.IsNullOrEmpty(tab.TableName))
                tab.TableName = "Table";
            tab.WriteXml(writer, XmlWriteMode.WriteSchema);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(writer.ToString());
            return xmlDoc;
        }

        /// <summary>
        /// Method to copy a DataSet object to a XmlDocument object.
        /// </summary>
        /// <param name="ds">DataSet object containing data to copy.</param>
        /// <returns>XmlDocument object containing the copied data.</returns>
        public XmlDocument CopyDataSetToXmlDocument(DataSet ds)
        {
            StringWriter writer = new StringWriter();
            ds.WriteXml(writer, XmlWriteMode.WriteSchema);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(writer.ToString());
            return xmlDoc;
        }


    }//end class
}//end namespace
