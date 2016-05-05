using System;
namespace PFDataAccessObjects
{
    public interface IDatabaseProvider
    {
       
        //properties
        PFDataProcessorObjects.DatabasePlatform DbPlatform { get; }
        string ConnectionString { get; set; }
        System.Data.Common.DbConnection Connection { get; }
        System.Data.Common.DbCommand Command { get; }
        System.Data.CommandType CommandType { get; set; }
        bool IsConnected { get; }
        string SQLQuery { get; set; }

        
        
        string DatabaseName { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string ServerName { get; set; }

        
       
        //methods
        void OpenConnection();
        void CloseConnection();

        
        bool CreateTable(System.Data.DataTable dt);
        bool CreateTable(System.Data.DataTable dt, out string createScript);
        bool DropTable(string schemaName, string tableName);
        bool DropTable(string tableName);
        bool TableExists(string schemaName, string tableName);
        bool TableExists(string tableName);

        PFCollectionsObjects.PFList<PFDataProcessorObjects.PFTableDef> GetTableList();
        int CreateTablesFromTableDefs(PFCollectionsObjects.PFList<PFDataProcessorObjects.PFTableDef> tableDefs);
        int CreateTablesFromTableDefs(PFCollectionsObjects.PFList<PFDataProcessorObjects.PFTableDef> tableDefs, bool dropBeforeCreate);
        PFCollectionsObjects.PFList<PFDataProcessorObjects.PFTableDef> ConvertTableDefs(PFCollectionsObjects.PFList<PFDataProcessorObjects.PFTableDef> tableDefs, string newSchemaName);
        int CopyTableDataFromTableDefs(PFCollectionsObjects.PFList<PFDataProcessorObjects.PFTableDef> tableDefs, string newSchemaName, bool dropBeforeCreate, IDatabaseProvider dbProvider);

        void ImportDataFromDataTable(System.Data.DataTable dt);

        int RunNonQuery();
        int RunNonQuery(string sqlText, System.Data.CommandType pCommandType);
        System.Data.Common.DbDataReader RunQueryDataReader();
        System.Data.Common.DbDataReader RunQueryDataReader(string sqlQuery, System.Data.CommandType pCommandType);
        System.Data.DataSet RunQueryDataSet();
        System.Data.DataSet RunQueryDataSet(string sqlQuery, System.Data.CommandType pCommandType);
        System.Data.DataTable RunQueryDataTable();
        System.Data.DataTable RunQueryDataTable(string sqlQuery, System.Data.CommandType pCommandType);

        void ProcessDataReader(System.Data.Common.DbDataReader rdr);
        void ProcessDataSet(System.Data.DataSet ds);
        void ProcessDataTable(System.Data.DataTable tab);

        
        void ExtractDelimitedDataFromDataReader(System.Data.Common.DbDataReader rdr, string columnSeparator, string lineTerminator, bool columnNamesOnFirstLine);
        void ExtractDelimitedDataFromDataSet(System.Data.DataSet ds, string columnSeparator, string lineTerminator, bool columnNamesOnFirstLine);
        void ExtractDelimitedDataFromTable(System.Data.DataTable tab, string columnSeparator, string lineTerminator, bool columnNamesOnFirstLine);
        void ExtractFixedLengthDataFromDataReader(System.Data.Common.DbDataReader rdr, bool lineTerminator, bool columnNamesOnFirstLine, bool allowDataTruncation);
        void ExtractFixedLengthDataFromDataSet(System.Data.DataSet ds, bool lineTerminator, bool columnNamesOnFirstLine, bool allowDataTruncation);
        void ExtractFixedLengthDataFromTable(System.Data.DataTable tab, bool lineTerminator, bool columnNamesOnFirstLine, bool allowDataTruncation);

        void SaveDataReaderToXmlFile(System.Data.Common.DbDataReader rdr, string filePath);
        void SaveDataReaderToXmlSchemaFile(System.Data.Common.DbDataReader rdr, string filePath);
        void SaveDataReaderWithSchemaToXmlFile(System.Data.Common.DbDataReader rdr, string filePath);
        void SaveDataSetToXmlFile(System.Data.DataSet ds, string filePath);
        void SaveDataSetToXmlSchemaFile(System.Data.DataSet ds, string filePath);
        void SaveDataSetWithSchemaToXmlFile(System.Data.DataSet ds, string filePath);
        void SaveDataTableToXmlFile(System.Data.DataTable dt, string filePath);
        void SaveDataTableToXmlSchemaFile(System.Data.DataTable dt, string filePath);
        void SaveDataTableWithSchemaToXmlFile(System.Data.DataTable dt, string filePath);

        System.Data.DataSet LoadXmlFileToDataSet(string filePath);
        System.Data.DataTable LoadXmlFileToDataTable(string filePath);

    }
}
