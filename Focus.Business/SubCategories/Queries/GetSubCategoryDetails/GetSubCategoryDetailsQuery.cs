using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.SubCategories.Queries.GetSubCategoryDetails
{
    public class GetSubCategoryDetailsQuery : IRequest<SubCategoryDetailsLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetSubCategoryDetailsQuery, SubCategoryDetailsLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetSubCategoryDetailsQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<SubCategoryDetailsLookUpModel> Handle(GetSubCategoryDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var subCategories = await _context.SubCategories.FindAsync(request.Id);

                    if (subCategories != null)
                    {
                        return SubCategoryDetailsLookUpModel.Create(subCategories);
                    }
                    throw new NotFoundException(nameof(subCategories), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
