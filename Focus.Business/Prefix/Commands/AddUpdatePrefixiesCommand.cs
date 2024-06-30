using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.Prefix.Model;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Prefix.Commands
{
    public class AddUpdatePrefixiesCommand : IRequest<Guid>
    {
        public PrefixiesLookupModel Pre { get; set; }

        public class Handler : IRequestHandler<AddUpdatePrefixiesCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddUpdatePrefixiesCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdatePrefixiesCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var prefix = _context.Prefixies.FirstOrDefault();
                    if (prefix != null)
                    {
                        prefix.SInvoice = request.Pre.SInvoice;
                        prefix.SReturn = request.Pre.SReturn;
                        prefix.SOrder = request.Pre.SOrder;
                        prefix.SQutation = request.Pre.SQutation;
                        prefix.PInvoice = request.Pre.PInvoice;
                        prefix.PReturn = request.Pre.PReturn;
                        prefix.POrder = request.Pre.POrder;
                        prefix.SOrdeTracking = request.Pre.SOrdeTracking;
                        prefix.Employee = request.Pre.Employee;
                        prefix.ReceiptInvoice = request.Pre.ReceiptInvoice;
                        prefix.GlobalInvoice = request.Pre.GlobalInvoice;
                        prefix.CreditNote = request.Pre.CreditNote;
                        prefix.DebitNote = request.Pre.DebitNote;
                        prefix.ProformaSaleInvoice = request.Pre.ProformaSaleInvoice;
                        prefix.CustomerPayReceipt = request.Pre.CustomerPayReceipt;
                        prefix.AdvanceSaleReceipt = request.Pre.AdvanceSaleReceipt;
                        prefix.SupplierPayReceipt = request.Pre.SupplierPayReceipt;
                        prefix.AdvancePurchaseReceipt = request.Pre.AdvancePurchaseReceipt;


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
                        return prefix.Id;
                    }
                    else
                    {
                        var prefixies = new Prefixies
                        {
                            SInvoice = request.Pre.SInvoice,
                            SReturn = request.Pre.SReturn,
                            SOrder = request.Pre.SOrder,
                            SQutation = request.Pre.SQutation,
                            PInvoice = request.Pre.PInvoice,
                            POrder = request.Pre.POrder,
                            PReturn = request.Pre.PReturn,
                            SOrdeTracking = request.Pre.SOrdeTracking,
                            Employee = request.Pre.Employee,
                            GlobalInvoice = request.Pre.GlobalInvoice,
                            ReceiptInvoice = request.Pre.ReceiptInvoice,
                            DebitNote = request.Pre.DebitNote,
                            CreditNote = request.Pre.CreditNote,
                            ProformaSaleInvoice = request.Pre.ProformaSaleInvoice,
                            CustomerPayReceipt = request.Pre.CustomerPayReceipt,
                            AdvanceSaleReceipt = request.Pre.AdvanceSaleReceipt,
                            SupplierPayReceipt = request.Pre.SupplierPayReceipt,
                            AdvancePurchaseReceipt = request.Pre.AdvancePurchaseReceipt,
                    };
                        await _context.Prefixies.AddAsync(prefixies, cancellationToken);


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
                        return prefixies.Id;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
