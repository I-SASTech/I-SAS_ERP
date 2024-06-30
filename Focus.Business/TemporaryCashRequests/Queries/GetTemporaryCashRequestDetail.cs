using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;
using Focus.Business.Interface;
using Focus.Business.TemporaryCashRequests.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.TemporaryCashRequests.Queries
{
    public class GetTemporaryCashRequestDetail : IRequest<TemporaryCashRequestModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetTemporaryCashRequestDetail, TemporaryCashRequestModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetTemporaryCashRequestDetail> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<TemporaryCashRequestModel> Handle(GetTemporaryCashRequestDetail request, CancellationToken cancellationToken)
            {
                try
                {
                    var po = await _context.TemporaryCashRequests
                        .AsNoTracking()
                        .Include(x => x.TemporaryCashRequestItems)
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

                    if (po.IsCashRequesterUser)
                    {
                        var userName = await _context.CashRequestUsers.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == po.UserId, cancellationToken: cancellationToken);
                        return new TemporaryCashRequestModel
                        {
                            Id = po.Id,
                            Date = po.Date,
                            RegistrationNo = po.RegistrationNo,
                            UserId = po.UserId,
                            UserName = userName?.Name,
                            Note = po.Note,
                            IsCashRequesterUser = po.IsCashRequesterUser,
                            NewUser = po.NewUser,
                            AttachmentList = attachmentList,
                            TemporaryCashItems = po.TemporaryCashRequestItems.Select(x => new TemporaryCashRequestItemModel
                            {
                                Id = x.Id,
                                Description = x.Description,
                                Amount = x.Amount
                            }).ToList()
                        };
                    }
                    else
                    {
                        var userName = await _context.EmployeeRegistrations.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == po.UserId, cancellationToken: cancellationToken);
                        return new TemporaryCashRequestModel
                        {
                            Id = po.Id,
                            Date = po.Date,
                            RegistrationNo = po.RegistrationNo,
                            UserId = po.UserId,
                            UserName = userName?.EnglishName,
                            Note = po.Note,
                            IsCashRequesterUser = po.IsCashRequesterUser,
                            NewUser = po.NewUser,
                            AttachmentList = attachmentList,
                            TemporaryCashItems = po.TemporaryCashRequestItems.Select(x => new TemporaryCashRequestItemModel
                            {
                                Id = x.Id,
                                Description = x.Description,
                                Amount = x.Amount
                            }).ToList()
                        };
                    }

                    
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
