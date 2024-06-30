using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.JournalVouchers.Queries.GetJournalVoucherDetail
{
    public class JournalVoucherDetailQuery : IRequest<JournalVoucherLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<JournalVoucherDetailQuery, JournalVoucherLookupModel>
        {
            private readonly IApplicationDbContext _context;
            public readonly ILogger _logger;


            public Handler(IApplicationDbContext context, ILogger<JournalVoucherDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<JournalVoucherLookupModel> Handle(JournalVoucherDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var po = await _context.JournalVouchers
                        .AsNoTracking()
                        .Include(x => x.JournalVoucherItems)
                        .ThenInclude(x => x.Account)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);


                    var attachments = _context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == po.Id)
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


                    var items = new List<JournalVoucherItemLookupModel>();
                    foreach (var item in po.JournalVoucherItems)
                    {
                        items.Add(new JournalVoucherItemLookupModel
                        {
                            Id = item.Id,
                            AccountId=item.AccountId,
                            AccountName=item.Account.Name,
                            Description = item.Description,
                            Debit = item.Debit,
                            Credit = item.Credit,
                            PaymentMode = item.PaymentMode,
                            PaymentModes = item.PaymentMode == 0 || item.PaymentMode == SalePaymentType.Default ? "" : item.PaymentMode.ToString(),
                            PaymentMethod = item.PaymentMethod,
                            PaymentMethods = item.PaymentMethod == 0 ? "" : item.PaymentMethod.ToString(),
                            ChequeNo = item.ChequeNo,
                            JournalVoucherId =item.JournalVoucherId
                           
                           
                        });
                    }
                    return new JournalVoucherLookupModel
                    {
                        Id = po.Id,
                        Date = po.Date,
                        VoucherNumber = po.VoucherNumber,
                        Comments = po.Comments,
                        Narration = po.Narration,
                        ApprovalStatus = po.ApprovalStatus,
                        IsStockTransfer = po.IsStockTransfer,
                        JournalVoucherItems = items,
                        AttachmentList = attachmentList,


                    };
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);

                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}