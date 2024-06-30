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

namespace Focus.Business.ProductMasters.Commands.AddUpdateProductMaster
{
    public class AddUpdateProductMasterCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateProductMasterCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateProductMasterCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateProductMasterCommand request, CancellationToken cancellationToken)
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
                        var productMasters = await _context.ProductMasters.FindAsync(request.Id);
                        if (productMasters == null)
                        {
                            throw new NotFoundException("Product not found", "");
                        }

                        if (request.Name != "")
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.ProductMasters.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (productMasters.Name != request.Name && isBrandNameExist)
                                throw new ObjectAlreadyExistsException("Product Master " + request.Name + " Already Exist");
                        }
                        if (request.NameArabic != "")
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.ProductMasters.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (productMasters.NameArabic != request.NameArabic && isBrandNameExist)
                                throw new ObjectAlreadyExistsException("Product Master " + request.NameArabic + " Already Exist");
                        }

                        productMasters.Name = request.Name;
                        productMasters.NameArabic = request.NameArabic;
                        productMasters.Description = request.Description;
                        productMasters.isActive = request.IsActive;

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
                                Code = _code
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

                        return productMasters.Id;
                    }
                    else
                    {
                        //Check ProductMaster name is already exists
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.ProductMasters.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isBrandNameExist != null)
                                throw new ObjectAlreadyExistsException("Product Master " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.ProductMasters.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (isBrandNameExist != null)
                                throw new ObjectAlreadyExistsException("Product Master " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.ProductMasters.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isBrandNameExist != null)
                                throw new ObjectAlreadyExistsException("Product Master " + request.NameArabic + " Already Exist");
                        }

                        //New productMaster Added
                        var productMaster = new ProductMaster
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            Code = request.Code,
                            isActive = request.IsActive
                        };
                        //Add Department to database
                        await _context.ProductMasters.AddAsync(productMaster, cancellationToken);

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
                        return productMaster.Id;
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
