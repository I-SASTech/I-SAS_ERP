using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchList
{
    public class GetProductionBatchListQuery : PagedRequest, IRequest<PagedResult<List<ProductionBatchLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetProductionBatchListQuery, PagedResult<List<ProductionBatchLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(IApplicationDbContext context, ILogger<GetProductionBatchListQuery> logger, IMapper mapper, UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<PagedResult<List<ProductionBatchLookUpModel>>> Handle(GetProductionBatchListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var po = _context.ProductionBatchs
                            .AsNoTracking()
                            .Include(x => x.RecipeNo)
                            .Include(x => x.ProductionBatchItems)
                            .ThenInclude(x => x.Product)
                            .Where(x => x.ApprovalStatus == ApprovalStatus.Approved && !x.IsClose)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            po = po.Where(x => x.BranchId == request.BranchId);
                        }

                        var pb = await po.OrderBy(x => x.RegistrationNo)
                          .ProjectTo<ProductionBatchLookUpModel>(_mapper.ConfigurationProvider)
                          .ToListAsync(cancellationToken);

                        return new PagedResult<List<ProductionBatchLookUpModel>>
                        {
                            Results = pb
                        };

                    }
                    else
                    {
                        var query = _context.ProductionBatchs
                            .AsNoTracking()
                            .Include(x => x.RecipeNo)
                            .Include(x => x.ProductionBatchItems)
                            .ThenInclude(x => x.Product)
                            .AsQueryable();

                        if (request.BranchId != null)
                        {
                            query = query.Where(x => x.BranchId == request.BranchId);
                        }

                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;


                            query = query.Where(x =>
                                x.RegistrationNo.Contains(searchTerm) || x.Date.ToString("d").Contains(searchTerm));

                        }
                        var userList = _userManager.Users.ToList();
                        query = query.OrderByDescending(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);


                        var q1Query = query.Select(productionBatch => new ProductionBatchLookUpModel
                           {
                               Id = productionBatch.Id,
                               RegistrationNumber = productionBatch.RegistrationNo,
                               DamageStock = productionBatch.DamageStock,
                               RemainingStock = productionBatch.RemainingStock,
                               ReciptName = productionBatch.RecipeNo == null ? null : productionBatch.RecipeNo.RegistrationNo,
                               NetAmount = Convert.ToDecimal(productionBatch.NoOfBatches) * productionBatch.RecipeQuantity,
                               Date = productionBatch.Date.ToString("MM/dd/yyyy hh:mm tt"),
                               CompleteDate = productionBatch.CompleteDate == null ? null : productionBatch.CompleteDate.Value.ToString("MM/dd/yyyy hh:mm tt"),
                               ProcessDate = productionBatch.ProcessDate == null ? null : productionBatch.ProcessDate.Value.ToString("MM/dd/yyyy hh:mm tt"),
                               TransferDate = productionBatch.TransferDate == null ? null : productionBatch.TransferDate.Value.ToString("MM/dd/yyyy hh:mm tt"),
                               StartTime = productionBatch.StartTime == null ? null : productionBatch.StartTime.Value.ToString("MM/dd/yyyy hh:mm tt"),
                               EndTime = productionBatch.EndTime == null ? null : productionBatch.EndTime.Value.ToString("MM/dd/yyyy hh:mm tt"),
                               LateReason = productionBatch.LateReason,
                               LateReasonCompletion = productionBatch.LateReasonCompletion,
                               ProcessBy = productionBatch.ProcessBy,
                               CompleteBy = productionBatch.CompleteBy,
                               TransferBy = productionBatch.TransferBy,
                               //CreatedBy = userList.Where(x => x.Id == EF.Property<string>(productionBatch, "CreatedById").ToString()).Select(x => x.UserName).FirstOrDefault(),

                           }).ToList();


                        return new PagedResult<List<ProductionBatchLookUpModel>>
                        {
                            Results = q1Query,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = q1Query.Count / request.PageSize
                        };
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
