using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using DocumentFormat.OpenXml.InkML;
using Focus.Business.Citys.Queries.GetCityList;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Focus.Business.LoginHistories.Queries.GetLoginHistoryList
{
    public class GetLoginHistoryListQuery : IRequest<List<LoginHistoryModel>>
    {
        public Guid UserId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public class Handler : IRequestHandler<GetLoginHistoryListQuery, List<LoginHistoryModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;
            public readonly IUserHttpContextProvider _ContextProvider;
            private readonly IConfiguration _configuration;

            public Handler(IApplicationDbContext context,
                ILogger<GetCityListQuery> logger,
                IMapper mapper,
                IUserHttpContextProvider provider,
                IConfiguration configuration)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
                _ContextProvider = provider;
                _configuration = configuration;
            }
            public Task<List<LoginHistoryModel>> Handle(GetLoginHistoryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var companyId = _ContextProvider.GetCompanyId().ToString();
                    var loginHistory = request.FromDate == DateTime.MinValue ? _context.LoginHistories.Where(x => x.UserId == request.UserId && x.LoginDateTime > DateTime.UtcNow.AddDays(-7)).ToList() : _context.LoginHistories.Where(x => x.UserId == request.UserId && x.LoginDateTime.Date >= request.FromDate.Date && x.LoginDateTime.Date <= request.ToDate.Date).OrderBy(x=>x.LoginDateTime).ToList();
                    var sqlQuery =
                        "select r.Name as userRole from NobleUserRoles as u inner join NobleRoles as r on u.RoleId = r.Id where UserId = '" +
                        request.UserId + "' and u.CompanyId = '" +companyId + "'";
                    var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    var userRole = connection.Query<string>(sqlQuery).FirstOrDefault();
                    if(loginHistory.Count > 0)
                        loginHistory.RemoveAt(loginHistory.Count - 1);
                    var historyModelList = new List<LoginHistoryModel>();
                    foreach (var history in loginHistory)
                    {
                        var model = new LoginHistoryModel()
                        {
                            UserName = _ContextProvider.GetUserName(),
                            LoginDate = history.LoginDateTime.ToString("d"),
                            LoginTime = history.LoginDateTime.ToString("T"),
                            LogoutDate = history.LogoutDateTime.ToString("d"),
                            LogoutTime = history.LogoutDateTime.ToString("T"),
                            UserRole = userRole
                        };
                        historyModelList.Add(model);
                    }
                    

                    return Task.FromResult(historyModelList);

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
