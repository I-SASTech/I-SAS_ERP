using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Common;

namespace Focus.Business.AdditionalCompany.Queries.GetCompanyAttachments
{
    public class GetCompanyAttachmentsQuery : PagedRequest, IRequest<PagedResult<List<CompanyAttachmentLookupModel>>>
    {
        public Guid CompanyId { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetCompanyAttachmentsQuery, PagedResult<List<CompanyAttachmentLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetCompanyAttachmentsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<CompanyAttachmentLookupModel>>> Handle(GetCompanyAttachmentsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.CompanyAttachments.AsNoTracking()
                        .Where(x => x.CompanyId == request.CompanyId)
                        .AsQueryable();


                    query = query.OrderByDescending(x => x.Date);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var companyAttachments = await query.ToListAsync(cancellationToken);

                    var attachments = new List<CompanyAttachmentLookupModel>();
                    if (companyAttachments.Count > 0)
                    {
                        foreach (var companyAttachment in companyAttachments)
                        {
                            attachments.Add(new CompanyAttachmentLookupModel()
                            {
                                CompanyId = companyAttachment.CompanyId,
                                Logo = companyAttachment.Logo,
                                BusinessLicence = companyAttachment.BusinessLicence,
                                CctvLicence = companyAttachment.CctvLicence,
                                CivilDefenceLicense = companyAttachment.CivilDefenceLicense,
                                Date = companyAttachment.Date,
                                CommercialRegistration = companyAttachment.CommercialRegistration
                            });
                        }
                    }
                    else
                    {
                        attachments.Add(new CompanyAttachmentLookupModel()
                        {
                            CompanyId = request.CompanyId,
                            Logo = "",
                            BusinessLicence = "",
                            CctvLicence = "",
                            CivilDefenceLicense = "",
                            CommercialRegistration = ""
                        });
                    }

                    return new PagedResult<List< CompanyAttachmentLookupModel >>()
                    {
                        Results = attachments,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = companyAttachments.Count / request.PageSize
                    };
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
