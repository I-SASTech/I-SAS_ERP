using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PurchaseOrders.Queries.PurchaseOrderDetailQuery
{
    //Not Working 
   public class PurchaseOrderDetailQuery : IRequest<PurchaseOrderLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<PurchaseOrderDetailQuery, PurchaseOrderLookupModel>
        {
            private readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PurchaseOrderDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PurchaseOrderLookupModel> Handle(PurchaseOrderDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var po = await _context.PurchaseOrders
                        .AsNoTracking()
                        .Include(x => x.PurchaseOrderItems)
                        .ThenInclude(x=>x.TaxRate)
                        .Include(x=>x.Supplier)
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

                    var items = new List<PurchaseOrderItemLookupModel>();
                    foreach (var item in po.PurchaseOrderItems)
                    {
                        items.Add(new PurchaseOrderItemLookupModel
                        {
                            Id=item.Id,
                            ProductId=item.ProductId,
                            TaxRateId=item.TaxRateId,
                            Discount=item.Discount,
                            FixDiscount=item.FixDiscount,
                            Quantity=item.Quantity,
                            UnitPrice=item.UnitPrice,
                            PurchaseOrderId=item.PurchaseOrderId
                        });
                    }
                    return new PurchaseOrderLookupModel
                    {
                        Id = po.Id,
                        RegistrationNo = po.RegistrationNo,
                        Date = po.Date,
                        SupplierId = po.SupplierId,
                        InvoiceNo = po.InvoiceNo,
                        InvoiceDate = po.InvoiceDate,
                        Note = po.Note,
                        ApprovalStatus = po.ApprovalStatus,
                        PurchaseOrderItems = items


                    };
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}