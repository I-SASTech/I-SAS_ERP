using AutoMapper;
using AutoMapper.QueryableExtensions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Microsoft.Extensions.Configuration;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : PagedRequest, IRequest<PagedResult<CategoryListModel>>
    {
        public bool isActive;
        public string SearchTerm { get; set; }
        public bool IsTouch { get; set; }
        public Guid? CompanyId { get; set; }

        public class Handler : IRequestHandler<GetCategoryListQuery, PagedResult<CategoryListModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            public readonly IMediator _Mediator;
            private string _code;
            public Handler(IApplicationDbContext context,
                IConfiguration configuration,
                IUserHttpContextProvider contextProvider,
                ILogger<GetCategoryListQuery> logger,
                IMapper mapper, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                Configuration = configuration;
                _contextProvider = contextProvider;
                _Mediator = mediator;

            }
            public async Task<PagedResult<CategoryListModel>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    IQueryable<Category> query;
                    #region CategoryAccount

                    if (request.CompanyId == null)
                    {
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var isSampleCategoryExist = await _context.Categories
                                .FirstOrDefaultAsync(x => x.Name == "Temporary Category", cancellationToken);

                        if (isSampleCategoryExist == null)
                        {
                            var autoNo = await _Mediator.Send(new GetCategoryCodeQuery(), cancellationToken);
                            var accountsWithCost = _context.Accounts.Include(x => x.CostCenter).AsNoTracking()
                                .AsQueryable();
                            if (accountsWithCost == null)
                                throw new NotFoundException("Category Accounts ", "Temporary Category");

                            var accountCount = await accountsWithCost.Where(x => x.CostCenter.Code == "111000")
                                .OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
                            if (accountCount == null)
                                throw new NotFoundException("Category Account  Code 111000 ", "Temporary Category");

                            var accountCogs = await accountsWithCost.Where(x => x.CostCenter.Code == "600001")
                                .OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
                            if (accountCogs == null)
                                throw new NotFoundException("Category Account  Code 600001 ", "Temporary Category");

                            var accountSale = await accountsWithCost.Where(x => x.CostCenter.Code == "420000")
                                .OrderBy(x => x.Code).LastOrDefaultAsync(cancellationToken);
                            if (accountSale == null)
                                throw new NotFoundException("Category Account  Code 420000 ", "Temporary Category");

                            var accounts = new List<Account>()
                                {
                                    new Account
                                    {
                                        Name = "Temporary Category Inventory",
                                        Description = "Temporary Category" + "" + autoNo,
                                        Code = (int.Parse(accountCount.Code) + 1).ToString(),
                                        CostCenterId = accountCount.CostCenterId,
                                        IsActive = true
                                    },
                                    new Account
                                    {
                                        Name = "Temporary Category COGS",
                                        Description = "Temporary Category" + "" + autoNo,
                                        Code = (int.Parse(accountCogs.Code) + 1).ToString(),
                                        CostCenterId = accountCogs.CostCenterId,
                                        IsActive = true
                                    },
                                    new Account
                                    {
                                        Name = "Temporary Category Sale Account",
                                        Description = "Temporary Category" + "" + autoNo,
                                        Code = (int.Parse(accountSale.Code) + 1).ToString(),
                                        CostCenterId = accountSale.CostCenterId,
                                        IsActive = true
                                    }
                                };

                            await _context.Accounts.AddRangeAsync(accounts, cancellationToken);
                            var category = new Category
                            {
                                Name = "Temporary Category",
                                Code = autoNo,
                                SaleAccountId = accounts[2].Id,
                                COGSAccountId = accounts[1].Id,
                                InventoryAccountId = accounts[0].Id,
                                isActive = true
                            };

                            await _context.Categories.AddAsync(category, cancellationToken);

                            await _Mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = _context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            await _context.SaveChangesAsync(cancellationToken);
                        }
                    }
                    #endregion
                    if (request.isActive == false)
                    {
                        query = _context.Categories.AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm;



                            query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                     x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                      x.NameArabic.ToLower().Contains(searchTerm.ToLower()));


                        }
                        query = query.OrderBy(x => x.Code);
                        var count = await query.CountAsync(cancellationToken: cancellationToken);
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);


                        var categoriesList = await query
                            .ProjectTo<CategoryLookUpModel>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);


                        return new PagedResult<CategoryListModel>
                        {


                            Results = new CategoryListModel
                            {
                                Categories = categoriesList
                            },
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = categoriesList.Count / request.PageSize
                        };
                    }
                    else
                    {
                        if (request.IsTouch)
                        {
                            
                            using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                            var terminalCategories = conn.Query<TerminalCategory>("select * from TerminalCategories where TerminalCategories.CompanyId = '" + _contextProvider.GetCompanyId() + "' AND TerminalCategories.TerminalId = '" + _contextProvider.GetCounterId() + "'").Select(x => x.CategoryId).ToList();

                            


                                query = _context.Categories.AsNoTracking().AsQueryable();








                            if (!string.IsNullOrEmpty(request.SearchTerm))
                            {
                                var searchTerm = request.SearchTerm;
                                query = query.Where(x => x.Code.ToLower().Contains(searchTerm.ToLower()) ||
                                                         x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                         x.NameArabic.ToLower().Contains(searchTerm.ToLower()));
                            }

                            if (terminalCategories.Count > 0)
                            {
                                var categoriesList = await query
                                    .OrderBy(x => x.Code)
                                    .Where(x => x.isActive && terminalCategories.Contains(x.Id))
                                    .ProjectTo<CategoryLookUpModel>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
                                return new PagedResult<CategoryListModel>
                                {
                                    Results = new CategoryListModel
                                    {
                                        Categories = categoriesList
                                    },
                                };

                            }
                            else
                            {

                                var categoriesList = await query
                                    .OrderBy(x => x.Code)
                                    .Where(x => x.isActive)
                                    .ProjectTo<CategoryLookUpModel>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
                                return new PagedResult<CategoryListModel>
                                {
                                    Results = new CategoryListModel
                                    {
                                        Categories = categoriesList
                                    },
                                };
                            }
                            

                           
                        }
                        else
                        {
                            query = _context.Categories.AsNoTracking().AsQueryable();
                            if (request.CompanyId != null)
                            {
                                _context.DisableTenantFilter = true;
                                query = _context.Categories.Where(x => EF.Property<Guid>(x, "CompanyId") == request.CompanyId).AsNoTracking().AsQueryable();
                            }
                            

                            var categoriesList = await query
                                .OrderBy(x => x.Code)
                                .Where(x => x.isActive)
                                .ProjectTo<CategoryLookUpModel>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken);

                            return new PagedResult<CategoryListModel>
                            {
                                Results = new CategoryListModel
                                {
                                    Categories = categoriesList
                                }
                            };
                        }

                    }


                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
