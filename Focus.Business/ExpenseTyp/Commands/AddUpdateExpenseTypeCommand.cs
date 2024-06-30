using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ExpenseTyp.Commands
{
    public class AddUpdateExpenseTypeCommand : IRequest<Guid>
    {
        public ExpenseTypeLookUpModel ExpenseType { get; set; }
        public class Handler : IRequestHandler<AddUpdateExpenseTypeCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateExpenseTypeCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateExpenseTypeCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Update
                    if (request.ExpenseType.Id != Guid.Empty)
                    {

                        var account = _context.LedgerAccounts.FirstOrDefault(x => x.AccountLedgerId == request.ExpenseType.Id);
                        if (account != null)
                        {
                            account.IsActive = request.ExpenseType.Status;
                            account.Name = request.ExpenseType.ExpenseTypeName;
                            account.NameArabic = request.ExpenseType.ExpenseTypeArabic;
                            _context.LedgerAccounts.Update(account);
                        }
                        var colors = await _context.ExpenseTypes.FindAsync(request.ExpenseType.Id);
                        if (colors == null)
                            throw new NotFoundException("Expense Types", request.ExpenseType.ExpenseTypeName);
                        
                        colors.ExpenseTypeName = request.ExpenseType.ExpenseTypeName;
                        colors.ExpenseTypeArabic = request.ExpenseType.ExpenseTypeArabic;
                        colors.AccountId = request.ExpenseType.AccountId;
                        colors.Description = request.ExpenseType.Description;
                        //colors.Ac = request.ExpenseType.ExpenseTypeArabic;
                        colors.Status = request.ExpenseType.Status;

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
                                //          BranchId = branch.Id,
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

                        // Return Department id after successfully updating data
                        return colors.Id;
                        //Check Department exist

                    }
                    else
                    {
                        //Add New
                        //if (request.Name != "" && request.NameArabic != "")
                        //{
                        //    var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);

                        //    if (isColorNameExist != null)
                        //        throw new ObjectAlreadyExistsException("Color " + request.Name + " " + request.NameArabic + " Already Exist");
                        //}
                        //else if (request.Name != "")
                        //{
                        //    var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

                        //    if (isColorNameExist != null)
                        //        throw new ObjectAlreadyExistsException("Color " + request.Name + " Already Exist");
                        //}
                        //else
                        //{
                        //    var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);

                        //    if (isColorNameExist != null)
                        //        throw new ObjectAlreadyExistsException("Color " + request.NameArabic + " Already Exist");
                        //}

                        var expenseType = new ExpenseType
                        {
                            ExpenseTypeName = request.ExpenseType.ExpenseTypeName,
                            ExpenseTypeArabic = request.ExpenseType.ExpenseTypeArabic,
                            AccountId = request.ExpenseType.AccountId,
                            Description = request.ExpenseType.Description,
                            Status = request.ExpenseType.Status,
                        };

                        //Add Department to database
                        await _context.ExpenseTypes.AddAsync(expenseType, cancellationToken);


                        var ledgerAccount = new LedgerAccount
                        {
                            Name = request.ExpenseType.ExpenseTypeName,
                            NameArabic = request.ExpenseType.ExpenseTypeArabic,
                            AccountId = request.ExpenseType.AccountId,
                            AccountLedgerId = expenseType.Id,
                            Description = request.ExpenseType.Description,
                            IsActive = request.ExpenseType.Status,
                            AccountLeaderType = AccountLeaderType.ExpenseType,
                        };

                        //Add Department to database
                        await _context.LedgerAccounts.AddAsync(ledgerAccount, cancellationToken);

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
                                Code = _code
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return expenseType.Id;
                    }
                    

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
