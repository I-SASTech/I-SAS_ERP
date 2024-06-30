using DocumentFormat.OpenXml.InkML;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.NobleUserRole.Commands.AddUpdateNobleUserRole
{
    public class AddUpdateNobleUserRoleCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateNobleUserRoleCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateNobleUserRoleCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateNobleUserRoleCommand request, CancellationToken cancellationToken)
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
                        var nobleRoles = await _context.NobleUserRoles.FindAsync(request.Id);

                        var isNobleRolesExist = await _context.NobleUserRoles
                            .Where(x => x.UserId == request.UserId && x.RoleId == request.RoleId)
                            .AnyAsync(cancellationToken);

                        if (nobleRoles == null)
                            throw new NotFoundException(request.UserId, request.Id);

                        if (nobleRoles.UserId != request.UserId && isNobleRolesExist)
                            return Guid.Empty;

                        nobleRoles.UserId = request.UserId;
                        nobleRoles.RoleId = request.RoleId;
                        nobleRoles.isActive = request.IsActive;

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
                                    Code = _code,
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
                     //           BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                           

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                        return nobleRoles.RoleId;
                    }
                    else
                    {
                        var isNobleRolesExist = await _context.NobleUserRoles
                            .Where(x => x.UserId == request.UserId && x.RoleId == request.RoleId)
                            .AnyAsync(cancellationToken);

                        if (!isNobleRolesExist)
                        {
                            var nobleRoles = new NobleUserRoles
                            {
                                UserId = request.UserId,
                                RoleId = request.RoleId,
                                isActive = request.IsActive
                            };

                            await _context.NobleUserRoles.AddAsync(nobleRoles, cancellationToken);

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
                                        Code = _code,
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
                            //        BranchId = branch.Id,
                                    Code = _code,
                                }).ToList();

                                list.AddRange(syncRecords);
                            } 

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = list,
                                IsServer=true
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                            return nobleRoles.RoleId;
                        }

                        return Guid.Empty;
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Role With User Already Exist");
                }
            }
        }
    }
}
