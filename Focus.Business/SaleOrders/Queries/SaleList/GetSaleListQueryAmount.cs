using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Focus.Business.AutoPurchasing;
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

namespace Focus.Business.SaleOrders.Queries.SaleList
{
    public class GetSaleListQueryAmount : IRequest<decimal>
    {
        public string SearchTerm { get; set; }
        public bool IsSaleDashboard { get; set; }
        public bool isSaleFilter { get; set; }
        public bool IsSaleReturnPost { get; set; }
        public bool IsExpense { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? Id { get; set; }

        public class Handler : IRequestHandler<GetSaleListQueryAmount, decimal>
        {
            public readonly IConfiguration Configuration;
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetSaleListQuery> logger,
                IConfiguration configuration, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _userManager = userManager;
            }

            public async Task<decimal> Handle(GetSaleListQueryAmount request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.Sales
                        .AsNoTracking()
                        .Include(x=>x.SaleInvoiceAdvancePayments)
                        .Where(x => !x.IsSaleReturn && x.InvoiceType != InvoiceType.SaleReturn && x.InvoiceType != InvoiceType.Hold && x.SaleOrderId == request.Id)
                        .AsQueryable();



                    
                    //var total = query.Sum(x => x.NetAmount);
                    var total = query.Select(x=>x.SaleInvoiceAdvancePayments.Sum(y=>y.Amount));

                    var amount = total.ToList().Sum();
                    return amount;



                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }

            }
        }
    }
}
