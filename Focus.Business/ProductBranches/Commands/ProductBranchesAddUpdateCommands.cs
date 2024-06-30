using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.ProductBranches.Models;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Models;

namespace Focus.Business.ProductBranches.Commands
{
    public class ProductBranchesAddUpdateCommands : IRequest<Message>
    {
        public ProductBranchesLookupModel Branches { get; set; }

        public class Handler : IRequestHandler<ProductBranchesAddUpdateCommands, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<ProductBranchesAddUpdateCommands> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Message> Handle(ProductBranchesAddUpdateCommands request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }


                    if (request.Branches.isSelectAll)
                    {
                        var products = await _context.Products.AsNoTracking().Select(x => x.Id).ToListAsync(cancellationToken: cancellationToken);

                        foreach (var item in request.Branches.branchIds)
                        {
                            var branchItemsList = await _context.BranchItems.Where(x => x.BranchId==item.Id).ToListAsync(cancellationToken: cancellationToken);
                            _context.BranchItems.RemoveRange(branchItemsList);

                            var branchItems = products.Select(x => new BranchItems
                            {
                                BranchId = item.Id,
                                ProductId = x
                            }).ToList();
                            await _context.BranchItems.AddRangeAsync(branchItems, cancellationToken);

                            var log = _context.SyncLog();
                            var list = new List<SyncRecordModel>();

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
                                BranchId = item.Id,
                                Code = _code,
                            }).ToList();
                            list.AddRange(syncRecords);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = list,
                                IsServer=true
                            }, cancellationToken);
                        }

                        return new Message
                        {
                            IsSuccess = true,
                            IsAddUpdate = "Data has been update Successfully",
                        };
                    }
                    else
                    {
                        foreach (var item in request.Branches.branchIds)
                        {
                            var branchItemsList = await _context.BranchItems.Where(x => x.BranchId==item.Id).ToListAsync(cancellationToken: cancellationToken);
                            _context.BranchItems.RemoveRange(branchItemsList);

                            var branchItems = request.Branches.productIds.Select(x => new BranchItems
                            {
                                BranchId = item.Id,
                                ProductId = x.Id
                            }).ToList();
                            await _context.BranchItems.AddRangeAsync(branchItems, cancellationToken);

                            var log = _context.SyncLog();
                            var list = new List<SyncRecordModel>();

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
                                BranchId = item.Id,
                                Code = _code,
                            }).ToList();
                            list.AddRange(syncRecords);

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = list,
                                IsServer=true
                            }, cancellationToken);
                        }


                        return new Message
                        {
                            IsSuccess = true,
                            IsAddUpdate = "Data has been update Successfully",
                        };
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
