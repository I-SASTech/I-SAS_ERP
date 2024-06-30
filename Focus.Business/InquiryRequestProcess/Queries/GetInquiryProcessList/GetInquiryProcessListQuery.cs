using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Brands.Queries.GetBrandList;

namespace Focus.Business.InquiryRequestProcess.Queries.GetInquiryProcessList
{
    public class GetInquiryProcessListQuery : PagedRequest, IRequest<PagedResult<InquiryProcessListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetInquiryProcessListQuery, PagedResult<InquiryProcessListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryProcessListQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PagedResult<InquiryProcessListModel>> Handle(GetInquiryProcessListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<InquiryProcess> query;
                    query = _context.InquiryProcesses.AsQueryable();

                    if (request.isActive)
                    {

                        var processList = await query.OrderBy(x=>x.Code).Where(x=>x.IsActive)
                            .ProjectTo<InquiryProcessLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<InquiryProcessListModel>
                        {


                            Results = new InquiryProcessListModel
                            {
                                InquiryProcessLookUp = processList
                            }
                        };

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;



                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.Name.ToLower().Contains(searchTerm.ToLower()));

                        }
                        query = query.OrderBy(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);


                        var processList = await query
                            .ProjectTo<InquiryProcessLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<InquiryProcessListModel>
                        {


                            Results = new InquiryProcessListModel
                            {
                                InquiryProcessLookUp = processList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = processList.Count / request.PageSize
                        };

                    }

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
