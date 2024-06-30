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

namespace Focus.Business.Transporters.Queries.GetTransporterDetails
{
   public class GetTransporterDetailQuery : IRequest<TransporterDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetTransporterDetailQuery, TransporterDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetTransporterDetailQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<TransporterDetailLookUpModel> Handle(GetTransporterDetailQuery request, CancellationToken cancellationToken)
            {
                var transporter = await _context.Transporters.FindAsync(request.Id);
                if (transporter == null)
                {
                    throw new NotFoundException(nameof(Transporter), request.Id);
                }
                return TransporterDetailLookUpModel.Create(transporter);

            }
        }
    }
}
