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

namespace Focus.Business.SubCategories.Commands.AddUpdateSubCategory
{
    public class AddUpdateSubCategoryCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateSubCategoryCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateSubCategoryCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateSubCategoryCommand request, CancellationToken cancellationToken)
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
                        var subCategories = await _context.SubCategories.FindAsync(request.Id);
                        if (subCategories == null)
                            throw new NotFoundException(request.Name, request.Id);

                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //var isNameExist = await Context.SubCategories
                            //    .AnyAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            //if (subCategories.Name != request.Name && isNameExist)
                            //    throw new ObjectAlreadyExistsException(" Sub Category " + request.Name+ " Already Exist");
                            //if (subCategories.NameArabic != request.NameArabic && isNameExist)
                            //    throw new ObjectAlreadyExistsException(" Sub Category " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isNameExist = await _context.SubCategories
                                .AnyAsync(x => x.Name == request.Name, cancellationToken);
                            if (subCategories.Name != request.Name && isNameExist)
                                throw new ObjectAlreadyExistsException(" Sub Category " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isNameExist = await _context.SubCategories
                                .AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (subCategories.NameArabic != request.NameArabic && isNameExist)
                                throw new ObjectAlreadyExistsException(" Sub Category " + request.NameArabic + " Already Exist");
                        }

                        subCategories.Name = request.Name;
                        subCategories.NameArabic = request.NameArabic;
                        subCategories.Code = request.Code;
                        subCategories.Description = request.Description;
                        subCategories.CategoryId = request.CategoryId;
                        subCategories.isActive = request.IsActive;

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

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Department id after successfully updating data
                        return subCategories.Id;
                        //Check Department exist

                    }
                    else
                    {
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            var isNameExist = await _context.SubCategories
                                .FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            if (isNameExist != null)
                                throw new ObjectAlreadyExistsException(" Sub Category " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isNameExist = await _context.SubCategories
                                .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
                            if (isNameExist != null)
                                throw new ObjectAlreadyExistsException(" Sub Category " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isNameExist = await _context.SubCategories
                                .FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (isNameExist != null)
                                throw new ObjectAlreadyExistsException(" Sub Category " + request.NameArabic + " Already Exist");
                        }

                        var subCategory = new SubCategory
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            Code = request.Code,
                            CategoryId = request.CategoryId,
                            isActive = request.IsActive
                        };

                        //Add Department to database
                        await _context.SubCategories.AddAsync(subCategory, cancellationToken);

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
                                //          BranchId = branch.Id,
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
                        return subCategory.Id;
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
                    throw new ApplicationException("Sub Category Name Already Exist");
                }
            }
        }
    }
}
