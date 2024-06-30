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
namespace Focus.Business.ProductionModule.GatePasses.Queries.GetGatePassesDetails
{
    public class GetGatePassesDetailQuery : IRequest<GatePassesLookUpModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetGatePassesDetailQuery, GatePassesLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetGatePassesDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<GatePassesLookUpModel> Handle(GetGatePassesDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var gatepasses = await _context.GatePasses
                        .AsNoTracking()
                        .Include(x=>x.EmployeeRegistration)
                        .Include(x => x.GatePassItems)
                        .ThenInclude(x => x.Product)
                        .FirstOrDefaultAsync(x => x.BatchProcessContractorId == request.Id, cancellationToken);

                    return new GatePassesLookUpModel
                    {
                        Id = gatepasses.Id,
                        RegistrationNo = gatepasses.RegistrationNo,
                        Date = gatepasses.Date,
                        Note = gatepasses.Note,
                        IsGatePass = gatepasses.IsGatePass,
                        ContractorName = gatepasses.EmployeeRegistration?.EnglishName,
                        ContractorNameArabic = gatepasses.EmployeeRegistration?.ArabicName,
                        FromDate = gatepasses.FromDate,
                        Dates = gatepasses.Date.ToString("MM/dd/yyyy hh:mm tt"),
                        FromDates = gatepasses.FromDate?.ToString("MM/dd/yyyy hh:mm tt"),
                        ToDates = gatepasses.ToDate?.ToString("MM/dd/yyyy hh:mm tt"),
                        ToDate = gatepasses.ToDate,
                        IsActive = gatepasses.IsActive,
                      
                        GatePassItems = gatepasses.GatePassItems.Select(x =>
                            new GatePassesItemLookUpModel()
                            {
                                Id = x.Id,
                                ProductId = x.ProductId,
                                GatePassId = x.GatePassId,
                                Quantity = x.Quantity,
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
                    throw new ApplicationException("Unable to gatepasses your request please contact support");
                }
            }
        }
    }
}