using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Claims.Command.UpdateClaims
{
    public class UpdateClaimsCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string User { get; set; }
        public Guid CounterId { get; set; }
        public Guid DayId { get; set; }
        public Guid CompanyId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public class Handler : IRequestHandler<UpdateClaimsCommand, string>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly IUserComponent _userComponent;
            private readonly IConfiguration _configuration;

            //Constructor
            public Handler(IApplicationDbContext context, ILogger<UpdateClaimsCommand> logger, IUserComponent userComponent, IConfiguration configuration)
            {
                Context = context;
                Logger = logger;
                _userComponent = userComponent;
                _configuration = configuration;
            }

            public async Task<string> Handle(UpdateClaimsCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var claimsList = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "GetModuleWiseClaims")
                        };

                    var role = await _userComponent.GetRoleByUser(request.User);

                    var userCompany = await Context.Companies
                        .Select(x => new
                        {
                            x.Id,
                            x.ParentId,
                            x.ClientParentId,
                            x.BusinessParentId,
                            x.IsProceed,
                        }).FirstOrDefaultAsync(x => x.Id == request.ApplicationUser.CompanyId, cancellationToken: cancellationToken);

                    var dayStart = await Context.DayStarts
                        .Select(x => new
                        {
                            x.Id,
                            x.IsActive,
                            x.IsDayStart,
                        }).FirstOrDefaultAsync(x => x.IsActive && x.IsDayStart, cancellationToken: cancellationToken);
                    
                    var userCounter = await Context.DayStarts
                        .Select(x => new
                        {
                            x.IsDayStart,
                            x.IsActive,
                            x.StartTerminalFor,
                            x.CounterId,
                        }).FirstOrDefaultAsync(x => !x.IsDayStart && x.IsActive && x.StartTerminalFor == request.ApplicationUser.UserName, cancellationToken: cancellationToken);


                    var claims = new List<Claim>
                            {
                               new Claim(ClaimTypes.Name,request.ApplicationUser.Email),

                                new Claim(JwtRegisteredClaimNames.Sub, request.ApplicationUser.Email),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim("Role", role),
                                new Claim("CompanyId", request.ApplicationUser.CompanyId.ToString()),
                                new Claim("UserId", request.ApplicationUser.Id),
                                new Claim("Email", request.ApplicationUser.Email),
                                new Claim("UserName", request.ApplicationUser.UserName),
                                new Claim("NobleCompanyId", userCompany.ParentId.ToString()),
                                new Claim("BusinessId", userCompany.BusinessParentId.ToString()),
                                new Claim("ClientParentId", userCompany.ClientParentId.ToString()),
                                new Claim("EmployeeId", request.ApplicationUser.EmployeeId.ToString()),
                                new Claim("IsProceed", userCompany.IsProceed.ToString(), ClaimValueTypes.Boolean),
                                new Claim("CounterId", userCounter!=null?userCounter.CounterId.ToString(): request.ApplicationUser.TerminalId== null? Guid.Empty.ToString():request.ApplicationUser.TerminalId.ToString()),
                                new Claim("DayId", dayStart!=null?dayStart.Id.ToString():Guid.Empty.ToString()),

                            };
                    var allClaims = claims.Concat(claimsList);
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwt:JwtKey").Value));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration.GetSection("jwt:JwtExpireDays").Value));

                    var token = new JwtSecurityToken(
                        _configuration.GetSection("jwt:JwtIssuer").Value,
                        _configuration.GetSection("jwt:JwtIssuer").Value,
                        claims: allClaims,
                        expires: expires,
                        signingCredentials: creds

                    );

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("DayStart Already Exist");
                }
            }
        }
    }
}
