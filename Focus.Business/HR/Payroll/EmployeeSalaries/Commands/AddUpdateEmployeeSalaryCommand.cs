using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.EmployeeSalaries.Commands
{
    public class AddUpdateEmployeeSalaryCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public EmployeeSalaryLookUpModel EmployeeSalary { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateEmployeeSalaryCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateEmployeeSalaryCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateEmployeeSalaryCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.EmployeeSalary.Id != Guid.Empty)
                    {

                        var employeeSalary = await Context.EmployeeSalaries.FindAsync(request.EmployeeSalary.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        if (employeeSalary == null)
                            throw new NotFoundException("Employee Salary", "not Found");

                        if (employeeSalary.EmployeeId != request.EmployeeSalary.EmployeeId)
                            throw new ObjectAlreadyExistsException("Employee Salary Already Exists");

                        Context.EmployeeSalaryDetails.RemoveRange(employeeSalary.EmployeeSalaryDetails);


                        employeeSalary.SalaryType = request.EmployeeSalary.SalaryType;
                        employeeSalary.PayPeriodId = request.EmployeeSalary.PayPeriodId;
                        employeeSalary.BaseSalary = request.EmployeeSalary.BaseSalary;
                        employeeSalary.WeekdayHourlySalary = request.EmployeeSalary.WeekdayHourlySalary;
                        employeeSalary.WeekendDayHourlySalary = request.EmployeeSalary.WeekendDayHourlySalary;
                        employeeSalary.SalaryTemplateId = request.EmployeeSalary.SalaryTemplateId;
                        employeeSalary.EmployeeId = request.EmployeeSalary.EmployeeId;
                        employeeSalary.StartingDate = request.EmployeeSalary.StartingDate;
                        employeeSalary.IncomeTax = request.EmployeeSalary.IncomeTax;
                        employeeSalary.AutoIncomeTax = request.EmployeeSalary.AutoIncomeTax;
                        employeeSalary.GosiRate = request.EmployeeSalary.GosiRate;
                        employeeSalary.EmployeeSalaryDetails = request.EmployeeSalary.SalaryDetailList.Select(x =>
                            new EmployeeSalaryDetail()
                            {
                                ItemId = x.ItemId,
                                Type = x.Type,
                                AmountType = x.AmountType,
                                TaxPlan = x.TaxPlan,
                                Amount = x.Amount,
                                Percent = x.Percent
                            }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);
                        return employeeSalary.Id;
                    }
                    else
                    {

                        var employeeExist = await Context.EmployeeSalaries.AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeSalary.EmployeeId, cancellationToken: cancellationToken);
                        if (employeeExist != null)
                            throw new ObjectAlreadyExistsException("Employee Salary Already Exist");

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var employeeSalary = new EmployeeSalary()
                        {
                            SalaryType = request.EmployeeSalary.SalaryType,
                            PayPeriodId = request.EmployeeSalary.PayPeriodId,
                            BaseSalary = request.EmployeeSalary.BaseSalary,
                            WeekdayHourlySalary = request.EmployeeSalary.WeekdayHourlySalary,
                            WeekendDayHourlySalary = request.EmployeeSalary.WeekendDayHourlySalary,
                            SalaryTemplateId = request.EmployeeSalary.SalaryTemplateId,
                            EmployeeId = request.EmployeeSalary.EmployeeId,
                            StartingDate = request.EmployeeSalary.StartingDate,
                            IncomeTax = request.EmployeeSalary.IncomeTax,
                            AutoIncomeTax = request.EmployeeSalary.AutoIncomeTax,
                            GosiRate = request.EmployeeSalary.GosiRate,
                            EmployeeSalaryDetails = request.EmployeeSalary.SalaryDetailList.Select(x =>
                                new EmployeeSalaryDetail()
                                {
                                    ItemId = x.ItemId,
                                    Type = x.Type,
                                    AmountType = x.AmountType,
                                    TaxPlan = x.TaxPlan,
                                    Amount = x.Amount,
                                    Percent = x.Percent
                                }).ToList()
                        };

                        //Add EmployeeSalaries to database
                        await Context.EmployeeSalaries.AddAsync(employeeSalary, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return employeeSalary.Id;


                    }

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
