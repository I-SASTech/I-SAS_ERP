using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.ChequeBooks.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ChequeBooks.ChequeAndGurantee
{
    public class AddUpdateChequeGurantee : IRequest<Guid>
    {
        //Get all variable in entity
        public ChequeBookItemLookUpModel ChequeBook { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateChequeGurantee, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateChequeGurantee> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateChequeGurantee request, CancellationToken cancellationToken)
            {

                try
                {

                    //Cheque Book Item
                    var chequebook = await Context.ChequeBookItems
                         .AsNoTracking()
                          .FirstOrDefaultAsync(x => x.Id == request.ChequeBook.Id);
                    // Cheque Book
                    var cheques = Context.ChequeBooks.AsNoTracking().FirstOrDefault(x => x.Id == chequebook.ChequeBookId);

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Banks
                    var banks = Context.Banks.AsNoTracking().FirstOrDefault(x => x.Id == request.ChequeBook.BankId);
                    if (banks == null)
                        throw new NotFoundException("No Bank Found", null);

                    if (request.ChequeBook.IssuedTo == Guid.Empty || request.ChequeBook.IssuedTo == null)
                    {



                        var expenseCostCenter = Context.Accounts.AsNoTracking()
                                .Include(x => x.CostCenter).OrderBy(x => x.Code).LastOrDefault(x => x.CostCenter.Code == "200000");
                        var issuerAccount = new Account
                        {
                            Code = (Convert.ToInt32(expenseCostCenter.Code) + 1).ToString(),
                            Name = request.ChequeBook.IssuedToName + " " + "Cheque Issuer Account",
                            NameArabic = request.ChequeBook.IssuedToName + " " + "حساب مصدر الشيك",
                            Description = request.ChequeBook.IssuedToName + " " + "Cheque Issue Account",
                            IsActive = true,
                            CostCenterId = expenseCostCenter.CostCenterId
                        };
                        await Context.Accounts.AddAsync(issuerAccount, cancellationToken);


                        var issued = new IssuedTo
                        {
                            Name = request.ChequeBook.IssuedToName,
                            AccountId = issuerAccount.Id

                        };
                        request.ChequeBook.IssuerAccount = issuerAccount.Id;
                        await Context.Issueds.AddAsync(issued, cancellationToken);
                    }


                    var accounts = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "10510001");
                    if (accounts == null)
                    {
                        var reservedAccount = Context.Accounts.AsNoTracking()
                          .Include(x => x.CostCenter).OrderBy(x => x.CostCenter.Code == "105000").LastOrDefault();
                        var reserved = new Account
                        {
                            Code = "10510001",
                            Name = "Reserved Bank Account",
                            NameArabic = "Reserved Bank Account",
                            Description = "Reserved Bank Account",
                            IsActive = true,
                            CostCenterId = reservedAccount.CostCenterId
                        };
                        await Context.Accounts.AddAsync(reserved, cancellationToken);
                        request.ChequeBook.ReservedAccount = reserved.Id;

                    }
                    else
                    {
                        request.ChequeBook.ReservedAccount = accounts.Id;
                    }




                    if (request.ChequeBook.AttachmentList != null)
                    {
                        var attachments = Context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == request.ChequeBook.Id)
                            .AsQueryable();

                        if (attachments.Any())
                        {
                            Context.Attachments.RemoveRange(attachments);
                        }

                        foreach (var item in request.ChequeBook.AttachmentList)
                        {
                            var attachment = new Attachment()
                            {
                                Date = item.Date,
                                DocumentId = request.ChequeBook.Id,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            };
                            //Add Attachments to database
                            await Context.Attachments.AddAsync(attachment, cancellationToken);

                        }
                    }
                     if ((request.ChequeBook.CashType == Domain.Enum.CashType.Reserved || request.ChequeBook.ChequeType == Domain.Enum.ChequeType.Normal || request.ChequeBook.ChequeType == Domain.Enum.ChequeType.Advance) && ((request.ChequeBook.StatusType == Domain.Enum.StatusType.Blocked || request.ChequeBook.StatusType == Domain.Enum.StatusType.Cancelled || request.ChequeBook.StatusType == Domain.Enum.StatusType.Returned)) && request.ChequeBook.IsCashed)
                    {

                        var reservedReverseTransaction = new Transaction()
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Reverse  Amount in reserved ACCount to " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = request.ChequeBook.ReservedAccount,
                            TransactionType = TransactionType.Reserved,
                            Credit = 0m,
                            Debit = request.ChequeBook.Amount
                        };
                        Context.Transactions.Add(reservedReverseTransaction);

                        var reservedbankTransaction = new Transaction()
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Bank  Amount in reserved ACCount to " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = banks.AccountId,
                            TransactionType = TransactionType.Reserved,
                            Credit = request.ChequeBook.Amount,
                            Debit = 0m
                        };


                        Context.Transactions.Add(reservedbankTransaction);
                        chequebook.IsPaid = true;


                    }


                    else if ((request.ChequeBook.CashType == Domain.Enum.CashType.Reserved || request.ChequeBook.ChequeType == Domain.Enum.ChequeType.Normal || request.ChequeBook.ChequeType == Domain.Enum.ChequeType.Advance) && request.ChequeBook.StatusType == Domain.Enum.StatusType.Cashed && request.ChequeBook.IsCashed)
                    {

                        var list = new List<Transaction>();


                        //Issuer
                        list.Add(new Transaction
                        {

                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Cashed" + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = chequebook.IssuerAccount,
                            TransactionType = TransactionType.Reserved,
                            Credit = 0m,
                            Debit = request.ChequeBook.Amount

                        });
                        //Reserved
                        list.Add(new Transaction
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Reserved Amount to " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = request.ChequeBook.ReservedAccount,
                            TransactionType = TransactionType.Reserved,
                            Credit = request.ChequeBook.Amount,
                            Debit = 0m

                        });

                     


                        await Context.Transactions.AddRangeAsync(list, cancellationToken);

                        chequebook.IsPaid = true;


                    }





                    else if ((request.ChequeBook.CashType == Domain.Enum.CashType.Reserved || request.ChequeBook.ChequeType == Domain.Enum.ChequeType.Normal) && request.ChequeBook.StatusType == Domain.Enum.StatusType.Cashed)
                    {

                        var list = new List<Transaction>();

                        //Reserved
                        list.Add(new Transaction
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Reserved Amount to " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = request.ChequeBook.ReservedAccount,
                            TransactionType = TransactionType.Reserved,
                            Credit = 0M,
                            Debit = request.ChequeBook.Amount

                        });
                        //Bank
                        list.Add(new Transaction
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Bank Amount" + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = banks.AccountId,
                            TransactionType = TransactionType.Reserved,
                            Credit = request.ChequeBook.Amount,
                            Debit = 0m

                        });
                        //Issuer
                        list.Add(new Transaction
                        {

                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Cashed" + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = request.ChequeBook.IssuerAccount,
                            TransactionType = TransactionType.Reserved,
                            Credit = 0m,
                            Debit = request.ChequeBook.Amount

                        });
                        //Reserved
                        list.Add(new Transaction
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Reserved Amount to " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = request.ChequeBook.ReservedAccount,
                            TransactionType = TransactionType.Reserved,
                            Credit = request.ChequeBook.Amount,
                            Debit = 0m

                        });

                       


                        await Context.Transactions.AddRangeAsync(list, cancellationToken);


                        chequebook.IsPaid = true;
                       


                    }
                    else if ((request.ChequeBook.CashType == Domain.Enum.CashType.Reserved || request.ChequeBook.ChequeType == Domain.Enum.ChequeType.Normal ) && (request.ChequeBook.StatusType == Domain.Enum.StatusType.Blocked || request.ChequeBook.StatusType == Domain.Enum.StatusType.Cancelled || request.ChequeBook.StatusType == Domain.Enum.StatusType.Returned))
                    {

                        var list = new List<Transaction>();

                        //Reserved
                        list.Add(new Transaction
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Reserved Amount to " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = request.ChequeBook.ReservedAccount,
                            TransactionType = TransactionType.Reserved,
                            Credit = request.ChequeBook.Amount,
                            Debit = 0m

                        }); 
                        //Bank
                        list.Add(new Transaction
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Bank Amount" + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = banks.AccountId,
                            TransactionType = TransactionType.Reserved,
                            Credit = 0M,
                            Debit = request.ChequeBook.Amount

                        });
                        //Reverse in Bank Account
                        list.Add(new Transaction
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Bank  Amount in reverserd ACCount to " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = banks.AccountId,
                            TransactionType = TransactionType.Reserved,
                            Credit = request.ChequeBook.Amount,
                            Debit = 0m

                        });
                        //Reverse in Reserved Account
                        list.Add(new Transaction
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Reverse  Amount in reverserd ACCount to " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = request.ChequeBook.ReservedAccount,
                            TransactionType = TransactionType.Reserved,
                            Credit = 0m,
                            Debit = request.ChequeBook.Amount

                        });
                      


                        await Context.Transactions.AddRangeAsync(list, cancellationToken);

                        chequebook.IsPaid = true;
                       

                    }
                    else if (request.ChequeBook.CashType == Domain.Enum.CashType.NotReserved && request.ChequeBook.StatusType == Domain.Enum.StatusType.Cashed)
                    {



                        var reservedTransaction = new Transaction()
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Bank Account Not Reserverd and Cashed to  " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = banks.AccountId,
                            TransactionType = TransactionType.NotReserved,
                            Credit = request.ChequeBook.Amount,
                            Debit = 0m
                        };

                        Context.Transactions.Add(reservedTransaction);

                        var issuerTransaction = new Transaction()
                        {
                            DocumentId = request.ChequeBook.Id.Value,
                            DocumentNumber = chequebook.Code,
                            Date = DateTime.UtcNow,
                            DocumentDate = request.ChequeBook.Date,
                            ApprovalDate = DateTime.UtcNow,
                            Description = "Cashed to this Account " + " " + request.ChequeBook.IssuedToName,
                            Year = DateTime.UtcNow.ToString("yyyy"),
                            AccountId = request.ChequeBook.IssuerAccount,
                            TransactionType = TransactionType.NotReserved,
                            Credit = 0M,
                            Debit = request.ChequeBook.Amount
                        };
                        Context.Transactions.Add(issuerTransaction);
                        chequebook.IsPaid = true;
                       


                    }
                    else if ((request.ChequeBook.CashType == Domain.Enum.CashType.Reserved ||  request.ChequeBook.ChequeType == Domain.Enum.ChequeType.Normal ) && !request.ChequeBook.IsCashed)
                    {
                        if(request.ChequeBook.ChequeType == Domain.Enum.ChequeType.Normal)
                        {
                            request.ChequeBook.CashType = Domain.Enum.CashType.Reserved;
                        }



                        var bankAmount = Context.Transactions.Where(x => x.AccountId == banks.AccountId)
                     .Sum(x => x.Debit - x.Credit);
                        if (bankAmount < 0)
                        {
                            bankAmount = bankAmount * -1;
                        }
                        if (bankAmount >= request.ChequeBook.Amount)
                        {

                            var reservedTransaction = new Transaction()
                            {
                                DocumentId = request.ChequeBook.Id.Value,
                                DocumentNumber = chequebook.Code,
                                Date = DateTime.UtcNow,
                                DocumentDate = request.ChequeBook.Date,
                                ApprovalDate = DateTime.UtcNow,
                                Description = "Reserved Amount  to " + "" + request.ChequeBook.IssuedToName,
                                Year = DateTime.UtcNow.ToString("yyyy"),
                                AccountId = request.ChequeBook.ReservedAccount,
                                TransactionType = TransactionType.Reserved,
                                Credit = 0m,
                                Debit = request.ChequeBook.Amount
                            };

                            Context.Transactions.Add(reservedTransaction);

                            var issuerTransaction = new Transaction()
                            {
                                DocumentId = request.ChequeBook.Id.Value,
                                DocumentNumber = chequebook.Code,
                                Date = DateTime.UtcNow,
                                DocumentDate = request.ChequeBook.Date,
                                ApprovalDate = DateTime.UtcNow,
                                Description = "Bank Amount to " + " " + request.ChequeBook.IssuedToName,
                                Year = DateTime.UtcNow.ToString("yyyy"),
                                AccountId = banks.AccountId,
                                TransactionType = TransactionType.Reserved,
                                Credit = request.ChequeBook.Amount,
                                Debit = 0
                            };
                            Context.Transactions.Add(issuerTransaction);
                            chequebook.IsCashed = true;
                           

                        }
                        else
                        {
                            throw new NotFoundException("There is No Balance or Your amount exceed the Bank Balance", null);
                        }

                    }







                    chequebook.ChequeTypeDate = request.ChequeBook.ChequeTypeDate;
                    chequebook.BookNo = request.ChequeBook.BookNo;
                    chequebook.SerialNo = request.ChequeBook.SerialNo;
                    chequebook.BankNo = request.ChequeBook.BankNo;
                    chequebook.ChequeNo = request.ChequeBook.ChequeNo;
                    chequebook.IsActive = true;
                    chequebook.IsUsed = true;
                    chequebook.Date = request.ChequeBook.Date;
                    chequebook.BankId = request.ChequeBook.BankId;
                    chequebook.ChequeDate = request.ChequeBook.ChequeDate;
                    chequebook.AlertDate = request.ChequeBook.AlertDate;
                    chequebook.StatusDate = request.ChequeBook.StatusDate;
                    chequebook.ValidityDate = request.ChequeBook.ValidityDate;
                    chequebook.IssuedTo = request.ChequeBook.IssuedTo;
                    chequebook.Remarks = request.ChequeBook.Remarks;
                    chequebook.IssuedToName = request.ChequeBook.IssuedToName;
                    chequebook.ShortDetail = request.ChequeBook.ShortDetail;
                    chequebook.Amount = request.ChequeBook.Amount;
                    chequebook.ChequeType = request.ChequeBook.ChequeType;
                    chequebook.StatusType = request.ChequeBook.StatusType;
                    chequebook.CashType = request.ChequeBook.CashType;
                    chequebook.ChequeBookId = request.ChequeBook.ChequeBookId;
                    chequebook.IssuerAccount = request.ChequeBook.IssuerAccount;

                    Context.ChequeBookItems.Update(chequebook);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = Context.SyncLog(),
                        Code = _code,
                        DocumentNo=chequebook.Code
                    }, cancellationToken);

                    //Save changes to database
                    await Context.SaveChangesAsync(cancellationToken);

                    return chequebook.Id;


                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
