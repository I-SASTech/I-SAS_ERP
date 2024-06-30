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

namespace Focus.Business.Accounting.Commands.AddUpdateAccount
{
    public class AddUpdateAccountCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid? CostCenterId { get; set; }

        public class Handler : IRequestHandler<AddUpdateAccountCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateAccountCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateAccountCommand request, CancellationToken cancellationToken)
            {
                try
                {
                   

                    if (request.Id != Guid.Empty)
                    {
                        var accounts = await _context.Accounts.FindAsync(request.Id);
                        if (accounts == null)
                            throw new NotFoundException(request.Code, request.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        accounts.Code = request.Code.ToUpper();
                        accounts.Name = request.Name;
                        accounts.NameArabic = request.NameArabic;
                        accounts.Description = request.Description;
                        accounts.IsActive = request.IsActive;
                        accounts.CostCenterId = request.CostCenterId;


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
                       //         BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }   

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true,
                            DocumentNo=request.Code
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        

                        return accounts.Id;
                    }
                    else
                    {
                        var codeExist = await _context.Accounts.Where(x => x.Code == request.Code).FirstOrDefaultAsync(cancellationToken);
                        if (codeExist != null)
                            throw new ObjectAlreadyExistsException(request.Code);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var account = new Account
                        {
                            Code = request.Code.ToUpper(),
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            IsActive = request.IsActive,
                            CostCenterId = request.CostCenterId
                        };


                        await _context.Accounts.AddAsync(account, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=account.Code
                        }, cancellationToken);

                        _context.SaveChanges();

                        return account.Id;
                    }

                }
                catch (ObjectAlreadyExistsException ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Account is already exist.");
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