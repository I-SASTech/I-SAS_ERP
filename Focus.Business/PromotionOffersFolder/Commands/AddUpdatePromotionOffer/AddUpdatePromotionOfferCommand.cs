using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Entities;
using Focus.Business.Common;
using Microsoft.EntityFrameworkCore;
using Focus.Business.BundleCategoriesItems.Models;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.PromotionOffersFolder.Commands.AddUpdatePromotionOffer
{
    public class AddUpdatePromotionOfferCommand : IRequest<Message>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public string Offer { get; set; }
        public decimal Discount { get; set; }
        public string DiscountType { get; set; }
        public decimal BaseQuantity { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
        public bool IsActive { get; set; }
        public decimal StockLimit { get; set; }
        public decimal UpToQuantity { get; set; }
        public bool IncludingBaseQuantity { get; set; }


        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public decimal Buy { get; set; }
        public decimal Get { get; set; }

        public string PromotionType { get; set; }
        public Guid? ProductGroupId { get; set; }
        public Guid? GetProductId { get; set; }
        public List<PromotionOfferItemModel> PromotionOfferItem { get; set; }
        public Guid? BranchId { get; set; }
        public List<BundlesOffersBranchesLookupModel> BranchesIdList { get; set; }

        public class Handler : IRequestHandler<AddUpdatePromotionOfferCommand, Message>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdatePromotionOfferCommand> logger, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
            }


            public async Task<Message> Handle(AddUpdatePromotionOfferCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Id != Guid.Empty)
                    {
                        var promotion = await Context.PromotionOffers.FindAsync(request.Id);

                        if (promotion == null)
                            throw new NotFoundException(request.Offer, request.Id);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        promotion.Offer = request.Offer;
                        promotion.Discount = request.Discount;
                        promotion.DiscountType = request.DiscountType;
                        promotion.BaseQuantity = request.BaseQuantity;
                        promotion.IncludingBaseQuantity = request.IncludingBaseQuantity;
                        promotion.UpToQuantity = request.UpToQuantity;
                        promotion.StockLimit = request.StockLimit;
                        promotion.ToDate = request.ToDate;
                        promotion.FromDate = request.FromDate;
                        promotion.IsActive = request.IsActive;
                        promotion.ProductId = request.ProductId;

                        promotion.Sunday = request.Sunday;
                        promotion.Monday = request.Monday;
                        promotion.Tuesday = request.Tuesday;
                        promotion.Wednesday = request.Wednesday;
                        promotion.Thursday = request.Thursday;
                        promotion.Friday = request.Friday;
                        promotion.Saturday = request.Saturday;
                        promotion.Buy = request.Buy;
                        promotion.Get = request.Get;
                        promotion.PromotionType = request.PromotionType;
                        promotion.ProductGroupId = request.ProductGroupId;
                        promotion.GetProductId = request.GetProductId;
                        foreach (var item in promotion.PromotionOfferItems)
                        {
                            item.Discount = request.Discount;
                            item.DiscountType = request.DiscountType;
                            item.BaseQuantity = request.BaseQuantity;
                            item.IncludingBaseQuantity = request.IncludingBaseQuantity;
                            item.UpToQuantity = request.UpToQuantity;
                            item.StockLimit = request.StockLimit;
                            item.IsActive = request.IsActive;
                            item.Buy = request.Buy;
                            item.Get = request.Get;
                            item.PromotionType = request.PromotionType;
                        }

                        if (promotion.BundleOfferBranches.Count > 0)
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
                                    PromotionOfferId = promotion.Id,

                                });
                            }

                            await Context.BundleOfferBranches.AddRangeAsync(bundleOfferBranches);

                        }


                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
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

                        var promotion = new PromotionOffer
                        {
                            Offer = request.Offer,
                            Discount = request.Discount,
                            DiscountType = request.DiscountType,
                            BaseQuantity = request.BaseQuantity,
                            IncludingBaseQuantity = request.IncludingBaseQuantity,
                            UpToQuantity = request.UpToQuantity,
                            StockLimit = request.StockLimit,
                            ToDate = request.ToDate,
                            FromDate = request.FromDate,
                            IsActive = request.IsActive,
                            ProductId = request.ProductId,

                            Sunday = request.Sunday,
                            Monday = request.Monday,
                            Tuesday = request.Tuesday,
                            Wednesday = request.Wednesday,
                            Thursday = request.Thursday,
                            Friday = request.Friday,
                            Saturday = request.Saturday,
                            Buy = request.Buy,
                            Get = request.Get,
                            PromotionType = request.PromotionType,
                            ProductGroupId = request.ProductGroupId,
                            GetProductId = request.GetProductId,
                            BranchId = request.BranchId,
                        };

                        if (request.PromotionType == "BuyNGetNSameGroup" || request.PromotionType == "GroupNFixOrPercentageDiscount")
                        {
                            var products = await Context.Products.Where(x => x.ProductGroupId == request.ProductGroupId).Select(x => new
                            {
                                x.Id
                            }).ToListAsync(cancellationToken: cancellationToken);

                            var list = new List<PromotionOfferItem>();
                            foreach (var item in products)
                            {
                                list.Add(new PromotionOfferItem()
                                {
                                    Discount = request.Discount,
                                    DiscountType = request.DiscountType,
                                    BaseQuantity = request.BaseQuantity,
                                    IncludingBaseQuantity = request.IncludingBaseQuantity,
                                    UpToQuantity = request.UpToQuantity,
                                    StockLimit = request.StockLimit,
                                    IsActive = request.IsActive,
                                    Buy = request.Buy,
                                    Get = request.Get,
                                    PromotionType = request.PromotionType,
                                    ProductId = item.Id,
                                });
                            }

                            promotion.PromotionOfferItems = list;
                        }
                        else
                        {
                            var list = new List<PromotionOfferItem>
                            {
                                new()
                                {
                                    Discount = request.Discount,
                                    DiscountType = request.DiscountType,
                                    BaseQuantity = request.BaseQuantity,
                                    IncludingBaseQuantity = request.IncludingBaseQuantity,
                                    UpToQuantity = request.UpToQuantity,
                                    StockLimit = request.StockLimit,
                                    IsActive = request.IsActive,
                                    Buy = request.Buy,
                                    Get = request.Get,
                                    PromotionType = request.PromotionType,
                                    ProductId = request.ProductId,
                                    GetProductId = request.GetProductId,
                                }
                            };

                            promotion.PromotionOfferItems = list;
                        }

                        await Context.PromotionOffers.AddAsync(promotion, cancellationToken);

                        var bundleOfferBranches = new List<BundleOfferBranches>();

                        if (request.BranchesIdList.Count > 0)
                        {
                            foreach (var item in request.BranchesIdList)
                            {
                                bundleOfferBranches.Add(new BundleOfferBranches
                                {
                                    BranchId = item.Id,
                                    PromotionOfferId = promotion.Id,

                                });
                            }
                        }

                        await Context.BundleOfferBranches.AddRangeAsync(bundleOfferBranches);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
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
                        IsAddUpdate = notFoundException.Message
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
