using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.SaveSalaryTaxSlab.Queries
{
    public class GetSalaryTaxSlabListQuery : PagedRequest, IRequest<PagedResult<List<SalaryTaxSlabLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<GetSalaryTaxSlabListQuery, PagedResult<List<SalaryTaxSlabLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetSalaryTaxSlabListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<SalaryTaxSlabLookUpModel>>> Handle(GetSalaryTaxSlabListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.TaxSalaries.AsNoTracking().AsQueryable();

                    if (request.IsDropDown)
                    {
                        var deduction = new List<SalaryTaxSlabLookUpModel>();
                        foreach (var item in query)
                        {
                            deduction.Add(new SalaryTaxSlabLookUpModel()
                            {
                                Id = item.Id,
                                FromDate = item.FromDate,
                                ToDate = item.ToDate
                            });
                        }

                        return new PagedResult<List<SalaryTaxSlabLookUpModel>>
                        {
                            Results = deduction
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x => x.FromDate.ToString("d").Contains(searchTerm) ||
                            x.ToDate.ToString("d").Contains(searchTerm));

                    }

                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<SalaryTaxSlabLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new SalaryTaxSlabLookUpModel()
                        {
                            Id = item.Id,
                            FromDate = item.FromDate,
                            ToDate = item.ToDate,
                        });
                    }

                    return new PagedResult<List<SalaryTaxSlabLookUpModel>>
                    {
                        Results = list,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = list.Count / request.PageSize
                    };





                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
