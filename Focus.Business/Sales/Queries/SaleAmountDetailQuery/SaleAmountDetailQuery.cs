using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleList;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.OpenXml4Net.OPC;

namespace Focus.Business.Sales.Queries.SaleAmountDetailQuery
{
    public class SaleAmountDetailQuery : IRequest<decimal>
    {
        public Guid Id { get; set; }
        public bool IsReturn { get; set; }
        public bool IsDiscountBeforeVat { get; private set; }

        public class Handler : IRequestHandler<SaleAmountDetailQuery, decimal>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            private readonly IUserHttpContextProvider _contextProvider;
            public readonly IConfiguration Configuration;

            public Handler(IApplicationDbContext context,
                ILogger<SaleAmountDetailQuery> logger,
                IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _contextProvider = contextProvider;
                Configuration = configuration;
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<decimal> Handle(SaleAmountDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    decimal NetAmount = 0;
                    var invoice = await _context.Sales
                        .AsNoTracking()
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.Product)
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.TaxRate)
                        .Include(x => x.SaleOrder)
                        .Include(x => x.Quotation)
                        .Include(x => x.Customer)
                        .ThenInclude(x => x.Account)
                        .Include(x => x.CashCustomer)
                        .Include(x => x.SalePayments)
                        .FirstOrDefaultAsync(x => x.Id == request.Id);
                    if (invoice != null)
                    {
                        NetAmount = !invoice.IsDiscountOnTransaction ? (((invoice.SaleItems.Count(x => x.IsFree == false) > 0 ?
                            invoice.SaleItems.Where(x => x.IsFree == false).Sum(x => (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل") ? ((x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount)) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100))) * (100 + x.TaxRate.Rate) / 100) :
                            (x.Discount == 0 ? ((x.Quantity * x.UnitPrice) - (x.FixDiscount + (x.FixDiscount * x.TaxRate.Rate / 100))) : ((x.Quantity * x.UnitPrice) - ((x.Quantity * x.UnitPrice) * x.Discount / 100)))) : 0) + invoice.Discount + invoice.UnRegisteredVAtAmount))
                            : Math.Round(invoice.SaleItems.Where(x => !x.IsFree).Sum(x => x.UnitPrice * x.Quantity) + ((invoice.TaxMethod == "Exclusive" || invoice.TaxMethod == "غير شامل") ? CalculateTotalVat(invoice) : 0) - CalculateTransactionLevelDiscount(invoice) + invoice.Discount + invoice.UnRegisteredVAtAmount, 2);

                    }
                    return NetAmount;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
                finally
                {
                    _context.DisableTenantFilter = false;
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
