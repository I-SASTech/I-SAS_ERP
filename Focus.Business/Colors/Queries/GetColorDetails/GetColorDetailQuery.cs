using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Colors.Queries.GetColorDetails
{
    public class GetProcessDetailQuery : IRequest<ColorDetailLookUpModel>
    {
        public Guid Id { get; set; }
        public bool IsVariance { get; set; }
        public Guid ProductId { get; set; }

        public class Handler : IRequestHandler<GetProcessDetailQuery, ColorDetailLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetProcessDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<ColorDetailLookUpModel> Handle(GetProcessDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var color = await _context.Colors.FindAsync(request.Id);



                    if (color != null)
                    {
                        if (request.IsVariance)
                        {
                            var variationInventories = await _context.VariationInventories.Where(x => x.ColorId == request.Id && x.ProductId == request.ProductId).Select(x => new VariationInventory()
                            {
                                ColorId = x.ColorId,
                                SizeId = x.SizeId,
                                Quantity = x.Quantity,
                            }).ToListAsync(cancellationToken: cancellationToken);
                            return new ColorDetailLookUpModel()
                            {
                                Id = color.Id,
                                Name = color.Name,
                                NameArabic = color.NameArabic,
                                Quantity = variationInventories.Sum(x => x.Quantity),
                                VariationInventories = variationInventories
                            };
                        }

                        return new ColorDetailLookUpModel()
                        {
                            Id = color.Id,
                            Name = color.Name,
                            NameArabic = color.NameArabic,
                            Description = color.Description,
                            Code = color.Code,
                            isActive = color.isActive
                        };
                    }
                    throw new NotFoundException(nameof(color), request.Id);
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
