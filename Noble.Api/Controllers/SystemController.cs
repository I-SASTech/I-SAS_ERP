using Focus.Business.Extensions;
using Focus.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Queries.GetSyncRecordsList;
using Focus.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.Extensions.Logging;
using Noble.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : BaseController
    {
        private IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IApplicationDbContext _context;
        private readonly IUserHttpContextProvider _contextProvider;
        private readonly ILogger<SystemController> _logger;

        public SystemController(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IApplicationDbContext context, IUserHttpContextProvider contextProvider, ILogger<SystemController> logger)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _contextProvider = contextProvider;
            _logger = logger;
        }

        #region System

        [DisplayName("GetBackUpPath")]
        [Route("api/System/GetBackUpPath")]
        [HttpGet("GetBackUpPath")]
        [Roles("CanBackUpData", "CanRestoreData")]
        public async Task<IActionResult> GetBackUpPath()
        {
            var currentCompanyId = User.Identity.CompanyId();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd;

            string commandText = $@"select BackupPath from CompanyAccountSetups where CompanyId = '{currentCompanyId}'";
            var backupPath = "";

            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                //connection.InfoMessage += Connection_InfoMessage;
                cmd = new SqlCommand(
                    $@"select BackupPath from CompanyAccountSetups where CompanyId = '{currentCompanyId}'", connection);
                da.SelectCommand = (SqlCommand)cmd;

                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    backupPath = Convert.ToString(dr["BackupPath"]);
                }

                connection.Close();
            }

            return Ok(backupPath);
        }

        [DisplayName("SaveBackUpPath")]
        [Route("api/System/SaveBackUpPath")]
        [HttpGet("SaveBackUpPath")]
        public async Task<IActionResult> SaveBackUpPath(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new ApplicationException("Backup path is not found.");
                }


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                var currentCompanyId = User.Identity.CompanyId();
                SqlCommand cmd;

                using (SqlConnection connection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    cmd = new SqlCommand(
                        "update CompanyAccountSetups set BackupPath = '" + path + "' where CompanyId  = '" +
                        currentCompanyId + "';", connection);
                    await cmd.ExecuteNonQueryAsync();

                    connection.Close();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        [DisplayName("Region Code")]
        [Route("api/System/BackUp")]
        [HttpGet("BackUp")]
        [Roles("CanBackUpData")]
        public async Task<IActionResult> BackUp(string path, bool isNewPath)
        {
            try
            {
                if (isNewPath)
                {
                    await SaveBackUpPath(path);
                }

                var datetime = DateTime.UtcNow;

                var backupPath = Path.Combine(path,
                    "Backup_" + datetime.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss") + ".bak");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (SqlConnection connection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    string commandText =
                        $@"BACKUP DATABASE [{connection.Database}] TO DISK = N'{backupPath}' WITH NOFORMAT, INIT, NAME = N'{connection.Database}-Full Database Backup'"; //, SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                    connection.Open();
                    //connection.InfoMessage += Connection_InfoMessage;
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = commandText;
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

            return Ok();
        }

        [DisplayName("Region Code")]
        [Route("api/System/Restore")]
        [HttpGet("Restore")]
        [Roles("CanRestoreData")]
        public async Task<IActionResult> Restore(string fileName, string path)
        {
            var backupPath = Path.Combine(path, fileName);
            try
            {
                if (!Directory.Exists(path))
                {
                    throw new ApplicationException("File path is not correct.");
                }

                if (!System.IO.File.Exists(backupPath))
                {
                    throw new ApplicationException("File not found.");
                }

                using (SqlConnection connection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    string commandText = $@" alter database [{connection.Database}] 
                                    set offline with rollback immediate 
                                   USE Master RESTORE DATABASE [{connection.Database}] FROM DISK = N'{backupPath}'
                                      alter database [{connection.Database}]
                                    set online";

                    connection.Open();
                    if (connection.DataSource.Contains("139.99.130.23"))
                    {
                        throw new ApplicationException("Please set local connection string.");
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = commandText;
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();
                    }

                    connection.Close();
                }

                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
            }

        }

        [DisplayName("DropTrigger")]
        [Route("api/System/DropTrigger")]
        [HttpGet("DropTrigger")]
        //   [Authorize(Roles = "Nobel Admin")]
        public async Task<IActionResult> DropTrigger()
        {
            var delTrigger = new StringBuilder();
            delTrigger.Append("DECLARE @TriggerName NVARCHAR(128)\r\nDECLARE @TableName NVARCHAR(128)\r\nDECLARE @SchemaName NVARCHAR(128)\r\n\r\nDECLARE curTriggers CURSOR FOR\r\nSELECT \r\n    s.name AS SchemaName,\r\n    t.name AS TableName,\r\n    tr.name AS TriggerName\r\nFROM sys.triggers tr\r\nINNER JOIN sys.tables t ON tr.parent_id = t.object_id\r\nINNER JOIN sys.schemas s ON t.schema_id = s.schema_id\r\n\r\nOPEN curTriggers\r\nFETCH NEXT FROM curTriggers INTO @SchemaName, @TableName, @TriggerName\r\n\r\nWHILE @@FETCH_STATUS = 0\r\nBEGIN\r\n    DECLARE @Sql NVARCHAR(MAX)\r\n    SET @Sql = 'DROP TRIGGER ' + QUOTENAME(@SchemaName) + '.' + QUOTENAME(@TriggerName)\r\n    \r\n    EXEC sp_executesql @Sql\r\n    \r\n    FETCH NEXT FROM curTriggers INTO @SchemaName, @TableName, @TriggerName\r\nEND\r\n\r\nCLOSE curTriggers\r\nDEALLOCATE curTriggers");

            

            try
            {

                using (SqlConnection connection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    var queryString = delTrigger.ToString();
                    var command = new SqlCommand(queryString, connection);
                    connection.Open();
                    command.ExecuteScalar();

                    connection.Close();
                    return Ok("Success");

                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
           
        }

        [DisplayName("SyncSetup")]
        [Route("api/System/SyncSetup")]
        [HttpGet("SyncSetup")]
        //   [Authorize(Roles = "Nobel Admin")]
        public async Task SyncSetup(Guid companyId)
        {

            //var currentCompanyId = User.Identity.CompanyId();
            var currentCompanyId = companyId;

            //if (companyId != Guid.Empty)
            //{
            //    currentCompanyId = companyId.Value;
            //}
            
            var str = new StringBuilder();
            str.Append(
                
                " Create PROCEDURE CreateTriggers " +
                " AS " +
                " BEGIN " +
                " SET NOCOUNT ON; " +

                " declare @count Int = 0; " +
                " declare @max Int = (select count(*) from information_schema.tables); " +
                " declare @value varchar(50); " +

                " while @count < @max " +
                " begin " +

                " set @value = (select table_name from " +
                " (select (row_number() over (order by (select null))) [index] , table_name from information_schema.tables where table_name <> 'SyncRecords'  and table_name <> '__EFMigrationsHistory' and table_name <> 'NoblePermissions') r " +
                " order by r.[index] offset @count " +
                " rows fetch next 1 rows only); " +

                " DECLARE @TriggerCode NVARCHAR(max); " +
                " DECLARE @Update NVARCHAR(max) = 'Update'; " +
                " DECLARE @Insert NVARCHAR(max) = 'Insert'; " +
                " DECLARE @Delete NVARCHAR(max) = 'Delete'; " +
                " DECLARE @rows NVARCHAR(max) = '0 Rows'; " +
                " Declare @companyId Nvarchar(50) = '" + currentCompanyId + "' " +
                " declare @ColumnName nvarchar(50); " +
                " declare @ColumnType nvarchar(50); " +
                " SELECT @ColumnName = sc.name " +
                " FROM sys.columns sc JOIN sys.tables t ON sc.object_id = t.object_id " +
                " left Join INFORMATION_SCHEMA.COLUMNS isc ON sc.name = isc.Column_NAME " +
                " WHERE t.name = @value  and sc.column_id = 1 " +
                " SELECT @ColumnType = Data_Type FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @value AND COLUMN_NAME = @ColumnName " +


                " declare @list varchar(1000) = 'ModulesNames, AspNetUserRoles , AspNetUserClaims, AccountTemplates, AspNetRoles, Companies, NobleModules, AspNetRoleClaims, AspNetUserLogins, AspNetUserTokens, ModulesRights, NoblePermissions '; " +

                " declare @IsGeneralTable varchar(6) = 'False'; " +

                " IF Exists(SELECT* FROM string_split(@list, ',') t where t.value like '%' + @value + '%') " +
                " set @IsGeneralTable = 'True'; " +

                " If  @IsGeneralTable = 'True' " +
                " begin " +

                " SET @TriggerCode = 'Create TRIGGER AUDIT_' + @value + '  " +
                " ON ' +@value+ ' " +
                " AFTER INSERT , UPDATE , Delete " +
                " as " +
                " BEGIN " +
                " SET NOCOUNT ON; " +
                " DECLARE @action char(6) " +
                " SET @action = " +
                " CASE " +
                " WHEN EXISTS(SELECT 1 FROM inserted) AND EXISTS(SELECT 1 FROM deleted) THEN '''+@Update+'''" +
                " WHEN EXISTS(SELECT 1 FROM inserted) THEN '''+@Insert+''' " +
                " WHEN EXISTS(SELECT 1 FROM deleted) THEN '''+@Delete+'''" +
                " ELSE '''+@rows+''' " +
                " END " +

                "  DECLARE @IdTable TABLE('+@ColumnName+' nvarchar(100)); " +
                " IF @action = '''+@Insert+'''  or @action = '''+@Update+''' " +
                "  begin" +
                "  INSERT INTO @IdTable('+@ColumnName+') " +

                " select '+@ColumnName+' from inserted; " +
                " End " +
                " ELSE " +
                "  begin" +
                "  INSERT INTO @IdTable('+@ColumnName+') " +

                " select '+@ColumnName+' from deleted; " +
                " End " +
                "  declare @count Int = 0; " +
                "  declare @max Int = (select count(*) from @IdTable); " +
                " declare @id varchar(50); " +
                " while @count < @max " +
                " begin " +
                " set @id = (select  '+@ColumnName+'  from " +
                " (select (row_number() over (order by (select null))) [index] ,  '+@ColumnName+'  from @IdTable) r " +
                " order by r.[index] offset @count " +
                " rows fetch next 1 rows only); " +
                " Print ('''') " +
                " set @count = @count + 1 " +
                " IF @action = '''+@Insert+''' " +
                " INSERT INTO SyncRecords(Action, ChangeDate, [Table] , ColumnId, ColumnType, CompanyId,ColumnName ) " +
                " SELECT " +
                " Action = @action, " +
                " ChangeDate = GETDATE(), " +
                " [Table] = '''+@value+''', " +
                " ColumnId = @id, " +
                " ColumnType = '''+@ColumnType+''', " +
                " CompanyId = '''+@companyId+''', " +
                " ColumnName = '''+@ColumnName+''' " +

                " FROM @IdTable  where '+@ColumnName+' = @id" +
                " ELSE " +
                " begin " +
                " INSERT INTO SyncRecords(Action, ChangeDate, [Table], ColumnId, ColumnType, CompanyId,ColumnName) " +
                " SELECT " +
                " Action = @action, " +
                " ChangeDate = GETDATE(), " +
                " [Table] = '''+@value+''', " +
                " ColumnId =@id, " +
                " ColumnType = '''+@ColumnType+''', " +
                " CompanyId = '''+@companyId+''', " +
                " ColumnName = '''+@ColumnName+''' " +

                " FROM @IdTable where '+@ColumnName+' = @id" +
                " end " +
                " end " +
                " END'; " +
                " if exists(select null from sys.triggers where Name = 'AUDIT_' + @value) " +
                " print 'exist' " +
                " else " +
                " EXEC(@TriggerCode); " +

                " end " +

                " else " +

                " begin " +

                " SET @TriggerCode = 'Create TRIGGER AUDIT_' + @value + '  " +
                " ON ' +@value+ ' " +
                " AFTER INSERT , UPDATE, Delete " +
                " as " +
                " BEGIN " +
                " SET NOCOUNT ON; " +
                " DECLARE @action char(6) " +
                " SET @action = " +
                " CASE " +
                " WHEN EXISTS(SELECT 1 FROM inserted) AND EXISTS(SELECT 1 FROM deleted) THEN '''+@Update+'''" +
                " WHEN EXISTS(SELECT 1 FROM inserted) THEN '''+@Insert+''' " +
                " WHEN EXISTS(SELECT 1 FROM deleted) THEN '''+@Delete+'''" +
                " ELSE '''+@rows+''' " +
                " END " +

                "  DECLARE @IdTable TABLE('+@ColumnName+' nvarchar(100), CompanyId nvarchar(50)); " +
                " IF @action = '''+@Insert+'''  or  @action = '''+@Update+''' " +
                "  begin" +
                "  INSERT INTO @IdTable('+@ColumnName+', CompanyId) " +

                " select '+@ColumnName+', CompanyId from inserted; " +
                " End " +
                " ELSE " +
                "  begin" +
                "  INSERT INTO @IdTable('+@ColumnName+', CompanyId) " +

                " select '+@ColumnName+', CompanyId from deleted; " +
                " End " +
                "  declare @count Int = 0; " +
                "  declare @max Int = (select count(*) from @IdTable); " +
                " declare @id varchar(50); " +
                " while @count < @max " +
                " begin " +
                " set @id = (select  '+@ColumnName+'  from " +
                " (select (row_number() over (order by (select null))) [index] ,  '+@ColumnName+'  from @IdTable) r " +
                " order by r.[index] offset @count " +
                " rows fetch next 1 rows only); " +
                " Print ('''') " +
                " set @count = @count + 1 " +
                " IF (@action = '''+@Insert+''' and (select Count(ColumnId) from SyncRecords where ColumnId =  @id) = 0) " +
                " INSERT INTO SyncRecords(Action, ChangeDate, [Table] , ColumnId, ColumnType, CompanyId,ColumnName, IsGeneral ) " +
                " SELECT " +
                " Action = @action, " +
                " ChangeDate = GETDATE(), " +
                " [Table] = '''+@value+''', " +
                " ColumnId = @id, " +
                " ColumnType = '''+@ColumnType+''', " +
                " CompanyId = (select CompanyId from @IdTable  where '+@ColumnName+' = @id), " +
                " ColumnName = '''+@ColumnName+''', " +
                " IsGeneral = 1 " +

                " FROM @IdTable  where '+@ColumnName+' = @id" +
                " ELSE " +
                " begin " +
                " INSERT INTO SyncRecords(Action, ChangeDate, [Table], ColumnId, ColumnType, CompanyId,ColumnName, IsGeneral) " +
                " SELECT " +
                " Action = @action, " +
                " ChangeDate = GETDATE(), " +
                " [Table] = '''+@value+''', " +
                " ColumnId =@id, " +
                " ColumnType = '''+@ColumnType+''', " +
                " CompanyId =  (select CompanyId from @IdTable  where '+@ColumnName+' = @id), " +
                " ColumnName = '''+@ColumnName+''', " +
                " IsGeneral = 1 " +

                " FROM @IdTable where '+@ColumnName+' = @id" +
                " end " +
                " end " +
                " END'; " +
                " if exists(select null from sys.triggers where Name = 'AUDIT_' + @value) " +
                " print 'exist' " +
                " else " +
                " EXEC(@TriggerCode); " +

                " end " +






                "  set @count = @count + 1; " +
                " end " +
                " end "
            );

            try
            {

                using (SqlConnection connection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    string queryString = str.ToString();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                   
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0}, {1}",
                                reader[0], reader[1]));
                        }
                    }

                    connection.Close();

                    var cmd = new SqlCommand("CreateTriggers")
                    { CommandType = CommandType.StoredProcedure, CommandTimeout = 0 };
                    var cnx = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    cmd.Connection = cnx;
                    cnx.Open();

                    var result = await cmd.ExecuteNonQueryAsync();
                    cnx.Close();
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
            finally
            {
                using (SqlConnection connection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    string queryString =
                        "SELECT  count(*)  FROM sys.objects WHERE   object_id = OBJECT_ID(N'CreateTriggers')  AND type IN(N'P', N'PC')";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    var exists = (int)command.ExecuteScalar() > 0;

                    if (exists)
                    {
                        command.CommandText = "DROP PROCEDURE CreateTriggers";

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(String.Format("{0}, {1}",
                                    reader[0], reader[1]));
                            }
                        }
                    }
                }
            }
        }

        [DisplayName("PushDataRecord")]
        [Route("api/System/PushDataRecord")]
        [HttpGet("PushDataRecord")]
        [Roles("CanPushRecord")]
        public async Task PushDataRecord()
        {
            // Start a local transaction.
            if (string.IsNullOrEmpty(Configuration.GetConnectionString("ServerConnection")))
                return;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlDataAdapter da1 = new SqlDataAdapter();

            SqlCommand cmd;
            SqlCommand cmd1;

            bool isExist = false;

            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                //SqlTransaction transaction;

                //transaction = connection.BeginTransaction();
                
                cmd = new SqlCommand(
                    "SELECT top 100 * FROM SyncRecords where Pull = 0 and Push = 0 order by ChangeDate;", connection);
                try
                {


                    da.SelectCommand = (SqlCommand)cmd;

                    da.Fill(ds);

                    var records = new List<SyncRecord>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        records.Add(new SyncRecord
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Action = Convert.ToString(dr["Action"]),
                            ColumnId = Convert.ToString(dr["ColumnId"]),
                            Table = Convert.ToString(dr["Table"]),
                            ColumnType = Convert.ToString(dr["ColumnType"]),
                            CompanyId = Guid.Parse(Convert.ToString(dr["CompanyId"])),
                            PushDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss"),
                            ChangeDate = Convert.ToDateTime(dr["ChangeDate"]),
                            ColumnName = Convert.ToString(dr["ColumnName"])

                        });
                    }

                    if (records.Count <= 0)
                        return;

                    var queryServer = "";
                    var queryServerSyncRecord = "";
                    var queryDefault = "";
                    using (SqlConnection connection1 =
                        new SqlConnection(Configuration.GetConnectionString("ServerConnection")))
                    {
                        //var firstHundredRecord = records.Count > 100 ? records.Take(100).ToList() : records;

                        connection1.Open();
                        foreach (var item in records)
                        {
                            DataSet ds1 = new DataSet();

                            cmd1 = new SqlCommand("select * from " + item.Table + " where " + item.ColumnName + " = '" + item.ColumnId + "'", connection);

                            da1.SelectCommand = (SqlCommand)cmd1;

                            da1.Fill(ds1);
                            if (ds1.Tables[0].Rows.Count <= 0)
                            {

                                var date = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
                                if (item.Action != "Delete")
                                {
                                    queryDefault += "Update SyncRecords set Push = 1,Pull=1, PushDate = '" + date +
                                                    "' where Id =" + item.Id + "; ";
                                    continue;
                                }

                                queryServer += "if (select count(*) from " + item.Table + " where  " + item.ColumnName +
                                               " = '" + item.ColumnId + "') = 1" +
                                               " begin " +
                                               " delete from " + item.Table + "  where " + item.ColumnName + " = '" +
                                               item.ColumnId + "'; end ";

                                queryServerSyncRecord +=
                                     " Update SyncRecords set Push = 1, Pull = 1, PushDate = '" + date +
                                                "' where ColumnId = '" + item.ColumnId + "'; ";
                                queryDefault += "Update SyncRecords set Push = 1, PushDate = '" + date +
                                                "' where Id =" + item.Id + "; ";
                                continue;


                            }
                            var count = ds1.Tables[0].Rows[0].ItemArray.Count();
                            var countStart = 0;
                            var colVal = "";
                            foreach (var i in ds1.Tables[0].Rows[0].ItemArray)
                            {
                                //if (ds1.Tables[0].Columns[countStart].DataType.Name
                                if (countStart == 0 && item.ColumnType == "int")
                                {
                                    countStart++;
                                    continue;
                                }

                                colVal += i is DateTime
                                    ? "'" + Convert.ToDateTime(i).ToString("yyyy-MM-ddThh:mm:ss") + "'"
                                    : "'" + i + "'";
                                if (countStart < count - 1)
                                    colVal += ",";
                                countStart++;
                            }

                            count = ds1.Tables[0].Columns.Count;
                            countStart = 0;
                            var colName = "";
                            foreach (var c in ds1.Tables[0].Columns)
                            {
                                if (countStart == 0 && item.ColumnType == "int")
                                {
                                    countStart++;
                                    continue;
                                }

                                colName += countStart == count - 1 ? "[" + c + "]" : "[" + c + "],";

                                countStart++;
                            }

                            var updateCol = "";
                            countStart = 0;
                            foreach (var c in ds1.Tables[0].Columns)
                            {
                                if (countStart == 0)
                                {
                                    countStart++;
                                    continue;
                                }
                                if (ds1.Tables[0].Rows[0].ItemArray[countStart] is DateTime)
                                {
                                    updateCol += "[" + c + "]" + " = '" + Convert.ToDateTime(ds1.Tables[0].Rows[0].ItemArray[countStart]).ToString("yyyy-MM-ddThh:mm:ss") + "'";
                                }
                                else if (ds1.Tables[0].Columns[countStart].DataType.Name == "String" && ds1.Tables[0].Rows[0].ItemArray[countStart].ToString() == "")
                                {
                                    updateCol += "[" + c + "]" + " = ''";
                                }
                                else if (ds1.Tables[0].Columns[countStart].DataType.Name == "Guid" && ds1.Tables[0].Rows[0].ItemArray[countStart].ToString() == "")
                                {
                                    updateCol += "[" + c + "]" + " = NULL";
                                }
                                else
                                {
                                    updateCol += "[" + c + "]" + " = '" + ds1.Tables[0].Rows[0].ItemArray[countStart] + "'";
                                }
                                if (countStart < count - 1)
                                    updateCol += ",";
                                countStart++;
                            }

                            //Push Record to Server

                            if (item.Action == "Insert")
                            {
                                isExist = false;

                                cmd =
                                    new SqlCommand("select case when exists(select null  from " + item.Table + " where  " + item.ColumnName + "= '" + item.ColumnId + "') then 1 else 0 end;"
                                        , connection1);

                                isExist = (int)cmd.ExecuteScalar() == 1;
                                if (isExist)
                                {
                                    // var newCol = "";
                                    if (item.Table != "AspNetUserClaims")
                                        updateCol = updateCol.Replace("''", "NULL");

                                    if (item.ColumnType == "int")
                                    {
                                        queryServer += "update " + item.Table + " set " + updateCol + " where " +
                                                item.ColumnName + "= " + item.ColumnId + "; ";

                                    }
                                    else
                                    {
                                        queryServer += "update " + item.Table + " set " + updateCol + " where " +
                                                 item.ColumnName + "= '" + item.ColumnId + "'; ";

                                    }
                                }
                                else
                                {
                                    if (item.Table != "AspNetUserClaims")
                                        colVal = colVal.Replace("''", "NULL");
                                    queryServer += "Insert into " + item.Table + "(" + colName + ") values(" + colVal + "); ";

                                    colName = "";
                                }


                            }
                            else if (item.Action == "Update")
                            {
                                if (item.Table != "AspNetUserClaims")
                                    updateCol = updateCol.Replace("''", "NULL");

                                queryServer += "update " + item.Table + " set " + updateCol + " where " + item.ColumnName +
                                         "= '" + item.ColumnId + "'; ";


                            }
                            else
                            {
                                var isSoftDelete = false;
                                foreach (var c in ds1.Tables[0].Columns)
                                {
                                    if (c.ToString() == "IsDeleted")
                                    {
                                        isSoftDelete = true;
                                    }
                                }

                                await using SqlConnection connection11 = new SqlConnection(Configuration.GetConnectionString("ServerConnection"));
                                if (isSoftDelete)
                                {
                                    connection11.Open();
                                    cmd = new SqlCommand("update " + item.Table + " set " + updateCol + " where " + item.ColumnName + "= '" + item.ColumnId + "';", connection11);

                                    cmd.ExecuteNonQuery();
                                    connection11.Close();
                                }
                                else
                                {
                                    connection11.Open();
                                    cmd = new SqlCommand("DELETE from " + item.Table + " where " + item.ColumnName + "= '" + item.ColumnId + "';", connection11);

                                    cmd.ExecuteNonQuery();
                                    connection11.Close();
                                }
                            }

                            var pushDate = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
                            queryServerSyncRecord +=
                               "Update SyncRecords set Push = 1, Pull = 1, PushDate = '" + pushDate +
                                                "' where ColumnId = '" + item.ColumnId + "'; ";
                            queryDefault += "Update SyncRecords set Push = 1, PushDate = '" + pushDate +
                                            "' where Id =" + item.Id + "; ";


                            //cmd.Transaction = transaction;

                            //cmd.ExecuteNonQuery();

                        }

                        if (!string.IsNullOrEmpty(queryServer) && !string.IsNullOrEmpty(queryDefault))
                        {

                            cmd = new SqlCommand(queryServer, connection1);
                            cmd.ExecuteNonQuery();


                            if (!string.IsNullOrEmpty(Configuration.GetConnectionString("BackupConnection")))
                            {
                                using (SqlConnection connection2 = new SqlConnection(Configuration.GetConnectionString("BackupConnection")))
                                {
                                    connection2.Open();
                                    cmd =
                                        new SqlCommand(queryServer, connection2);

                                    cmd.ExecuteNonQuery();
                                    connection2.Close();
                                }
                            }
                            cmd =
                                new SqlCommand(queryDefault, connection);
                            cmd.ExecuteNonQuery();
                            cmd =
                                new SqlCommand(queryServerSyncRecord, connection1);
                            cmd.ExecuteNonQuery();

                            //records.RemoveRange(0, firstHundredRecord.Count);
                        }
                        connection1.Close();
                    }
                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

            //Recursive Calling 
            await PushDataRecord();

        }

        [DisplayName("PullData")]
        [Route("api/System/PullData")]
        [HttpGet("PullData")]
        [Roles("CanPullRecord")]
        public async Task<IActionResult> PullData(string path)
        {
            // Start a local transaction.

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlDataAdapter da1 = new SqlDataAdapter();

            SqlCommand cmd;
            SqlCommand cmd1;

            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("ServerConnection")))
            {
                connection.Open();
                //SqlTransaction transaction;

                //transaction = connection.BeginTransaction();
                cmd = new SqlCommand("SELECT * FROM SyncRecords where Pull = 0 and push = 0;", connection);

                try
                {

                    da.SelectCommand = (SqlCommand)cmd;

                    da.Fill(ds);

                    var records = new List<SyncRecord>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        records.Add(new SyncRecord
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Action = Convert.ToString(dr["Action"]),
                            ColumnId = Convert.ToString(dr["ColumnId"]),
                            Table = Convert.ToString(dr["Table"]),
                            ColumnType = Convert.ToString(dr["ColumnType"]),
                            CompanyId = Guid.Parse(Convert.ToString(dr["CompanyId"])),
                            PushDate = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss"),
                            ChangeDate = Convert.ToDateTime(dr["ChangeDate"]),
                            ColumnName = Convert.ToString(dr["ColumnName"])

                        });
                    }

                    var countRecord = 0;
                    foreach (var item in records.OrderBy(x => x.ChangeDate))
                    {
                        DataSet ds1 = new DataSet();

                        cmd1 = new SqlCommand("select * from " + item.Table + " where " + item.ColumnName + " = '" + item.ColumnId + "'", connection);

                        da1.SelectCommand = (SqlCommand)cmd1;

                        da1.Fill(ds1);




                        if (ds1.Tables[0].Rows.Count <= 0)
                        {

                            var date = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");
                            if (item.Action != "Delete")
                            {
                                var queryS = "Update SyncRecords set Push = 1,Pull=1, PushDate = '" + date +
                                                "' where Id =" + item.Id + "; ";
                                cmd =
                                    new SqlCommand(queryS, connection);
                                //cmd.Transaction = transaction;

                                cmd.ExecuteNonQuery();
                                
                                continue;
                            }

                            var query = "";
                            query= "if (select count(*) from " + item.Table + " where  " + item.ColumnName +
                                           " = '" + item.ColumnId + "') = 1" +
                                           " begin " +
                                           " delete from " + item.Table + "  where " + item.ColumnName + " = '" +
                                           item.ColumnId + "'; end ";

                            
                            using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                            {
                                connection1.Open();
                                cmd =
                                    new SqlCommand(query, connection1);

                                cmd.ExecuteScalar();


                                query =
                                  "Update SyncRecords set Push = 1, Pull = 1, PullDate = '" + date +
                                               "' where ColumnId = '" + item.ColumnId + "'; ";

                                cmd =
                                    new SqlCommand(query, connection1);

                                cmd.ExecuteScalar();
                                connection1.Close();
                            }
                            
                            query = "Update SyncRecords set Push = 1, PushDate = '" + date + "' where Id =" + item.Id + "; ";
                            cmd = new SqlCommand(query, connection);
                            //cmd.Transaction = transaction;

                            cmd.ExecuteNonQuery();
                            continue;


                        }


                        var count = ds1.Tables[0].Rows[0].ItemArray.Count();
                        var countStart = 0;
                        var colVal = "";
                        foreach (var i in ds1.Tables[0].Rows[0].ItemArray)
                        {
                            //if (ds1.Tables[0].Columns[countStart].DataType.Name
                            if (countStart == 0)
                            {
                                if (item.ColumnType == "int")
                                {
                                    countStart++;
                                    continue;
                                }
                            }

                            if (countStart == count - 1)
                            {
                                if (i is DateTime)
                                {
                                    colVal += "'" + Convert.ToDateTime(i).ToString("yyyy-MM-ddThh:mm:ss") + "'";
                                }

                                else
                                {
                                    colVal += "'" + i + "'";
                                }
                            }
                            else
                            {
                                if (i is DateTime)
                                {
                                    colVal += "'" + Convert.ToDateTime(i).ToString("yyyy-MM-ddThh:mm:ss") + "', ";
                                }
                                else
                                {
                                    colVal += "'" + i + "', ";
                                }
                            }

                            countStart++;
                        }

                        count = ds1.Tables[0].Columns.Count;
                        countStart = 0;
                        var colName = "";
                        foreach (var c in ds1.Tables[0].Columns)
                        {
                            if (countStart == 0)
                            {
                                if (item.ColumnType == "int")
                                {
                                    countStart++;
                                    continue;
                                }
                            }

                            if (countStart == count - 1)
                            {
                                colName += c;
                            }
                            else
                            {
                                colName += "[" + c + "],";

                            }

                            countStart++;
                        }

                        var updateCol = "";
                        countStart = 0;
                        foreach (var c in ds1.Tables[0].Columns)
                        {
                            if (countStart == 0)
                            {
                                countStart++;
                                continue;
                            }

                            if (countStart == count - 1)
                            {
                                if (ds1.Tables[0].Rows[0].ItemArray[countStart] is DateTime)
                                {
                                    updateCol += "[" + c + "]" + " = '" +
                                                 Convert.ToDateTime(ds1.Tables[0].Rows[0].ItemArray[countStart])
                                                     .ToString("yyyy-MM-ddThh:mm:ss") + "'";
                                }
                                else if (ds1.Tables[0].Columns[countStart].DataType.Name == "String" &&
                                         ds1.Tables[0].Rows[0].ItemArray[countStart].ToString() == "")
                                {
                                    updateCol += "[" + c + "]" + " = ''";
                                }
                                else if (ds1.Tables[0].Columns[countStart].DataType.Name == "Guid" &&
                                         ds1.Tables[0].Rows[0].ItemArray[countStart].ToString() == "")
                                {
                                    updateCol += "[" + c + "]" + " = NULL";
                                }
                                else
                                {
                                    updateCol += "[" + c + "]" + " = '" + ds1.Tables[0].Rows[0].ItemArray[countStart] + "'";
                                }
                            }
                            else
                            {
                                if (ds1.Tables[0].Rows[0].ItemArray[countStart] is DateTime)
                                {
                                    updateCol += "[" + c + "]" + " = '" +
                                                 Convert.ToDateTime(ds1.Tables[0].Rows[0].ItemArray[countStart])
                                                     .ToString("yyyy-MM-ddThh:mm:ss") + "',";
                                }
                                else if (ds1.Tables[0].Columns[countStart].DataType.Name == "String" &&
                                         ds1.Tables[0].Rows[0].ItemArray[countStart].ToString() == "")
                                {
                                    updateCol += "[" + c + "]" + " = '',";
                                }
                                else if (ds1.Tables[0].Columns[countStart].DataType.Name == "Guid" &&
                                         ds1.Tables[0].Rows[0].ItemArray[countStart].ToString() == "")
                                {
                                    updateCol += "[" + c + "]" + " = NULL,";
                                }
                                else
                                {
                                    updateCol += "[" + c + "]" + " = '" + ds1.Tables[0].Rows[0].ItemArray[countStart] + "',";
                                }
                            }

                            countStart++;
                        }

                        //var disable = "DISABLE TRIGGER AUDIT_" + item.Table + " ON " + item.Table + "; ";
                        //var enable = " ENABLE  TRIGGER AUDIT_" + item.Table + " ON " + item.Table + "; ";

                        if (item.Action == "Insert")
                        {
                            bool isExist = false;

                            using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                            {
                                var newCol = colVal.Replace("''", "NULL");
                                connection1.Open();
                                cmd =
                                    new SqlCommand( " select case when exists(select null  from " +
                                                   item.Table + " where  " + item.ColumnName + "= '" + item.ColumnId +
                                                   "') then 1 else 0 end;"
                                        , connection1);

                                isExist = (int)cmd.ExecuteScalar() == 1;
                                connection1.Close();
                            }

                            if (isExist)
                            {
                                using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                                {
                                    if (item.Table != "AspNetUserClaims")
                                        updateCol = updateCol.Replace("''", "NULL");
                                    

                                    connection1.Open();
                                    if (item.ColumnType == "int")
                                    {
                                        cmd =
                                            new SqlCommand( "update " + item.Table + " set " + updateCol +
                                                           " where " + item.ColumnName + "= " + item.ColumnId + ";"
                                                           
                                                , connection1);
                                    }
                                    else
                                    {
                                        cmd =
                                            new SqlCommand( "update " + item.Table + " set " + updateCol +
                                                           " where " + item.ColumnName + "= '" + item.ColumnId + "';"
                                                , connection1);
                                    }

                                    var dd = cmd.ExecuteNonQuery();
                                    connection1.Close();
                                }
                            }
                            else
                            {
                                using (SqlConnection connection1 =
                                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                                {
                                    if (item.Table != "AspNetUserClaims")
                                        colVal = colVal.Replace("''", "NULL");
                                    connection1.Open();
                                    cmd =
                                        new SqlCommand( "Insert into " + item.Table + "(" + colName +
                                                       ") values(" + colVal + ");"
                                            , connection1);

                                    var dd = cmd.ExecuteNonQuery();
                                    connection1.Close();
                                    colName = "";
                                }
                            }
                        }
                        else if (item.Action == "Update")
                        {

                            using (SqlConnection connection1 =
                                new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                            {
                                if (item.Table != "AspNetUserClaims")
                                    updateCol = updateCol.Replace("''", "NULL");

                                connection1.Open();
                                cmd =
                                    new SqlCommand( "update " + item.Table + " set " + updateCol + " where " + item.ColumnName + "= '" + item.ColumnId + "';", connection1);

                                var dd = cmd.ExecuteNonQuery();
                                connection1.Close();
                            }

                        }
                        else
                        {
                            throw new ApplicationException("ex");
                        }

                        var pullDate = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");

                        using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                        {
                            connection1.Open();
                            cmd =
                                new SqlCommand(
                                    "Update SyncRecords set Push = 1, Pull = 1, PullDate = '" + pullDate +
                                                "' where ColumnId = '" + item.ColumnId + "'; "
                                    , connection1);

                            var dd = cmd.ExecuteNonQuery();
                            connection1.Close();

                        }

                        cmd =
                            new SqlCommand("Update SyncRecords set Pull = 1, PullDate = '" + pullDate + "' where Id =" +
                                           item.Id + ";"
                                , connection);
                        //cmd.Transaction = transaction;

                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                    //transaction.Commit();
                }

                catch (Exception ex)
                {
                    //transaction.Rollback();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

            return Ok();


        }

        [Route("api/System/GetPushRecordsInformation")]
        [HttpGet("GetPushRecordsInformation")]
        [Roles("CanPushRecord")]
        public async Task<IActionResult> GetPushRecordsInformation(string status, int? pageNumber, int pageSize)
        {
            var currentCompanyId = User.Identity.CompanyId();
            var page = pageSize <= 20 ? 20 : pageSize;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var categoryListModel = await Mediator.Send(new GetSyncRecordListQuery
            {
                PageNumber = pageNumber ?? 1,
                Status = status ?? "All",
                PageSize = page,
                IsPush = true,
                CompanyId = currentCompanyId
            });
            ;
            return Ok(categoryListModel);
        }

        [DisplayName("Save Auto Sync")]
        [Route("api/System/SaveAutoSync")]
        [HttpGet("SaveAutoSync")]
        [Roles("CanPushRecord")]
        public async Task<IActionResult> SaveAutoSync(bool isSync)
        {
            try
            {
                var currentCompanyId = User.Identity.CompanyId();
                SqlCommand cmd;

                using (SqlConnection connection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    cmd = new SqlCommand(
                        "update CompanyAccountSetups set AutoSync = '" + isSync + "' where CompanyId  = '" +
                        currentCompanyId + "';", connection);
                    await cmd.ExecuteNonQueryAsync();

                    connection.Close();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        [DisplayName("Save Auto Sync")]
        [Route("api/System/GetAutoSync")]
        [HttpGet("GetAutoSync")]
        [Roles("CanPushRecord")]
        public async Task<IActionResult> GetAutoSync()
        {
            try
            {
                var currentCompanyId = User.Identity.CompanyId();
                var status = false;
                using (SqlConnection connection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    status = await connection.ExecuteScalarAsync<bool>(
                        "SELECT AutoSync FROM CompanyAccountSetups WHERE CompanyId = '" + currentCompanyId + "'");

                    connection.Close();
                }

                return Ok(status);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        [Route("api/System/GetPullRecordsInformation")]
        [HttpGet("GetPullRecordsInformation")]
        [Roles("CanPullRecord")]
        public async Task<IActionResult> GetPullRecordsInformation(string status, int? pageNumber, int pageSize)
        {
            var currentCompanyId = User.Identity.CompanyId();
            var page = pageSize <= 20 ? 20 : pageSize;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            var categoryListModel = await Mediator.Send(new GetSyncRecordListQuery
            {
                PageNumber = pageNumber ?? 1,
                Status = status ?? "All",
                PageSize = page,
                IsPush = false,
                CompanyId = currentCompanyId
            });
            return Ok(categoryListModel);
        }


        [DisplayName("Flush Records")]
        [Route("api/System/FlushRecords")]
        [HttpGet("FlushRecords")]
        //[Roles("CanFlushDatabase", "CanResetDatabase")]
        public async Task<IActionResult> FlushRecords(string records)
        {
            try
            {
                
                var delTrigger = new StringBuilder();
                 delTrigger.Append("DECLARE @TriggerName NVARCHAR(128)\r\nDECLARE @TableName NVARCHAR(128)\r\nDECLARE @SchemaName NVARCHAR(128)\r\n\r\nDECLARE curTriggers CURSOR FOR\r\nSELECT \r\n    s.name AS SchemaName,\r\n    t.name AS TableName,\r\n    tr.name AS TriggerName\r\nFROM sys.triggers tr\r\nINNER JOIN sys.tables t ON tr.parent_id = t.object_id\r\nINNER JOIN sys.schemas s ON t.schema_id = s.schema_id\r\n\r\nOPEN curTriggers\r\nFETCH NEXT FROM curTriggers INTO @SchemaName, @TableName, @TriggerName\r\n\r\nWHILE @@FETCH_STATUS = 0\r\nBEGIN\r\n    DECLARE @Sql NVARCHAR(MAX)\r\n    SET @Sql = 'DROP TRIGGER ' + QUOTENAME(@SchemaName) + '.' + QUOTENAME(@TriggerName)\r\n    \r\n    EXEC sp_executesql @Sql\r\n    \r\n    FETCH NEXT FROM curTriggers INTO @SchemaName, @TableName, @TriggerName\r\nEND\r\n\r\nCLOSE curTriggers\r\nDEALLOCATE curTriggers");


                

                var currentCompanyId = User.Identity.CompanyId();
                SqlCommand cmd;

                using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {

                    connection.Open();

                    var queryString = delTrigger.ToString();
                    var command = new SqlCommand(queryString, connection);
                    command.ExecuteScalar();

                    var dbName = connection.Database;
                    var categoryList = _context.Categories.AsNoTracking().ToList();
                    var employeeRegistrationList = _context.EmployeeRegistrations.AsNoTracking().ToList();
                    var bankList = _context.Banks.AsNoTracking().ToList();
                    var contactList = _context.Contacts.AsNoTracking().ToList();
                    var nobleRoleList = _context.NobleRoles.Where(x => x.Name != "Admin").ToList();
                    var inventoryBlindList = _context.InventoryBlinds.AsNoTracking().Include(x => x.InventoryBlindDetails)
                        .ToList();

                    var companyId = _contextProvider.GetCompanyId();
                    var allTableName = "SELECT TABLE_NAME FROM [" + dbName + "].information_schema.tables;";

                    var notRemoveAbleList = new List<string>()
                    {
                        "LoginPermissions", "NobleModules", "NoblePermissions", "NobleRoles","NobleUserRoles",
                        "__EFMigrationsHistory", "AccountTemplates", "AspNetRoleClaims", "AspNetRoles",
                        "AspNetUserClaims", "AspNetUserLogins", "AspNetUserRoles", "AspNetUsers",
                        "AspNetUserTokens", "Companies", "CompanyLicences", "CompanyPermissions",
                        "NobleRolePermissions","InventoryBlindDetails", "ModulesNames", "ModulesRights","NobleGroups"
                    };
                    if (records.Contains("Contact"))
                    {
                        var notRemoveAbleListWithoutContact = new List<string>()
                        {
                            "Origins", "Accounts", "AccountsLevelOne", "AccountsLevelTwo","AccountTypes",
                            "ProductMasters", "Products", "PromotionOfferItems", "PromotionOffers",
                            "Brands", "BundleCategories", "RolesPermissions", "Colors",
                            "CompanyAccountSetups", "CompanyAttachments", "companyInformations", "Sizes",
                            "ContactAttachments","ContactBankAccounts", "Contacts", "SubCategories",
                            "CostCenters","TaxRates", "Units","Categories","Warehouses","Terminals","LedgerAccounts","CompanySubmissionPeriods","YearlyPeriods",
                            "ItemViewSetups","ItemViewSetupForPrint","Currencies","CompanyAccountSetups","PrintSettings","Companies","PaymentOptions",
                            "DenominationSetups"
                        };
                        notRemoveAbleList.AddRange(notRemoveAbleListWithoutContact);
                    }
                    else if (records.Contains("ProductInfo"))
                    {
                        var notRemoveAbleListWithoutContact = new List<string>()
                        {
                            "Origins", "Accounts", "AccountsLevelOne", "AccountsLevelTwo","AccountTypes",
                            "ProductMasters", "Products", "PromotionOfferItems", "PromotionOffers","CompanyAccountSetups","Currencies","CompanySubmissionPeriods",
                            "Brands", "BundleCategories", "RolesPermissions", "Colors","LedgerAccounts",
                            "CompanyAccountSetups", "CompanyAttachments", "companyInformations", "Sizes","ItemViewSetupForPrint"
                            , "SubCategories","CostCenters","TaxRates", "Units","Categories","Warehouses","Terminals","ItemViewSetups","YearlyPeriods",
                            "PrintSettings","Companies","PaymentOptions","DenominationSetups"
                        };
                        notRemoveAbleList.AddRange(notRemoveAbleListWithoutContact);
                    }
                    else if (records.Contains("AllData"))
                    {
                        var notRemoveAbleListWithoutContact = new List<string>()
                        {
                            "Accounts", "AccountsLevelOne", "AccountsLevelTwo","AccountTypes", "RolesPermissions",
                            "CompanyAccountSetups", "CompanyAttachments", "companyInformations","CostCenters","Warehouses","Terminals"
                        };
                        notRemoveAbleList.AddRange(notRemoveAbleListWithoutContact);
                    }
                    var data = connection.Query<string>(allTableName).ToList();
                    foreach (var tableName in notRemoveAbleList)
                    {
                        data.Remove(tableName);
                    }

                    var removeConstraints = $"EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';";
                    var traOfRemoveConstraints = connection.BeginTransaction();
                    SqlCommand cmdRemoveConstraints = new SqlCommand(removeConstraints, connection, traOfRemoveConstraints);

                    // Execute the delete in batches of 1000
                    cmdRemoveConstraints.CommandText = removeConstraints;
                    await cmdRemoveConstraints.ExecuteNonQueryAsync();

                    traOfRemoveConstraints.Commit();


                    foreach (var tableName in data)
                    {
                        var checkColumnQuery = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}' AND COLUMN_NAME = 'CompanyId'";

                        using (var checkCmd = new SqlCommand(checkColumnQuery, connection))
                        {
                            var columnExists = (int)checkCmd.ExecuteScalar() > 0;
                          
                            int rowsDeleted;
                            do
                            {
                                if (columnExists)
                                {
                                    var resetDatabase1 = $"DELETE TOP (1000) FROM {tableName} where CompanyId= '{companyId}';";
                                    var tra = connection.BeginTransaction();
                                    SqlCommand cmd1 = new SqlCommand(resetDatabase1, connection, tra);

                                    // Execute the delete in batches of 1000
                                    cmd1.CommandText = resetDatabase1;
                                    rowsDeleted = await cmd1.ExecuteNonQueryAsync();

                                    tra.Commit();
                                }
                                else
                                {
                                    var resetDatabase1 = $"DELETE TOP (1000) FROM {tableName}";
                                    var tra = connection.BeginTransaction();
                                    SqlCommand cmd1 = new SqlCommand(resetDatabase1, connection, tra);

                                    // Execute the delete in batches of 1000
                                    cmd1.CommandText = resetDatabase1;
                                    rowsDeleted = await cmd1.ExecuteNonQueryAsync();

                                    tra.Commit();
                                }
                            }
                            while (rowsDeleted > 0);

                          
                        }
                    }


                    var resetDatabase2 = "";

                    foreach (var blind in inventoryBlindList)
                    {
                        resetDatabase2 = "delete from InventoryBlindDetails where InventoryBlindId='" + blind.Id + "';";
                    }

                    if (inventoryBlindList.Count > 0)
                    {
                        var tra = connection.BeginTransaction();
                        cmd = new SqlCommand(resetDatabase2, connection, tra);
                        await cmd.ExecuteNonQueryAsync();
                        tra.Commit();
                    }


                    if (!records.Contains("Reset"))
                    {
                        var resetDatabase3 = "";
                        //remove employee registration account
                        foreach (var employee in employeeRegistrationList)
                        {
                            if (employee.PayableAccountId != null && employee.EmployeeAccountId != null)
                            {
                                resetDatabase3 = resetDatabase3 + "delete from Accounts where Id = '" + employee.PayableAccountId +
                                                "';delete from Accounts where Id = '" + employee.EmployeeAccountId + "';";
                            }
                        }
                        if (employeeRegistrationList.Count > 0)
                        {
                            var tra = connection.BeginTransaction();
                            cmd = new SqlCommand(resetDatabase3, connection, tra);
                            await cmd.ExecuteNonQueryAsync();
                            tra.Commit();
                        }
                        //remove account from bank account

                        //foreach (var bank in bankList)
                        //{
                        //    resetDatabase = resetDatabase + " delete from Accounts where Id = '" + bank.AccountId + "';";
                        //}


                        if (!records.Contains("Contact"))
                        {
                            var resetDatabase4 = "";
                            // remove account of Contact
                            foreach (var contact in contactList)
                            {
                                resetDatabase4 = "delete from Accounts where Id = '" + contact.AccountId + "';";
                            }

                            if (contactList.Count > 0)
                            {
                                var tra = connection.BeginTransaction();
                                cmd = new SqlCommand(resetDatabase4, connection, tra);
                                await cmd.ExecuteNonQueryAsync();
                                tra.Commit();
                            }
                        }

                        if (records.Contains("AllData"))
                        {
                            var resetDatabase5 = "";

                            foreach (var category in categoryList)
                            {
                                //remove account from category account
                                resetDatabase5 = resetDatabase5 + "delete from Accounts where Id = '" + category.COGSAccountId +
                                                "';delete from Accounts where Id = '" + category.InventoryAccountId +
                                                "';delete from Accounts where Id = '" + category.SaleAccountId + "';";
                            }

                            if (categoryList.Count > 0)
                            {
                                var tra = connection.BeginTransaction();
                                cmd = new SqlCommand(resetDatabase5, connection, tra);
                                await cmd.ExecuteNonQueryAsync();
                                tra.Commit();
                            }
                        }
                    }
                    else
                    {
                        var resetDatabase6 = "";

                        resetDatabase6 = resetDatabase6 + "Update Companies " +
                                        "set LogoPath = '', NameArabic = '', CompanyNameEnglish = ''," +
                                        "CompanyNameArabic = '', CategoryInEnglish = '', CategoryInArabic = '', AddressArabic = ''," +
                                        "TermsCondition = 0, IsProceed = 0, Step1 = 0, Step2 = 0, Step3 = 0, Step4 = 0, Step5 = 0" +
                                        "where Id = '" + companyId + "';";
                        foreach (var rolePermission in nobleRoleList)
                        {
                            resetDatabase6 = resetDatabase6 + " delete from NobleRolePermissions where RoleId = '" + rolePermission.Id +
                                            "'; delete from NobleRoles where Id = '" + rolePermission.Id +
                                            "'; delete from NobleUserRoles where RoleId = '" + rolePermission.Id + "';";

                        }

                        if (nobleRoleList.Count > 0)
                        {
                            var tra = connection.BeginTransaction();
                            cmd = new SqlCommand(resetDatabase6, connection, tra);
                            await cmd.ExecuteNonQueryAsync();
                            tra.Commit();
                        }

                    }

                    var enableConstraints = $"EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';";
                    var traOfEnableConstraints = connection.BeginTransaction();
                    SqlCommand cmdEnableConstraints = new SqlCommand(enableConstraints, connection, traOfEnableConstraints);

                    // Execute the delete in batches of 1000
                    cmdEnableConstraints.CommandText = removeConstraints;
                    await cmdEnableConstraints.ExecuteNonQueryAsync();

                    //traOfRemoveConstraints.Commit();

                    connection.Close();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }


        }



        #endregion


        
        public static bool createFolder(string host, string targetPath, string UserId, string Password)
        {
            try
            {
                WebRequest request1 = WebRequest.Create(host + targetPath);
                request1.Method = WebRequestMethods.Ftp.MakeDirectory;
                request1.Credentials = new NetworkCredential(UserId, Password);
                using (var resp = (FtpWebResponse)request1.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        [HttpGet("UploadFiles")]
        [Roles("CanPushRecord")]
        public IActionResult UploadFiles()
        {
            string sourcePath = _hostingEnvironment.ContentRootPath + Configuration.GetSection("Ftp:LocalFolder").Value; ;
            string targetPath = Configuration.GetSection("Ftp:ServerFolder").Value + "/";
            string host = Configuration.GetSection("Ftp:FtpHost").Value;
            string userId = Configuration.GetSection("Ftp:FtpUser").Value;
            string password = Configuration.GetSection("Ftp:FtpPassword").Value;

            try
            {
                //var result = createFolder(host, targetPath, userId, password);
                var localDirectories = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
                FtpWebRequest request =
                    (FtpWebRequest)WebRequest.Create(host + targetPath);
                request.Credentials = new NetworkCredential(userId, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string names = reader.ReadToEnd();

                reader.Close();
                response.Close();
                var serverDirectories = names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (serverDirectories.Count == localDirectories.Length)
                    return Ok(StatusCode(200));
                foreach (var path in localDirectories)
                {
                    if (!serverDirectories.Contains(Path.GetFileName(path)))
                    {
                        var filename = Path.GetFileName(path);

                        FtpWebRequest uploadRequest =
                        (FtpWebRequest)WebRequest.Create(host + targetPath + filename);
                        uploadRequest.Credentials = new NetworkCredential(userId, password);
                        uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;

                        using (Stream fileStream = System.IO.File.OpenRead(path))
                        using (Stream ftpStream = uploadRequest.GetRequestStream())
                        {
                            fileStream.CopyTo(ftpStream);
                        }
                    }
                }

                return Ok(StatusCode(200));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " \n After Message \n " + ex.StackTrace);
                
                return Ok(ex.Message + " \n After Message \n " + ex.StackTrace);
            }
        }

        [HttpGet("DownloadFiles")]
        [Roles("CanPullRecord")]
        public IActionResult DownloadFiles(string ParentFolderpath)
        {
            try
            {
                var sourcePath = _hostingEnvironment.ContentRootPath  + Configuration.GetSection("Ftp:LocalFolder").Value; ;
                var targetPath = Configuration.GetSection("Ftp:ServerFolder").Value; ;
                string host = Configuration.GetSection("Ftp:FtpHost").Value;
                string userId = Configuration.GetSection("Ftp:FtpUser").Value;
                string password = Configuration.GetSection("Ftp:FtpPassword").Value;


                //Get All Files that  exist in local Directory 
                var localDirectories = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
                var localDirectoryFiles = new List<string>();
                foreach (var dir in localDirectories)
                {
                    localDirectoryFiles.Add(Path.GetFileName(dir));
                }
                // Get the object used to communicate with the server.
                FtpWebRequest request =
                    (FtpWebRequest)WebRequest.Create(host + targetPath);
                request.Credentials = new NetworkCredential(userId, password);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string names = reader.ReadToEnd();

                reader.Close();
                response.Close();

                var serverDirectories = names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (serverDirectories.Count == localDirectories.Length)
                    return Ok(StatusCode(200));
                foreach (var fileName in serverDirectories)
                {
                    if (!localDirectoryFiles.Contains(fileName))
                    {
                        FtpWebRequest downloadRequest =
                            (FtpWebRequest)WebRequest.Create(host + targetPath + "/" + fileName);

                        downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                        downloadRequest.Credentials = new NetworkCredential(userId, password);

                        if (!Directory.Exists(sourcePath))
                            Directory.CreateDirectory(sourcePath);

                        using (FtpWebResponse downloadResponse =
                            (FtpWebResponse)downloadRequest.GetResponse())
                        using (Stream sourceStream = downloadResponse.GetResponseStream())
                        using (Stream targetStream = System.IO.File.Create(sourcePath + "/" + fileName))
                        {
                            byte[] buffer = new byte[10240];
                            int read;
                            while ((read = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                targetStream.Write(buffer, 0, read);
                            }
                        }
                    }
                }

                return Ok(StatusCode(200));
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(200));
            }
        }

    }
}

