using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Syncfusion.XlsIO.Implementation.PivotAnalysis;

namespace Focus.Business.PushPullQuery
{
    public class PushPullListQuery : PagedRequest, IRequest<PagedResult<List<PushPullLookUpModel>>>
    {
        public string LogType { get; set; }
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<PushPullListQuery, PagedResult<List<PushPullLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger<PushPullListQuery> _logger;

            public Handler(IApplicationDbContext context, ILogger<PushPullListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PagedResult<List<PushPullLookUpModel>>> Handle(PushPullListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.LogType == "Fresh")
                    {
                        var record = _context.SyncRecords.ToList();
                        if (request.FromDate != null)
                        {
                            record = record.Where(x => x.ChangeDate.Date >= request.FromDate).ToList();
                        }

                        if (request.ToDate != null)
                        {
                            record = record.Where(x => x.ChangeDate.Date <= request.ToDate).ToList();
                        }
                        var query = record
                            .GroupBy(x => x.Code);

                        var count =  query.Count();

                        query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

                        var purchaseOrderList =  query
                            .Select(x => new PushPullLookUpModel
                            {
                                CompanyId = x.FirstOrDefault()!.CompanyId,
                                Table = x.FirstOrDefault()!.Table,
                                ColumnId = x.FirstOrDefault()!.ColumnId,
                                ColumnType = x.FirstOrDefault()!.ColumnType,
                                Push = x.FirstOrDefault()!.Push,
                                Pull = x.FirstOrDefault()!.Pull,
                                Action = x.FirstOrDefault()!.Action,
                                ChangeDate = x.FirstOrDefault()!.ChangeDate,
                                ColumnName = x.FirstOrDefault()!.ColumnName,
                                PushDate = x.FirstOrDefault()!.PushDate,
                                PullDate = x.FirstOrDefault()!.PullDate,
                                Code = x.FirstOrDefault()!.Code,
                                DocumentNo = x.FirstOrDefault()!.DocumentNo,
                            })
                            .OrderByDescending(x=>x.ChangeDate)
                            .ToList();

                        return new PagedResult<List<PushPullLookUpModel>>
                        {
                            Results = purchaseOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = (int)Math.Ceiling(count / (double)request.PageSize)
                        };
                    }
                    else
                    {

                        var record = _context.SyncPushPullRecords.ToList();
                        if (request.FromDate != null)
                        {
                            record = record.Where(x => x.ChangeDate.Date >= request.FromDate).ToList();
                        }

                        if (request.ToDate != null)
                        {
                            record = record.Where(x => x.ChangeDate.Date <= request.ToDate).ToList();
                        }
                        var query = record
                            .GroupBy(x => x.Code);

                        var count =  query.Count();

                        query = query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

                        var purchaseOrderList =  query
                            .Select(x => new PushPullLookUpModel
                            {
                                CompanyId = x.FirstOrDefault()!.CompanyId,
                                Table = x.FirstOrDefault()!.Table,
                                ColumnId = x.FirstOrDefault()!.ColumnId,
                                ColumnType = x.FirstOrDefault()!.ColumnType,
                                Push = x.FirstOrDefault()!.Push,
                                Pull = x.FirstOrDefault()!.Pull,
                                Action = x.FirstOrDefault()!.Action,
                                ChangeDate = x.FirstOrDefault()!.ChangeDate,
                                ColumnName = x.FirstOrDefault()!.ColumnName,
                                PushDate = x.FirstOrDefault()!.PushDate,
                                PullDate = x.FirstOrDefault()!.PullDate,
                                Code = x.FirstOrDefault()!.Code,
                                DocumentNo = x.FirstOrDefault()!.DocumentNumber,
                            })
                            .OrderByDescending(x => x.ChangeDate)

                            .ToList();

                        return new PagedResult<List<PushPullLookUpModel>>
                        {
                            Results = purchaseOrderList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = (int)Math.Ceiling(count / (double)request.PageSize)
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
