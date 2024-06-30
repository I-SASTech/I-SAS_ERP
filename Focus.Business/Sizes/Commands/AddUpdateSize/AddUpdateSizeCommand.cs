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

namespace Focus.Business.Sizes.Commands.AddUpdateSize
{
    public class AddUpdateSizeCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateSizeCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateSizeCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateSizeCommand request, CancellationToken cancellationToken)
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
                        var sizes = await _context.Sizes.FindAsync(request.Id);
                        if (sizes == null)
                            throw new NotFoundException("Size Name", request.Name);

                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //var isSizeNameExist = await Context.Sizes.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            //if (sizes.Name != request.Name && isSizeNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Size " + request.Name +" Already Exist");
                            //if (sizes.NameArabic != request.NameArabic && isSizeNameExist != null)
                            //    throw new ObjectAlreadyExistsException("Size " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isSizeNameExist = await _context.Sizes.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (sizes.Name != request.Name && isSizeNameExist != null)
                                throw new ObjectAlreadyExistsException("Size " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isSizeNameExist = await _context.Sizes.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (sizes.NameArabic != request.NameArabic && isSizeNameExist != null)
                                throw new ObjectAlreadyExistsException("Size " + request.NameArabic + " Already Exist");
                        }

                        sizes.Name = request.Name;
                        sizes.NameArabic = request.NameArabic;
                        sizes.Description = request.Description;
                        sizes.isActive = request.IsActive;

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
                                //      BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Department id after successfully updating data
                        return sizes.Id;
                        //Check Department exist

                    }
                    else
                    {
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            var isSizeNameExist = await _context.Sizes.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isSizeNameExist != null)
                                throw new ObjectAlreadyExistsException("Size " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isSizeNameExist = await _context.Sizes.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (isSizeNameExist != null)
                                throw new ObjectAlreadyExistsException("Size " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isSizeNameExist = await _context.Sizes.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isSizeNameExist != null)
                                throw new ObjectAlreadyExistsException("Size " + request.NameArabic + " Already Exist");
                        }


                        var size = new Size
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            Code = request.Code,
                            isActive = request.IsActive
                        };

                        //Add Department to database
                        await _context.Sizes.AddAsync(size, cancellationToken);

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
                                //       BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer = true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return size.Id;
                    }
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
