using Focus.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScheduledTasks.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO.Ports;

namespace ScheduledTasks.ScheduledTasks
{
    internal class AttendanceSms : IScheduledTask
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public IConfiguration Configuration { get; }
        public ILogger<PushDataService> _logger { get; }

        public AttendanceSms(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<PushDataService> logger)
        {
            _scopeFactory = scopeFactory;
            Configuration = configuration;
            _logger = logger;

        }
        public string Schedule => "*/1 * * * *";

        public async Task Invoke(CancellationToken cancellationToken)
        {
            await AttendanceSmsSend();
        }


        public async Task AttendanceSmsSend()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                SqlCommand cmd;

                using (SqlConnection connection1 = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                {
                    connection1.Open();
                    cmd = new SqlCommand("select * from ManualAttendances where IsSmsSend = 0;", connection1);

                    da.SelectCommand = (SqlCommand)cmd;

                    da.Fill(ds);



                    var record = new List<ManualAttendance>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        record.Add(new ManualAttendance
                        {
                            Id = Guid.Parse(Convert.ToString(dr["Id"])),
                            EmployeeId = Guid.Parse(Convert.ToString(dr["EmployeeId"])),
                            CheckIn = Convert.ToDateTime(dr["CheckIn"]),
                            CheckOut = string.IsNullOrEmpty(Convert.ToString(dr["CheckOut"])) ?DateTime.Today : Convert.ToDateTime(dr["CheckOut"]),
                            IsCheckIn = Convert.ToBoolean(dr["IsCheckIn"]),
                            IsOnLeave = Convert.ToBoolean(dr["IsOnLeave"]),
                            IsCheckOut = Convert.ToBoolean(dr["IsCheckOut"]),
                            IsAbsent = Convert.ToBoolean(dr["IsAbsent"]),
                        });
                    }

                    if (record.Count <= 0)
                        return;
                    var updateManualAttendance = string.Empty;
                    foreach (var attendance in record)
                    {
                        cmd = new SqlCommand("select Top 1 MobileNo from EmployeeRegistrations where Id = '" + attendance.EmployeeId + "'", connection1);
                        var mobileNumber = Convert.ToString(await cmd.ExecuteScalarAsync());
                        cmd = new SqlCommand("select EnglishName from EmployeeRegistrations where Id = '" + attendance.EmployeeId + "'", connection1);
                        var employeeName = Convert.ToString(await cmd.ExecuteScalarAsync());
                        if (!string.IsNullOrEmpty(mobileNumber))
                        {
                            cmd = new SqlCommand("select Top 1 ComPort from GsmSmsSetups", connection1);
                            var comPort = Convert.ToString(await cmd.ExecuteScalarAsync());

                            cmd = new SqlCommand("select Top 1 DefaultMessage from GsmSmsSetups", connection1);
                            //var defaultMessage = Convert.ToString(await cmd.ExecuteScalarAsync());

                            var defaultMessage = attendance.IsCheckIn ?
                                "Dear " + employeeName + ", \r\n Your CheckIn Date is " + attendance.CheckIn.Value.ToString("d") + " and Time is " + attendance.CheckIn.Value.ToString("hh:mm:ss")
                                : attendance.IsCheckOut ?
                                 "Dear " + employeeName + ", \r\n Your CheckOut Date is " + attendance.CheckOut.Value.ToString("d") + " and Time is " + attendance.CheckOut.Value.ToString("hh:mm:ss")
                                 : attendance.IsAbsent ?
                                 "Dear " + employeeName + ", \r\n You are absent and Dateis " + attendance.CheckIn.Value.ToString("d")
                                 : "Dear " + employeeName + ", \r\n You are on Leave and Dateis " + attendance.CheckIn.Value.ToString("d");

                            SerialPort sp = new SerialPort();
                            sp.PortName = comPort;
                            sp.Open();
                            sp.WriteLine("AT" + Environment.NewLine);

                            Thread.Sleep(100);
                            sp.WriteLine("AT+CMGF=1" + Environment.NewLine);
                            //Thread.Sleep(100); 
                            //sp.WriteLine("AT+CSCS=\"GSM\"" + Environment.NewLine);
                            Thread.Sleep(100);
                            sp.WriteLine("AT+CMGS=\"" + mobileNumber + "\"" + Environment.NewLine);
                            Thread.Sleep(100);

                            sp.WriteLine(defaultMessage + Environment.NewLine);
                            Thread.Sleep(200);

                            sp.Write(new byte[] { 26 }, 0, 1);
                            Thread.Sleep(100);
                            var response = sp.ReadExisting();
                            //if (response.Contains("Error"))
                            //    _logger.LogError(response);
                            sp.Close();
                            Thread.Sleep(100);
                        }
                        updateManualAttendance += "update ManualAttendances set IsSmsSend = 1 where Id = '" + attendance.Id + "'; ";
                    }
                    cmd = new SqlCommand(updateManualAttendance, connection1);
                    cmd.ExecuteNonQuery();
                    connection1.Close();
                }
                
            }
            catch (SqlException ex)
            {
                _logger.LogError("Sql Server error" + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Some thing went wrong" + ex.Message);
            }

        }
    }
}
