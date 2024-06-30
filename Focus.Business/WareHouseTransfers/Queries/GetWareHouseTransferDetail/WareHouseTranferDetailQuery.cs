using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Attachments.Commands;

namespace Focus.Business.WareHouseTransfers.Queries.GetWareHouseTransferDetail
{
   public class WareHouseTransferDetailQuery : IRequest<WareHouseTransferLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<WareHouseTransferDetailQuery, WareHouseTransferLookupModel>
        {
            private readonly IApplicationDbContext _context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<WareHouseTransferDetailQuery> logger)
            {
                _context = context;
                Logger = logger;
            }

            public async Task<WareHouseTransferLookupModel> Handle(WareHouseTransferDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var po = await _context.WareHouseTransfers
                        .AsNoTracking()
                        .Include(x => x.WareHouseTransferItems)
                        .ThenInclude(x => x.Product)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    var attachments = _context.Attachments
                        .AsNoTracking()
                        .Where(x => x.DocumentId == po.Id)
                        .AsQueryable();

                    var attachmentList = await attachments.Select(x => new AttachmentLookUpModel
                    {
                        Id = x.Id,
                        DocumentId = x.DocumentId,
                        Date = x.Date,
                        Description = x.Description,
                        FileName = x.FileName,
                        Path = x.Path
                    }).ToListAsync(cancellationToken: cancellationToken);


                    var items = new List<WareHouseTransferItemLookupModel>();
                    foreach (var item in po.WareHouseTransferItems)
                    {
                        items.Add(new WareHouseTransferItemLookupModel
                        {
                            Id=item.Id,
                            ProductId=item.ProductId,
                            Quantity=item.Quantity,
                            WareHouseTransferId=item.WareHouseTransferId,
                            Product = new ProductDropdownLookUpModel
                            {
                                Id = item.Product.Id,
                                BarCode = item.Product.BarCode,
                                Code = item.Product.Code,
                                EnglishName = item.Product.EnglishName,
                                ArabicName = item.Product.ArabicName,
                                DisplayName = item.Product.DisplayName,
                                IsExpire = item.Product.IsExpire,
                                UnitPerPack = item.Product.UnitPerPack,
                                Inventory = new Inventory()
                                {
                                    CurrentQuantity = _context.Stocks.FirstOrDefault(x => x.ProductId == item.ProductId && x.WareHouseId == po.FromWareHouseId)?.CurrentQuantity??0
                                }
                            }
                        });
                    }
                    return new WareHouseTransferLookupModel
                    {
                        Id = po.Id,
                        Code = po.Code,
                        Date = po.Date,
                        FromWareHouseId = po.FromWareHouseId,
                        ToWareHouseId = po.ToWareHouseId,
                        ApprovalStatus=po.ApprovalStatus,
                        WareHouseTransferItems = items,
                        AttachmentList = attachmentList,
                        FromBranchId = po.FromBranchId,
                        ToBranchId = po.ToBranchId,

                    };
                }
                catch (Exception e)
                {
                    Logger.LogError(e.Message);

                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}