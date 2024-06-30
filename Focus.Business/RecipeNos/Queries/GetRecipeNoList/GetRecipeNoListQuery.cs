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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;
using Focus.Domain.Enums;

namespace Focus.Business.RecipeNos.Queries.GetRecipeNoList
{
    public class GetRecipeNoListQuery : PagedRequest, IRequest<PagedResult<List<RecipeNoLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public ApprovalStatus Status { get; set; }
        public bool IsDropDown { get; set; }

        public class Handler : IRequestHandler<GetRecipeNoListQuery, PagedResult<List<RecipeNoLookUpModel>>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, ILogger<GetRecipeNoListQuery> logger, IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<PagedResult<List<RecipeNoLookUpModel>>> Handle(GetRecipeNoListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown == true)
                    {
                        var po = _context.RecipeNos
                            .AsNoTracking()
                            .Include(x=>x.Product)
                            .Include(x => x.RecipeItems)
                            .ThenInclude(x => x.Product)
                            .Where(x=>x.ApprovalStatus == ApprovalStatus.Approved && !x.IsClose && x.IsActive)
                            .AsQueryable();

                        var recipeNoList = new List<RecipeNoLookUpModel>();
                        foreach (var recipeNo in po)
                        {
                            var sOrder = RecipeNoLookUpModel.Create(recipeNo);
                            recipeNoList.Add(sOrder);
                        }
                        return new PagedResult<List<RecipeNoLookUpModel>>
                        {
                            Results = recipeNoList
                        };

                    }
                    else
                    {
                        var query = _context.RecipeNos
                            .AsNoTracking()
                            .Include(x=>x.RecipeItems)
                            .ThenInclude(x => x.Product)
                            .Include(x=>x.Product)
                            .AsQueryable();
                        if (Enum.IsDefined(typeof(ApprovalStatus), request.Status))
                        {
                            query = query.Where(x => x.ApprovalStatus == request.Status);
                        }
                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;
                           
                           
                                query = query.Where(x =>
                                    x.RegistrationNo.Contains(searchTerm) || x.Date.ToString().Contains(searchTerm));
                            
                        }
                        query = query.OrderByDescending(x => x.RegistrationNo);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);
                        
                        var recipeNoList = new List<RecipeNoLookUpModel>();
                        foreach (var recipeNo in query)
                        {
                            var sOrder = RecipeNoLookUpModel.Create(recipeNo);
                            recipeNoList.Add(sOrder);
                        }

                        return new PagedResult<List<RecipeNoLookUpModel>>
                        {
                            Results = recipeNoList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = recipeNoList.Count / request.PageSize
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
