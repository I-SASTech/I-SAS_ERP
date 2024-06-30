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

namespace Focus.Business.Colors.Commands.AddUpdateColor
{
    public class AddUpdateColorCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateColorCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateColorCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateColorCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Update
                    if (request.Id != Guid.Empty)
                    {
                        var colors = await Context.Colors.FindAsync(request.Id);
                        if (colors == null)
                            throw new NotFoundException("Color Name", request.Name);
                        
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);

                            //if (colors.Name != request.Name && isColorNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Color " + request.Name + " Already Exist");
                            //if (colors.NameArabic != request.NameArabic && isColorNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Color " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

                            if (colors.Name != request.Name && isColorNameExist != null)
                                throw new ObjectAlreadyExistsException("Color " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);

                            if (colors.NameArabic != request.NameArabic && isColorNameExist != null)
                                throw new ObjectAlreadyExistsException("Color " + request.NameArabic + " Already Exist");
                        }

                        colors.Name = request.Name;
                        colors.NameArabic = request.NameArabic;
                        colors.Description = request.Description;
                        colors.isActive = request.IsActive;

                        var log = Context.SyncLog();
                        var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
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
                               // BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        // Return Department id after successfully updating data
                        return colors.Id;
                        //Check Department exist

                    }
                    else
                    {
                        //Add New
                        if (request.Name!="" && request.NameArabic!="")
                        {
                            var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);

                            if (isColorNameExist != null)
                                throw new ObjectAlreadyExistsException("Color " + request.Name + " " +request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);

                            if (isColorNameExist != null)
                                throw new ObjectAlreadyExistsException("Color " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isColorNameExist = await Context.Colors.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);

                            if (isColorNameExist != null)
                                throw new ObjectAlreadyExistsException("Color " + request.NameArabic + " Already Exist");
                        }


                        var color = new Color
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            Code = request.Code,
                            isActive = request.IsActive
                        };

                        //Add Department to database
                        await Context.Colors.AddAsync(color, cancellationToken);

                        var log = Context.SyncLog();
                        var branches = await Context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
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
                           //     BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return color.Id;
                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
