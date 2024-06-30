using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Sales.Queries.CashVoucherQuery
{
    public class GetCashVoucherDetailQuery : IRequest<CashVoucherLookUpModel>
    {
        public string VoucherNo { get; set; }

        public class Handler : IRequestHandler<GetCashVoucherDetailQuery, CashVoucherLookUpModel>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            public readonly IMapper Mapper;
            public readonly UserManager<ApplicationUser> UserManager;

            public Handler(IApplicationDbContext context,
                ILogger<GetCashVoucherDetailQuery> logger,
                IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                UserManager = userManager;
                Context = context;
                Logger = logger;
                Mapper = mapper;
            }
            public async Task<CashVoucherLookUpModel> Handle(GetCashVoucherDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cashVoucher = await Context.CashVouchers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.VoucherNo == request.VoucherNo, cancellationToken);

                    if (cashVoucher == null)
                        throw new NotFoundException(request.VoucherNo+" Cash Voucher Is not found", "");

                    if (cashVoucher.IsActive)
                        throw new ObjectAlreadyExistsException("Voucher Is Already Used");

                    if (cashVoucher.Date.Date!=DateTime.UtcNow.Date)
                        throw new ApplicationException("Voucher Is Expired");


                    return new CashVoucherLookUpModel
                    {
                        Id = cashVoucher.Id,
                        Date = cashVoucher.Date,
                        Amount = cashVoucher.Amount,
                        SaleInvoiceId = cashVoucher.SaleInvoiceId,
                        SaleReturnId = cashVoucher.SaleReturnId
                    };
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
