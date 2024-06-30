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

namespace Focus.Business.Categories.Command.AddUpdateCategory
{
    public class AddUpdateCategoryCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public Guid? PurchaseAccountId { get; set; }
        public Guid? COGSAccountId { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public Guid? IncomeAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
        public string NameArabic { get; set; }
        public bool IsReturn { get; set; }
        public int ReturnDays { get; set; }
        public bool IsService { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateCategoryCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateCategoryCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdateCategoryCommand request, CancellationToken cancellationToken)
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
                        var categories = await Context.Categories.FindAsync(request.Id);
                        if (categories == null)
                            throw new NotFoundException("Category Name ", request.Name);

                        
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //var isCategoryNameExist = await Context.Categories
                            //    .AnyAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            //if (categories.Name != request.Name && isCategoryNameExist)
                            //    throw new ObjectAlreadyExistsException("Category Name " + request.Name + " Already Exist");
                            //if ( categories.NameArabic != request.NameArabic && isCategoryNameExist)
                            //    throw new ObjectAlreadyExistsException("Category Name " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isCategoryNameExist = await Context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken);
                            if (categories.Name != request.Name && isCategoryNameExist)
                                throw new ObjectAlreadyExistsException("Category Name " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isCategoryNameExist = await Context.Categories.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (categories.Name != request.Name && isCategoryNameExist)
                                throw new ObjectAlreadyExistsException("Category Name " + request.NameArabic + " Already Exist");
                        }

                        
                        categories.Name = request.Name;
                        categories.IsReturn = request.IsReturn;
                        categories.ReturnDays = request.ReturnDays;
                        categories.NameArabic = request.NameArabic;
                        categories.Description = request.Description;
                        categories.isActive = request.IsActive;
                        categories.IsService = request.IsService;

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

                        await Context.SaveChangesAsync(cancellationToken);
                        return categories.Id;

                    }
                    else
                    {

                        //Check Category name is already exists
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            var isCategoryNameExist = await Context.Categories.AnyAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken);
                            if (isCategoryNameExist)
                                throw new ObjectAlreadyExistsException("Category Name " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            var isCategoryNameExist = await Context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken);
                            if (isCategoryNameExist)
                                throw new ObjectAlreadyExistsException("Category Name " + request.Name + " Already Exist");
                        }
                        else
                        {
                            var isCategoryNameExist = await Context.Categories.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken);
                            if (isCategoryNameExist)
                                throw new ObjectAlreadyExistsException("Category Name " + request.NameArabic + " Already Exist");
                        }
                        

                        var category = new Category
                        {
                            Name = request.Name,
                            ReturnDays = request.ReturnDays,
                            IsReturn = request.IsReturn,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            Code = request.Code,
                            //SaleAccountId = accounts[2].Id,
                            //COGSAccountId = accounts[1].Id,
                            //InventoryAccountId = accounts[0].Id,
                            isActive = request.IsActive,
                            IsService = request.IsService
                        };

                        //Add Categories to database
                        await Context.Categories.AddAsync(category, cancellationToken);

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

                        await Context.SaveChangesAsync(cancellationToken);
                        return category.Id;
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
