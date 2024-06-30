using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.ProductGroups.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace Focus.Business.ProductGroups.Commands
{
    public class AddUpdateProductGroupCommand : IRequest<Guid>
    {
        public ProductGroupModel ProductGroup { get; set; }

        public class Handler : IRequestHandler<AddUpdateProductGroupCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface Context for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateProductGroupCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }
            public async Task<Guid> Handle(AddUpdateProductGroupCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    //Update
                    if (request.ProductGroup.Id != Guid.Empty)
                    {
                        var colors = await _context.ProductGroups.FindAsync(request.ProductGroup.Id);
                        if (colors == null)
                            throw new NotFoundException("Product Group", request.ProductGroup.Name);

                        foreach (var item in colors.Products)
                        {
                            item.ProductGroupId = null;
                        }


                        colors.Name = request.ProductGroup.Name;
                        colors.NameArabic = request.ProductGroup.NameArabic;
                        colors.Description = request.ProductGroup.Description;
                        colors.Status = request.ProductGroup.Status;

                        foreach (var item in request.ProductGroup.ProductList)
                        {
                            var product = await _context.Products.FindAsync(item.ProductId);
                            if (product != null)
                            {
                                product.ProductGroupId = colors.Id;
                            }
                        }

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

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Department id after successfully updating data
                        return colors.Id;
                        //Check Department exist

                    }
                    else
                    {
                        var isCodeExist = await _context.ProductGroups.AnyAsync(x => x.Code == request.ProductGroup.Code, cancellationToken: cancellationToken);

                        if (isCodeExist)
                            throw new ObjectAlreadyExistsException("Product Group " + request.ProductGroup.Code + " Already Exist");

                        var productGroup = new ProductGroup
                        {
                            Name = request.ProductGroup.Name,
                            NameArabic = request.ProductGroup.NameArabic,
                            Description = request.ProductGroup.Description,
                            Code = request.ProductGroup.Code,
                            Status = request.ProductGroup.Status
                        };
                        await _context.ProductGroups.AddAsync(productGroup, cancellationToken);


                        foreach (var item in request.ProductGroup.ProductList)
                        {
                            var product = await _context.Products.FindAsync(item.ProductId);
                            if (product!=null)
                            {
                                product.ProductGroupId = productGroup.Id;
                            }
                        }

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
                 //               BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = list,
                            IsServer=true
                        }, cancellationToken);

                        //Add Department to database
                        await _context.SaveChangesAsync(cancellationToken);
                        return productGroup.Id;
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
