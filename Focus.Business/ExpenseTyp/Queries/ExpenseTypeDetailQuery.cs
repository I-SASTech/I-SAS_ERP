using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.ExpenseTyp.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ExpenseTyp.Queries
{
    public class ExpenseTypeDetailQuery : IRequest<ExpenseTypeLookUpModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<ExpenseTypeDetailQuery, ExpenseTypeLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<ExpenseTypeDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ExpenseTypeLookUpModel> Handle(ExpenseTypeDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var expenseType = await _context.ExpenseTypes.FindAsync(request.Id);

                    if (expenseType != null)
                    {
                        return new ExpenseTypeLookUpModel
                        {
                            Id = expenseType.Id,
                            ExpenseTypeName = expenseType.ExpenseTypeName,
                            ExpenseTypeArabic = expenseType.ExpenseTypeArabic,
                            AccountId = expenseType.AccountId,
                            Description = expenseType.Description,
                            Status = expenseType.Status,
                        };
                    }
                    throw new NotFoundException(nameof(expenseType), request.Id);
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
