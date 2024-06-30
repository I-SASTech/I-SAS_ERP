using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.PriceRecordChanges.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Models;

namespace Focus.Business.PriceRecordChanges.Commands
{
    public class SavePriceRecordChnageCommand : IRequest<Message>
    {
        public List<PriceLabelsProuctsListLookupModel> PriceRecordChange { get; set; }

        public class Handler : IRequestHandler<SavePriceRecordChnageCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<Message> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(SavePriceRecordChnageCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.PriceRecordChange != null)
                    {

                        var priceChangeIds = request.PriceRecordChange.Where(x => x.IsActive).Select(item => item.Id).ToList();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

                        var priceRecordsToUpdate = await _context.PriceRecords
                            .Where(price => priceChangeIds.Contains(price.Id)).ToListAsync(cancellationToken: cancellationToken);

                        foreach (var item in request.PriceRecordChange)
                        {
                            var price = priceRecordsToUpdate.FirstOrDefault(p => p.Id == item.Id);

                            if (price != null)
                            {
                                price.ProductId = item.ProductId;
                                price.PriceLabelingId = item.PriceLabelingId;
                                price.Price = item.Price ?? 0;
                                price.SalePrice = item.SalePrice ?? 0;
                                price.PurchasePrice = item.PurchasePrice ?? 0;
                                price.CostPrice = item.CostPrice ?? 0;
                                price.IsActive = item.IsActive;
                                price.NewPrice = item.NewPrice ?? 0;
                            }
                        }

                        _context.PriceRecords.UpdateRange(priceRecordsToUpdate);

                        var log = _context.SyncLog();
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
                    }


                    return new Message
                    {
                        IsAddUpdate = "Data saved successfully"
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return new Message
                    {
                        IsAddUpdate = "Something went wrong"
                    };
                }
            }
        }
    }
}
