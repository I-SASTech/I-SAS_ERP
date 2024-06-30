using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.DayStarts.Commands.AddUpdateDayStarts;
using Focus.Business.DayStarts.Queries.GetTerminalOfCurrentUser;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Queries.SupervisorLogin
{
    public class SupervisorLoginQuery : IRequest<string>
    {
       
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsFlushData { get; set; }


        public class Handler : IRequestHandler<SupervisorLoginQuery, string>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IUserComponent _userComponent;
            private readonly IPrincipal _principal;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IMediator _mediator;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<SupervisorLoginQuery> logger,
                SignInManager<ApplicationUser> signInManager, IUserComponent userComponent, IPrincipal principal,
                UserManager<ApplicationUser> userManager, IMediator mediator)
            {
                Context = context;
                Logger = logger;
                _signInManager = signInManager;
                _userComponent = userComponent;
                _principal = principal;
                _userManager = userManager;
                _mediator = mediator;
            }

            public async Task<string> Handle(SupervisorLoginQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    SignInResult result;
                    ApplicationUser user;
                    if (request.Email.IndexOf('@') > -1)
                    {
                        //Validate email format
                        string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(request.Email))
                        {
                            result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, lockoutOnFailure: false);
                            user = await _userManager.FindByNameAsync(request.Email);
                            if (user != null)
                                request.Email = user.Email;
                           
                        }
                        else
                        {
                            user = await _userManager.FindByEmailAsync(request.Email);
                            result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                            
                        }

                    }
                    else
                    {
                        result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, lockoutOnFailure: false);
                        user = await _userManager.FindByNameAsync(request.Email);
                        if (user != null)
                            request.Email = user.Email;

                        
                    }

                    //var result = await _signInManager.PasswordSignInAsync(request.User, request.Password, false, lockoutOnFailure: false);

                    if (!result.Succeeded)
                    {
                        return "Not Valid Credential";
                    }

                    if (request.IsFlushData)
                    {
                        var userByEMAIL = await _userManager.FindByEmailAsync(request.Email);
                        if (userByEMAIL != null)
                        {
                            var roleId = await Context.NobleUserRoles.FirstOrDefaultAsync(x => x.UserId == userByEMAIL.Id,
                                cancellationToken);
                            if (roleId != null)
                            {
                                var nobleUser =
                                    await Context.NobleRoles.FirstOrDefaultAsync(x => x.Id == roleId.RoleId, cancellationToken);
                                if (nobleUser.Name == "Admin")
                                {
                                    return user.Id;
                                }
                            }
                            else
                            {
                                return "Not Valid Credential";
                            }
                        }

                        return "No Permission";
                    }
                    else
                    {
                        var loginPermission =
                            Context.LoginPermissions.FirstOrDefaultAsync(x => x.UserId == Guid.Parse(user.Id),
                                cancellationToken);
                        if (loginPermission == null)
                        {
                            return "No Permission";
                        }
                        if (loginPermission.Result.IsSupervisor)
                        {
                            return user.Id;
                        }
                        else
                        {
                            return "No Permission";
                        }
                    }
                    
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Some error occurred: " + exception.Message);
                }
            }
        }
    }
}
