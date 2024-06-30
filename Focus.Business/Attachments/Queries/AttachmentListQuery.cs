using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Attachments.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Attachments.Queries
{
    public class AttachmentListQuery : IRequest<List<AttachmentLookUpModel>>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<AttachmentListQuery, List<AttachmentLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<AttachmentListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<AttachmentLookUpModel>> Handle(AttachmentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.Attachments
                        .AsNoTracking()
                        .Where(x=>x.DocumentId==request.Id)
                        .AsQueryable();

                    var list =await query.Select(x => new AttachmentLookUpModel
                    {
                        Id = x.Id,
                        DocumentId = x.DocumentId,
                        Date = x.Date,
                        Description = x.Description,
                        FileName = x.FileName,
                        Path = x.Path
                    }).ToListAsync(cancellationToken: cancellationToken);

                    return list;


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
