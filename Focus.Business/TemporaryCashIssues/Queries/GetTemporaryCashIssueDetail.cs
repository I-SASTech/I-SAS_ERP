using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Business.Interface;
using Focus.Business.TemporaryCashIssues.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashIssues.Queries
{
    public class GetTemporaryCashIssueDetail : IRequest<TemporaryCashIssueLookUp>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetTemporaryCashIssueDetail, TemporaryCashIssueLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetTemporaryCashIssueDetail> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<TemporaryCashIssueLookUp> Handle(GetTemporaryCashIssueDetail request, CancellationToken cancellationToken)
            {
                try
                {
                    var po = await _context.TemporaryCashIssues
                        .AsNoTracking()
                        .Include(x => x.TemporaryCashIssueItems)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    var attachments = _context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == request.Id)
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

                    EmployeeRegistration employee = null;
                    if (po.CashIssuerId!=null)
                    {
                        employee = await _context.EmployeeRegistrations.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == po.CashIssuerId, cancellationToken: cancellationToken);
                    }

                    return new TemporaryCashIssueLookUp
                    {
                        Id = po.Id,
                        Date = po.Date,
                        RegistrationNo = po.RegistrationNo,
                        UserId = po.UserId,
                        Note = po.Note,
                        Reason = po.Reason,
                        IsCashRequesterUser = po.IsCashRequesterUser,
                        NewUser = po.NewUser,
                        TemporaryCashRequestId = po.TemporaryCashRequestId,
                        CashIssuerId = po.CashIssuerId,
                        CashIssuerNameEn = employee?.EnglishName,
                        CashIssuerNameAr = employee?.ArabicName,
                        AttachmentList = attachmentList,
                        TemporaryCashIssueItems = po.TemporaryCashIssueItems.Select(x => new TemporaryCashIssueItemModel()
                        {
                            Id = x.Id,
                            Description = x.Description,
                            Amount = x.Amount
                        }).ToList()
                    };
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
