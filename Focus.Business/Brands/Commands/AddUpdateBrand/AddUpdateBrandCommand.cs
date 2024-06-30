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

namespace Focus.Business.Brands.Commands.AddUpdateBrand
{
    public class AddUpdateBrandCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateBrandCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateBrandCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateBrandCommand request, CancellationToken cancellationToken)
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
                        var brands = await _context.Brands.FindAsync(request.Id);
                        if (brands == null)
                            throw new NotFoundException("Brand Name", request.Name);

                       
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //Check Brand name is already exists
                            //var isBrandNameExist = await Context.Brands.AnyAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            //if (brands.Name != request.Name && isBrandNameExist)
                            //    throw new ObjectAlreadyExistsException("Product Brand " + request.Name + " Already Exist");
                            //if (brands.NameArabic != request.NameArabic && isBrandNameExist)
                            //    throw new ObjectAlreadyExistsException("Product Brand " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.Brands.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (brands.Name != request.Name && isBrandNameExist)
                                throw new ObjectAlreadyExistsException("Product Brand " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.Brands.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (brands.NameArabic != request.NameArabic && isBrandNameExist)
                                throw new ObjectAlreadyExistsException("Product Brand " + request.NameArabic + " Already Exist");
                        }

                        brands.Name = request.Name;
                        brands.NameArabic = request.NameArabic;
                        brands.Description = request.Description;
                        brands.isActive = request.IsActive;

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
                            IsServer=true
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        return brands.Id;
                    }
                    else
                    {
                        if (request.Name!="" && request.NameArabic!="")
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.Brands.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isBrandNameExist!=null)
                                throw new ObjectAlreadyExistsException("Product Brand " + request.Name +" "+ request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.Brands.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (isBrandNameExist != null)
                                throw new ObjectAlreadyExistsException("Product Brand " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check Brand name is already exists
                            var isBrandNameExist = await _context.Brands.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isBrandNameExist != null)
                                throw new ObjectAlreadyExistsException("Product Brand " + request.NameArabic + " Already Exist");
                        }


                        //New brand Added
                        var brand = new Brand
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            Code = request.Code,
                            isActive = request.IsActive
                        };
                        //Add Department to database
                        await _context.Brands.AddAsync(brand, cancellationToken);

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
                            //   BranchId = branch.Id,
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
                        return brand.Id;
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
