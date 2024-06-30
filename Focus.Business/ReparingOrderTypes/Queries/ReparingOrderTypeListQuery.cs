using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.ReparingOrderTypes.Commands;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ReparingOrderTypes.Queries
{
    public class ReparingOrderTypeListQuery : PagedRequest, IRequest<PagedResult<List<ReparingOrderTypeLookUpModel>>>
    {
        public ReparingOrderTypeEnum RepairingOrderTypes { get; set; }

        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<ReparingOrderTypeListQuery, PagedResult<List<ReparingOrderTypeLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<ReparingOrderTypeListQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<PagedResult<List<ReparingOrderTypeLookUpModel>>> Handle(ReparingOrderTypeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }
                    if (request.RepairingOrderTypes == ReparingOrderTypeEnum.WarrantyCategory)
                    {
                        var noWarranty = _context.ReparingOrderTypes.AsNoTracking().FirstOrDefault(x => x.Name == "No Warranty");
                        if (noWarranty == null)
                        {
                            var repairingOrderType = new ReparingOrderType
                            {
                                Name = "No Warranty",
                                ReparingOrderTypeEnums = ReparingOrderTypeEnum.WarrantyCategory,
                                isActive = true,
                            };
                            //Add Department to database
                            await _context.ReparingOrderTypes.AddAsync(repairingOrderType, cancellationToken);
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                        }
                    }



                    var query = _context.ReparingOrderTypes.AsNoTracking().Where(x => x.ReparingOrderTypeEnums == request.RepairingOrderTypes).AsQueryable();
                    if (request.BranchId != null)
                    {
                        query = query.Where(x => x.BranchId == request.BranchId);
                    }

                    if (request.IsDropDown)
                    {
                        query = query.Where(x => x.isActive);
                        var repairingOrderType = new List<ReparingOrderTypeLookUpModel>();
                        foreach (var item in query)
                        {
                            repairingOrderType.Add(new ReparingOrderTypeLookUpModel()
                            {
                                Id = item.Id,
                                Name = item.Name,
                                NameArabic = item.NameArabic,
                                isActive = item.isActive,
                                ReparingOrderTypes = item.ReparingOrderTypeEnums,
                            });
                        }

                        return new PagedResult<List<ReparingOrderTypeLookUpModel>>
                        {
                            Results = repairingOrderType
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x =>
                             x.Name.Contains(searchTerm)
                            || x.NameArabic.Contains(searchTerm));

                    }

                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<ReparingOrderTypeLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new ReparingOrderTypeLookUpModel()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            NameArabic = item.NameArabic,
                            isActive = item.isActive,
                            ReparingOrderTypes = item.ReparingOrderTypeEnums,
                        });
                    }

                    return new PagedResult<List<ReparingOrderTypeLookUpModel>>
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
