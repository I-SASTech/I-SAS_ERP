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

namespace Focus.Business.EmployeeRegistrations.Queries.GetEmployeeAttachments
{
    public class GetEmployeeAttachmentsQuery : PagedRequest, IRequest<PagedResult<List<EmployeeAttachmentLookupModel>>>
    {
        public Guid Id { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetEmployeeAttachmentsQuery, PagedResult<List<EmployeeAttachmentLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetEmployeeAttachmentsQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<EmployeeAttachmentLookupModel>>> Handle(GetEmployeeAttachmentsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    request.PageSize = 5;
                    var query = _context.EmployeeRegistrationAttachments.AsNoTracking()
                        .Where(x => x.EmployeeId == request.Id)
                        .AsQueryable();

                    query = query.OrderByDescending(x => x.Date);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var employeeAttachment = await query.ToListAsync(cancellationToken);

                    var attachments = new List<EmployeeAttachmentLookupModel>();
                    if (employeeAttachment.Count > 0)
                    {
                        foreach (var employee in employeeAttachment)
                        {
                            attachments.Add(new EmployeeAttachmentLookupModel()
                            {
                                EmployeeId = employee.EmployeeId,
                                CNIC = employee.CNIC,
                                DrivingLicense = employee.DrivingLicense,
                                Passport = employee.Passport,
                                Photo = employee.Photo,
                                Date = employee.Date
                            });
                        }
                    }
                    else
                    {
                        attachments.Add(new EmployeeAttachmentLookupModel()
                        {
                            EmployeeId = request.Id,
                            CNIC = "",
                            DrivingLicense = "",
                            Passport = "",
                            Photo = ""
                        });
                    }

                    return new PagedResult<List<EmployeeAttachmentLookupModel>>()
                    {
                        Results = attachments,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = employeeAttachment.Count / request.PageSize
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
