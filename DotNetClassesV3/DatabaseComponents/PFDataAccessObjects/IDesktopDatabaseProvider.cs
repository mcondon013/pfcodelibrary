using System;
namespace PFDataAccessObjects
{
#pragma warning disable 1591
    public interface IDesktopDatabaseProvider
    {

        //properties
        PFDataAccessObjects.DatabasePlatform DbPlatform { get; }
        string ConnectionString { get; set; }
        System.Data.Common.DbConnection Connection { get; }
        System.Data.Common.DbCommand Command { get; }
        System.Data.CommandType CommandType { get; set; }
        bool IsConnected { get; }
        int CommandTimeout { get; set; }
        string SQLQuery { get; set; }
        PFCollectionsObjects.PFKeyValueList<string, string> ConnectionStringKeyVals { get; }
        string DatabasePath { get; set; }


        //string DatabaseName { get; set; }
        //string Username { get; set; }
        //string Password { get; set; }
        //string ServerName { get; set; }



        //methods
        void OpenConnection();
        void CloseConnection();

        bool CreateDatabase(string connectionString);
        bool CreateDatabase(string databasePath, string pathToTemplateDatabase);

        bool CreateTable(System.Data.DataTable dt);
        bool CreateTable(System.Data.DataTable dt, out string createScript);
        bool DropTable(string catalogName, string schemaName, string tableName);
        bool DropTable(string schemaName, string tableName);
        bool DropTable(string tableName);
        bool DropTable(PFTableDef td);
        bool TableExists(string catalogName, string schemaName, string tableName);
        bool TableExists(string schemaName, string tableName);
        bool TableExists(string tableName);
        bool TableExists(PFTableDef td);

        bool TypeIsUserTable(System.Data.DataRow dr);
        string GetFullTableName(System.Data.DataRow dr);
        TableNameQualifiers GetTableNameQualifiers(System.Data.DataRow dr);
        string RebuildFullTableName(PFTableDef tabDef, string newSchemaName);
        string BuildTableCreateStatement(System.Data.DataTable dt);

        PFCollectionsObjects.PFList<PFDataAccessObjects.PFTableDef> GetTableList();
        PFCollectionsObjects.PFList<PFDataAccessObjects.PFTableDef> GetTableList(string[] includePatterns, string[] excludePatterns);
        int CreateTablesFromTableDefs(PFCollectionsObjects.PFList<PFDataAccessObjects.PFTableDef> tableDefs);
        int CreateTablesFromTableDefs(PFCollectionsObjects.PFList<PFDataAccessObjects.PFTableDef> tableDefs, bool dropBeforeCreate);
        PFCollectionsObjects.PFList<PFDataAccessObjects.PFTableDef> ConvertTableDefs(PFCollectionsObjects.PFList<PFDataAccessObjects.PFTableDef> tableDefs, string newSchemaName);
        PFCollectionsObjects.PFList<PFDataAccessObjects.TableCopyDetails> CopyTableDataFromTableDefs(PFDataAccessObjects.PFDatabase sourceDatabase, string newSchemaName, bool dropBeforeCreate);
        PFCollectionsObjects.PFList<PFDataAccessObjects.TableCopyDetails> CopyTableDataFromTableDefs(PFDataAccessObjects.PFDatabase sourceDatabase, string[] includePatterns, string[] excludePatterns, string newSchemaName, bool dropBeforeCreate);

        void ImportDataFromDataTable(System.Data.DataTable dt);
        void ImportDataFromDataTable(System.Data.DataTable dt, int updateBatchSize);

        int RunNonQuery();
        int RunNonQuery(string sqlText, System.Data.CommandType pCommandType);
        System.Data.Common.DbDataReader RunQueryDataReader();
        System.Data.Common.DbDataReader RunQueryDataReader(string sqlQuery, System.Data.CommandType pCommandType);
        System.Data.DataSet RunQueryDataSet();
        System.Data.DataSet RunQueryDataSet(string sqlQuery, System.Data.CommandType pCommandType);
        System.Data.DataTable RunQueryDataTable();
        System.Data.DataTable RunQueryDataTable(string sqlQuery, System.Data.CommandType pCommandType);
        System.Data.DataTable GetQueryDataSchema();
        System.Data.DataTable GetQueryDataSchema(string sqlQuery, System.Data.CommandType pCommandType);

        System.Data.DataTable ConvertDataReaderToDataTable(System.Data.Common.DbDataReader rdr);
        System.Data.DataTable ConvertDataReaderToDataTable(System.Data.Common.DbDataReader rdr, string tableName);

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
#pragma warning restore 1591
}
