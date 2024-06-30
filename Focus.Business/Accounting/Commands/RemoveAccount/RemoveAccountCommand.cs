using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Accounting.Commands.RemoveAccount
{
    public class RemoveAccountCommand : IRequest<bool>
    {
        public Guid? Id { get; set; }
        public class Handler : IRequestHandler<RemoveAccountCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<RemoveAccountCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<bool> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    

                    var account = await _context.Accounts.FindAsync(request.Id);
                    if (account == null)
                        throw new NotFoundException(request.Id.ToString(), request.Id);

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //if (account..Any() || account.Budget.Any())
                    //    throw new NotFoundException(request.Id.ToString(), request.Id);

                    _context.Accounts.Remove(account);


                    var log = _context.SyncLog();
                    var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    var list = new List<SyncRecordModel>();
                    if (branches.Any())
                    {
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
                    }
                    else
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
                //            BranchId = branch.Id,
                            Code = _code,
                        }).ToList();

                        list.AddRange(syncRecords);
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = list,
                        IsServer=true
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}