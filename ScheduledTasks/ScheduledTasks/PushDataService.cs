using Focus.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScheduledTasks.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledTasks.ScheduledTasks
{

    class PushDataService : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public IConfiguration Configuration { get; }
        public ILogger<PushDataService> _logger { get; }

        public PushDataService(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<PushDataService> logger)
        {
            _scopeFactory = scopeFactory;
            Configuration = configuration;
            _logger = logger;

        }
        public string Schedule => "*/1 * * * *";

        public async Task Invoke(CancellationToken cancellationToken)
        {
            await PushDataRecord();
        }


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

            bool isExist;
            try
            {
                using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection1.Open();
                    cmd = new SqlCommand("select case when exists(select Top 1 * from CompanyAccountSetups where AutoSync = 1) then 1 else 0 end;", connection1);

                    isExist = (int)(await cmd.ExecuteScalarAsync()) == 1;
                    connection1.Close();
                }
                if (!isExist)
                    return;
            }
            catch (SqlException ex)
            {
                _logger.LogError("Sql Server error" + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Some thing went wrong" + ex.Message);
            }

            using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                //SqlTransaction transaction;

                //transaction = connection.BeginTransaction();

                cmd = new SqlCommand("SELECT top 150 * FROM SyncRecords where Pull = 0 and Push = 0 order by ChangeDate;", connection);
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
                    using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("ServerConnection")))
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
                               " Update SyncRecords set Push = 1, Pull = 1, PushDate = '" + pushDate +
                                                "' where ColumnId = '" + item.ColumnId + "'; ";
                            queryDefault += " Update SyncRecords set Push = 1, PushDate = '" + pushDate +
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
                catch (SqlException ex)
                {
                    _logger.LogError("Sql Server error" + ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Some thing went wrong" + ex.Message);
                }
                finally
                {
                    connection.Close();

                }
            }
        }
    }
}
