using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.AutoPurchasing
{
    public class AutoPurchaseDetailQuery : IRequest<AutoPurchaseSettingLookUp>
    {
        public class Handler : IRequestHandler<AutoPurchaseDetailQuery, AutoPurchaseSettingLookUp>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AutoPurchaseDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<AutoPurchaseSettingLookUp> Handle(AutoPurchaseDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var autoPurchaseSetting = await _context.AutoPurchaseSettings.FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    if (autoPurchaseSetting != null)
                    {
                        var data = new AutoPurchaseSettingLookUp()
                        {
                            Id = autoPurchaseSetting.Id,
                            Day = autoPurchaseSetting.Day,
                            TaxMethod = autoPurchaseSetting.TaxMethod,
                            TaxRateId = autoPurchaseSetting.TaxRateId,
                            WareHouseId = autoPurchaseSetting.WareHouseId
                        };
                        return data;
                    }
                    return null;
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
