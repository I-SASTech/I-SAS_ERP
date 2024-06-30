using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PushPullQuery
{
    public class PushPullDetailQuery : IRequest<List<PushPullLookUpModel>>
    {
        public string LogType { get; set; }
        public String DocumentCode { get; set; }


        public class Handler : IRequestHandler<PushPullDetailQuery, List<PushPullLookUpModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<PushPullDetailQuery> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<List<PushPullLookUpModel>> Handle(PushPullDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.LogType == "Fresh")
                    {
                        var prefix = _context.SyncRecords.Where(x=>x.Code==request.DocumentCode)
                            .Select(x => new PushPullLookUpModel
                            {
                                CompanyId = x.CompanyId,
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = x.ChangeDate,
                                ColumnName = x.ColumnName,
                                PushDate = x.PushDate,
                                PullDate = x.PullDate,
                                Code = x.Code,
                                DocumentNo = x.DocumentNo,
                            })
                            .ToList();

                        return prefix;


                    }
                    else
                    {
                        var prefix = _context.SyncPushPullRecords.Where(x => x.Code == request.DocumentCode)
                            .Select(x => new PushPullLookUpModel
                            {
                                CompanyId = x.CompanyId,
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = x.ChangeDate,
                                ColumnName = x.ColumnName,
                                PushDate = x.PushDate,
                                PullDate = x.PullDate,
                                Code = x.Code,
                                DocumentNo = x.DocumentNumber,
                            })
                            .ToList();

                        return prefix;


                    }


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Something went wrong " + ex.Message);
                }
            }
        }
    }
}
