using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.HR.Payroll.Deductions.Commands;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Deductions.Queries
{
    public class DeductionListQuery : PagedRequest, IRequest<PagedResult<List<DeductionLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<DeductionListQuery, PagedResult<List<DeductionLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<DeductionListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<DeductionLookUpModel>>> Handle(DeductionListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var query = _context.Deductions.AsNoTracking().AsQueryable();

                    if (request.IsDropDown)
                    {
                        query = query.Where(x => x.Active);
                        query = query.OrderBy(x => x.Code);
                        var deduction = new List<DeductionLookUpModel>();
                        foreach (var item in query)
                        {
                            deduction.Add(new DeductionLookUpModel()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                NameInPayslip = item.NameInPayslip,
                                NameInPayslipArabic = item.NameInPayslipArabic,
                                Amount = item.Amount,
                                AmountType = item.AmountType,
                                TaxPlan = item.TaxPlan,
                                Active = item.Active,
                            });
                        }

                        return new PagedResult<List<DeductionLookUpModel>>
                        {
                            Results = deduction
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

                    var list = new List<DeductionLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new DeductionLookUpModel()
                        {
                            Id = item.Id,
                            Code = item.Code,
                            NameInPayslip = item.NameInPayslip,
                            NameInPayslipArabic = item.NameInPayslipArabic,
                            Amount = item.Amount,
                            AmountType = item.AmountType,
                            TaxPlan = item.TaxPlan,
                            Active = item.Active,
                        });
                    }

                    return new PagedResult<List<DeductionLookUpModel>>
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
