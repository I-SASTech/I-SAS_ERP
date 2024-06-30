using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherNumber;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.MultiUps.Commands.AddMultiUp
{
    public class AddUpdateMultiUpCommand : IRequest<Message>
    {
        //Get all variable in entity
        public MultiUpLookUp MultiUp { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateMultiUpCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            private readonly IMediator _mediator;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateMultiUpCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;

            }


            public async Task<Message> Handle(AddUpdateMultiUpCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.MultiUp.Type == "Update")
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var multiUps = await Context.MultiUps.FindAsync(request.MultiUp.Id);

                        if (multiUps == null)
                            throw new NotFoundException("Good Receive", request.MultiUp.RegistrationNo);

                        if (request.MultiUp.IsReturn)
                        {
                            request.MultiUp.IsReturn = true;
                            request.MultiUp.IsCashed = false;
                        }
                        else if (request.MultiUp.IsPrint)
                        {
                            if (multiUps.IsPrint == false)
                            {
                                request.MultiUp.Discount = request.MultiUp.CashAmount;
                                request.MultiUp.Status = "Cash Received";
                                request.MultiUp.IsCashed = true;
                                request.MultiUp.IsComplete = true;
                                if (request.MultiUp.CustomerId != null)
                                {
                                    var contacts = Context.Contacts.AsNoTracking().FirstOrDefault(x => x.Id == request.MultiUp.CustomerId);
                                    var accounts = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "10100001");
                                    if (contacts != null && accounts != null)
                                    {
                                        var voucherNumber = await _mediator.Send(new GetPaymentVoucherNumberQuery
                                        {
                                            PaymentVoucherType = PaymentVoucherType.BankReceipt
                                        }, cancellationToken);
                                        var paymentVoucher = new PaymentVoucher
                                        {
                                            Date = request.MultiUp.Date,
                                            VoucherNumber = voucherNumber,
                                            PaymentVoucherType = PaymentVoucherType.BankReceipt,
                                            ApprovalStatus = ApprovalStatus.Approved,
                                            MultiUpId = multiUps.Id,
                                            PaymentMode = SalePaymentType.Cash,
                                            ContactAccountId = contacts.AccountId.Value,
                                            BankCashAccountId = accounts.Id,
                                            Amount = multiUps.CashAmount,
                                            BranchId = multiUps.BranchId,

                                        };
                                        await Context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);


                                        var period = await Context.CompanySubmissionPeriods
                        .FirstOrDefaultAsync(x => x.PeriodStart <= request.MultiUp.Date.Date
                                                  && x.PeriodEnd >= request.MultiUp.Date.Date, cancellationToken: cancellationToken);
                                        var list = new List<Transaction>();

                                        var sales = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "42000001");


                                        list.Add(new Transaction
                                        {


                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = paymentVoucher.VoucherNumber,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = DateTime.UtcNow,
                                            ApprovalDate = DateTime.UtcNow,
                                            Description = paymentVoucher.Narration,
                                            Year = DateTime.UtcNow.Year.ToString(),
                                            AccountId = paymentVoucher.ContactAccountId,
                                            TransactionType = TransactionType.SaleInvoice,
                                            Debit = request.MultiUp.CashAmount,
                                            Credit = 0m,
                                            PeriodId = period.Id,
                                            BranchId = multiUps.BranchId,





                                        });
                                        list.Add(new Transaction
                                        {

                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = paymentVoucher.VoucherNumber,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = DateTime.UtcNow,
                                            ApprovalDate = DateTime.UtcNow,
                                            Description = paymentVoucher.Narration,
                                            Year = DateTime.UtcNow.Year.ToString(),
                                            AccountId = sales.Id,
                                            TransactionType = TransactionType.SaleInvoice,
                                            Debit = 0m,
                                            Credit = request.MultiUp.CashAmount,
                                            PeriodId = period.Id,
                                            BranchId = multiUps.BranchId,
                                        });

                                        list.Add(new Transaction
                                        {


                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = paymentVoucher.VoucherNumber,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = DateTime.UtcNow,
                                            ApprovalDate = DateTime.UtcNow,
                                            Description = paymentVoucher.Narration,
                                            Year = DateTime.UtcNow.Year.ToString(),
                                            AccountId = paymentVoucher.BankCashAccountId,
                                            TransactionType = TransactionType.SaleInvoice,
                                            Debit = request.MultiUp.CashAmount,
                                            Credit = 0m,
                                            PeriodId = period.Id,
                                            BranchId = multiUps.BranchId,





                                        });
                                        list.Add(new Transaction
                                        {

                                            DocumentId = paymentVoucher.Id,
                                            DocumentNumber = paymentVoucher.VoucherNumber,
                                            Date = DateTime.UtcNow,
                                            DocumentDate = DateTime.UtcNow,
                                            ApprovalDate = DateTime.UtcNow,
                                            Description = paymentVoucher.Narration,
                                            Year = DateTime.UtcNow.Year.ToString(),
                                            AccountId = paymentVoucher.ContactAccountId,
                                            TransactionType = TransactionType.SaleInvoice,
                                            Debit = 0m,
                                            Credit = request.MultiUp.CashAmount,
                                            PeriodId = period.Id,
                                            BranchId = multiUps.BranchId,
                                        });

                                        foreach (var transaction in list)
                                        {
                                            await Context.Transactions.AddAsync(transaction, cancellationToken);
                                        }


                                    }

                                }

                            }
                            else
                            {
                                if (multiUps.AdvanceAmount != multiUps.CashAmount)
                                {
                                    if (request.MultiUp.CustomerId != null)
                                    {

                                        var contacts = Context.Contacts.AsNoTracking().FirstOrDefault(x => x.Id == request.MultiUp.CustomerId);
                                        var accounts = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "10100001");
                                        if (contacts != null && accounts != null)
                                        {
                                            var voucherNumber = await _mediator.Send(new GetPaymentVoucherNumberQuery
                                            {
                                                PaymentVoucherType = PaymentVoucherType.CashReceipt
                                            }, cancellationToken);
                                            var paymentVoucher = new PaymentVoucher
                                            {
                                                Date = request.MultiUp.Date,
                                                VoucherNumber = voucherNumber,
                                                PaymentVoucherType = PaymentVoucherType.CashPay,
                                                ApprovalStatus = ApprovalStatus.Approved,
                                                ReparingOrderId = multiUps.Id,
                                                PaymentMode = SalePaymentType.Cash,
                                                ContactAccountId = contacts.AccountId.Value,
                                                BankCashAccountId = accounts.Id,
                                                Amount = request.MultiUp.AdvanceAmount,
                                                BranchId = multiUps.BranchId,

                                            };
                                            await Context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);


                                            var period = await Context.CompanySubmissionPeriods
                            .FirstOrDefaultAsync(x => x.PeriodStart <= request.MultiUp.Date.Date
                                                      && x.PeriodEnd >= request.MultiUp.Date.Date, cancellationToken: cancellationToken);
                                            var list = new List<Transaction>();





                                            list.Add(new Transaction
                                            {


                                                DocumentId = paymentVoucher.Id,
                                                DocumentNumber = paymentVoucher.VoucherNumber,
                                                Date = DateTime.UtcNow,
                                                DocumentDate = DateTime.UtcNow,
                                                ApprovalDate = DateTime.UtcNow,
                                                Description = paymentVoucher.Narration,
                                                Year = DateTime.UtcNow.Year.ToString(),
                                                AccountId = paymentVoucher.BankCashAccountId,
                                                TransactionType = TransactionType.CashPay,
                                                Debit = request.MultiUp.CashAmount - request.MultiUp.Discount,
                                                Credit = 0m,
                                                PeriodId = period.Id,
                                                BranchId = multiUps.BranchId,





                                            });
                                            list.Add(new Transaction
                                            {

                                                DocumentId = paymentVoucher.Id,
                                                DocumentNumber = paymentVoucher.VoucherNumber,
                                                Date = DateTime.UtcNow,
                                                DocumentDate = DateTime.UtcNow,
                                                ApprovalDate = DateTime.UtcNow,
                                                Description = paymentVoucher.Narration,
                                                Year = DateTime.UtcNow.Year.ToString(),
                                                AccountId = paymentVoucher.ContactAccountId,
                                                TransactionType = TransactionType.CashPay,
                                                Debit = 0m,
                                                Credit = request.MultiUp.CashAmount - request.MultiUp.Discount,
                                                PeriodId = period.Id,
                                                BranchId = multiUps.BranchId,
                                            });
                                            request.MultiUp.Discount = request.MultiUp.CashAmount;
                                            foreach (var transaction in list)
                                            {
                                                await Context.Transactions.AddAsync(transaction, cancellationToken);
                                            }

                                        }

                                    }

                                }
                            }

                        }

                        else if (request.MultiUp.IsRepared)
                        {
                            request.MultiUp.Status = "Repared";
                        }

                        multiUps.RemaningPrice = request.MultiUp.AdvanceAmount - request.MultiUp.CashAmount;
                        multiUps.Date = request.MultiUp.Date;
                        multiUps.IsPrint = request.MultiUp.IsPrint;
                        multiUps.RegistrationNo = request.MultiUp.RegistrationNo;
                        multiUps.CustomerId = request.MultiUp.CustomerId;
                        multiUps.EmployeeId = request.MultiUp.EmployeeId;
                        multiUps.ReceivedDate = request.MultiUp.ReceivedDate;
                        multiUps.Status = request.MultiUp.Status;
                        multiUps.JobAssignId = request.MultiUp.JobAssignId;
                        multiUps.CompleteDate = request.MultiUp.CompleteDate;
                        multiUps.EstimateAmount = request.MultiUp.EstimateAmount;
                        multiUps.RegisteredById = request.MultiUp.RegisteredById;
                        multiUps.Remarks = request.MultiUp.Remarks;
                        multiUps.ExpectedDate = request.MultiUp.ExpectedDate;
                        multiUps.CradNumber = request.MultiUp.CradNumber;
                        multiUps.PurchaseDate = request.MultiUp.PurchaseDate;
                        multiUps.DealerRef = request.MultiUp.DealerRef;
                        multiUps.FaultInfo = request.MultiUp.FaultInfo;
                        multiUps.IsCollected = request.MultiUp.IsCollected;
                        multiUps.IsRepared = request.MultiUp.IsRepared;
                        multiUps.IsReturn = request.MultiUp.IsReturn;
                        multiUps.IsCashed = request.MultiUp.IsCashed;
                        multiUps.IsComplete = request.MultiUp.IsComplete;


                        multiUps.PaymentType = request.MultiUp.PaymentType;
                        multiUps.CashAmount = request.MultiUp.CashAmount;
                        multiUps.AdvanceAmount = request.MultiUp.AdvanceAmount;

                        Context.MultiUPSLineItems.RemoveRange(multiUps.MultiUPSLineItems);

                        multiUps.MultiUPSLineItems = request.MultiUp.MultiUpsLineItems.Select(x =>
                            new MultiUPSLineItem()
                            {

                                WarrantyCategoryId = x.WarrantyCategoryId,
                                UpsDescriptionId = x.UpsDescriptionId,
                                AcessoryIncludedId = x.AcessoryIncludedId,
                                ProblemId = x.ProblemId,
                                JobAssignId = x.JobAssignId,
                                CompleteDate = x.CompleteDate,
                                Status = x.IsComplete? "Complete": "In Progress",
                                SerialNo = x.SerialNo,
                                EstimateAmount = x.EstimateAmount,
                                IsComplete = x.IsComplete,



                            }).ToList();


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                            DocumentNo=multiUps.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = multiUps.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };
                        return message;



                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var multiUp = new MultiUp
                        {
                            Id = request.MultiUp.Id,
                            Date = request.MultiUp.Date,
                            RegistrationNo = request.MultiUp.RegistrationNo,
                            CustomerId = request.MultiUp.CustomerId,
                            EmployeeId = request.MultiUp.EmployeeId,
                            ReceivedDate = request.MultiUp.ReceivedDate,
                            Status = request.MultiUp.Status,
                            JobAssignId = request.MultiUp.JobAssignId,
                            CompleteDate = request.MultiUp.CompleteDate,
                            EstimateAmount = request.MultiUp.EstimateAmount,
                            RegisteredById = request.MultiUp.RegisteredById,
                            ExpectedDate = request.MultiUp.ExpectedDate,
                            Remarks = request.MultiUp.Remarks,
                            CradNumber = request.MultiUp.CradNumber,
                            PurchaseDate = request.MultiUp.PurchaseDate,
                            DealerRef = request.MultiUp.DealerRef,
                            FaultInfo = request.MultiUp.FaultInfo,
                            AdvanceAmount = request.MultiUp.AdvanceAmount,
                            CashAmount = request.MultiUp.CashAmount,
                            PaymentType = request.MultiUp.PaymentType,
                            IsComplete = request.MultiUp.IsComplete,
                            IsCashed = request.MultiUp.IsCashed,
                            IsCollected = request.MultiUp.IsCollected,
                            IsReturn = request.MultiUp.IsReturn,
                            IsRepared = request.MultiUp.IsRepared,
                            BranchId = request.MultiUp.BranchId,
                            RemaningPrice = request.MultiUp.AdvanceAmount - request.MultiUp.CashAmount,

                            MultiUPSLineItems = request.MultiUp.MultiUpsLineItems.Select(x =>
                             new MultiUPSLineItem()
                             {

                                 WarrantyCategoryId = x.WarrantyCategoryId,
                                 UpsDescriptionId = x.UpsDescriptionId,
                                 AcessoryIncludedId = x.AcessoryIncludedId,
                                 ProblemId = x.ProblemId,
                                 JobAssignId = x.JobAssignId,
                                 CompleteDate = x.CompleteDate,
                                 Status = x.IsComplete ? "Complete" : "In Progress",
                                 SerialNo = x.SerialNo,
                                 EstimateAmount = x.EstimateAmount,
                                 IsComplete = x.IsComplete,



                             }).ToList(),




                        };
                        await Context.MultiUps.AddAsync(multiUp, cancellationToken);


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                            DocumentNo=multiUp.RegistrationNo
                        }, cancellationToken);


                        await Context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = multiUp.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;



                    }

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
