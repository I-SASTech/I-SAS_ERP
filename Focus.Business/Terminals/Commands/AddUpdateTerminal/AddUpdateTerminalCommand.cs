using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Terminals.Commands.AddUpdateTerminal
{
    public class AddUpdateTerminalCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public Guid? AccountId { get; set; }
        public string Code { get; set; }
        public string MACAddress { get; set; }
        public string IPAddress { get; set; }
        public bool isActive { get; set; }
        public Guid? CashAccountId { get; set; }
        public Guid? PosTerminalId { get; set; }
        public string PrinterName { get; set; }
        public TerminalType TerminalType { get; set; }
        public bool OnPageLoadItem { get; set; }
        public bool OverWrite { get; set; }
        public ICollection<Guid> CategoryId { get; set; }

        public Guid? CompanyId { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string BusinessNameEnglish { get; set; }
        public string BusinessTypeEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public string BusinessNameArabic { get; set; }
        public string BusinessTypeArabic { get; set; }
        public string BusinessLogo { get; set; }
        public TerminalUserType TerminalUserType { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateTerminalCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateTerminalCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateTerminalCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.Id != Guid.Empty)
                    {
                        //Data already exist ; Update data
                        //get data from database using id
                        var terminals = _context.Terminals.FirstOrDefault(x => x.Id == request.Id);
                        if (request.CompanyId != null)
                        {
                            _context.DisableTenantFilter = true;
                            terminals = _context.Terminals.FirstOrDefault(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId && x.Id == request.Id);
                        }
                        

                        var isTerminalsCodeExist = await _context.Terminals
                            .Where(x => x.Code == request.Code)
                            .AnyAsync(cancellationToken);

                        if (terminals == null)
                            throw new NotFoundException(request.Code, request.Id);

                        if (terminals.Code != request.Code && isTerminalsCodeExist)
                            return Guid.Empty;

                        terminals.Code = request.Code;
                        terminals.AccountId = request.AccountId;
                        terminals.PosTerminalId = request.PosTerminalId;
                        terminals.MACAddress = request.MACAddress;
                        terminals.IPAddress = request.IPAddress;
                        terminals.IsActive = request.isActive;
                        terminals.PrinterName = request.PrinterName;
                        terminals.TerminalType = request.TerminalType;
                        terminals.OnPageLoadItem = request.OnPageLoadItem;
                        terminals.CompanyNameEnglish = request.CompanyNameEnglish;
                        terminals.BusinessNameEnglish = request.BusinessNameEnglish;
                        terminals.BusinessTypeEnglish = request.BusinessTypeEnglish;
                        terminals.CompanyNameArabic = request.CompanyNameArabic;
                        terminals.BusinessNameArabic = request.BusinessNameArabic;
                        terminals.BusinessTypeArabic = request.BusinessTypeArabic;
                        terminals.TerminalUserType = request.TerminalUserType;
                        if (request.CompanyId != null)
                        {
                            terminals.BusinessLogo = request.BusinessLogo;

                        }
                        terminals.OverWrite = request.OverWrite;

                        _context.TerminalCategories.RemoveRange(terminals.TerminalCategories);

                        if (request.CategoryId != null)
                        {
                            foreach (var id in request.CategoryId)
                            {
                                var terminalCategory = new TerminalCategory()
                                {
                                    CategoryId = id,
                                    TerminalId = terminals.Id,
                                };
                                await _context.TerminalCategories.AddAsync(terminalCategory, cancellationToken);
                            }
                        }
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
                                //       BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        //Save changes to database

                        await _context.SaveChangesAsync(cancellationToken);
                        if (request.CompanyId != null)
                        {
                            _context.SetModified(terminals, "CompanyId", (Guid)request.CompanyId);
                            var terminalCategories =
                                await _context.TerminalCategories.Where(x=>x.TerminalId == terminals.Id).ToListAsync(cancellationToken: cancellationToken);
                            foreach (var t in terminalCategories)
                            {
                                var terminalCategory = terminalCategories.FirstOrDefault(x => x.Id == t.Id);

                                _context.SetModified(terminalCategory, "CompanyId", (Guid)request.CompanyId);
                            }

                            _context.SaveChangesAfter();
                        }
                        // Return Department id after successfully updating data
                        return terminals.Id;
                        //Check Department exist

                    }
                    else
                    {
                        //New User Added
                        if (request.CompanyId != null)
                        {
                            _context.DisableTenantFilter = true;
                        }
                        var terminals = _context.Terminals.AsNoTracking().AsQueryable();
                        //Check Department name is already exists
                        var isTerminalCodeExist = await terminals
                            .Where(x => x.Code == request.Code)
                            .AnyAsync(cancellationToken);

                        var cashAccount = await _context.Accounts.AsNoTracking().Include(x => x.CostCenter).OrderBy(x => x.CostCenter.Code == "101000").LastOrDefaultAsync(cancellationToken);
                        var terminalCashAccount = (Account)null;
                        if (request.TerminalType == TerminalType.CashCounter)
                        {
                            terminalCashAccount = new Account
                            {
                                IsActive = true,
                                Name = "Counter Cash" + " " + (terminals.Count() + 1),
                                Code = request.Code,
                                CostCenterId = cashAccount.CostCenterId,
                            };
                            await _context.Accounts.AddAsync(terminalCashAccount, cancellationToken);
                        }




                        if (!isTerminalCodeExist)
                        {
                            // Create a object of Department class that made in entity

                            var terminal = new Terminal
                            {
                                Code = request.Code,
                                MACAddress = request.MACAddress,
                                IPAddress = request.IPAddress,
                                AccountId = request.AccountId,
                                PosTerminalId = request.PosTerminalId,
                                CashAccountId = terminalCashAccount?.Id,
                                IsActive = request.isActive,
                                PrinterName = request.PrinterName,
                                TerminalType = request.TerminalType,
                                OnPageLoadItem = request.OnPageLoadItem,
                                CompanyNameEnglish = request.CompanyNameEnglish,
                                BusinessNameEnglish = request.BusinessNameEnglish,
                                BusinessTypeEnglish = request.BusinessTypeEnglish,
                                CompanyNameArabic = request.CompanyNameArabic,
                                BusinessNameArabic = request.BusinessNameArabic,
                                BusinessTypeArabic = request.BusinessTypeArabic,
                                BusinessLogo = request.BusinessLogo,
                                OverWrite = request.OverWrite,
                                TerminalUserType = request.TerminalUserType
                            };

                            //Add Department to database
                            await _context.Terminals.AddAsync(terminal, cancellationToken);

                            if (request.CategoryId != null)
                            {
                                foreach (var id in request.CategoryId)
                                {
                                    var terminalCategory = new TerminalCategory()
                                    {
                                        CategoryId = id,
                                        TerminalId = terminal.Id,
                                    };
                                    await _context.TerminalCategories.AddAsync(terminalCategory, cancellationToken);
                                }
                            }
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
                                    //       BranchId = branch.Id,
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
                            return terminal.Id;
                        }

                        return Guid.Empty;
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Department Code Already Exist");
                }
            }
        }

    }
}
