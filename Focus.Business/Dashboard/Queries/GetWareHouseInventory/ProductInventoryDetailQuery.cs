using AutoMapper;
using Focus.Business.Common;
using Focus.Business.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Dapper;
using Focus.Business.Products.Queries.GetProductList;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using Microsoft.Extensions.Configuration;
using NPOI.OpenXml4Net.OPC;
using Unit = MediatR.Unit;

namespace Focus.Business.Dashboard.Queries.GetWareHouseInventory
{
    public class ProductInventoryDetailQuery : PagedRequest, IRequest<List<ProductInventoryLookUpModel>>
    {
        public Guid ProductId { get; set; }

        public class Handler : IRequestHandler<ProductInventoryDetailQuery, List<ProductInventoryLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            private readonly IMapper _mapper;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context, ILogger<ProductInventoryDetailQuery> logger, IMapper mapper, IConfiguration configuration, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                Configuration = configuration;
                _logger = logger;
                _mapper = mapper;
                _contextProvider = contextProvider;
            }

            public async Task<List<ProductInventoryLookUpModel>> Handle(ProductInventoryDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    using (var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection")))
                    {

                        var productInventories =
                            conn.Query<ProductInventoryLookUpModel>(
                                "SELECT f.* FROM (SELECT ProductId, Products.EnglishName as ProductName, WareHouseId, Warehouses.Name as WarehouseName, Warehouses.NameArabic as WarehouseNameArabic, CurrentQuantity, Date, " +
                                "ROW_NUMBER() OVER(PARTITION BY WareHouseId ORDER BY Date DESC)  rownum " +
                                "FROM Inventories " +
                                " inner join Warehouses on Warehouses.Id = Inventories.WareHouseId " +
                                " inner join Products on Products.Id = Inventories.ProductId " +
                                " WHERE ProductId = '" +
                                request.ProductId + "'" +
                                " and Inventories.CompanyId= '" +
                                _contextProvider.GetCompanyId() + "') f WHERE rownum =1").ToList();





                        return productInventories;
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
