using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Domain.Entities;

namespace Focus.Business.SyncRecords.Queries.GetSyncRecordsList
{
    public class GetSyncRecordListQuery : PagedRequest, IRequest<PagedResult<SyncRecordListModel>>
    {
        public string Status { get; set; }
        public bool IsPush { get; set; }
        public Guid CompanyId { get; set; }

        public class Handler : IRequestHandler<GetSyncRecordListQuery, PagedResult<SyncRecordListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context,
                ILogger<SyncRecordListModel> logger,
                IMapper mapper, IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }
            public async Task<PagedResult<SyncRecordListModel>> Handle(GetSyncRecordListQuery request, CancellationToken cancellationToken)
            {

                var syncType = " CompanyId = '" + request.CompanyId + "' and Pull = 0 and Push";
                var connection = "DefaultConnection";
                var allRecord = " CompanyId = '" + request.CompanyId + "' and Pull = 0 ";
                var recordSync = 0;
                var recordPending = 0;
                var count = 0;

                if (!request.IsPush)
                {
                    syncType = " CompanyId = '" + request.CompanyId + "' and Push = 0 and Pull";
                    connection = "ServerConnection";
                    allRecord = " CompanyId = '" + request.CompanyId + "' and Push = 0 ";
                }

                using (var conn = new SqlConnection(Configuration.GetConnectionString(connection)))
                {
                    try
                    {
                        conn.Open();
                        IEnumerable<SyncRecord> query;
                        //IEnumerable<SyncRecord> query1;

                        if (request.Status == "Sync")
                        {


                            var sqlQuery = "SELECT Count(*) FROM SyncRecords where " + syncType + " = 0" + ";" +
                                 "SELECT Count(*) FROM SyncRecords where " + syncType + " = 1" + ";" +
                                 "SELECT Count(*) FROM SyncRecords where " + syncType + " = 1" + ";" +
                                 "SELECT * FROM SyncRecords where " + syncType + " = 1 order by ChangeDate Desc, Id OFFSET ( " + ((request.PageNumber) - 1) * request.PageSize
                                 + ") ROWS FETCH NEXT (" + request.PageSize + ") ROWS ONLY" + ";";


                            using (var multi = conn.QueryMultiple(sqlQuery, null))
                            {
                                recordPending = multi.Read<int>().Single();
                                recordSync = multi.Read<int>().Single();
                                count = multi.Read<int>().Single();
                                query = multi.Read<SyncRecord>().ToList();

                            }
                        }
                        else if (request.Status == "NotSync")
                        {

                            var sqlQuery = "SELECT Count(*) FROM SyncRecords where " + syncType + " = 0" + ";" +
                                 "SELECT Count(*) FROM SyncRecords where " + syncType + " = 1" + ";" +
                                 "SELECT Count(*) FROM SyncRecords where " + syncType + " = 0" + ";" +
                                 "SELECT * FROM SyncRecords where " + syncType + " = 0 order by ChangeDate Desc, Id OFFSET ( " + ((request.PageNumber) - 1) * request.PageSize
                                 + ") ROWS FETCH NEXT (" + request.PageSize + ") ROWS ONLY" + ";";


                            using (var multi = conn.QueryMultiple(sqlQuery, null))
                            {
                                recordPending = multi.Read<int>().Single();
                                recordSync = multi.Read<int>().Single();
                                count = multi.Read<int>().Single();
                                query = multi.Read<SyncRecord>().ToList();

                            }
                        }
                        else
                        {
                            var sqlQuery = "SELECT Count(*) FROM SyncRecords where " + syncType + " = 0" +
                                  ";" +
                                  "SELECT Count(*) FROM SyncRecords where " + syncType + " = 1" + ";" +
                                  "SELECT Count(*) FROM SyncRecords where " + allRecord + ";" +
                                  "SELECT * FROM SyncRecords where " + allRecord + " order by ChangeDate Desc, Id OFFSET ( " + ((request.PageNumber) - 1) * request.PageSize
                                  + ") ROWS FETCH NEXT (" + request.PageSize + ") ROWS ONLY" + ";";


                            using (var multi = conn.QueryMultiple(sqlQuery, null))
                            {
                                recordPending = multi.Read<int>().Single();
                                recordSync = multi.Read<int>().Single();
                                count = multi.Read<int>().Single();
                                query = multi.Read<SyncRecord>().ToList();

                            }


                        }

                        var syncLookUpModel = new List<Products.Queries.GetProductList.SyncRecordLookUpModel>();

                        foreach (var sync in query)
                        {
                            syncLookUpModel.Add(new SyncRecordLookUpModel()
                            {
                                Id = sync.Id,
                                Pull = sync.Pull,
                                Push = sync.Push,
                                PushDate = sync.PushDate,
                                PullDate = sync.PullDate,
                                Action = sync.Action,
                                ChangeDate = sync.ChangeDate.ToString("dd/MM/yyyy hh:mm"),
                                ColumnName = sync.ColumnName,
                                IsGeneral = sync.IsGeneral,
                                Table = sync.Table,
                                ColumnType = sync.ColumnType,
                                Synced = recordSync,
                                Pending = recordPending
                            });
                        }

                        return new PagedResult<SyncRecordListModel>
                        {
                            Results = new SyncRecordListModel
                            {
                                SyncRecords = syncLookUpModel
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = syncLookUpModel.Count / request.PageSize
                        };
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError(exception.Message);
                        throw new ApplicationException("List Error");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

            }
        }
    }
}
