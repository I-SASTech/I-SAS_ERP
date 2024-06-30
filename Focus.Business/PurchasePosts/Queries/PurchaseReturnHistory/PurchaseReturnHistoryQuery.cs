using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail;
using MediatR;

namespace Focus.Business.PurchasePosts.Queries.PurchaseReturnHistory
{
    public class PurchaseReturnHistoryQuery : IRequest<PurchaseReturnList>
    {
        public Guid Id { get; set; }
        public bool IsReturnView { get; set; }
        public bool IsMultiUnit { get; set; }

        public class Handler : IRequestHandler<PurchaseReturnHistoryQuery, PurchaseReturnList>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            public readonly IMediator Mediator;

            public Handler(IApplicationDbContext context,
                ILogger<PurchaseReturnHistoryQuery> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                Mediator = mediator;
            }
            public async Task<PurchaseReturnList> Handle(PurchaseReturnHistoryQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseReturnLists = new List<PurchasePostLookupModel>();
                    var purchases = _context.PurchasePosts.AsNoTracking().Where(x => x.PurchaseInvoiceId == request.Id).Select(x => x.Id).ToList();
                    foreach (var item in purchases)
                    {
                        var results = await Mediator.Send(new PurchasePostDetailQuery
                        {
                            Id = item,
                            IsReturnView = request.IsReturnView,
                            IsMultiUnit = request.IsMultiUnit

                        }, cancellationToken);
                        //foreach (var d in results.PurchasePostItems)
                        //{
                        //    d.Quantity = request.IsMultiUnit
                        //        ? Convert.ToInt32(Convert.ToInt32(d.Quantity) % Convert.ToInt32(d.UnitPerPack))
                        //        : d.Quantity;
                        //    d.HighQty = request.IsMultiUnit
                        //        ? Convert.ToInt32(Convert.ToInt32(d.Quantity) / Convert.ToInt32(d.UnitPerPack))
                        //        : 0;
                        //}
                        purchaseReturnLists.Add(new PurchasePostLookupModel
                        {
                            Id = results.Id,
                            RegistrationNo = results.RegistrationNo,
                            PurchaseOrderNo = results?.PurchaseOrderNo,
                            TaxRatesName = results.TaxRatesName,
                            WareHouseName = results.WareHouseName,
                            DiscountAmount = results.DiscountAmount,
                            Date = results.Date,
                            SupplierId = results.SupplierId,
                            SupplierName = results.SupplierName,
                            InvoiceNo = results.InvoiceNo,
                            InvoiceDate = results.InvoiceDate,
                            IsPurchaseReturn = results.IsPurchaseReturn,
                            WareHouseId = results.WareHouseId,
                            TaxMethod = results.TaxMethod,
                            TaxRateId = results.TaxRateId,
                            IsRaw = results.IsRaw,
                            PurchasePostItems = results.PurchasePostItems
                        });

                    }





                    return new PurchaseReturnList
                    {
                        PurchaseReturnListHistory = purchaseReturnLists
                    };



                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
