using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.ProductionBatchs.ProcessContractor;
using Focus.Business.ProductionModule.Processes;
using Focus.Business.Sales.Queries.GetSaleDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchDetails
{
   public class GetBatchProcessDetailQuery : IRequest<List<ProcessesLookUpModel>>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBatchProcessDetailQuery, List<ProcessesLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetBatchProcessDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<ProcessesLookUpModel>> Handle(GetBatchProcessDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var productionBatch =  _context.BatchProcesses
                        .AsNoTracking()
                        .Include(x => x.BatchProcessContractors)
                        .ThenInclude(x=>x.ContractorItems)
                        .ThenInclude(x => x.Product)
                        .Include(x=>x.BatchProcessContractors)
                        .ThenInclude(x=>x.Contractor)
                        .Include(x=>x.Process)
                        .ThenInclude(x => x.ProcessItems)
                        .Where(x => x.ProductionBatchId == request.Id).ToList();
                      
                        


                    if (productionBatch != null)
                    {

                        var processList = new List<ProcessesLookUpModel>();
                        foreach (var x in productionBatch)
                        {
                            processList.Add(new ProcessesLookUpModel()
                            {
                                Id = x.Id,
                                ProductionBatchId = x.ProductionBatchId,
                                Code = x.Code,
                                EnglishName = x.EnglishName,
                                ArabicName = x.ArabicName,
                                Description = x.Description,
                                IsActive = x.IsActive,
                                Date = x.Date,
                                ProcessContractors = x.BatchProcessContractors.Where(z => z.IsActive).Select(y =>
                                    new ProcessContractorLookUpModel()
                                    {
                                        Id = y.Id,
                                        ProductionBatchId = x.ProductionBatchId,
                                        ProcessId = x.Id,
                                        ContractorId = y.ContractorId,
                                        FromDate = y.FromDate,
                                        FromDates = y.FromDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                        ToDates = y.ToDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                        ToDate = y.ToDate,
                                        IsActive = y.IsActive,
                                        ContractorType = y.ContractorType,
                                        ApprovalStatus = y.ApprovalStatus,
                                        ContractorNameEn = y.Contractor?.EnglishName,
                                        ContractorAccountId = y.Contractor?.EmployeeAccountId,
                                        ContractorNameAr = y.Contractor?.ArabicName,
                                        ContractorItems = y.ContractorItems.Select(item =>
                                            new ContractorItemsLookUpModel()
                                            {
                                                Id = item.Id,
                                                ProductId = item.ProductId,
                                                Quantity = item.Quantity,
                                                HighQuantity = item.HighQuantity,
                                                Waste = item.Waste,
                                                //WareHouseId=item.WarehouseId==null,
                                                //WarehouseName = item.Warehouse?.Name,
                                                UnitPerPack = item.UnitPerPack,
                                                BasicUnit = item.BasicUnit,
                                                LevelOneUnit = item.LevelOneUnit,
                                                Product = new ProductDropdownLookUpModel
                                                {
                                                    Id = item.Product.Id,
                                                    BarCode = item.Product.BarCode,
                                                    Code = item.Product.Code,
                                                    EnglishName = item.Product.EnglishName,
                                                    ArabicName = item.Product.ArabicName,
                                                    //Inventory = (item.Product.Inventories == null || item.Product.Inventories.Count == 0)
                                                    //    ? null
                                                    //    : new Inventory()
                                                    //    {
                                                    //        CurrentQuantity = item.Product.Inventories.LastOrDefault(x => x.ProductId == item.Product.Id)
                                                    //            .CurrentQuantity,
                                                    //    },

                                                }

                                            }).ToList(),
                                      


                                    }).ToList(),
                                ProcessItems = x.Process.ProcessItems.Select(i =>
                                    new ProcessItemLookUpModel()
                                    {
                                        Id = i.Id,
                                        ProductId = i.ProductId,
                                        ProcessId = i.ProcessId,
                                    }).ToList(),


                            });

                        }

                        return processList;

                    }
                    throw new NotFoundException(nameof(productionBatch), request.Id);
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
