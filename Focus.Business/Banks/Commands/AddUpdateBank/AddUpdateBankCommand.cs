using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
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

namespace Focus.Business.Banks.Commands.AddUpdateBank
{
    public class AddUpdateBankCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string BankName { get; set; }
        public string ShortName { get; set; }
        public string NameArabic { get; set; }
        public string Code { get; set; }
        public string IsActive { get; set; }
        public string AccoutName { get; set; }
        public Guid AccountId { get; set; }
        public string BranchName { get; set; }
        public string AccountNumber { get; set; }
        public string IBNNumber { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactName { get; set; }
        public string ManagerName { get; set; }
        public string ManagerContectualNumber { get; set; }
        public string AccounType { get; set; }
        public string AccoutNameArabic { get; set; }
        public Guid? CurrencyId { get; set; }
        public string BranchCode { get; set; }
        public string BranchAddress { get; set; }
        public string SwiftCode { get; set; }
        public bool Active { get; set; }
        public string Reference { get; set; }
        public string AccountType { get; set; }


        public class Handler : IRequestHandler<AddUpdateBankCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateBankCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateBankCommand request, CancellationToken cancellationToken)
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
                        var banks = await _context.Banks.FindAsync(request.Id);
                        if (banks == null)
                            throw new NotFoundException(request.BankName, request.Id);


                        if (request.BankName != "" && request.NameArabic != "")
                        {
                            //Check Department name is already exists
                            //var isBankNameExist = await Context.Banks.FirstOrDefaultAsync(x => x.BankName == request.BankName || x.NameArabic == request.NameArabic, cancellationToken);
                            //if (banks.BankName != request.BankName && isBankNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Bank " + request.BankName + " Already Exist");
                            //if (banks.NameArabic != request.NameArabic && isBankNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Bank " + request.NameArabic + " Already Exist");
                        }
                        if (request.AccountNumber != "")
                        {
                            //Check Department name is already exists
                            var isBankNameExist = await _context.Banks.FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber, cancellationToken);
                            if (banks.AccountNumber != request.AccountNumber && isBankNameExist != null)
                                throw new ObjectAlreadyExistsException("Bank Account No  " + request.AccountNumber + " Already Exist");
                        }
                        else
                        {
                            //Check Department name is already exists
                            //var isBankNameExist = await Context.Banks.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            //if (banks.NameArabic != request.NameArabic && isBankNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Bank " + request.NameArabic + " Already Exist");
                        }


                        var account = await _context.Accounts.FindAsync(banks.AccountId);
                        if (account == null)
                            throw new NotFoundException(request.BankName, request.Id);

                        account.Name = request.AccoutName;
                        account.Description = request.BranchName + "" + request.AccountNumber;

                        banks.AccountType = request.AccountType;
                        banks.ShortName = request.ShortName;
                        banks.BankName = request.BankName;
                        banks.NameArabic = request.NameArabic;
                        banks.AccountId = banks.AccountId;
                        banks.BranchName = request.BranchName;
                        banks.AccountNumber = request.AccountNumber;
                        banks.AccoutNameArabic = request.AccoutNameArabic;
                        banks.IBNNumber = request.IBNNumber;
                        banks.AccounType = request.AccounType;
                        banks.ManagerContectualNumber = request.ManagerContectualNumber;
                        banks.ManagerName = request.ManagerName;
                        banks.ContactName = request.ContactName;
                        banks.ContactPerson = request.ContactPerson;
                        banks.Location = request.Location;
                        banks.AccoutName = request.AccoutName;
                        banks.IsActive = request.IsActive;
                        banks.CurrencyId = request.CurrencyId;
                        banks.BranchCode = request.BranchCode;
                        banks.BranchAddress = request.BranchAddress;
                        banks.SwiftCode = request.SwiftCode;
                        banks.Active = request.Active;
                        banks.Reference = request.Reference;

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
                            IsServer=true
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Department id after successfully updating data
                        return banks.Id;
                        //Check Department exist

                    }
                    else
                    {
                        //New User Added

                        if (request.BankName != "" && request.NameArabic != "")
                        {
                            ////Check Department name is already exists
                            //var isBankNameExist = await Context.Banks.FirstOrDefaultAsync(x => x.BankName == request.BankName || x.NameArabic == request.NameArabic, cancellationToken);
                            //if (isBankNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Bank " + request.BankName +" " + request.NameArabic + " Already Exist");
                        }
                        if (request.AccountNumber != "")
                        {
                            //Check Department name is already exists
                            var isBankNameExist = await _context.Banks.FirstOrDefaultAsync(x => x.AccountNumber == request.AccountNumber, cancellationToken);
                            if (isBankNameExist != null)
                                throw new ObjectAlreadyExistsException("Bank Account No " + request.AccountNumber + " Already Exist");
                        }
                        else
                        {
                            ////Check Department name is already exists
                            //var isBankNameExist = await Context.Banks.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            //if (isBankNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Bank " + request.NameArabic + " Already Exist");
                        }




                        var account = new Account();

                        var accountCount = await _context.Accounts.Include(x => x.CostCenter).Where(x => x.CostCenter.Code == "105000").OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);

                        if (accountCount != null)
                        {
                            account = new Account
                            {
                                Name = request.BankName,
                                NameArabic = request.NameArabic,
                                Description = request.BranchName + " " + request.AccoutName + " " + request.AccountNumber,
                                Code = (int.Parse(accountCount.Code) + 1).ToString(),
                                CostCenterId = accountCount.CostCenterId,
                                IsActive = true
                            };

                        }
                        await _context.Accounts.AddAsync(account, cancellationToken);
                        if (accountCount != null)
                        {
                            var bank = new Bank
                            {
                                AccountType = request.AccountType,
                                ShortName = request.ShortName,
                                BankName = request.BankName,
                                NameArabic = request.NameArabic,
                                Code = request.Code,
                                IsActive = request.IsActive,
                                AccoutName = request.AccoutName,
                                AccoutNameArabic = request.AccoutNameArabic,
                                Location = request.Location,
                                ContactPerson = request.ContactPerson,
                                ContactName = request.ContactName,
                                ManagerName = request.ManagerName,
                                ManagerContectualNumber = request.ManagerContectualNumber,
                                AccounType = request.AccounType,
                                AccountId = account.Id,
                                BranchName = request.BranchName,
                                AccountNumber = request.AccountNumber,
                                IBNNumber = request.IBNNumber,
                                CurrencyId = request.CurrencyId,
                                SwiftCode = request.SwiftCode,
                                BranchAddress = request.BranchAddress,
                                BranchCode = request.BranchCode,
                                Active = request.Active,
                                Reference = request.Reference,
                            };

                            //Add Department to database
                            await _context.Banks.AddAsync(bank, cancellationToken);

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
                                IsServer=true
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return bank.Id;
                        }


                        return Guid.Empty;
                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
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
