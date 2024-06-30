using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Contacts.Queries.GetContactAttachments
{
    public class GetContactAttachmentsQuery : PagedRequest, IRequest<PagedResult<List<ContactAttachmentLookupModel>>>
    {
        public Guid Id { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetContactAttachmentsQuery, PagedResult<List<ContactAttachmentLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetContactAttachmentsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ContactAttachmentLookupModel>>> Handle(GetContactAttachmentsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    request.PageSize = 5;
                    var query = _context.ContactAttachments.AsNoTracking()
                        .Where(x => x.ContactId == request.Id)
                        .AsQueryable();

                    query = query.OrderByDescending(x => x.Date);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var contactAttachments = await query.ToListAsync(cancellationToken);

                    var attachments = new List<ContactAttachmentLookupModel>();
                    if (contactAttachments.Count > 0)
                    {
                        foreach (var contactAttachment in contactAttachments)
                        {
                            attachments.Add(new ContactAttachmentLookupModel()
                            {
                                ContactId = contactAttachment.ContactId,
                                CommercialRegistration = contactAttachment.CommercialRegistration,
                                VATCertificate = contactAttachment.VATCertificate,
                                ZakatCertificate = contactAttachment.ZakatCertificate,
                                Date = contactAttachment.Date
                            });
                        }
                    }
                    else
                    {
                        attachments.Add(new ContactAttachmentLookupModel()
                        {
                            ContactId = request.Id,
                            CommercialRegistration = "",
                            VATCertificate = "",
                            ZakatCertificate = ""
                        });
                    }

                    return new PagedResult<List<ContactAttachmentLookupModel>>()
                    {
                        Results = attachments,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = contactAttachments.Count / request.PageSize
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
