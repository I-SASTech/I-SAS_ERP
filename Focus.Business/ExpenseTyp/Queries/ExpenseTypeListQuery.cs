using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.Common;
using Focus.Business.ExpenseTyp.Commands;
using Focus.Business.HR.Payroll.Allowances.Commands.AddAllowance;
using Focus.Business.HR.Payroll.Allowances.Queries;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ExpenseTyp.Queries
{
    public class ExpenseTypeListQuery : PagedRequest, IRequest<PagedResult<List<ExpenseTypeLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<ExpenseTypeListQuery, PagedResult<List<ExpenseTypeLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<ExpenseTypeListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<List<ExpenseTypeLookUpModel>>>  Handle(ExpenseTypeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.ExpenseTypes.AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.ExpenseTypeName.ToLower().Contains(searchTerm.ToLower()) ||
                                                 x.ExpenseTypeArabic.ToLower().Contains(searchTerm.ToLower()) );

                    }
                    query = query.OrderBy(x => x.ExpenseTypeName);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var colorList = new List<ExpenseTypeLookUpModel>();
                    foreach (var item in query)
                    {
                        colorList.Add(new ExpenseTypeLookUpModel()
                        {
                            Id = item.Id,
                            ExpenseTypeArabic = item.ExpenseTypeArabic,
                            ExpenseTypeName = item.ExpenseTypeName,
                            Status = item.Status,
                        });
                    }

                    return new PagedResult<List<ExpenseTypeLookUpModel>>
                    {
                        Results = colorList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = colorList.Count / request.PageSize
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
