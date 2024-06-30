using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.ProductGroups.Model;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.ProductGroups.Queries
{
    public class GetProductGroupDetailQueries : IRequest<ProductGroupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetProductGroupDetailQueries, ProductGroupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetProductGroupDetailQueries> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ProductGroupModel> Handle(GetProductGroupDetailQueries request, CancellationToken cancellationToken)
            {
                try
                {
                    var productGroup = await _context.ProductGroups
                        .Select(productGroup => new ProductGroupModel()
                        {
                            Id = productGroup.Id,
                            Name = productGroup.Name,
                            NameArabic = productGroup.NameArabic,
                            Description = productGroup.Description,
                            Code = productGroup.Code,
                            Status = productGroup.Status,
                            ProductList = productGroup.Products.Select(x => new ProductListForGroupModel
                            {
                                ProductId = x.Id,
                                Name = x.DisplayName,
                                CategoryName = x.Category.Name,
                                SalePrice = x.SalePrice,
                                PurchasePrice = x.PurchasePrice,
                                
                            }).ToList()
                        }).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);



                    if (productGroup != null)
                    {
                        return productGroup;
                    }
                    throw new NotFoundException(nameof(productGroup), request.Id);
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
