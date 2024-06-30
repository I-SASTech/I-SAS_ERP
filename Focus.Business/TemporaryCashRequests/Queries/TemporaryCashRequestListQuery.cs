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

namespace Focus.Business.TemporaryCashRequests.Queries
{
    public class TemporaryCashRequestListQuery : PagedRequest, IRequest<PagedResult<List<TemporaryCashRequestListLookUp>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public bool IsQuotation { get; set; }
        public bool IsService { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<TemporaryCashRequestListQuery, PagedResult<List<TemporaryCashRequestListLookUp>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<TemporaryCashRequestListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<TemporaryCashRequestListLookUp>>> Handle(TemporaryCashRequestListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var query = _context.TemporaryCashRequests
                            .AsNoTracking()
                            .Include(x => x.TemporaryCashRequestItems)
                            .Where(x=>!x.IsClose && x.ApprovalStatus == ApprovalStatus.Approved)
                            .AsQueryable();

                        var listEmployee = _context.EmployeeRegistrations.AsNoTracking().AsQueryable();
                        var listCashRequestUsers = _context.CashRequestUsers.AsNoTracking().AsQueryable();

                        var temporaryCash = new List<TemporaryCashRequestListLookUp>();
                        foreach (var item in query)
                        {
                            var userName = item.IsCashRequesterUser ?
                                listCashRequestUsers.FirstOrDefault(x => x.Id == item.UserId)?.Name :
                                listEmployee.FirstOrDefault(x => x.Id == item.UserId)?.EnglishName;
                            temporaryCash.Add(new TemporaryCashRequestListLookUp()
                            {
                                Id = item.Id,
                                Date = item.Date.ToString("d"),
                                RegistrationNo = item.RegistrationNo,
                                UserName= userName,
                                Amount = item.TemporaryCashRequestItems.Sum(x => x.Amount)
                            });
                        }
                        return new PagedResult<List<TemporaryCashRequestListLookUp>>
                        {
                            Results = temporaryCash
                        };

                    }
                    else
                    {
                        var query = _context.TemporaryCashRequests
                            .AsNoTracking()
                            .Include(x => x.TemporaryCashRequestItems)
                            .Where(x => x.ApprovalStatus == request.Status)
                            .AsQueryable();

                        var listEmployee = _context.EmployeeRegistrations.AsNoTracking().AsQueryable();
                        var listCashRequestUsers = _context.CashRequestUsers.AsNoTracking().AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                            query = query.Where(x =>
                                x.RegistrationNo.Contains(searchTerm) || x.Date.ToString("d").Contains(searchTerm));
                        }

                        query = query.OrderByDescending(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var temporaryCash = new List<TemporaryCashRequestListLookUp>();
                        foreach (var item in query)
                        {
                            var userName = item.IsCashRequesterUser?
                                listCashRequestUsers.FirstOrDefault(x => x.Id == item.UserId)?.Name :
                                listEmployee.FirstOrDefault(x => x.Id == item.UserId)?.EnglishName;
                            temporaryCash.Add(new TemporaryCashRequestListLookUp()
                            {
                                Id = item.Id,
                                Date = item.Date.ToString("d"),
                                UserName = userName,
                                RegistrationNo = item.RegistrationNo,
                                IsClose = item.IsClose,
                                Amount = item.TemporaryCashRequestItems.Sum(x=>x.Amount)
                            });
                        }

                        return new PagedResult<List<TemporaryCashRequestListLookUp>>
                        {
                            Results = temporaryCash,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = temporaryCash.Count / request.PageSize
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
