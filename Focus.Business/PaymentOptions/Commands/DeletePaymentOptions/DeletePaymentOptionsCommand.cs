using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PaymentOptions.Commands.DeletePaymentOptions
{
    public class DeletePaymentOptionsCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeletePaymentOptionsCommand, Guid>
        {
            //Create an object of IApplicationDbContext for delete the data from database
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            
            public Handler(IApplicationDbContext context, ILogger<DeletePaymentOptionsCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(DeletePaymentOptionsCommand request, CancellationToken cancellationToken)
            {
                //Get Data from database in specific id which we want to delete
                var paymentOptions = await _context.PaymentOptions.FindAsync(request.Id);

                Random rnd = new Random();
                for (int i = 0; i < 11; i++)
                {
                    _code += rnd.Next(0, 9).ToString();
                }

                //Check data is exist
                if (paymentOptions != null)
                {
                    _context.PaymentOptions.Remove(paymentOptions);

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
                            //BranchId = branch.Id,
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
        }
    }
}
