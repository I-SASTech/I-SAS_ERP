using Focus.Business.Interface;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using Focus.Domain.Entities;
using System.Linq;
using Focus.Domain.Interface;

namespace Focus.Business.HoldTypeSetup.Commands
{
    public class PermanentDeleteHoldSetupDeleteCommand : IRequest<bool>
    {
        public class Handler : IRequestHandler<PermanentDeleteHoldSetupDeleteCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            public readonly IConfiguration Configuration;
            private readonly IUserHttpContextProvider _contextProvider;


            public Handler(IApplicationDbContext context, IUserHttpContextProvider contextProvider, IConfiguration configuration)
            {
                _context = context;
                Configuration = configuration;
                _contextProvider = contextProvider;
            }

            public async Task<bool> Handle(PermanentDeleteHoldSetupDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    using var conn = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                    var companyId = _contextProvider.GetCompanyId();
                    var hold = await _context.HoldSetups.FirstOrDefaultAsync();
                    if (hold != null)
                    {
                        var holdDays = 0;
                        var holdMonths = 0;

                        if(hold.HoldRecordType == "1 Week")
                        {
                            holdDays = 7;
                        }
                        else if(hold.HoldRecordType == "2 Weeks")
                        {
                            holdDays = 14;
                        }
                        else if (hold.HoldRecordType == "3 Weeks")
                        {
                            holdDays = 21;
                        }
                        else
                        {
                            string input = hold.HoldRecordType;

                            Match match = Regex.Match(input, @"\d+");

                            if (match.Success && int.TryParse(match.Value, out int months))
                            {
                                holdMonths = months; 
                            }
                            else
                            {
                                holdMonths = 0;
                            }
                        }

                        if(holdDays > 0)
                        {
                            DateTime totalMonths = DateTime.Now.AddDays(-(holdDays + 1));

                            string sb = "DELETE FROM Sales WHERE Date <=" + "'" + totalMonths + "'" +
                                        "AND InvoiceType =" + 0 + " And CompanyId=" + "'" + companyId + "'";

                            conn.Query<Sale>(sb).AsQueryable();
                        }
                        if(holdMonths > 0)
                        {
                            DateTime totalMonths = DateTime.Now.AddMonths(-(holdMonths));

                            string sb = "DELETE FROM Sales WHERE Date <=" + "'" + totalMonths + "'" +
                                        "AND InvoiceType =" + 0 + " And CompanyId=" + "'" + companyId + "'";

                            conn.Query<Sale>(sb).AsQueryable();
                        }
                    }
                    return false;
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
    }
}
