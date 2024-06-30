using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Commands;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Queries
{
    public class GetSalaryTaxSlabDetailQuery : IRequest<SalaryTaxSlabLookUpModel>
    {
        public Guid? Id { get; set; }

        public class Handler : IRequestHandler<GetSalaryTaxSlabDetailQuery, SalaryTaxSlabLookUpModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetSalaryTaxSlabDetailQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<SalaryTaxSlabLookUpModel> Handle(GetSalaryTaxSlabDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Id!=null)
                    {
                        var taxSalary = await _context.TaxSalaries.FindAsync(request.Id);
                        if (taxSalary != null)
                        {

                            return new SalaryTaxSlabLookUpModel()
                            {
                                Id = taxSalary.Id,
                                FromDate = taxSalary.FromDate,
                                ToDate = taxSalary.ToDate,
                                SalaryTaxSlabList = taxSalary.SalaryTaxSlabs.Select(x => new SalaryTaxSlab
                                {
                                    Id = x.Id,
                                    IncomeFrom = x.IncomeFrom,
                                    IncomeTo = x.IncomeTo,
                                    FixedTax = x.FixedTax,
                                    Rate = x.Rate
                                }).ToList()
                            };
                        }
                        throw new NotFoundException(nameof(taxSalary), request.Id);
                    }
                    else
                    {
                        var taxSalary = await _context.TaxSalaries
                            .AsNoTracking()
                            .Include(x=>x.SalaryTaxSlabs)
                            .FirstOrDefaultAsync(x=>x.FromDate.Date<=DateTime.UtcNow.Date && x.ToDate.Date >= DateTime.UtcNow.Date, cancellationToken: cancellationToken);
                        
                        if (taxSalary != null)
                        {
                            return new SalaryTaxSlabLookUpModel()
                            {
                                Id = taxSalary.Id,
                                FromDate = taxSalary.FromDate,
                                ToDate = taxSalary.ToDate,
                                SalaryTaxSlabList = taxSalary.SalaryTaxSlabs.Select(x => new SalaryTaxSlab
                                {
                                    Id = x.Id,
                                    IncomeFrom = x.IncomeFrom,
                                    IncomeTo = x.IncomeTo,
                                    FixedTax = x.FixedTax,
                                    Rate = x.Rate
                                }).ToList()
                            };
                        }
                        return null;
                    }

                    
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
