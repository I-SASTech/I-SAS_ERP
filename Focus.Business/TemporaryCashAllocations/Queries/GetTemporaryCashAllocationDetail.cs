using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.TemporaryCashAllocations.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashAllocations.Queries
{
    public class GetTemporaryCashAllocationDetail : IRequest<TemporaryCashAllocationLookUp>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetTemporaryCashAllocationDetail, TemporaryCashAllocationLookUp>
        {
            private readonly IApplicationDbContext _context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<GetTemporaryCashAllocationDetail> logger)
            {
                _context = context;
                Logger = logger;
            }

            public async Task<TemporaryCashAllocationLookUp> Handle(GetTemporaryCashAllocationDetail request, CancellationToken cancellationToken)
            {
                try
                {
                    var po = await _context.TemporaryCashAllocations
                        .AsNoTracking()
                        .Include(x=>x.Account)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    if (po == null)
                        throw new NotFoundException("Purchase Order", "Was not found");

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

                    var employee = await _context.EmployeeRegistrations.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == po.UserEmployeeId, cancellationToken: cancellationToken);


                    return new TemporaryCashAllocationLookUp
                    {
                        Id = po.Id,
                        Date = po.Date,
                        VoucherNumber = po.VoucherNumber,
                        Narration = po.Narration,
                        ChequeNumber = po.ChequeNumber,
                        ApprovalStatus = po.ApprovalStatus,
                        Amount = po.Amount,
                        PaymentVoucherType = po.PaymentVoucherType,
                        BankCashAccountId = po.BankCashAccountId,
                        UserEmployeeId = po.UserEmployeeId,
                        BankCashAccountName = po.Account?.Name,
                        ContactAccountName = employee?.EnglishName,
                        DraftBy = po.DraftBy,
                        ApprovedBy = po.ApprovedBy,
                        RejectBy = po.RejectBy,
                        VoidBy = po.VoidBy,
                        Reason = po.Reason,
                        DraftDate = po.DraftDate,
                        ApprovedDate = po.ApprovedDate,
                        RejectDate = po.RejectDate,
                        VoidDate = po.VoidDate,
                        PaymentMethod = po.PaymentMethod,
                        PaymentMode = po.PaymentMode,
                        AttachmentList = attachmentList,
                    };
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
