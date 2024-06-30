using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Interface;
using Focus.Business.Inventories.Queries.GetLatestInventory;
using Focus.Business.PurchaseOrders.Queries.GetPurchaseOrdeRegistrationNo;
using Focus.Business.PurchaseTemplate.Command;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Focus.Business.AutoPurchasing
{
    public class AddAutoPurchaseCommand : IRequest<bool>
    {
        public class Handler : IRequestHandler<AddAutoPurchaseCommand, bool>
        {
            //Create variable of IApplicationDbContext for Add and update database

            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            private readonly IConfiguration _configuration;
            private string _code;
            public Handler(IApplicationDbContext context, IMediator mediator, IConfiguration configuration)
            {
                _context = context;
                _mediator = mediator;
                _configuration = configuration;
            }

            public async Task<bool> Handle(AddAutoPurchaseCommand request, CancellationToken cancellationToken)
            {
                //_context.DisableTenantFilter = true;

                //using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                //var companyAccountSetup = conn.Query<CompanyLookUp>("select * from CompanyAccountSetups").FirstOrDefault();
                //var autoPurchaseTemplate = conn.Query<AutoPurchaseTemplate>("select * ,from AutoPurchaseTemplates left  JOIN AutoPurchaseTemplateItems ON AutoPurchaseTemplateItems.Id = AutoPurchaseTemplates.UnitId where AutoPurchaseTemplates.IsActive=1").AsQueryable();

                var purchaseTemplates = _context.AutoPurchaseTemplates
                    .AsNoTracking()
                    .Include(x => x.AutoPurchaseTemplateItems)
                    .ThenInclude(x => x.Product)
                    .Where(x => x.IsActive)
                    .AsQueryable();

                //var setting = await _context.AutoPurchaseSettings.AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken);
                //if (setting == null)
                //    throw new NotFoundException("Auto Purchase Setting is not defined", "");

                

                foreach (var template in purchaseTemplates)
                {

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Add Purchase Order
                    var purchaseOrderAutoNo = await _mediator.Send(new GetPurchaseOrderRegistrationNoQuery(), cancellationToken);
                    var purchaseOrder = new PurchaseOrder()
                    {
                        Date = DateTime.UtcNow,
                        RegistrationNo = purchaseOrderAutoNo,
                        SupplierId = template.SupplierId,
                        Note = "Auto Purchase Order Create",
                        ApprovalStatus = ApprovalStatus.Draft,
                        TaxMethod = template.TaxMethod,
                        TaxRateId = template.TaxRateId
                    };
                    await _context.PurchaseOrders.AddAsync(purchaseOrder, cancellationToken);

                    //Add Purchase Order Version
                    var purchaseOrderVersion = new PurchaseOrderVersion()
                    {
                        Version = "V.00",
                        PurchaseOrderId = purchaseOrder.Id,
                        Code = purchaseOrderAutoNo,
                        Date = DateTime.UtcNow
                    };
                    await _context.PurchaseOrderVersions.AddAsync(purchaseOrderVersion, cancellationToken);


                    //Add Purchase Order Item
                    var itemList = new List<PurchaseOrderItem>();
                    foreach (var item in template.AutoPurchaseTemplateItems)
                    {
                        if (Convert.ToInt32(item.Product.StockLevel) > 0)
                        {
                            var currentInventory = await _mediator.Send(new GetLatestInventoryQuery()
                            {
                                ProductId = item.ProductId,
                                WareHouseId = template.WareHouseId
                            }, cancellationToken);

                            if (currentInventory.CurrentQuantity <= Convert.ToInt32(item.Product.StockLevel))
                            {
                                itemList.Add(new PurchaseOrderItem
                                {
                                    ProductId = item.ProductId,
                                    TaxRateId = template.TaxRateId,
                                    Discount = item.Discount,
                                    FixDiscount = item.FixDiscount,
                                    Quantity = item.Quantity,
                                    RemainingQuantity = item.Quantity,
                                    SerialNo = "",
                                    GuaranteeDate = null,
                                    ExpiryDate = null,
                                    BatchNo = "",
                                    HighQty = item.HighQty,
                                    UnitPerPack = item.UnitPerPack,
                                    UnitPrice = Math.Round(item.UnitPrice, 2),
                                    PurchaseOrderId = purchaseOrder.Id,
                                    PurchaseOrderVersionId = purchaseOrderVersion.Id
                                });
                            }
                            await _context.PurchaseOrderItems.AddRangeAsync(itemList, cancellationToken);
                        }

                    }


                    if (itemList.Count > 0)
                    {
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo= purchaseOrder.RegistrationNo
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                    }

                }

                _context.DisableTenantFilter = false;
                return true;

            }
        }
    }
}
