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

namespace Focus.Business.Departments.Queries.GetDepartmentDetails
{
    public class GetDepartmentDetailQuery:IRequest<DepartmentDetailLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetDepartmentDetailQuery, DepartmentDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetDepartmentDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<DepartmentDetailLookUpModel> Handle(GetDepartmentDetailQuery request, CancellationToken cancellationToken)
            {
                var department = await _context.Departments.FindAsync(request.Id);

                if (department != null)
                {
                    return DepartmentDetailLookUpModel.Create(department);
                }
                throw new NotFoundException(nameof(department), request.Id);
            }
        }
    }
}
