using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Focus.Business.ProductionModule.Processes.Queries.GetProcessesDetails
{
    public class GetProcessesDetailQuery : IRequest<ProcessesLookUpModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetProcessesDetailQuery, ProcessesLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetProcessesDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ProcessesLookUpModel> Handle(GetProcessesDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var process = await _context.Processes
                        .AsNoTracking()
                        .Include(x => x.ProcessContractors)
                        .ThenInclude(x => x.Contractor)
                        .Include(x => x.ProcessItems)
                        .ThenInclude(x => x.Product)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    return new ProcessesLookUpModel
                    {
                        Id = process.Id,
                        Code = process.Code,
                        Color = process.Color,
                        Date = process.Date,
                        EnglishName = process.EnglishName,
                        ArabicName = process.ArabicName,
                        Description = process.Description,
                        IsActive = process.IsActive,
                        ProcessContractors = process.ProcessContractors.Select(x =>
                            new ProcessContractorLookUpModel()
                            {
                                Id = x.Id,
                                ContractorId = x.ContractorId,
                                ProcessId = x.ProcessId,
                                ContractorNameEn = x.Contractor?.EnglishName,
                                ContractorNameAr = x.Contractor?.ArabicName,
                            }).ToList(),
                        ProcessItems = process.ProcessItems.Select(x =>
                            new ProcessItemLookUpModel()
                            {
                                Id = x.Id,
                                ProductId = x.ProductId,
                                ProcessId = x.ProcessId,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = x.Product.Id,
                                    BarCode = x.Product.BarCode,
                                    Code = x.Product.Code,
                                    EnglishName = x.Product.EnglishName,
                                    ArabicName = x.Product.ArabicName,
                                    Inventory = (x.Product.Inventories == null || x.Product.Inventories.Count == 0)
                                        ? null
                                        : new Inventory()
                                        {
                                            CurrentQuantity = x.Product.Inventories.OrderBy(x => x.ProductId == x.Product.Id).LastOrDefault().CurrentQuantity,
                                        },
                                }
                            }).ToList(),
                    };
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