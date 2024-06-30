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

namespace Focus.Business.PriceLabelings.Commands.AddUpdatePriceLabeling
{
    public class AddUpdatePriceLabelingCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string NameArabic { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdatePriceLabelingCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdatePriceLabelingCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }


            public async Task<Guid> Handle(AddUpdatePriceLabelingCommand request, CancellationToken cancellationToken)
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
                        var priceLabelings = await _context.PriceLabelings.FindAsync(request.Id);
                        if (priceLabelings == null)
                            throw new NotFoundException("PriceLabeling Name", request.Name);

                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //Check PriceLabeling name is already exists
                            //var isPriceLabelingNameExist = await Context.PriceLabelings.AnyAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            //if (priceLabelings.Name != request.Name && isPriceLabelingNameExist)
                            //    throw new ObjectAlreadyExistsException("Product PriceLabeling " + request.Name + " Already Exist");
                            //if (priceLabelings.NameArabic != request.NameArabic && isPriceLabelingNameExist)
                            //    throw new ObjectAlreadyExistsException("Product PriceLabeling " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check PriceLabeling name is already exists
                            var isPriceLabelingNameExist = await _context.PriceLabelings.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (priceLabelings.Name != request.Name && isPriceLabelingNameExist)
                                throw new ObjectAlreadyExistsException("Product PriceLabeling " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check PriceLabeling name is already exists
                            var isPriceLabelingNameExist = await _context.PriceLabelings.AnyAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (priceLabelings.NameArabic != request.NameArabic && isPriceLabelingNameExist)
                                throw new ObjectAlreadyExistsException("Product PriceLabeling " + request.NameArabic + " Already Exist");
                        }

                        priceLabelings.Name = request.Name;
                        priceLabelings.NameArabic = request.NameArabic;
                        priceLabelings.Description = request.Description;
                        priceLabelings.IsActive = request.IsActive;
                        priceLabelings.Price = request.Price;

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
                                //BranchId = branch.Id,
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

                        return priceLabelings.Id;
                    }
                    else
                    {
                        if (request.Name != "" && request.NameArabic != "")
                        {
                            //Check PriceLabeling name is already exists
                            var isPriceLabelingNameExist = await _context.PriceLabelings.FirstOrDefaultAsync(x => x.Name == request.Name || x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isPriceLabelingNameExist != null)
                                throw new ObjectAlreadyExistsException("Product PriceLabeling " + request.Name + " " + request.NameArabic + " Already Exist");
                        }
                        else if (request.Name != "")
                        {
                            //Check PriceLabeling name is already exists
                            var isPriceLabelingNameExist = await _context.PriceLabelings.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
                            if (isPriceLabelingNameExist != null)
                                throw new ObjectAlreadyExistsException("Product PriceLabeling " + request.Name + " Already Exist");
                        }
                        else
                        {
                            //Check PriceLabeling name is already exists
                            var isPriceLabelingNameExist = await _context.PriceLabelings.FirstOrDefaultAsync(x => x.NameArabic == request.NameArabic, cancellationToken: cancellationToken);
                            if (isPriceLabelingNameExist != null)
                                throw new ObjectAlreadyExistsException("Product PriceLabeling " + request.NameArabic + " Already Exist");
                        }

                        var priceLabeling = new PriceLabeling
                        {
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            Code = request.Code,
                            Price = request.Price,
                            IsActive = request.IsActive
                        };

                        await _context.PriceLabelings.AddAsync(priceLabeling, cancellationToken);

                        var productList = await _context.Products.Select(p => new PriceRecord
                            {
                                Name = "",
                                Price = 0,
                                IsActive = false,
                                ProductId = p.Id,
                                SalePrice = p.SalePrice,
                                CostPrice = p.CostPrice ?? 0,
                                PurchasePrice = p.PurchasePrice,
                                NewPrice = 0,
                                PriceLabelingId = priceLabeling.Id
                            }).ToListAsync(cancellationToken);

                        await _context.PriceRecords.AddRangeAsync(productList, cancellationToken);

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
                                //BranchId = branch.Id,
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

                        return priceLabeling.Id;
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
