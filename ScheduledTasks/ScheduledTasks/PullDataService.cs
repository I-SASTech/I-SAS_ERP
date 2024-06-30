using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScheduledTasks.Interface;

namespace ScheduledTasks.ScheduledTasks
{
    class PullDataService : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public IConfiguration Configuration { get; }
        public ILogger<PullDataService> _logger { get; }

        public PullDataService(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<PullDataService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            Configuration = configuration;
        }
        public string Schedule => "*/1 * * * *";

        public async Task Invoke(CancellationToken cancellationToken)
        {
            await PullDataRecord();
        }


        private async Task PullDataRecord()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlDataAdapter da1 = new SqlDataAdapter();

            SqlCommand cmd;
            SqlCommand cmd1;
            bool isExist = false;
            try
            {
                await using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
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

            await using SqlConnection defaultConnection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("ServerConnection"));
            defaultConnection.Open();
            connection.Open();
            //SqlTransaction transaction;

            var br = new SqlCommand("SELECT * FROM Branches;", defaultConnection);
            var branch = (Guid?)await br.ExecuteScalarAsync();
            defaultConnection.Close();

            if (branch != null)
            {
                cmd = new SqlCommand("SELECT * FROM SyncRecords where Pull = 0 and push = 0 and BranchId='"+branch+"';", connection);

            }
            else
            {
                cmd = new SqlCommand("SELECT * FROM SyncRecords where Pull = 0 and push = 0;", connection);
            }


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

                foreach (var item in records.OrderBy(x => x.ChangeDate))
                {
                    DataSet ds1 = new DataSet();

                    cmd1 = new SqlCommand("select * from " + item.Table + " where " + item.ColumnName + " = '" + item.ColumnId + "'", connection);

                    da1.SelectCommand = cmd1;
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

                        var query = "if (select count(*) from " + item.Table + " where  " + item.ColumnName +
                                    " = '" + item.ColumnId + "') = 1" +
                                    " begin " +
                                    " delete from " + item.Table + "  where " + item.ColumnName + " = '" +
                                    item.ColumnId + "'; end ";


                        await using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                        {
                            connection1.Open();
                            cmd = new SqlCommand(query, connection1);

                            cmd.ExecuteScalar();


                            query = "Update SyncRecords set Push = 1, Pull = 1, PullDate = '" + date +
                                    "' where ColumnId = '" + item.ColumnId + "'; ";


                            cmd = new SqlCommand(query, connection1);

                            cmd.ExecuteScalar();
                            connection1.Close();
                        }

                        query = "Update SyncRecords set Push = 1, PushDate = '" + date +
                                "' where Id =" + item.Id + "; ";
                        cmd =
                            new SqlCommand(query, connection);
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
                        await using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                        {
                            var newCol = colVal.Replace("''", "NULL");
                            connection1.Open();
                            cmd = new SqlCommand(" select case when exists(select null  from " + item.Table + " where  " + item.ColumnName + "= '" + item.ColumnId + "') then 1 else 0 end;", connection1);

                            isExist = (int)cmd.ExecuteScalar() == 1;
                            connection1.Close();
                        }

                        if (isExist)
                        {
                            await using SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                            if (item.Table != "AspNetUserClaims")
                                updateCol = updateCol.Replace("''", "NULL");


                            connection1.Open();
                            if (item.ColumnType == "int")
                            {
                                cmd = new SqlCommand("update " + item.Table + " set " + updateCol + " where " + item.ColumnName + "= " + item.ColumnId + ";", connection1);
                            }
                            else
                            {
                                cmd = new SqlCommand("update " + item.Table + " set " + updateCol + " where " + item.ColumnName + "= '" + item.ColumnId + "';", connection1);
                            }

                            var dd = cmd.ExecuteNonQuery();
                            connection1.Close();
                        }
                        else
                        {
                            await using SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                            if (item.Table != "AspNetUserClaims")
                                colVal = colVal.Replace("''", "NULL");
                            if (item.Table == "BranchUsers")
                            {
                                var branchId = ds1.Tables[0].Rows[0].ItemArray[1].ToString().ToLower();
                                
                                if (branch!=null && branch.ToString().ToLower()==branchId)
                                {
                                    connection1.Open();
                                    cmd = new SqlCommand("Insert into " + item.Table + "(" + colName + ") values(" + colVal + ");", connection1);

                                    cmd.ExecuteNonQuery();
                                    connection1.Close();
                                }
                            }
                            else
                            {
                                try
                                {
                                    connection1.Open();
                                    cmd = new SqlCommand("Insert into " + item.Table + "(" + colName + ") values(" + colVal + ");", connection1);
                                    cmd.ExecuteNonQuery();
                                    connection1.Close();
                                }
                                catch (Exception e)
                                {
                                    continue;
                                }
                               
                            }
                        }
                    }
                    else if (item.Action == "Update")
                    {
                        await using SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        if (item.Table != "AspNetUserClaims")
                            updateCol = updateCol.Replace("''", "NULL");

                        connection1.Open();
                        cmd = new SqlCommand("update " + item.Table + " set " + updateCol + " where " + item.ColumnName + "= '" + item.ColumnId + "';", connection1);

                        cmd.ExecuteNonQuery();
                        connection1.Close();
                    }
                    else
                    {
                        var isSoftDelete = false;
                        foreach (var c in ds1.Tables[0].Columns)
                        {
                            if (c.ToString()=="IsDeleted")
                            {
                                isSoftDelete = true;
                            }
                        }

                        await using SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        if (isSoftDelete)
                        {
                            connection1.Open();
                            cmd = new SqlCommand("update " + item.Table + " set " + updateCol + " where " + item.ColumnName + "= '" + item.ColumnId + "';", connection1);

                            cmd.ExecuteNonQuery();
                            connection1.Close();
                        }
                        else
                        {
                            connection1.Open();
                            cmd = new SqlCommand("DELETE from " + item.Table + " where " + item.ColumnName + "= '" + item.ColumnId + "';", connection1);

                            cmd.ExecuteNonQuery();
                            connection1.Close();
                        }
                    }

                    var pullDate = DateTime.UtcNow.ToString("yyyy-MM-ddThh:mm:ss");

                    await using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection1.Open();
                        cmd = new SqlCommand("Update SyncRecords set Push = 1, Pull = 1, PullDate = '" + pullDate + "' where ColumnId = '" + item.ColumnId + "'; ", connection1);

                        cmd.ExecuteNonQuery();
                        connection1.Close();

                    }

                    cmd = new SqlCommand("Update SyncRecords set Pull = 1, PullDate = '" + pullDate + "' where Id =" + item.Id + ";", connection);
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
    }


}
