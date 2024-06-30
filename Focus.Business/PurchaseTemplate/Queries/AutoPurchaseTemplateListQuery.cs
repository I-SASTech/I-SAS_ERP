using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchaseTemplate.Queries
{
    public class AutoPurchaseTemplateListQuery : PagedRequest, IRequest<PagedResult<List<PurchaseTemplateListLookUp>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<AutoPurchaseTemplateListQuery, PagedResult<List<PurchaseTemplateListLookUp>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AutoPurchaseTemplateListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<PurchaseTemplateListLookUp>>> Handle(AutoPurchaseTemplateListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var po = _context.AutoPurchaseTemplates
                            .AsNoTracking()
                            .Include(x => x.AutoPurchaseTemplateItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.AutoPurchaseTemplateItems)
                            .ThenInclude(x => x.Product)
                            .Include(x => x.Supplier)
                            .Where(x => x.IsActive)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            po = po.Where(x => x.BranchId == request.BranchId);
                        }

                        var purchaseOrderList = new List<PurchaseTemplateListLookUp>();
                        foreach (var purchaseOrder in po)
                        {
                            purchaseOrderList.Add(new PurchaseTemplateListLookUp()
                            {
                                Id = purchaseOrder.Id,
                                Day = purchaseOrder.Day,
                                RegistrationNumber = purchaseOrder.RegistrationNo,
                                IsActive = purchaseOrder.IsActive,
                                SupplierName = purchaseOrder.Supplier?.EnglishName,
                                SupplierNameArabic = purchaseOrder.Supplier?.ArabicName,
                                SupplierAdvanceAccountId = purchaseOrder.Supplier?.AdvanceAccountId,
                                NetAmount = CalculateTotalAmount(purchaseOrder.AutoPurchaseTemplateItems.ToList(), purchaseOrder.TaxMethod),
                                Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                            });
                        }
                        return new PagedResult<List<PurchaseTemplateListLookUp>>
                        {
                            Results = purchaseOrderList
                        };

                    }
                    else
                    {
                        var query = _context.AutoPurchaseTemplates
                            .AsNoTracking()
                            .Include(x => x.AutoPurchaseTemplateItems)
                            .ThenInclude(x => x.TaxRate)
                            .Include(x => x.AutoPurchaseTemplateItems)
                            .ThenInclude(x => x.Product)
                            .Include(x => x.Supplier)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }
                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate);
                        }
                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate);
                        }


                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                            query = query.Where(x => x.RegistrationNo.Contains(searchTerm) || x.Supplier.EnglishName.Contains(searchTerm) || x.Supplier.ArabicName.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));
                        }

                        query = query.OrderBy(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        var branchList = _context.Branches.AsNoTracking().ToList();

                        var purchaseOrderList = new List<PurchaseTemplateListLookUp>();
                        foreach (var purchaseOrder in query)
                        {
                            purchaseOrderList.Add(new PurchaseTemplateListLookUp()
                            {
                                Id = purchaseOrder.Id,
                                Day = purchaseOrder.Day,
                                RegistrationNumber = purchaseOrder.RegistrationNo,
                                IsActive = purchaseOrder.IsActive,
                                SupplierName = purchaseOrder.Supplier?.EnglishName,
                                SupplierNameArabic = purchaseOrder.Supplier?.ArabicName,
                                SupplierAdvanceAccountId = purchaseOrder.Supplier?.AdvanceAccountId,
                                NetAmount = CalculateTotalAmount(purchaseOrder.AutoPurchaseTemplateItems.ToList(), purchaseOrder.TaxMethod),
                                Date = purchaseOrder.Date.ToString("MM/dd/yyyy hh:mm tt"),
                                BranchCode = branchList.FirstOrDefault(x => x.Id == purchaseOrder.BranchId)?.Code,

                            });
                        }

                        return new PagedResult<List<PurchaseTemplateListLookUp>>
                        {
                            Results = purchaseOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = purchaseOrderList.Count / request.PageSize
                        };
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
        public static decimal CalculateTotalAmount(List<AutoPurchaseTemplateItem> purchaseItem, string taxMethod)
        {
            decimal netAmount = 0;
            foreach (var item in purchaseItem)
            {
                var qty = item.Quantity;
                var discount = item.Discount == 0 ? item.FixDiscount * qty : (item.UnitPrice * qty * item.Discount) / 100;
                var amountWithDiscount = (item.UnitPrice * qty) - discount;
                var vat = (taxMethod == "Exclusive" || taxMethod == "غير شامل") ? ((amountWithDiscount * item.TaxRate.Rate) / 100) : 0;
                netAmount += amountWithDiscount + vat;
            }

            return netAmount;
        }
    }


}
