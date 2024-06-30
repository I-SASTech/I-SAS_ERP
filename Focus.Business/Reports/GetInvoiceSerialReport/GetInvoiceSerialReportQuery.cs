using Focus.Business.Interface;
using Focus.Business.Reports.SaleInvoiceReport;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.Reports.GetInvoiceSerialReport
{
    public class GetInvoiceSerialReportQuery : IRequest<List<InvoiceSerialReportModel>>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SerialNumber { get; set; }
        public string BranchId { get; set; }
        public class Handler : IRequestHandler<GetInvoiceSerialReportQuery, List<InvoiceSerialReportModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context,
                ILogger<GetInvoiceSerialReportQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<InvoiceSerialReportModel>> Handle(GetInvoiceSerialReportQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var fromDate = request.FromDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);
                    var toDate = request.ToDate.Date.ToString("yyyy-MM-dd", new CultureInfo("en-US").DateTimeFormat);

                    var saleList = _context.Sales
                        .AsNoTracking()
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.Customer)
                        .Include(x => x.CashCustomer)
                        .Where(x => x.Date >= request.FromDate.Date && x.Date <= request.ToDate.Date.AddDays(1) && x.InvoiceType != InvoiceType.Hold && x.SaleItems.Any(x=>x.SerialNo == request.SerialNumber))
                        .AsQueryable();

                    if (!string.IsNullOrEmpty(request.BranchId))
                    {
                        var branchIdList = new List<string>(request.BranchId.Split(','));
                        saleList = saleList.Where(x => branchIdList.Contains(x.BranchId.ToString()));
                    }
                    var list = new List<InvoiceSerialReportModel>();

                    foreach (var invoice in saleList)
                    {
                        
                        list.Add(new InvoiceSerialReportModel
                        {
                            InvoiceNo = invoice.RegistrationNo,
                            Date = invoice.Date.ToString("d"),
                            CustomerName = invoice.CustomerId == null ? invoice.CashCustomer.Name : invoice.Customer.EnglishName,
                            CustomerNameArabic = invoice.Customer == null ? "" : invoice.Customer.ArabicName,
                            Id= invoice.Id,
                            Total = !invoice.IsDiscountOnTransaction ? (((invoice.SaleItems.Count(x => x.IsFree == false) > 0 ?
                            invoice.SaleItems.Where(x => x.IsFree == false).Sum(x => (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount)) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))) * (100 + x.TaxRate.Rate) / 100) :
                            (x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100))) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100)))) : 0) + invoice.Discount + invoice.UnRegisteredVAtAmount))
                            : Math.Round(invoice.SaleItems.Where(x => !x.IsFree).Sum(x => x.UnitPrice * x.Quantity) + ((invoice.TaxMethod == "Exclusive" || invoice.TaxMethod == "غير شامل") ? CalculateTotalVat(invoice) : 0) - CalculateTransactionLevelDiscount(invoice) + invoice.Discount + invoice.UnRegisteredVAtAmount, 2),

                        });
                    }

                    return list;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }

            public decimal CalculateTotalVat(Sale invoice)
            {
                var sumQuantity = invoice.SaleItems.Sum(product => product.IsFree ? 0 : product.Quantity);
                var total = invoice.SaleItems.Sum(prod => (prod.IsFree ? 0 : ((prod.TaxMethod == "Inclusive" || prod.TaxMethod == "شامل") ? ((((prod.Quantity * prod.UnitPrice) - (prod.FixDiscount + (prod.FixDiscount * prod.TaxRate.Rate / 100)) - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (invoice.IsBeforeTax ? (((prod.Quantity * prod.UnitPrice) * invoice.TransactionLevelDiscount) / 100) : 0))) * prod.TaxRate.Rate) / (100 + prod.TaxRate.Rate) :
                ((((prod.Quantity * prod.UnitPrice) - prod.FixDiscount - (prod.Quantity * prod.UnitPrice * prod.Discount / 100)) - (prod.IsFree ? 0 : (invoice.IsBeforeTax && !invoice.IsFixed && invoice.IsDiscountOnTransaction ? (((prod.Quantity * prod.UnitPrice) * invoice.TransactionLevelDiscount) / 100) : (invoice.IsBeforeTax && invoice.IsFixed && invoice.IsDiscountOnTransaction ? (invoice.TransactionLevelDiscount / sumQuantity * prod.Quantity) : 0)))) * prod.TaxRate.Rate) / 100)));

                return total;
            }
            public decimal CalculateTransactionLevelDiscount(Sale sale)
            {
                var sumQuantity = sale.SaleItems.Sum(product => product.IsFree ? 0 : product.Quantity);
                var total = (sale.IsBeforeTax && sale.IsDiscountOnTransaction) ? (sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? (sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : (x.UnitPrice * x.Quantity))) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) / 100) : (sale.IsFixed ? sale.TransactionLevelDiscount : sale.TransactionLevelDiscount * (sale.SaleItems.Sum(x => x.IsFree ? 0 : x.UnitPrice * x.Quantity) + ((sale.TaxMethod == "Inclusive" || sale.TaxMethod == "شامل") ? 0 : CalculateTotalVat(sale))) / 100);

                return total;
            }
        }
    }
}
