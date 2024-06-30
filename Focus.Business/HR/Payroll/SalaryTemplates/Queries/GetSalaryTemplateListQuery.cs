using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.SalaryTemplates.Queries
{
    public class GetSalaryTemplateListQuery : PagedRequest, IRequest<PagedResult<List<SalaryTemplateListLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<GetSalaryTemplateListQuery, PagedResult<List<SalaryTemplateListLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetSalaryTemplateListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<SalaryTemplateListLookUpModel>>> Handle(GetSalaryTemplateListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.SalaryTemplates.AsNoTracking().AsQueryable();

                    if (request.IsDropDown)
                    {
                        query = query.OrderBy(x => x.Code);
                        var salaryTemplate = new List<SalaryTemplateListLookUpModel>();
                        foreach (var item in query)
                        {
                            salaryTemplate.Add(new SalaryTemplateListLookUpModel()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                StructureName = item.StructureName,
                            });
                        }

                        return new PagedResult<List<SalaryTemplateListLookUpModel>>
                        {
                            Results = salaryTemplate
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x =>
                            x.Code.Contains(searchTerm)
                            || x.StructureName.Contains(searchTerm));

                    }

                    query = query.OrderBy(x => x.Code);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<SalaryTemplateListLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new SalaryTemplateListLookUpModel()
                        {
                            Id = item.Id,
                            Code = item.Code,
                            StructureName = item.StructureName,
                        });
                    }

                    return new PagedResult<List<SalaryTemplateListLookUpModel>>
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
