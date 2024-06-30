using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NPOI.SS.Formula.Functions;
using JwtSecurityToken = System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

namespace Focus.Business.Claims.Command.ModuleWiseClaim
{
    public class GetModuleWiseClaims : IRequest<List<ModuleWiseClaimsLookupModel>>
    {
        public string Email { get; set; }
        public string User { get; set; }
        public Guid CounterId { get; set; }
        public Guid DayId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? BranchId { get; set; }

        public class Handler : IRequestHandler<GetModuleWiseClaims, List<ModuleWiseClaimsLookupModel>>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly IUserComponent _userComponent;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ICompanyComponent _companyComponent;
            private readonly IConfiguration _configuration;

            //Constructor
            public Handler(IApplicationDbContext context, ILogger<GetModuleWiseClaims> logger
                , IUserComponent userComponent, UserManager<ApplicationUser> userManager,
                ICompanyComponent companyComponent, IConfiguration configuration)
            {
                Context = context;
                Logger = logger;
                _userComponent = userComponent;
                _userManager = userManager;
                _companyComponent = companyComponent;
                _configuration = configuration;
            }

            public async Task<List<ModuleWiseClaimsLookupModel>> Handle(GetModuleWiseClaims request, CancellationToken cancellationToken)
            {
                try
                {

                    var nobleRoleUser =await Context.NobleUserRoles.AsNoTracking().Where(x => x.UserId == request.User).Select(x => x.RoleId).ToListAsync(cancellationToken: cancellationToken);
                    
                    var rolesPermissions =await Context.NobleRolePermissions
                        .AsNoTracking()
                        .Include(x => x.CompanyPermissions)
                        .Where(x => nobleRoleUser.Contains(x.RoleId))
                        .ToListAsync(cancellationToken: cancellationToken);

                    var modules =await Context.NobleModules.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    var moduleWiseClaims = new List<ModuleWiseClaims>();
                    
                        foreach (var module in modules)
                        {
                            var climsList = new List<Claim>();
                            var permissionList =
                                rolesPermissions.Where(x => x.CompanyPermissions.NobleModuleId == module.Id);
                            foreach (var rol in permissionList)
                            {
                                climsList.Add(new Claim(ClaimTypes.Role, rol.CompanyPermissions == null ? "null" : rol.CompanyPermissions.Value));

                            }

                            if (climsList.Count > 0)
                            {
                                moduleWiseClaims.Add(new ModuleWiseClaims()
                                {
                                    Claims = climsList,
                                    ModuleName = module.ModuleName
                                });
                            }
                        }

                    

                    var role = await _userComponent.GetRoleByUser(request.User);
                    
                    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.User, cancellationToken);

                    var userCompany = _companyComponent.GetCompanyById(user.CompanyId);
                    
                    var dayStart = await Context.DayStarts.FirstOrDefaultAsync(x => x.IsActive && x.IsDayStart, cancellationToken: cancellationToken);
                    var userCounter = await Context.DayStarts.FirstOrDefaultAsync(x => !x.IsDayStart && x.IsActive && x.StartTerminalFor == user.UserName, cancellationToken: cancellationToken);
                    
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.Email),

                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("Role", role),
                        new Claim("CompanyId", user.CompanyId.ToString()),
                        new Claim("UserId", user.Id),
                        new Claim("Email", user.Email),
                        new Claim("UserName", user.UserName),
                        new Claim("NobleCompanyId", userCompany?.ParentId.ToString()),
                        new Claim("BusinessId", userCompany?.BusinessId.ToString()),
                        new Claim("ClientParentId", userCompany?.ClientParentId.ToString()),
                        new Claim("EmployeeId", user.EmployeeId.ToString()),
                        new Claim("CounterId", userCounter!=null?userCounter.CounterId.ToString(): user.TerminalId== null? Guid.Empty.ToString():user.TerminalId.ToString()),
                        new Claim("DayId", dayStart!=null?dayStart.Id.ToString():Guid.Empty.ToString()),
                        new Claim("IsProceed", userCompany.IsProceed.ToString(), ClaimValueTypes.Boolean),

                    };
                    //var userbyEMAIL = await _userManager.FindByEmailAsync(user.Email);
                    var roles = await _userManager.GetRolesAsync(user);

                    var claimsWithRoles = roles.Select(a => new Claim(ClaimTypes.Role, a));
                    var allClaims = claims.Concat(claimsWithRoles);
                    //allClaims = allClaims.Concat(climsList);
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwt:JwtKey").Value));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration.GetSection("jwt:JwtExpireDays").Value));
                    var tokenList = new List<ModuleWiseClaimsLookupModel>();
                    foreach (var claim in moduleWiseClaims)
                    {
                        var concatClaims = allClaims.Concat(claim.Claims);
                        var token = new JwtSecurityToken(
                            _configuration.GetSection("jwt:JwtIssuer").Value,
                            _configuration.GetSection("jwt:JwtIssuer").Value,
                            claims: concatClaims,
                            expires: expires,
                            signingCredentials: creds

                        );
                        var issueToken = new JwtSecurityTokenHandler().WriteToken(token);
                        tokenList.Add(new ModuleWiseClaimsLookupModel()
                        {
                            TokenName = claim.ModuleName,
                            Token = issueToken
                        });
                    }


                    return tokenList;




                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("DayStart Already Exist");
                }
            }
        }

        public class ModuleWiseClaims
        {
            public string ModuleName { get; set; }
            public List<Claim> Claims { get; set; }
        }
    }
}
