using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Currencies.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteCurrencyCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor
            public Handler(IApplicationDbContext context, ILogger<DeleteCurrencyCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator= mediator;
            }
            public async Task<Guid> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    //Get Data from database in specific id which we want to delete
                    var currency = await _context.Currencies.FindAsync(request.Id);

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Check data is exist
                    if (currency != null)
                    {
                        _context.Currencies.Remove(currency);

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
                                //    BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);

                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return request.Id;
                    }
                    //throw exception which data not exist
                    throw new NotFoundException("Not Found", request.Id);
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
