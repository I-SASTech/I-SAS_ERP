using Focus.Business.Attachments.Commands;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Focus.Business.CreditNotes.Model;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using System.Text;

namespace Focus.Business.CreditNotes.Queries
{
    public class GetCreditNoteDetailQuery : IRequest<CreditNotesModel>
    {
        public Guid Id { get; set; }
        public bool SimpleQuery { get; set; }
        public class Handler : IRequestHandler<GetCreditNoteDetailQuery, CreditNotesModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            public readonly IMediator Mediator;
            private readonly IHostingEnvironment _hostingEnvironment;



            public Handler(IApplicationDbContext context,
                ILogger<GetCreditNoteDetailQuery> logger, IMediator mediator, IHostingEnvironment hostingEnvironment)
            {
                _context = context;
                _logger = logger;
                Mediator = mediator;
                _hostingEnvironment = hostingEnvironment;
            }

            public async Task<CreditNotesModel> Handle(GetCreditNoteDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.SimpleQuery)
                    {
                        var saleOrder12 = await _context.CreditNotes
                     .AsNoTracking()
                     .Include(x => x.CreditNoteItems)
                     .ThenInclude(x => x.Product)
                     .ThenInclude(x => x.Unit)
                     .Select(credit => new CreditNotesModel
                     {
                         Id = credit.Id,
                         Code = credit.Code,
                         Date = credit.Date,
                         ContactId = credit.ContactId,
                         SaleId = credit.SaleId,
                         PurchasePostId = credit.PurchasePostId,
                         IsInventoryTransaction = credit.IsInventoryTransaction,
                         IsItemDescription = credit.IsItemDescription,
                         Narration = credit.Narration,
                         GrossAmount = credit.GrossAmount,
                         VatAmount = credit.VatAmount,
                         Amount = credit.Amount,
                         TaxMethod = credit.TaxMethod,
                         TaxRateId = credit.TaxRateId,
                         TerminalId = credit.TerminalId,
                         IsCreditNote = credit.IsCreditNote,
                         ApprovalStatus = credit.ApprovalStatus,
                         WareHouseId = credit.WareHouseId,
                         CreatedBy = credit.CreatedBy,
                         Mobile = credit.Contact.ContactNo1,
                         CustomerAddress = credit.Contact.Address,
                         CustomerAddress2 = credit.Contact.DeliveryAddress,
                         Country = credit.Contact.Country,
                         BillingZipCode = credit.Contact.BillingZipCode,
                         City = credit.Contact.City,
                         CustomerNameEn = credit.Contact.EnglishName,
                         CustomerVat = credit.Contact.VatNo,
                         CustomerNameAr = credit.Contact.ArabicName,
                         CustomerCRN = credit.Contact.CommercialRegistrationNo,
                         DeliveryAddress = credit.Contact.DeliveryAddress,

                         SaleOrderItems = credit.CreditNoteItems.Select(item => new CreditNotesItemModel()
                         {
                             ProductId = item.ProductId,
                             TaxRateId = item.TaxRateId,
                             TaxMethod = item.TaxMethod,
                             WareHouseId = credit.WareHouseId,
                             Discount = item.Discount,
                             FixDiscount = item.FixDiscount,
                             Quantity = item.Quantity,
                             UnitPrice = item.UnitPrice,
                             Total = item.UnitPrice * item.Quantity,
                             Code = item.Product.Code,
                             Description = item.Product.Description,
                             GrossAmount = item.TotalWithOutAmount,
                             TotalAmount = item.TotalAmount,
                             VatAmount = item.VatAmount,
                             DiscountAmount = item.DiscountAmount,
                             ProductName = !string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.DisplayNameForPrint : item.Product.DisplayName,
                             UnitName = !string.IsNullOrEmpty(item.Product.Unit.Name) ? item.Product.Unit.Name : "",

                             Product = new ProductDropdownLookUpModel
                             {
                                 Id = item.Product.Id,
                                 BarCode = item.Product.BarCode,
                                 StyleNumber = item.Product.StyleNumber,
                                 Length = item.Product.Length,
                                 Width = item.Product.Width,
                                 UnitPerPack = item.Product.UnitPerPack,
                                 WholesaleQuantity = item.Product.WholesaleQuantity,
                                 WholesalePrice = item.Product.WholesalePrice,

                                 Code = item.Product.Code,
                                 SaleReturnDays = item.Product.SaleReturnDays,
                                 EnglishName = item.Product.EnglishName,
                                 ArabicName = item.Product.ArabicName,
                                 LevelOneUnit = item.Product.LevelOneUnit,
                                 BasicUnit = item.Product.Unit.Name,
                                 IsExpire = item.Product.IsExpire,
                                 ServiceItem = item.Product.ServiceItem,
                                 Serial = item.Product.Serial,
                                 HighUnitPrice = item.Product.HighUnitPrice,
                                 Guarantee = item.Product.Guarantee,
                                 TaxRateId = item.Product.TaxRateId.Value,
                             }

                         }).ToList()
                     }).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


                        StringBuilder addressBuilder = new StringBuilder();

                        string address1 = saleOrder12.CustomerAddress;
                        string address2 = saleOrder12.CustomerAddress2;
                        string country1 = saleOrder12.Country;
                        string zipCode1 = saleOrder12.BillingZipCode;
                        string city1 = saleOrder12.City;

                        if (!string.IsNullOrEmpty(address1))
                        {
                            addressBuilder.AppendLine(address1);
                        }

                        if (!string.IsNullOrEmpty(address2))
                        {
                            addressBuilder.AppendLine(address2);
                        }

                        string zipCode = string.IsNullOrEmpty(zipCode1) ? "" : zipCode1;
                        string city = string.IsNullOrEmpty(city1) ? "" : city1;
                        string country = string.IsNullOrEmpty(country1) ? "" : country1;

                        string lastLine = $"{zipCode}{(string.IsNullOrEmpty(zipCode) ? "" : ", ")}{city}{(string.IsNullOrEmpty(city) ? "" : ", ")}{country}".Trim();
                        if (!string.IsNullOrEmpty(lastLine))
                        {
                            addressBuilder.AppendLine(lastLine);
                        }

                        saleOrder12.CustomerAddress = addressBuilder.ToString().TrimEnd('\r', '\n');




                        if (saleOrder12 != null)
                        {
                            var saleNo = _context.Sales.AsNoTracking().Select(x => new
                            {
                                Id = x.Id,
                                RegistrationNo = x.RegistrationNo
                            }).FirstOrDefault(x => x.Id == saleOrder12.SaleId);
                            if (saleNo != null)
                            {
                                saleOrder12.SaleNo = saleNo.RegistrationNo;
                            }
                        }





                        return saleOrder12;



                    }


                    var saleOrder = await _context.CreditNotes
                        .AsNoTracking()
                        .Include(x => x.CreditNoteItems)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Unit)
                        .Select(credit => new CreditNotesModel
                        {
                            Id = credit.Id,
                            Code = credit.Code,
                            Date = credit.Date,
                            ContactId = credit.ContactId,
                            SaleId = credit.SaleId,
                            PurchasePostId = credit.PurchasePostId,
                            IsInventoryTransaction = credit.IsInventoryTransaction,
                            IsItemDescription = credit.IsItemDescription,
                            Narration = credit.Narration,
                            GrossAmount = credit.GrossAmount,
                            VatAmount = credit.VatAmount,
                            Amount = credit.Amount,
                            TaxMethod = credit.TaxMethod,
                            TaxRateId = credit.TaxRateId,
                            TerminalId = credit.TerminalId,
                            IsCreditNote = credit.IsCreditNote,
                            ApprovalStatus = credit.ApprovalStatus,
                            WareHouseId = credit.WareHouseId,
                            CreatedBy = credit.CreatedBy,
                            Mobile = credit.Contact.ContactNo1,
                            CustomerAddress = credit.Contact.Address,
                            CustomerNameEn = credit.Contact.EnglishName,
                            CustomerVat = credit.Contact.VatNo,
                            CustomerNameAr = credit.Contact.ArabicName,
                            CustomerCRN = credit.Contact.CommercialRegistrationNo,
                            DeliveryAddress = credit.Contact.DeliveryAddress,
                            WareHouseName = credit.CreditNoteItems.FirstOrDefault().WareHouse.Name,

                            SaleOrderItems = credit.CreditNoteItems.Select(item => new CreditNotesItemModel()
                            {
                                ProductId = item.ProductId,
                                TaxRateId = item.TaxRateId,
                                TaxMethod = item.TaxMethod,
                                WareHouseId = credit.WareHouseId,
                                Discount = item.Discount,
                                FixDiscount = item.FixDiscount,
                                Quantity = item.Quantity,
                                UnitPrice = item.UnitPrice,
                                TaxRate = item.TaxRate.Rate.ToString("0.00"),
                                Total = item.UnitPrice * item.Quantity,
                                Code = item.Product.Code,
                                Description = item.Product.Description,
                                GrossAmount = item.TotalWithOutAmount,
                                TotalAmount = item.TotalAmount,
                                VatAmount = item.VatAmount,
                                DiscountAmount = item.DiscountAmount,
                                ProductName = !string.IsNullOrEmpty(item.Product.DisplayNameForPrint) ? item.Product.DisplayNameForPrint : item.Product.DisplayName,
                                UnitName = !string.IsNullOrEmpty(item.Product.Unit.Name) ? item.Product.Unit.Name : "",

                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = item.Product.Id,
                                    BarCode = item.Product.BarCode,
                                    StyleNumber = item.Product.StyleNumber,
                                    Length = item.Product.Length,
                                    Width = item.Product.Width,
                                    UnitPerPack = item.Product.UnitPerPack,
                                    WholesaleQuantity = item.Product.WholesaleQuantity,
                                    WholesalePrice = item.Product.WholesalePrice,

                                    Code = item.Product.Code,
                                    SaleReturnDays = item.Product.SaleReturnDays,
                                    EnglishName = item.Product.EnglishName,
                                    ArabicName = item.Product.ArabicName,
                                    LevelOneUnit = item.Product.LevelOneUnit,
                                    BasicUnit = item.Product.Unit.Name,
                                    IsExpire = item.Product.IsExpire,
                                    ServiceItem = item.Product.ServiceItem,
                                    Serial = item.Product.Serial,
                                    HighUnitPrice = item.Product.HighUnitPrice,
                                    Guarantee = item.Product.Guarantee,
                                    TaxRateId = item.Product.TaxRateId.Value,
                                }

                            }).ToList()
                        }).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    var taxRate = await _context.TaxRates.FirstOrDefaultAsync(x => x.Id == saleOrder.TaxRateId);
                    saleOrder.TaxRateName = taxRate != null ? taxRate.Name : "";


                    if (saleOrder != null)
                    {
                        var saleNo = _context.Sales.AsNoTracking().Select(x => new
                        {
                            Id = x.Id,
                            RegistrationNo = x.RegistrationNo
                        }).FirstOrDefault(x => x.Id == saleOrder.SaleId);
                        if (saleNo != null)
                        {
                            saleOrder.SaleNo = saleNo.RegistrationNo;
                        }

                        var attachmentList = await _context.Attachments.Where(x => x.DocumentId == saleOrder.Id).Select(x => new AttachmentLookUpModel
                        {
                            Id = x.Id,
                            DocumentId = x.DocumentId,
                            Date = x.Date,
                            Description = x.Description,
                            FileName = x.FileName,
                            Path = x.Path
                        }).ToListAsync(cancellationToken: cancellationToken);

                        saleOrder.AttachmentList = attachmentList;

                        return saleOrder;
                    }
                    throw new NotFoundException(nameof(saleOrder), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
            public async Task<string> GetBaseImage(string filePath)
            {
                if (string.IsNullOrEmpty(filePath))
                    return filePath;

                var imageExist = File.Exists(_hostingEnvironment.WebRootPath + filePath);

                if (!imageExist)
                    return "";

                var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }
    }
}
