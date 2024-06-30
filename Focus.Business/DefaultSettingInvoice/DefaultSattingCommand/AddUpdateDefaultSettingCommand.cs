using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Models;
using System.Collections.Generic;

namespace Focus.Business.DefaultSettingInvoice.DefaultSattingCommand
{
    public class AddUpdateDefaultSettingCommand : IRequest<Guid>
    {
        public DefaultSettingModel DefaultSettingModel { get; set; }
       
        public class Handler : IRequestHandler<AddUpdateDefaultSettingCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateDefaultSettingCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            
            public async Task<Guid> Handle(AddUpdateDefaultSettingCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    var data =await _context.DefaultSettings.AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken);
                    if (data == null)
                    {
                        var defaultSetting = new Domain.Entities.DefaultSetting()
                        {
                            IsSaleCredit = request.DefaultSettingModel.IsSaleCredit,
                            IsPurchaseCredit = request.DefaultSettingModel.IsPurchaseCredit,
                            IsCustomeCredit = request.DefaultSettingModel.IsCustomeCredit,
                            IsCustomerPayCredit = request.DefaultSettingModel.IsCustomerPayCredit,
                            IsSupplierPayCredit = request.DefaultSettingModel.IsSupplierPayCredit,
                            IsSupplierCredit = request.DefaultSettingModel.IsSupplierCredit,
                            IsCashCustomer = request.DefaultSettingModel.IsCashCustomer,
                            UserId = request.DefaultSettingModel.UserId,
              
                        };
                        await _context.DefaultSettings.AddAsync(defaultSetting, cancellationToken);


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
                        return defaultSetting.Id;
                    }
                    else
                    {
                        
                        data.IsSaleCredit = request.DefaultSettingModel.IsSaleCredit;
                        data.IsPurchaseCredit = request.DefaultSettingModel.IsPurchaseCredit;
                        data.IsCustomeCredit = request.DefaultSettingModel.IsCustomeCredit;
                        data.IsCustomerPayCredit = request.DefaultSettingModel.IsCustomerPayCredit;
                        data.IsSupplierPayCredit = request.DefaultSettingModel.IsSupplierPayCredit;
                        data.IsSupplierCredit = request.DefaultSettingModel.IsSupplierCredit;
                        data.UserId = request.DefaultSettingModel.UserId;
                        data.IsCashCustomer = request.DefaultSettingModel.IsCashCustomer;
                        _context.DefaultSettings.Update(data);


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
                        return data.Id;
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
