using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.FinancialYearSettings.Models;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.FinancialYearSettings.Queries
{
    public class GetFinancialYearSettingDetailQuery : IRequest<FinancialYearSettingModel>
    {
        public class Handler : IRequestHandler<GetFinancialYearSettingDetailQuery, FinancialYearSettingModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetFinancialYearSettingDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<FinancialYearSettingModel> Handle(GetFinancialYearSettingDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var financialYear = await _context.FinancialYearSettings
                        .AsNoTracking()
                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                    
                    if (financialYear != null)
                    {
                        return new FinancialYearSettingModel
                        {
                            Id = financialYear.Id,
                            IsAutoClosing = financialYear.IsAutoClosing,
                            ClosingType = financialYear.ClosingType,
                            
                        };
                    }

                    return new FinancialYearSettingModel();
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
