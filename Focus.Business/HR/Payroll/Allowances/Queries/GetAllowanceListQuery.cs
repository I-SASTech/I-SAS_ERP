using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Common;
using Focus.Business.HR.Payroll.Allowances.Commands.AddAllowance;
using Focus.Business.Interface;
using Focus.Business.Purchases.Queries.GetPurchaseOrderList;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.Allowances.Queries
{
    public class GetAllowanceListQuery : PagedRequest, IRequest<PagedResult<List<AllowanceLookUp>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }
        public bool IsActive { get; set; }

        public class Handler : IRequestHandler<GetAllowanceListQuery, PagedResult<List<AllowanceLookUp>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context,
                ILogger<GetAllowanceListQuery> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _mediator = mediator;
            }
            public async Task<PagedResult<List<AllowanceLookUp>>> Handle(GetAllowanceListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var countAllowances = _context.AllowanceTypes.Count();
                    if (countAllowances ==0)
                    {

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var allowanceTypes = new List<AllowanceType>()
                        {
                            new AllowanceType
                            {
                                Name="Medical Allowance",
                                NameArabic= "بدل الطبي",
                                IsActive=true


                            },
                            new AllowanceType
                            {
                                Name="Mobile Allowance",
                                NameArabic= "بدل المحمول",
                                IsActive=true


                            },
                            new AllowanceType
                            {
                                Name="Transport Allowance",
                                NameArabic= "بدل الناقل",
                                IsActive=true


                            },
                            new AllowanceType
                            {
                                Name="Overtime Allowance",
                                NameArabic= "بدل العمل الإضافي",
                                IsActive=true


                            },
                           
                        };

                        await _context.AllowanceTypes.AddRangeAsync(allowanceTypes, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                    }


                    var query = _context.Allowances.AsNoTracking().Include(x=>x.AllowanceType).AsQueryable();
                    if (request.IsDropDown)
                    {
                        query = query.Where(x => x.IsActive);
                        query = query.OrderBy(x => x.Code);
                        var allowanceList = new List<AllowanceLookUp>();
                        foreach (var item in query)
                        {
                            allowanceList.Add(new AllowanceLookUp()
                            {
                                Id = item.Id,
                                Code = item.Code,
                                AllowanceNameEn = item.AllowanceType.Name,
                                AllowanceNameAr = item.AllowanceType.NameArabic,
                                IsActive = item.IsActive,
                                TaxPlan = item.TaxPlan,
                                AmountType = item.AmountType,
                                Description = item.Description,
                                Amount = item.Amount,
                            });
                        }

                        return new PagedResult<List<AllowanceLookUp>>
                        {
                            Results = allowanceList
                        };
                    }



                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x =>
                            x.AllowanceType.Name.Contains(searchTerm) ||
                            x.Code.Contains(searchTerm) ||
                            x.AllowanceType.NameArabic.Contains(searchTerm));

                    }
                    query = query.OrderBy(x => x.Code);
                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<AllowanceLookUp>();
                    foreach (var item in query)
                    {
                        list.Add(new AllowanceLookUp()
                        {
                            Id = item.Id,
                            Code = item.Code,
                            AllowanceNameEn = item.AllowanceType.Name,
                            AllowanceNameAr = item.AllowanceType.NameArabic,
                            IsActive = item.IsActive,
                            TaxPlan = item.TaxPlan,
                            AmountType = item.AmountType,
                            Description = item.Description,
                            Amount = item.Amount,
                        });
                    }

                    return new PagedResult<List<AllowanceLookUp>>
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
