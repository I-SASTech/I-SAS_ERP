using Focus.Business.Interface;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Sales.Commands.AddCashCustomer
{
    public class AddCashCustomerCommand : IRequest<Guid>
    {
        public CashCustomerLookupModel CashCustomer { get; set; }

        public class Handler : IRequestHandler<AddCashCustomerCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddCashCustomerCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddCashCustomerCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    var code = await AutoGenerateCashCustomer();

                    if (string.IsNullOrEmpty(request.CashCustomer.Name))
                        throw new ApplicationException("Name is required");

                    if (string.IsNullOrEmpty(request.CashCustomer.Mobile))
                        throw new ApplicationException("Mobile is required");

                    if (string.IsNullOrEmpty(request.CashCustomer.Code))
                        throw new ApplicationException("Code is required");

                    var cashCustomer = new CashCustomer
                    {
                        Name = request.CashCustomer.Name,
                        Mobile = request.CashCustomer.Mobile,
                        Code = code,
                        Email = request.CashCustomer.Email,
                        CustomerId = request.CashCustomer.CashCustomerId
                    };

                    await _context.CashCustomer.AddAsync(cashCustomer, cancellationToken);

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = _context.SyncLog(),
                        Code = _code,
                        DocumentNo= cashCustomer.Code
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    
                    return cashCustomer.Id;
                }

                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);

                    throw new ApplicationException(e.Message);
                }
            }

            public async Task<string> AutoGenerateCashCustomer()
            {
                var sale = await _context.CashCustomer
                       .OrderBy(x => x.Code)
                       .LastOrDefaultAsync();

                if (sale != null)
                {
                    if (string.IsNullOrEmpty(sale.Code))
                    {
                        return GenerateCodeFirstTime();
                    }

                    return GenerateNewCode(sale.Code);
                }

                return GenerateCodeFirstTime();
            }

            public string GenerateCodeFirstTime()
            {
                return "CA-00001";
            }

            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "CA-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
