using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.QuotationTemplates.Queries
{
    public class GetQuotationTemplateListQuery : PagedRequest, IRequest<PagedResult<List<QuotationTemplateListLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsService { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<GetQuotationTemplateListQuery, PagedResult<List<QuotationTemplateListLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetQuotationTemplateListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<QuotationTemplateListLookUpModel>>> Handle(GetQuotationTemplateListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var po = _context.QuotationTemplates
                            .AsNoTracking()
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && x.IsService==request.IsService)
                            .AsQueryable();
                        var purchaseOrderList = new List<QuotationTemplateListLookUpModel>();
                        foreach (var purchaseOrder in po)
                        {
                            var sOrder = QuotationTemplateListLookUpModel.Create(purchaseOrder);
                            purchaseOrderList.Add(sOrder);
                        }
                        return new PagedResult<List<QuotationTemplateListLookUpModel>>
                        {
                            Results = purchaseOrderList
                        };

                    }
                    else
                    {
                        var query = _context.QuotationTemplates
                            .Where(x=>x.IsService==request.IsService)
                            .AsQueryable();

                        if (request.FromDate != null)
                        {
                            query = query.Where(x => x.Date.Date >= request.FromDate);
                        }
                        if (request.ToDate != null)
                        {
                            query = query.Where(x => x.Date.Date <= request.ToDate);
                        }
                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                           x.RegistrationNo.Contains(searchTerm) ||  x.Date.ToString().Contains(searchTerm));

                        }
                        query = query.OrderBy(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var purchaseOrderList = new List<QuotationTemplateListLookUpModel>();
                        foreach (var purchaseOrder in query)
                        {
                            var sOrder = QuotationTemplateListLookUpModel.Create(purchaseOrder);
                            purchaseOrderList.Add(sOrder);
                        }

                        return new PagedResult<List<QuotationTemplateListLookUpModel>>
                        {
                            Results = purchaseOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = purchaseOrderList.Count / request.PageSize
                        };
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
