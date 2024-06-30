using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
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
using Focus.Domain.Enums;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionModule.SampleRequests.Queries.GetSampleRequestList
{
    public class GetSampleRequestList : PagedRequest, IRequest<PagedResult<List<SampleRequestLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }

        public class Handler : IRequestHandler<GetSampleRequestList, PagedResult<List<SampleRequestLookupModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetSampleRequestList> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<PagedResult<List<SampleRequestLookupModel>>> Handle(GetSampleRequestList request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown == true)
                    {
                        var po = _context.SampleRequests
                            .AsNoTracking()
                            .Include(x=>x.Customer)
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved)
                            .AsQueryable();
                        var list = new List<SampleRequestLookupModel>();
                        foreach (var item in po)
                        {
                            list.Add(new SampleRequestLookupModel()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                EnglishName = item.EnglishName,
                                CustomerNameEn = item.Customer.EnglishName,
                                CustomerNameAr = item.Customer.ArabicName,
                                NoOfSampleRequests = item.NoOfSampleRequests,
                                SampleDate = item.Date.ToString("MM/dd/yyyy hh:mm tt")
                            });
                        }

                        return new PagedResult<List<SampleRequestLookupModel>>
                        {
                            Results = list
                        };

                    }
                    else
                    {
                        var query = _context.SampleRequests
                            .AsNoTracking()   
                            .Include(x=>x.Customer)
                            .AsQueryable();
                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                            x.Code.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));

                        }
                        query = query.OrderByDescending(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        var list = new List<SampleRequestLookupModel>();
                        foreach (var item in query)
                        {
                            list.Add(new SampleRequestLookupModel()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                CustomerNameEn = item.Customer.EnglishName,
                                CustomerNameAr = item.Customer.ArabicName,
                                NoOfSampleRequests = item.NoOfSampleRequests,
                                SampleDate = item.Date.ToString("MM/dd/yyyy hh:mm tt")
                            });
                        }



                        return new PagedResult<List<SampleRequestLookupModel>>
                        {
                            Results = list,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = list.Count / request.PageSize
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
