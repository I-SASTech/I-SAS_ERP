using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using Focus.Business.Sales.Queries.GetSaleDetail;

namespace Focus.Business.QuotationTemplates.Commands.AddQuotationTemplate
{
    public class GetQuotationTemplateDetailQuery : IRequest<QuotationTemplateLookUp>
    {
        public Guid Id { get; set; }
        public bool IsMultiUnit { get; set; }


        public class Handler : IRequestHandler<GetQuotationTemplateDetailQuery, QuotationTemplateLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetQuotationTemplateDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<QuotationTemplateLookUp> Handle(GetQuotationTemplateDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchaseOrder = await _context.QuotationTemplates.FindAsync(request.Id);


                    if (purchaseOrder != null)
                    {
                        var poItems = new List<QuotationTemplateItemLookUpModel>();
                        foreach (var item in purchaseOrder.QuotationTemplateItems)
                        {
                            poItems.Add(new QuotationTemplateItemLookUpModel
                            {
                                Id = item.Id,
                                UnitPrice = item.UnitPrice,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                                ProductName = string.IsNullOrEmpty(item.Product.EnglishName) ? item.Product.ArabicName : item.Product.EnglishName,
                                ProductNameEn = item.Product.EnglishName,
                                ProductNameAr = item.Product.ArabicName,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = item.Product.Id,
                                    BarCode = item.Product.BarCode,
                                    Code = item.Product.Code,
                                    EnglishName = item.Product.EnglishName,
                                    ArabicName = item.Product.ArabicName,
                                   


                                }
                            });
                        }



                        return new QuotationTemplateLookUp
                        {
                            Id = purchaseOrder.Id,
                            Date = purchaseOrder.Date,
                            IsService = purchaseOrder.IsService,
                            ApprovalStatus = purchaseOrder.ApprovalStatus,
                            RegistrationNo = purchaseOrder.RegistrationNo,
                            Description = purchaseOrder.Description,
                            TemplateName = purchaseOrder.TemplateName,
                            QuotationTemplateItems= poItems

                        };
                    }
                    throw new NotFoundException(nameof(purchaseOrder), request.Id);
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
