using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.InvoiceDefaults.Queries
{
    public class InvoiceDefaultDetailQuery:IRequest<InvoiceDefaultLookUpModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<InvoiceDefaultDetailQuery, InvoiceDefaultLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<InvoiceDefaultDetailQuery> logger)
            {
                _context = context;
                _logger = logger;

            }

            public async Task<InvoiceDefaultLookUpModel> Handle(InvoiceDefaultDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var invoiceDefault =  _context.InvoiceDefaults.FirstOrDefault();

                if (invoiceDefault != null)
                    {
                        return new InvoiceDefaultLookUpModel
                        {
                            Id = invoiceDefault.Id,
                            IsSalePrice = invoiceDefault.IsSalePrice,
                            IsCustomerPrice = invoiceDefault.IsCustomerPrice,
                            IsSalePriceLabel = invoiceDefault.IsSalePriceLabel,
                            IsCustomerPriceLabel = invoiceDefault.IsCustomerPriceLabel,

                        };

                    }

                    return new InvoiceDefaultLookUpModel();
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

