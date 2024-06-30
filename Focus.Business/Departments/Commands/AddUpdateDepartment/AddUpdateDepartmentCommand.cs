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

namespace Focus.Business.Departments.Commands.AddUpdateDepartment
{
    public class AddUpdateDepartmentCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public Guid? BankId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateDepartmentCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateDepartmentCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateDepartmentCommand request, CancellationToken cancellationToken)
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
                        var departments = await _context.Departments.FindAsync(request.Id);
                        if (departments == null)
                            throw new NotFoundException("Department Name", request.Name);

                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //Check Department name is already exists
                            //var isDepartmentNameExist = await Context.Departments.AnyAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            //if (departments.Name != request.Name && isDepartmentNameExist)
                            //    throw new ObjectAlreadyExistsException("Product Department " + request.Name + " Already Exist");
                            //if (departments.NameArabic != request.NameArabic && isDepartmentNameExist)
                            //    throw new ObjectAlreadyExistsException("Product Department " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check Department name is already exists
                            var isDepartmentNameExist = await _context.Departments.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (departments.Name != request.Name && isDepartmentNameExist)
                                throw new ObjectAlreadyExistsException(" Department " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check Department name is already exists
                            var isDepartmentNameExist = await _context.Departments.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (departments.NameArabic != request.NameArabic && isDepartmentNameExist)
                                throw new ObjectAlreadyExistsException(" Department " + request.NameArabic + " Already Exist");
                        }

                        departments.Name = request.Name;
                        departments.BankId = request.BankId;
                        departments.NameArabic = request.NameArabic;
                        departments.Description = request.Description;
                        departments.IsActive = request.IsActive;

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
                                //  BranchId = branch.Id,
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

                        return departments.Id;
                    }
                    else
                    {
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //Check Department name is already exists
                            var isDepartmentNameExist = await _context.Departments.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isDepartmentNameExist != null)
                                throw new ObjectAlreadyExistsException("Department " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check Department name is already exists
                            var isDepartmentNameExist = await _context.Departments.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (isDepartmentNameExist != null)
                                throw new ObjectAlreadyExistsException("Department " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check Department name is already exists
                            var isDepartmentNameExist = await _context.Departments.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isDepartmentNameExist != null)
                                throw new ObjectAlreadyExistsException("Department " + request.NameArabic + " Already Exist");
                        }


                        //New department Added
                        var department = new Department
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            BankId = request.BankId,
                            Description = request.Description,
                            Code = request.Code,
                            IsActive = request.IsActive
                        };
                        //Add Department to database
                        await _context.Departments.AddAsync(department, cancellationToken);

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
                                //     BranchId = branch.Id,
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
                        return department.Id;
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
