using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Zones.Commands.AddUpdateZone
{
    public class AddUpdateZoneCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public class Handler : IRequestHandler<AddUpdateZoneCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMediator _mediator;
            private readonly ILogger _logger;
            private string _code;
            public Handler(IApplicationDbContext context, ILogger<AddUpdateZoneCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;

            }
            public async Task<Guid> Handle(AddUpdateZoneCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.Id != Guid.Empty)
                    {
                        var zone = await _context.Zones.FindAsync(request.Id);
                        if (zone == null)
                            throw new NotFoundException(request.Name, request.Id);

                        var isZoneNameExist = await _context.Zones
                            .Where(x => x.Name == request.Name)
                            .FirstOrDefaultAsync(cancellationToken);

                        zone.Name = request.Name;
                        zone.City = request.City;

                        var log = _context.SyncLog();
                        var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var list = new List<SyncRecordModel>();
                        if (branches.Any())
                        {
                            foreach (var branch in branches)
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    BranchId = branch.Id,
                                    Code = _code
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                        }
                        else
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                //         BranchId = branch.Id,
                                Code = _code
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return zone.Id;

                    }
                    else
                    {
                        var isZoneNameExist = await _context.Zones
                            .Where(x => x.Name == request.Name)
                            .AnyAsync(cancellationToken);


                        if (!isZoneNameExist)
                        {
                            var zone = new Zone
                            {
                                Name = request.Name,
                                City = request.City

                            };
                            await _context.Zones.AddAsync(zone, cancellationToken);

                            var log = _context.SyncLog();
                            var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                            var list = new List<SyncRecordModel>();
                            if (branches.Any())
                            {
                                foreach (var branch in branches)
                                {
                                    var syncRecords = log.Select(x => new SyncRecordModel
                                    {
                                        Table = x.Table,
                                        ColumnId = x.ColumnId,
                                        CompanyId = x.CompanyId,
                                        ColumnType = x.ColumnType,
                                        Push = x.Push,
                                        Pull = x.Pull,
                                        Action = x.Action,
                                        ChangeDate = DateTime.Now,
                                        PullDate = x.PullDate,
                                        PushDate = x.PushDate,
                                        ColumnName = x.ColumnName,
                                        BranchId = branch.Id,
                                        Code = _code
                                    }).ToList();

                                    list.AddRange(syncRecords);
                                }
                            }
                            else
                            {
                                var syncRecords = log.Select(x => new SyncRecordModel
                                {
                                    Table = x.Table,
                                    ColumnId = x.ColumnId,
                                    CompanyId = x.CompanyId,
                                    ColumnType = x.ColumnType,
                                    Push = x.Push,
                                    Pull = x.Pull,
                                    Action = x.Action,
                                    ChangeDate = DateTime.Now,
                                    PullDate = x.PullDate,
                                    PushDate = x.PushDate,
                                    ColumnName = x.ColumnName,
                                    //         BranchId = branch.Id,
                                    Code = _code
                                }).ToList();

                                list.AddRange(syncRecords);
                            }
                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = list,
                                IsServer = true
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return zone.Id;

                        }

                        return Guid.Empty;



                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Zone Name Already Exist");
                }
            }
        }
    }
}
