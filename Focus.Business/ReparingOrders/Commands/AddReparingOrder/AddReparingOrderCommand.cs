using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

namespace Focus.Business.ReparingOrders.Commands.AddReparingOrder
{
    public class AddUpdateReparingOrderCommand : IRequest<Message>
    {
        //Get all variable in entity
        public ReparingOrderLookUp ReparingOrder { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateReparingOrderCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
          
            private string _code;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateReparingOrderCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;

            }


            public async Task<Message> Handle(AddUpdateReparingOrderCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.ReparingOrder.Type == "Update")
                    {


                        var reparingOrders = await _context.ReparingOrders.FindAsync(request.ReparingOrder.Id);

                        if (reparingOrders == null)
                            throw new NotFoundException("Good Receive", request.ReparingOrder.RegistrationNo);


                        //if (request.ReparingOrder.IsComplete)
                        //{
                        //    request.ReparingOrder.IsComplete = true;
                        //    request.ReparingOrder.IsCashed = false;
                        //}
                        if (request.ReparingOrder.IsReturn)
                        {
                            request.ReparingOrder.IsReturn = true;
                            request.ReparingOrder.IsCashed = false;
                        }
                        else if (request.ReparingOrder.IsPrint)
                        {
                            if (reparingOrders.IsPrint == false)
                            {
                                request.ReparingOrder.Discount = request.ReparingOrder.CashAmount;
                                request.ReparingOrder.Status = "Cash Received";
                                request.ReparingOrder.IsCashed = true;
                                request.ReparingOrder.IsComplete = true;
                                if (request.ReparingOrder.CustomerId != null)
                                {
                                    var contacts = _context.Contacts.AsNoTracking().FirstOrDefault(x => x.Id == request.ReparingOrder.CustomerId);
                                    var accounts = _context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "10100001");
                                    if (contacts != null && accounts != null)
                                    {
                                        var voucherNumber = await _mediator.Send(new GetPaymentVoucherNumberQuery
                                        {
                                            PaymentVoucherType = PaymentVoucherType.BankReceipt
                                        }, cancellationToken);
                                        var paymentVoucher = new PaymentVoucher
                                        {
                                            Date = request.ReparingOrder.Date,
                                            VoucherNumber = voucherNumber,
                                            PaymentVoucherType = PaymentVoucherType.BankReceipt,
                                            ApprovalStatus = ApprovalStatus.Approved,
                                            ReparingOrderId = reparingOrders.Id,
                                            PaymentMode = SalePaymentType.Cash,
                                            ContactAccountId = contacts.AccountId.Value,
                                            BankCashAccountId = accounts.Id,
                                            Amount = reparingOrders.AdvanceAmount,
                                            BranchId = reparingOrders.BranchId,

                                        };
                                        await _context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);


                                        var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= request.ReparingOrder.Date.Date
                                                  && x.PeriodEnd >= request.ReparingOrder.Date.Date, cancellationToken: cancellationToken);
                                        var list = new List<Transaction>();

                                        var sales = _context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "42000001");


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
                                            Debit = request.ReparingOrder.AdvanceAmount,
                                            Credit = 0m,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
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
                                            Credit = request.ReparingOrder.AdvanceAmount,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
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
                                            Debit = request.ReparingOrder.CashAmount,
                                            Credit = 0m,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
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
                                            Credit = request.ReparingOrder.CashAmount,
                                            PeriodId = period.Id,
                                            BranchId = paymentVoucher.BranchId,
                                        });

                                        foreach (var transaction in list)
                                        {
                                            await _context.Transactions.AddAsync(transaction, cancellationToken);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (reparingOrders.AdvanceAmount != reparingOrders.CashAmount)
                                {
                                    if (request.ReparingOrder.CustomerId != null)
                                    {

                                        var contacts = _context.Contacts.AsNoTracking().FirstOrDefault(x => x.Id == request.ReparingOrder.CustomerId);
                                        var accounts = _context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "10100001");
                                        if (contacts != null && accounts != null)
                                        {
                                            var voucherNumber = await _mediator.Send(new GetPaymentVoucherNumberQuery
                                            {
                                                PaymentVoucherType = PaymentVoucherType.CashReceipt
                                            }, cancellationToken);
                                            var paymentVoucher = new PaymentVoucher
                                            {
                                                Date = request.ReparingOrder.Date,
                                                VoucherNumber = voucherNumber,
                                                PaymentVoucherType = PaymentVoucherType.CashPay,
                                                ApprovalStatus = ApprovalStatus.Approved,
                                                ReparingOrderId = reparingOrders.Id,
                                                PaymentMode = SalePaymentType.Cash,
                                                ContactAccountId = contacts.AccountId.Value,
                                                BankCashAccountId = accounts.Id,
                                                Amount = request.ReparingOrder.AdvanceAmount,
                                                BranchId = reparingOrders.BranchId,
                                            };
                                            await _context.PaymentVouchers.AddAsync(paymentVoucher, cancellationToken);


                                            var period = await _context.CompanySubmissionPeriods.FirstOrDefaultAsync(x => x.PeriodStart <= request.ReparingOrder.Date.Date
                                                      && x.PeriodEnd >= request.ReparingOrder.Date.Date, cancellationToken: cancellationToken);
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
                                                Debit = request.ReparingOrder.CashAmount - request.ReparingOrder.Discount,
                                                Credit = 0m,
                                                PeriodId = period.Id,
                                                BranchId = reparingOrders.BranchId,
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
                                                Credit = request.ReparingOrder.CashAmount - request.ReparingOrder.Discount,
                                                PeriodId = period.Id,
                                                BranchId = reparingOrders.BranchId,
                                            });
                                            request.ReparingOrder.Discount = request.ReparingOrder.CashAmount;
                                            foreach (var transaction in list)
                                            {
                                                await _context.Transactions.AddAsync(transaction, cancellationToken);
                                            }

                                        }

                                    }

                                }
                            }

                        }

                        else if (request.ReparingOrder.IsRepared)
                        {
                            request.ReparingOrder.Status = "Repared";
                        }


                        reparingOrders.Date = request.ReparingOrder.Date;
                        reparingOrders.IsPrint = request.ReparingOrder.IsPrint;
                        reparingOrders.RegistrationNo = request.ReparingOrder.RegistrationNo;
                        reparingOrders.CustomerId = request.ReparingOrder.CustomerId;
                        reparingOrders.EmployeeId = request.ReparingOrder.EmployeeId;
                        reparingOrders.WarrantyCategoryId = request.ReparingOrder.WarrantyCategoryId;
                        reparingOrders.UpsDescriptionId = request.ReparingOrder.UpsDescriptionId;
                        reparingOrders.SerialNo = request.ReparingOrder.SerialNo;
                        reparingOrders.ProblemId = request.ReparingOrder.ProblemId;
                        reparingOrders.AcessoryIncludedId = request.ReparingOrder.AcessoryIncludedId;
                        reparingOrders.ReceivedDate = request.ReparingOrder.ReceivedDate;
                        reparingOrders.Status = request.ReparingOrder.Status;
                        reparingOrders.JobAssignId = request.ReparingOrder.JobAssignId;
                        reparingOrders.CompleteDate = request.ReparingOrder.CompleteDate;
                        reparingOrders.EstimateAmount = request.ReparingOrder.EstimateAmount;
                        reparingOrders.RegisteredById = request.ReparingOrder.RegisteredById;
                        reparingOrders.Remarks = request.ReparingOrder.Remarks;
                        reparingOrders.ExpectedDate = request.ReparingOrder.ExpectedDate;
                        reparingOrders.CradNumber = request.ReparingOrder.CradNumber;
                        reparingOrders.PurchaseDate = request.ReparingOrder.PurchaseDate;
                        reparingOrders.DealerRef = request.ReparingOrder.DealerRef;
                        reparingOrders.FaultInfo = request.ReparingOrder.FaultInfo;
                        reparingOrders.RemaningPrice = request.ReparingOrder.AdvanceAmount - request.ReparingOrder.CashAmount;
                        reparingOrders.Discount = request.ReparingOrder.Discount;
                        reparingOrders.IsCollected = request.ReparingOrder.IsCollected;
                        reparingOrders.IsRepared = request.ReparingOrder.IsRepared;
                        reparingOrders.IsReturn = request.ReparingOrder.IsReturn;
                        reparingOrders.IsCashed = request.ReparingOrder.IsCashed;
                        reparingOrders.IsComplete = request.ReparingOrder.IsComplete;
                        reparingOrders.PaymentType = request.ReparingOrder.PaymentType;
                        reparingOrders.CashAmount = request.ReparingOrder.CashAmount;
                        reparingOrders.AdvanceAmount = request.ReparingOrder.AdvanceAmount;

                        _context.ReparingItems.RemoveRange(reparingOrders.ReparingItems);

                        reparingOrders.ReparingItems = request.ReparingOrder.ReparingItems.Select(x =>
                            new ReparingItem()
                            {

                                ProductId = x.ProductId,
                                Quantity = x.Quantity,


                            }).ToList();


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=reparingOrders.RegistrationNo
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = reparingOrders.Id,
                            IsAddUpdate = "Data has been Updated successfully"
                        };
                        return message;



                    }
                    else
                    {


                        var reparingOrder = new ReparingOrder
                        {
                            Id = request.ReparingOrder.Id,
                            Date = request.ReparingOrder.Date,
                            RegistrationNo = request.ReparingOrder.RegistrationNo,
                            CustomerId = request.ReparingOrder.CustomerId,
                            EmployeeId = request.ReparingOrder.EmployeeId,
                            WarrantyCategoryId = request.ReparingOrder.WarrantyCategoryId,
                            UpsDescriptionId = request.ReparingOrder.UpsDescriptionId,
                            SerialNo = request.ReparingOrder.SerialNo,
                            ProblemId = request.ReparingOrder.ProblemId,
                            AcessoryIncludedId = request.ReparingOrder.AcessoryIncludedId,
                            ReceivedDate = request.ReparingOrder.ReceivedDate,
                            Status = request.ReparingOrder.Status,
                            JobAssignId = request.ReparingOrder.JobAssignId,
                            CompleteDate = request.ReparingOrder.CompleteDate,
                            EstimateAmount = request.ReparingOrder.EstimateAmount,
                            RegisteredById = request.ReparingOrder.RegisteredById,
                            ExpectedDate = request.ReparingOrder.ExpectedDate,
                            Remarks = request.ReparingOrder.Remarks,
                            CradNumber = request.ReparingOrder.CradNumber,
                            PurchaseDate = request.ReparingOrder.PurchaseDate,
                            DealerRef = request.ReparingOrder.DealerRef,
                            FaultInfo = request.ReparingOrder.FaultInfo,
                            RemaningPrice = request.ReparingOrder.AdvanceAmount - request.ReparingOrder.CashAmount,
                            AdvanceAmount = request.ReparingOrder.AdvanceAmount,
                            CashAmount = request.ReparingOrder.CashAmount,
                            PaymentType = request.ReparingOrder.PaymentType,
                            IsComplete = request.ReparingOrder.IsComplete,
                            IsCashed = request.ReparingOrder.IsCashed,
                            Discount = request.ReparingOrder.Discount,
                            IsCollected = request.ReparingOrder.IsCollected,
                            IsReturn = request.ReparingOrder.IsReturn,
                            IsRepared = request.ReparingOrder.IsRepared,
                            BranchId = request.ReparingOrder.BranchId,
                            ReparingItems = request.ReparingOrder.ReparingItems.Select(x =>
                             new ReparingItem()
                             {

                                 ProductId = x.ProductId,
                                 Quantity = x.Quantity,


                             }).ToList(),




                        };
                        await _context.ReparingOrders.AddAsync(reparingOrder, cancellationToken);
                        //if (request.ReparingOrder.CustomerId != null && request.ReparingOrder.CashAmount!=0 )
                        //{
                        //    var contacts = Context.Contacts.AsNoTracking().FirstOrDefault(x => x.Id == request.ReparingOrder.CustomerId);
                        //    var accounts = Context.Accounts.AsNoTracking().FirstOrDefault(x => x.Code == "10100001");
                        //    if (contacts != null && accounts != null)
                        //    {
                        //        var voucherNumber = await _mediator.Send(new GetPaymentVoucherNumberQuery
                        //        {
                        //            PaymentVoucherType = PaymentVoucherType.CashReceipt
                        //        }, cancellationToken);
                        //        var paymentVoucher = new PaymentVoucher
                        //        {
                        //            Date = request.ReparingOrder.Date,
                        //            VoucherNumber = voucherNumber,
                        //            PaymentVoucherType = PaymentVoucherType.CashReceipt,
                        //            ApprovalStatus = ApprovalStatus.Approved,
                        //            ReparingOrderId = reparingOrder.Id,
                        //            PaymentMode = SalePaymentType.Cash,
                        //            ContactAccountId = contacts.AccountId.Value,
                        //            BankCashAccountId = accounts.Id,
                        //            Amount = request.ReparingOrder.CashAmount,

                        //        };

                        //        var invoiceNo = await _mediator.Send(new AddUpdatePaymentVoucherCommand
                        //        {
                        //            Id = paymentVoucher.Id,
                        //            Date = paymentVoucher.Date,
                        //            VoucherNumber = paymentVoucher.VoucherNumber,
                        //            Narration = paymentVoucher.Narration,
                        //            ChequeNumber = paymentVoucher.ChequeNumber,
                        //            BankCashAccountId = paymentVoucher.BankCashAccountId,
                        //            ContactAccountId = paymentVoucher.ContactAccountId,
                        //            PaymentVoucherType = paymentVoucher.PaymentVoucherType,
                        //            Amount = paymentVoucher.Amount,
                        //            SaleInvoice = paymentVoucher.SaleInvoice,
                        //            PurchaseInvoice = paymentVoucher.PurchaseInvoice,
                        //            ApprovalStatus = paymentVoucher.ApprovalStatus,
                        //            PaymentMethod = paymentVoucher.PaymentMethod,
                        //            PaymentMode = paymentVoucher.PaymentMode,
                        //        }, cancellationToken);
                        //    }

                        //}

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo=reparingOrder.RegistrationNo
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = reparingOrder.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;



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
