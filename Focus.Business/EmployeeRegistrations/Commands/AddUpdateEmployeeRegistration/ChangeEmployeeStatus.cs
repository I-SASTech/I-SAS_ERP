using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.EmployeeRegistrations.Commands.AddUpdateEmployeeRegistration
{
    public class ChangeEmployeeStatus : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsUser { get; set; }

        public class Handler : IRequestHandler<ChangeEmployeeStatus, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<ChangeEmployeeStatus> logger, UserManager<ApplicationUser> userManager, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _userManager = userManager;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(ChangeEmployeeStatus request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Id!=Guid.Empty)
                    {

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var employee = Context.EmployeeRegistrations.FirstOrDefault(x => x.Id == request.Id);
                        if (employee==null)
                        {
                            throw new NotFoundException("employee not found", "");
                        }


                        employee.IsActive = request.IsActive;
                        Context.EmployeeRegistrations.Update(employee);


                        var payAbleAccount = Context.LedgerAccounts.Where(x => x.AccountLedgerId == employee.Id).ToList();

                        foreach (var account in payAbleAccount)
                        {
                            account.IsActive = request.IsActive;
                        }



                        var userData = await _userManager.FindByEmailAsync(employee.Email);
                        if (userData != null)
                        {
                            userData.IsActive = request.IsActive;
                            await _userManager.UpdateAsync(userData);
                        }

                        var log = Context.SyncLog();
                        var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        foreach (var branch in branches)
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        // Return Employee id after successfully updating data
                        return employee.Id;

                    }

                    return Guid.Empty;

                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Employee Name Already Exist");
                }
            }
        }
    }
}
