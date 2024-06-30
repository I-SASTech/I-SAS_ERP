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
using DocumentFormat.OpenXml.Presentation;
using Focus.Business.Brands.Queries.GetBrandList;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.InquiryRequestType.Queries.GetInquiryTypeList
{
    public class GetInquiryTypeListQuery : PagedRequest, IRequest<PagedResult<InquiryTypeListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public class Handler : IRequestHandler<GetInquiryTypeListQuery, PagedResult<InquiryTypeListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            public readonly IMediator _Mediator;
            private string _code;
            public Handler(IApplicationDbContext context,
                ILogger<GetInquiryTypeListQuery> logger,
                IMapper mapper,
                IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _Mediator = mediator;
            }
            public async Task<PagedResult<InquiryTypeListModel>> Handle(GetInquiryTypeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    IQueryable<InquiryType> query;
                    query = _context.InquiryTypes.AsQueryable();
                    if (query.Count() < 2)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var dataList = new List<string>()
                        {
                            "New","Upgrade","Maintenance","Renewal"
                        };
                        var isTypeExist = await _context.InquiryTypes.AnyAsync(x => x.Name == "New" && x.Name == "Upgrade" && x.Name == "Maintenance" && x.Name == "Renewal", cancellationToken: cancellationToken);
                        if (!isTypeExist)
                        {
                            foreach (var data in dataList)
                            {
                                var autoCode = await _Mediator.Send(new GetInquiryTypeCodeQuery(), cancellationToken);
                                //New brand Added
                                var inq = new InquiryType()
                                {
                                    Name = data,
                                    Label = data,
                                    Description = data,
                                    Code = autoCode,
                                    IsActive = true
                                };
                                //Add Department to database
                                await _context.InquiryTypes.AddAsync(inq, cancellationToken);

                                await _Mediator.Send(new AddUpdateSyncRecordCommand()
                                {
                                    SyncRecordModels = _context.SyncLog(),
                                    Code = _code,
                                }, cancellationToken);

                                await _context.SaveChangesAsync(cancellationToken);
                            }
                            
                            
                        }
                    }
                    if (request.isActive)
                    {

                        var typeList = await query.OrderBy(x=>x.Code).Where(x=>x.IsActive)
                            .ProjectTo<InquiryTypeLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<InquiryTypeListModel>
                        {


                            Results = new InquiryTypeListModel
                            {
                                InquiryTypeLookUp = typeList
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


                        var typeList = await query
                            .ProjectTo<InquiryTypeLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);

                        return new PagedResult<InquiryTypeListModel>
                        {


                            Results = new InquiryTypeListModel
                            {
                                InquiryTypeLookUp = typeList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = typeList.Count / request.PageSize
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
