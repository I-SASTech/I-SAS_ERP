using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;
using System.Collections.Generic;
using Focus.Business.BundleCategoriesItems.Models;

namespace Focus.Business.BundleCategoriesItems.Commands.AddUpdateBundleCategory
{
    public class AddUpdateBundleCategoryCommand : IRequest<Message>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Offer { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }
        public bool isActive { get; set; }
        public int quantityLimit { get; set; }
        public decimal StockLimit { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BranchId { get; set; }
        public List<BundlesOffersBranchesLookupModel> BranchesIdList { get; set; }

        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateBundleCategoryCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateBundleCategoryCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Message> Handle(AddUpdateBundleCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Id != Guid.Empty)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }


                        var promotion = await Context.BundleCategories.FindAsync(request.Id);
                        
                        if (promotion == null)
                            throw new NotFoundException(request.Offer, request.Id);
                        

                        promotion.Offer = request.Offer;
                        promotion.Buy = request.Buy;
                        promotion.Get = request.Get;
                        promotion.IsActive = request.isActive;
                        promotion.QuantityLimit = request.quantityLimit;
                        promotion.StockLimit = request.StockLimit;
                        promotion.FromDate = request.FromDate;
                        promotion.ToDate = request.ToDate;
                        promotion.ProductId = request.ProductId;

                        if(promotion.BundleOfferBranches.Count > 0)
                        {
                            Context.BundleOfferBranches.RemoveRange(promotion.BundleOfferBranches);
                        }

                        var bundleOfferBranches = new List<BundleOfferBranches>();

                        if (request.BranchesIdList.Count > 0)
                        {
                            foreach (var item in request.BranchesIdList)
                            {
                                bundleOfferBranches.Add(new BundleOfferBranches
                                {
                                    BranchId = item.Id,
                                    BundleCategoriesId = promotion.Id,

                                });
                            }
                        }

                        await Context.BundleOfferBranches.AddRangeAsync(bundleOfferBranches, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            BranchId = promotion.BranchId,
                            Code = _code,
                           
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        var message = new Message
                        {
                            Id = promotion.Id,
                            IsAddUpdate = "Data has been Update successfully"
                        };
                        return message;
                    }
                    else
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var promotion = new BundleCategory
                        {
                            Offer = request.Offer,
                            Buy = request.Buy,
                            Get = request.Get,
                            IsActive = request.isActive,
                            QuantityLimit = request.quantityLimit,
                            StockLimit = request.StockLimit,
                            FromDate = request.FromDate,
                            ToDate = request.ToDate,
                            ProductId = request.ProductId,
                            BranchId = request.BranchId,
                        };

                        await Context.BundleCategories.AddAsync(promotion, cancellationToken);


                        var bundleOfferBranches = new List<BundleOfferBranches>();

                        if(request.BranchesIdList.Count > 0)
                        {
                            foreach (var item in request.BranchesIdList)
                            {
                                bundleOfferBranches.Add(new BundleOfferBranches
                                {
                                    BranchId = item.Id,
                                    BundleCategoriesId = promotion.Id,

                                });
                            }
                        }

                        await Context.BundleOfferBranches.AddRangeAsync(bundleOfferBranches, cancellationToken);
                        

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            BranchId = promotion.BranchId,
                            Code = _code,
                        }, cancellationToken);


                        await Context.SaveChangesAsync(cancellationToken);

                        var message = new Message
                        {
                            Id = promotion.Id,
                            IsAddUpdate = "Data has been added successfully"
                        };
                        return message;
                    }
                }
                catch (NotFoundException notFoundException)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = notFoundException.Message,

                    };
                    Logger.LogError(notFoundException.Message);
                    return message;
                }
                catch (Exception e)
                {
                    var message = new Message
                    {
                        Id = Guid.Empty,
                        IsAddUpdate = "Something went wrong! Contact to Support"
                    };
                    Logger.LogError(e.Message);
                    return message;
                }
            }
        }
    }
}
