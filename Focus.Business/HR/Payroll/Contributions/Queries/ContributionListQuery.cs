using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.HR.Payroll.Contributions.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Contributions.Queries
{
    public class ContributionListQuery : PagedRequest, IRequest<PagedResult<List<ContributionLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<ContributionListQuery, PagedResult<List<ContributionLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ContributionListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ContributionLookUpModel>>> Handle(ContributionListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.Contributions.AsNoTracking().AsQueryable();

                    if (request.IsDropDown)
                    {
                        query = query.Where(x => x.Active);
                        query = query.OrderBy(x => x.Code);
                        var contribution = new List<ContributionLookUpModel>();
                        foreach (var item in query)
                        {
                            contribution.Add(new ContributionLookUpModel()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                NameInPayslip = item.NameInPayslip,
                                NameInPayslipArabic = item.NameInPayslipArabic,
                                AmountType = item.AmountType,
                                Amount = item.Amount,
                                Active = item.Active,
                            });
                        }

                        return new PagedResult<List<ContributionLookUpModel>>
                        {
                            Results = contribution
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x =>
                            x.Code.Contains(searchTerm) 
                            || x.NameInPayslip.Contains(searchTerm)
                            || x.NameInPayslipArabic.Contains(searchTerm));

                    }

                    query = query.OrderBy(x => x.Code);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<ContributionLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new ContributionLookUpModel()
                        {
                            Id = item.Id,
                            Code = item.Code,
                            NameInPayslip = item.NameInPayslip,
                            NameInPayslipArabic = item.NameInPayslipArabic,
                            Amount = item.Amount,
                            AmountType = item.AmountType,
                            Active = item.Active,
                        });
                    }

                    return new PagedResult<List<ContributionLookUpModel>>
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
