using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeaveGroup.Commands;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.EmployeeToAspNetUser.Command
{
    public class EmployeeToAspNetUserAddCommand : IRequest<Message>
    {
        public bool Individual { get; set; }
        public Guid? EmployeeId { get; set; }
        public class Handler : IRequestHandler<EmployeeToAspNetUserAddCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IUserHttpContextProvider _contextProvider;
            private readonly ISendEmail _sendEmail;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IPrincipal _principal;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<EmployeeToAspNetUserAddCommand> logger, ISendEmail sendEmail, IUserHttpContextProvider contextProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IPrincipal principal, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _userManager = userManager;
                _contextProvider = contextProvider;
                _sendEmail = sendEmail;
                _roleManager = roleManager;
                _principal = principal;
                _mediator = mediator;
            }
            public async Task<Message> Handle(EmployeeToAspNetUserAddCommand request, CancellationToken cancellationToken)
            {
                try
                {
                   
                    int length = 8;
                    var passwordHasher = new PasswordHasher<IdentityUser>();
                    var userRoles =  Context.NobleRoles.FirstOrDefault( x=> x.Name == "HrUser");
                    var identityRole = new IdentityRole();
                    var companyId = _contextProvider.GetCompanyId();

                    var roleName = "";
                    var normalizeName = "";
                    if (userRoles == null)
                    {
                        var companyNobleRole = new NobleRoles()
                        {
                            Name = "HrUser",
                            NormalizedName = "HRUSER",
                            IsActive = true
                        };
                        await Context.NobleRoles.AddAsync(companyNobleRole, cancellationToken);
                        roleName = companyNobleRole.Name;
                        normalizeName = companyNobleRole.NormalizedName;
                    }
                    else
                    {
                        roleName = userRoles.Name;
                        normalizeName = userRoles.NormalizedName;
                    }

                    var isIdentityRoleExists = _roleManager.Roles.FirstOrDefault(x=> x.Name == roleName);
                    if (request.Individual)
                    {
                        var employeeDetails = await Context.EmployeeRegistrations.FirstOrDefaultAsync(x => x.Id == request.EmployeeId, cancellationToken: cancellationToken);
                        if (employeeDetails == null)
                            throw new NotFoundException("User Not Found", "");

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        var userDetail = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == employeeDetails.Email, cancellationToken: cancellationToken);

                        if(userDetail == null)
                        {
                            var register = new UserDetailDto()
                            {
                                Email = employeeDetails.Email,
                                FullName = employeeDetails.EnglishName.Replace(" ", "").ToLower(),
                                Password = GeneratePassword(length),
                                EmployeeId = employeeDetails.Id,
                                FirstName = employeeDetails.EnglishName,
                            };

                            var user = new ApplicationUser
                            {
                                UserName = register.FullName,
                                Email = register.Email,
                                Password = register.Password,
                                FirstName = register.FirstName,
                                LastName = "",
                                CompanyId = companyId,
                                EmailConfirmed = true,
                                EmployeeId= employeeDetails.Id,
                                PhoneNumber = register.PhoneNumber,
                            };

                            var result = await _userManager.CreateAsync(user, register.Password);

                            if (result.Succeeded)
                            {
                                if (isIdentityRoleExists == null)
                                {
                                    identityRole.Name = roleName;
                                    identityRole.NormalizedName = normalizeName;
                                    await _roleManager.CreateAsync(identityRole);

                                    await _userManager.AddToRoleAsync(user, register.RoleName = roleName);
                                }
                                else
                                {
                                    await _userManager.AddToRoleAsync(user, register.RoleName = roleName);
                                }

                                var claims = new List<Claim>
                                {
                                    new Claim("Email",user.Email),
                                    new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                                    new Claim("Organization",_principal.Identity.Organization()),
                                    new Claim("CompanyId",companyId.ToString()),
                                    new Claim("NobleCompanyId",_principal.Identity.NobelCompanyId().ToString()),
                                    new Claim("ClientParentId",companyId.ToString()),
                                };
                                await _userManager.AddClaimsAsync(user, claims);

                                var sendEmail = new SendEmailDto()
                                {
                                    Subject = "User Name and Password For Login",
                                    EmailTo = user.Email,
                                    YourEmail = user.Email,
                                    Password = user.Password,
                                    UserName = user.UserName,
                                };

                                await _sendEmail.SendEmailAsync(sendEmail, "", "", "");
                            }

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = Context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            await Context.SaveChangesAsync(cancellationToken);
                            return new Message
                            {
                                IsAddUpdate = "Data Added Successfully"
                            };
                        }
                        else
                        {
                            Random rnd1 = new Random();
                            for (int i = 0; i < 11; i++)
                            {
                                _code += rnd1.Next(0, 9).ToString();
                            }

                            var sendEmail = new SendEmailDto()
                            {
                                Subject = "User Name and Password For Login",
                                EmailTo = userDetail.Email,
                                YourEmail = userDetail.Email,
                                Password = userDetail.Password,
                                UserName = userDetail.UserName,
                            };

                            await _sendEmail.SendEmailAsync(sendEmail, "", "", "");

                            await _mediator.Send(new AddUpdateSyncRecordCommand()
                            {
                                SyncRecordModels = Context.SyncLog(),
                                Code = _code,
                            }, cancellationToken);

                            await Context.SaveChangesAsync(cancellationToken);
                            return new Message
                            {
                                IsAddUpdate = "Email Has Sent Successfully"
                            };
                        }
                    }
                    else
                    {
                        var employyeeList = await Context.EmployeeRegistrations.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                        var userList = await _userManager.Users.ToListAsync(cancellationToken: cancellationToken);

                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        foreach (var employee in employyeeList)
                        {
                            var employeeDetails = userList.FirstOrDefault(x => x.Email == employee.Email);
                            if (employeeDetails == null)
                            {
                                var register = new UserDetailDto()
                                {
                                    Email = employee.Email,
                                    FullName = employee.EnglishName.Replace(" ", "").ToLower(),
                                    Password = GeneratePassword(length),
                                    EmployeeId = employee.Id,
                                    FirstName = employee.EnglishName,
                                };

                                var user = new ApplicationUser
                                {
                                    UserName = register.FullName,
                                    Email = register.Email,
                                    Password = register.Password,
                                    FirstName = register.FirstName,
                                    LastName = "",
                                    CompanyId = companyId,
                                    EmailConfirmed = true,
                                    PhoneNumber = register.PhoneNumber,
                                    EmployeeId= employee.Id,
                                };
                                var result = await _userManager.CreateAsync(user, register.Password);
                                if (result.Succeeded)
                                {
                                    if (isIdentityRoleExists == null)
                                    {
                                        identityRole.Name = roleName;
                                        identityRole.NormalizedName = normalizeName;
                                        await _roleManager.CreateAsync(identityRole);

                                        await _userManager.AddToRoleAsync(user, register.RoleName = roleName);
                                    }
                                    else
                                    {
                                        await _userManager.AddToRoleAsync(user, register.RoleName = roleName);
                                    }

                                    var claims = new List<Claim>
                                {
                                    new Claim("Email",user.Email),
                                    new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                                    new Claim("Organization",_principal.Identity.Organization()),
                                    new Claim("CompanyId",companyId.ToString()),
                                    new Claim("NobleCompanyId",_principal.Identity.NobelCompanyId().ToString()),
                                    new Claim("ClientParentId",companyId.ToString()),
                                };
                                    await _userManager.AddClaimsAsync(user, claims);

                                    var sendEmail = new SendEmailDto()
                                    {
                                        Subject = "User Name and Password For Login",
                                        EmailTo = user.Email,
                                        YourEmail = user.Email,
                                        Password = user.Password,
                                        UserName = user.UserName,
                                    };

                                    await _sendEmail.SendEmailAsync(sendEmail, "", "", "");
                                }
                            }
                        }

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = Context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await Context.SaveChangesAsync(cancellationToken);
                        return new Message
                        {
                            IsAddUpdate = "Data Added Successfully"
                        };
                    } 
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            } 
        }
        static string GeneratePassword(int length)
        {
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string symbolChars = "@#$";
            const string digitChars = "0123456789";
            StringBuilder passwordBuilder = new StringBuilder();
            Random random = new Random();

            // add one uppercase letter
            passwordBuilder.Append(upperChars[random.Next(upperChars.Length)]);
            // add one lowercase letter
            passwordBuilder.Append(lowerChars[random.Next(lowerChars.Length)]);
            // add one symbol
            passwordBuilder.Append(symbolChars[random.Next(symbolChars.Length)]);
            // add one digit
            passwordBuilder.Append(digitChars[random.Next(digitChars.Length)]);

            // fill the remaining password length with random characters
            int remainingLength = length - passwordBuilder.Length;
            while (0 < remainingLength--)
            {
                string validChars = upperChars + lowerChars + symbolChars + digitChars;
                passwordBuilder.Append(validChars[random.Next(validChars.Length)]);
            }

            // shuffle the password characters
            string password = passwordBuilder.ToString();
            char[] passwordChars = password.ToCharArray();
            for (int i = 0; i < length; i++)
            {
                int j = random.Next(length);
                char temp = passwordChars[i];
                passwordChars[i] = passwordChars[j];
                passwordChars[j] = temp;
            }

            return new string(passwordChars);
        }
    }
}
