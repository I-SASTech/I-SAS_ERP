using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.ProductionBatchs.ProcessContractor;
using Focus.Business.ProductionModule.Processes;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchDetails
{
    public class GetProductionBatchDetailQuery : IRequest<ProductionBatchDetailLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetProductionBatchDetailQuery, ProductionBatchDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetProductionBatchDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ProductionBatchDetailLookUpModel> Handle(GetProductionBatchDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var productionBatch = await _context.ProductionBatchs
                        .AsNoTracking()
                        .Include(x=>x.BatchProcesses)
                        .ThenInclude(x=>x.BatchProcessContractors)
                        .ThenInclude(x=>x.ContractorItems)
                        .ThenInclude(x=>x.Product)
                        .Include(x => x.BatchProcesses)
                        .ThenInclude(x => x.BatchProcessContractors)
                        .ThenInclude(x=>x.Contractor)
                        .Include(x=>x.BatchProcesses)
                        .ThenInclude(x=>x.Process)
                        .ThenInclude(x=>x.ProcessItems)
                        .Include(x=>x.ProductionBatchItems)
                        .ThenInclude(x=>x.Product)
                        .FirstOrDefaultAsync(x=>x.Id==request.Id,cancellationToken);


                    if (productionBatch != null)
                    {

                        var poItems = new List<ProductionBatchItemLookupModel>();
                        foreach (var item in productionBatch.ProductionBatchItems)
                        {
                            poItems.Add(new ProductionBatchItemLookupModel
                            {
                                Id = item.Id,
                                CurrentQuantity = item.CurrentQuantity,
                                RemainingQuantity = item.RemainingQuantity,
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
                            });
                        }

                        return new ProductionBatchDetailLookUpModel
                        {
                            Id = productionBatch.Id,
                            Date = productionBatch.Date,
                            IsClose = productionBatch.IsClose,
                            NoOfBatches = productionBatch.NoOfBatches,
                            RecipeNoId = productionBatch.RecipeNoId,
                            SaleOrderId = productionBatch.SaleOrderId,
                            IsActive = productionBatch.IsActive,
                            ApprovalStatus = productionBatch.ApprovalStatus,
                            RegistrationNo = productionBatch.RegistrationNo,
                            Note = productionBatch.Note,
                            StartTime = productionBatch.StartTime == null ? null : productionBatch.StartTime.Value.ToString("MM/dd/yyyy hh:mm"),
                            EndTime = productionBatch.EndTime == null ? null : productionBatch.EndTime.Value.ToString("MM/dd/yyyy hh:mm"),
                            ProcessBy = productionBatch.ProcessBy,
                            ProcessDate= productionBatch.ProcessDate == null ? null : productionBatch.ProcessDate.Value.ToString("MM/dd/yyyy hh:mm"),
                            CompleteBy= productionBatch.CompleteBy,
                            CompleteDate= productionBatch.CompleteDate == null ? null : productionBatch.CompleteDate.Value.ToString("MM/dd/yyyy hh:mm"),
                            TransferBy = productionBatch.TransferBy,
                            RecipeQuantity = productionBatch.RecipeQuantity,
                            DamageStock = productionBatch.DamageStock,
                            RemainingStock = productionBatch.RemainingStock,
                            LateReason = productionBatch.LateReason,
                            LateReasonCompletion = productionBatch.LateReasonCompletion,
                            TransferDate= productionBatch.TransferDate == null ? null : productionBatch.TransferDate.Value.ToString("MM/dd/yyyy hh:mm"),


                            ProductionBatchItems = poItems,
                            ProcessList = productionBatch.BatchProcesses.Select(x =>
                                new ProcessesLookUpModel()
                                {
                                    Id = x.Id,
                                    ProductionBatchId = productionBatch.Id,
                                    Code = x.Code,
                                    ApprovalStatus=x.ApprovalStatus,
                                    EnglishName = x.EnglishName,
                                    ArabicName = x.ArabicName,
                                    Description = x.Description,
                                    IsActive = x.IsActive,
                                    Date = x.Date,
                                    ProcessContractors = x.BatchProcessContractors.Where(z => z.IsActive).Select(y =>
                                         new ProcessContractorLookUpModel()
                                         {
                                             Id = y.Id,
                                             ProductionBatchId = productionBatch.Id,
                                             ProcessId = y.BatchProcessId,
                                             BatchProcessId = y.BatchProcessId,
                                             ContractorId = y.ContractorId,
                                             FromDate = y.FromDate,
                                             FromDates = y.FromDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                             ToDates = y.ToDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                             ToDate = y.ToDate,
                                             IsActive = y.IsActive,
                                             ContractorType = y.ContractorType,
                                             ApprovalStatus = y.ApprovalStatus,
                                             ContractorNameEn = y.Contractor?.EnglishName,
                                             ContractorAccountId=y.Contractor?.EmployeeAccountId,
                                             ContractorNameAr = y.Contractor?.ArabicName,
                                             ContractorItems = y.ContractorItems.Select(item => 
                                                 new ContractorItemsLookUpModel()
                                                 {
                                                     Id = item.Id,
                                                     ProductId = item.ProductId,
                                                     Quantity = item.Quantity,
                                                     HighQuantity = item.HighQuantity,
                                                     Waste = item.Waste,
                                                     //ProductionBatchItemId=item.p
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

                                                 }).ToList()
                                             

                                         }).ToList(),
                                    ProcessItems = x.Process.ProcessItems.Select(i =>
                                        new ProcessItemLookUpModel()
                                        {
                                            Id = i.Id,
                                            ProductId = i.ProductId,
                                            ProcessId = i.ProcessId,
                                        }).ToList(),

                                }).ToList(),

                        };
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
