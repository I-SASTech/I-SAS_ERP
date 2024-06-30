using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQuery : IRequest<CategoryDetailLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetCategoryDetailsQuery, CategoryDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCategoryDetailsQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<CategoryDetailLookUpModel> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var categories = await _context.Categories.FindAsync(request.Id);
                    if (categories == null)
                        throw new NotFoundException("categories Details", "");
                    return CategoryDetailLookUpModel.Create(categories);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
