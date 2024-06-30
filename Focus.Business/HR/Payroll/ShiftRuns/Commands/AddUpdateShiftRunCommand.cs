using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.HR.Payroll.ShiftAssigns.Model;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Focus.Business.HR.Payroll.ShiftRuns.Commands
{
    public class AddUpdateShiftRunCommand : IRequest<Message>
    {
        public class Handler : IRequestHandler<AddUpdateShiftRunCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            public IConfiguration Configuration { get; }
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateShiftRunCommand> logger, IMediator mediator, IConfiguration configuration)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                Configuration = configuration;
            }


            public async Task<Message> Handle(AddUpdateShiftRunCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    List<ShiftAssignModel> query;

                    if (DateTime.Now.ToString("dddd") == "Sunday")
                    {
                        await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        query = connection.Query<ShiftAssignModel>("select Id, Code, FromDate, ToDate, CompanyId, CreatedById from ShiftAssigns where IsActive = 1 AND Sunday = 1;").ToList();

                        connection.Close();
                    }
                    else if (DateTime.Now.ToString("dddd") == "Monday")
                    {
                        await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        query = connection.Query<ShiftAssignModel>("select Id, Code, FromDate, ToDate, CompanyId, CreatedById from ShiftAssigns where IsActive = 1 AND Monday = 1;").ToList();

                        connection.Close();
                    }
                    else if (DateTime.Now.ToString("dddd") == "Tuesday")
                    {
                        await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        query = connection.Query<ShiftAssignModel>("select Id, Code, FromDate, ToDate, CompanyId, CreatedById from ShiftAssigns where IsActive = 1 AND Tuesday = 1;").ToList();

                        connection.Close();
                    }
                    else if (DateTime.Now.ToString("dddd") == "Wednesday")
                    {
                        await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        query = connection.Query<ShiftAssignModel>("select Id, Code, FromDate, ToDate, CompanyId, CreatedById from ShiftAssigns where IsActive = 1 AND Wednesday = 1;").ToList();

                        connection.Close();
                    }
                    else if (DateTime.Now.ToString("dddd") == "Thursday")
                    {
                        await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        query = connection.Query<ShiftAssignModel>("select Id, Code, FromDate, ToDate, CompanyId, CreatedById from ShiftAssigns where IsActive = 1 AND Thursday = 1;").ToList();

                        connection.Close();
                    }
                    else if (DateTime.Now.ToString("dddd") == "Friday")
                    {
                        await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        query = connection.Query<ShiftAssignModel>("select Id, Code, FromDate, ToDate, CompanyId, CreatedById from ShiftAssigns where IsActive = 1 AND Friday = 1;").ToList();

                        connection.Close();
                    }
                    else
                    {
                        await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                        query = connection.Query<ShiftAssignModel>("select Id, Code, FromDate, ToDate, CompanyId, CreatedById from ShiftAssigns where IsActive = 1 AND Saturday = 1;").ToList();

                        connection.Close();
                    }


                    foreach (var item in query)
                    {
                        if (item.FromDate.Hour == DateTime.Now.Hour)
                        {
                            await using SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                            var employeeList = connection.Query<ShiftAssignItemModel>("select EmployeeId from ShiftEmployeeAssigns where ShiftAssignId = '" + item.Id + "' AND IsActive = 1;").ToList();

                            var id = Guid.NewGuid();
                            await connection.ExecuteAsync("insert into ShiftRuns(Id, ShiftAssignId, FromTime, ToTime, IsActive, Code, CompanyId, CreatedById, ModifiedById, CreatedOn, ModifiedOn) values ('" + id + "', '" + item.Id + "', '" + item.FromDate + "','" + item.ToDate + "', '" + item.IsActive + "', '" + item.Code + "', '" + item.CompanyId + "', '" + item.CreatedById + "', '" + item.CreatedById + "', '" + DateTime.Now + "', '" + DateTime.Now + "')");
                            foreach (var employee in employeeList)
                            {
                                string processQuery = "INSERT INTO ShiftRunEmployees VALUES (@Id, @ShiftRunId, @EmployeeId, @CompanyId, @CreatedById, @CreatedOn, @ModifiedById, @ModifiedOn)";
                                await connection.ExecuteAsync(processQuery, new { Id = Guid.NewGuid(), ShiftRunId = id, employee.EmployeeId, item.CompanyId, item.CreatedById, CreatedOn = DateTime.Now, ModifiedById = item.CreatedById, ModifiedOn = DateTime.Now });
                            }

                            connection.Close();
                        }
                    }



                    return new Message
                    {
                        Id = default,
                        IsAddUpdate = null,
                        IsDeleted = null,
                        SaleId = default,
                        IsDoublePrint = false
                    };

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
