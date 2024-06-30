using Focus.Business.Interface;
using Focus.Business.TransactionApplicationLogs.Model;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PushPullQuery
{
    public class PushPullQuery : IRequest<Guid>
    {
        public TransactionApplicationLogsLookupModel Pre { get; set; }

        public class Handler : IRequestHandler<PushPullQuery, Guid>
        {
            private readonly IApplicationDbContext _context;

            private readonly ILogger _logger;
            public Handler(IApplicationDbContext context, ILogger<PushPullQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<Guid> Handle(PushPullQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var prefix = _context.TransactionApplicationLogs
                        .FirstOrDefault();



                  


                    if (prefix != null)
                    {
                        // For Fresh Record( Move Record SyncRecord to  SyncPushPullRecord and DELETE SyncRecord Record )
                        if (prefix.FreshLogMovingDays > 0)
                        {
                            DateTime dateTime = DateTime.Now.AddDays(-prefix.FreshLogMovingDays);
                            var syncList = _context.SyncRecords.Where(x => x.ChangeDate.Date < dateTime.Date && (!x.Push || !x.Pull)).ToList();

                            foreach (var x in syncList)
                            {
                                var transactionApplicationLogs = new SyncPushPullRecord()
                                {
                                    CompanyId = x.CompanyId,
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    ChangeDate = x.ChangeDate,
                                    PushDate = x.PushDate,
                                    PullDate = x.PullDate,
                                    ColumnName = x.ColumnName,
                                    IsGeneral = x.IsGeneral,
                                    BranchId = x.BranchId,
                                    Code = x.Code,
                                    DocumentNumber = x.DocumentNo,

                                };
                                await _context.SyncPushPullRecords.AddAsync(transactionApplicationLogs, cancellationToken);
                            }
                            _context.SyncRecords.RemoveRange(syncList);
                        }
                        


                        // For Remove History Record SyncPushPullRecord Table

                        if (prefix.DeleteFromHistory > 0)
                        {
                            DateTime historyDateTime = DateTime.Now.AddDays(-prefix.DeleteFromHistory);


                            var synRecord = _context.SyncPushPullRecords.Where(x => x.ChangeDate.Date < historyDateTime.Date).ToList();

                            _context.SyncPushPullRecords.RemoveRange(synRecord);


                        }
                        await _context.SaveChangesAsync(cancellationToken);


                    }



                    return Guid.Empty;

                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
