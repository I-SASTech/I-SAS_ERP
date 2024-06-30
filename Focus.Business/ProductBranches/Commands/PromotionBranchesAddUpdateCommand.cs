﻿using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.ProductBranches.Models;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ProductBranches.Commands
{
    public class PromotionBranchesAddUpdateCommand : IRequest<Message>
    {
        public ProductBranchesLookupModel Branches { get; set; }

        public class Handler : IRequestHandler<PromotionBranchesAddUpdateCommand, Message>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IUserHttpContextProvider _contextProvider;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<PromotionBranchesAddUpdateCommand> logger, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _contextProvider = contextProvider;

            }
            public async Task<Message> Handle(PromotionBranchesAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Branches.isSelectAll)
                    {

                        var products = await _context.PromotionOffers.AsNoTracking().Select(x => new
                        {
                            Id = x.Id
                        }).ToListAsync();

                        var branches = await _context.BundleOfferBranches.AsNoTracking().ToListAsync();

                        var removebranchItems = new List<BundleOfferBranches>();
                        var branchItems = new List<BundleOfferBranches>();
                        foreach (var item in products)
                        {
                            foreach (var branch in request.Branches.branchIds)
                            {
                                var isExist = branches.FirstOrDefault(x => x.PromotionOfferId == item.Id && x.BranchId == branch.Id);
                                if(isExist == null)
                                {
                                    branchItems.Add(new BundleOfferBranches
                                    {
                                        BranchId = branch.Id,
                                        PromotionOfferId = item.Id,
                                    });
                                }
                            }
                        }

                        await _context.BundleOfferBranches.AddRangeAsync(branchItems);

                        await _context.SaveChangesAsync();

                        return new Message
                        {
                            IsSuccess = true,
                            IsAddUpdate = "Data has been update Successfully",
                        };

                    }
                    else
                    {
                        var branches = await _context.BundleOfferBranches.AsNoTracking().ToListAsync();

                        var branchItems = new List<BundleOfferBranches>();
                        foreach (var item in request.Branches.productIds)
                        {
                            foreach (var branch in request.Branches.branchIds)
                            {
                                var isExist = branches.FirstOrDefault(x => x.PromotionOfferId == item.Id && x.BranchId == branch.Id);
                                if (isExist == null)
                                {
                                    branchItems.Add(new BundleOfferBranches
                                    {
                                        BranchId = branch.Id,
                                        PromotionOfferId = item.Id,
                                    });
                                }
                            }

                        }
                        await _context.BundleOfferBranches.AddRangeAsync(branchItems);
                        await _context.SaveChangesAsync();

                        return new Message
                        {
                            IsSuccess = true,
                            IsAddUpdate = "Data has been update Successfully",
                        };
                    }

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
