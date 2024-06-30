using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Designations.Queries.GetDesignationDetails
{
    public class GetDesignationDetailQuery:IRequest<DesignationDetailLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetDesignationDetailQuery, DesignationDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDesignationDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<DesignationDetailLookUpModel> Handle(GetDesignationDetailQuery request, CancellationToken cancellationToken)
            {
                var designation = await _context.Designations.FindAsync(request.Id);

                if (designation != null)
                {
                    return DesignationDetailLookUpModel.Create(designation);
                }
                throw new NotFoundException(nameof(Designation), request.Id);
            }
        }
    }
}
