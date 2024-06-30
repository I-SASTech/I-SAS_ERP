using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Attachments.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseBills.Queries.GetPurchaseBillDetails
{
    public class GetPurchaseBillDetailQuery : IRequest<PurchaseBillLookupModel>
    {
        public Guid Id { get; set; }
        public bool IsPayement { get; set; }

        public class Handler : IRequestHandler<GetPurchaseBillDetailQuery, PurchaseBillLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetPurchaseBillDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PurchaseBillLookupModel> Handle(GetPurchaseBillDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseBill =  _context.PurchaseBills.AsNoTracking()
                        .Include(x=>x.BillAttachments)
                        .Include(x=>x.BillerAccount)
                        .Include(x=>x.PurchaseBillItems)
                        .ThenInclude(x=>x.Account)
                        .FirstOrDefault(x => x.Id == request.Id);
                    var poItems = new List<BillAttachment>();

                    var sumDailyExpense = await _context.DailyExpenses.AsNoTracking().Where(x => x.BillerAccountId == request.Id).SumAsync(x => x.TotalAmount);

                    if (purchaseBill != null)
                    {


                        var attachments = _context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == purchaseBill.Id)
                            .AsQueryable();

                        var attachmentList = await attachments.Select(x => new AttachmentLookUpModel
                        {
                            Id = x.Id,
                            DocumentId = x.DocumentId,
                            Date = x.Date,
                            Description = x.Description,
                            FileName = x.FileName,
                            Path = x.Path
                        }).ToListAsync(cancellationToken: cancellationToken);

                        return new PurchaseBillLookupModel
                        {
                            Id = purchaseBill.Id,
                            Date = purchaseBill.Date,
                            DueDate = purchaseBill.DueDate,
                            ApprovalStatus = purchaseBill.ApprovalStatus,
                            RegistrationNo = purchaseBill.RegistrationNo,
                            BillerAccount = purchaseBill.BillerAccount.Name,
                            Narration = purchaseBill.Narration,
                            Reference = purchaseBill.Reference,
                            TaxMethod = purchaseBill.TaxMethod,
                            BillerId = purchaseBill.BillerId,
                            AttachmentList = attachmentList,
                            RemainingAmount = purchaseBill.RemainingAmount,
                            BillDate = purchaseBill.BillDate,
                            TotalAmount = request.IsPayement ? (purchaseBill.PurchaseBillItems.Sum(x => x.Amount)) - sumDailyExpense : purchaseBill.PurchaseBillItems.Sum(x => x.Amount),
                            BillAttachments = purchaseBill.BillAttachments.Select(x=>
                                new BillAttachmentLookupModel()
                                {
                                    Path = x.Path,
                                    Name = x.Name,
                                    Date = x.Date,
                                    Description = x.Description

                                }).ToList(),
                            PurchaseBillItems = purchaseBill.PurchaseBillItems.Select(x =>
                                new PurchaseBillItemLookupModel()
                                {
                                    Id=x.Id,
                                    Description = x.Description,
                                    Amount = x.Amount,
                                    AccountName = x.Account.Name,
                                    AccountId = x.AccountId,
                                    PurchaseBillId = x.PurchaseBillId


                                }).ToList()
                        };
                    }
                    throw new NotFoundException(nameof(PurchaseBill), request.Id);
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
