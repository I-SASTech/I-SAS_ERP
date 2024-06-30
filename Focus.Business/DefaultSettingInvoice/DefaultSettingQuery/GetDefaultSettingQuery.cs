using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Focus.Business.DefaultSettingInvoice.DefaultSattingCommand;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.DefaultSettingInvoice.DefaultSettingQuery
{
    public class GetDefaultSettingQuery : IRequest<DefaultSettingModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetDefaultSettingQuery, DefaultSettingModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetDefaultSettingQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<DefaultSettingModel> Handle(GetDefaultSettingQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var defaultSetting =  await _context.DefaultSettings.FirstOrDefaultAsync();
                    if (defaultSetting == null)
                        return null;

                    var data = new DefaultSettingModel()
                    {
                        IsSaleCredit = defaultSetting.IsSaleCredit,
                        IsPurchaseCredit = defaultSetting.IsPurchaseCredit,
                        IsCustomeCredit = defaultSetting.IsCustomeCredit,
                        IsCustomerPayCredit = defaultSetting.IsCustomerPayCredit,
                        IsSupplierPayCredit = defaultSetting.IsSupplierPayCredit,
                        IsSupplierCredit = defaultSetting.IsSupplierCredit,
                        IsCashCustomer = defaultSetting.IsCashCustomer,
                        UserId = defaultSetting.UserId,
                    };
                    return data;
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
