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

namespace Focus.Business.Designations.Commands.AddUpdateDesignation
{
    public class AddUpdateDesignationCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateDesignationCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateDesignationCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateDesignationCommand request, CancellationToken cancellationToken)
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
                        var designations = await _context.Designations.FindAsync(request.Id);
                        if (designations == null)
                            throw new NotFoundException("Designation Name", request.Name);
                        
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //Check Designation name is already exists
                            //var isDesignationNameExist = await Context.Designations.AnyAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            //if (designations.Name != request.Name && isDesignationNameExist)
                            //    throw new ObjectAlreadyExistsException("Product Designation " + request.Name + " Already Exist");
                            //if (designations.NameArabic != request.NameArabic && isDesignationNameExist)
                            //    throw new ObjectAlreadyExistsException("Product Designation " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check Designation name is already exists
                            var isDesignationNameExist = await _context.Designations.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (designations.Name != request.Name && isDesignationNameExist)
                                throw new ObjectAlreadyExistsException(" Designation " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check Designation name is already exists
                            var isDesignationNameExist = await _context.Designations.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (designations.NameArabic != request.NameArabic && isDesignationNameExist)
                                throw new ObjectAlreadyExistsException(" Designation " + request.NameArabic + " Already Exist");
                        }

                        designations.Name = request.Name;
                        designations.NameArabic = request.NameArabic;
                        designations.Description = request.Description;
                        designations.IsActive = request.IsActive;

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
                            //    BranchId = branch.Id,
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
                        await _context.SaveChangesAsync(cancellationToken);

                        return designations.Id;
                    }
                    else
                    {
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //Check Designation name is already exists
                            var isDesignationNameExist = await _context.Designations.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isDesignationNameExist != null)
                                throw new ObjectAlreadyExistsException(" Designation " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check Designation name is already exists
                            var isDesignationNameExist = await _context.Designations.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (isDesignationNameExist != null)
                                throw new ObjectAlreadyExistsException(" Designation " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check Designation name is already exists
                            var isDesignationNameExist = await _context.Designations.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isDesignationNameExist != null)
                                throw new ObjectAlreadyExistsException(" Designation " + request.NameArabic + " Already Exist");
                        }


                        //New designation Added
                        var designation = new Designation
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            Code = request.Code,
                            IsActive = request.IsActive
                        };



                        //Add Designation to database
                        await _context.Designations.AddAsync(designation, cancellationToken);

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
                            IsServer=true
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return designation.Id;

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
