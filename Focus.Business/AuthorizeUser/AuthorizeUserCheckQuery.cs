using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.AuthorizeUser
{
    public class AuthorizeUserCheckQuery : IRequest<AuthorizeUserLookUp>
    {
        public AuthorizeUserLookUp Authorize { get; set; }
        public class Handler : IRequestHandler<AuthorizeUserCheckQuery, AuthorizeUserLookUp>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly UserManager<ApplicationUser> _userManager;


            //Constructor
            public Handler(IApplicationDbContext context, ILogger<AuthorizeUserCheckQuery> logger,
                SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                Logger = logger;
                _signInManager = signInManager;
                _userManager = userManager;
            }
            public async Task<AuthorizeUserLookUp> Handle(AuthorizeUserCheckQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    SignInResult result = new SignInResult();
                    ApplicationUser user;
                    if (request.Authorize.UserName.IndexOf('@') > -1)
                    {
                        //Validate email format
                        string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(request.Authorize.UserName))
                        {
                            result = await _signInManager.PasswordSignInAsync(request.Authorize.UserName, request.Authorize.Password, false, lockoutOnFailure: false);
                            user = await _userManager.FindByNameAsync(request.Authorize.UserName);
                            if (user != null && user.IsActive)
                            {
                                return new AuthorizeUserLookUp
                                {
                                    IsLoginFail = true
                                };
                            }
                        }
                        else
                        {
                            user = await _userManager.FindByEmailAsync(request.Authorize.UserName);
                            if (user != null)
                                result = await _signInManager.PasswordSignInAsync(user.UserName, request.Authorize.Password, false, lockoutOnFailure: false);
                            if (user != null && user.IsActive)
                            {
                                return new AuthorizeUserLookUp
                                {
                                    IsLoginFail = true
                                };
                            }
                        }

                    }
                    else
                    {
                        result = await _signInManager.PasswordSignInAsync(request.Authorize.UserName, request.Authorize.Password, false, lockoutOnFailure: false);
                        user = await _userManager.FindByNameAsync(request.Authorize.UserName);
                        if (user != null && user.IsActive)
                        {
                            return new AuthorizeUserLookUp
                            {
                                IsLoginFail = true
                            };
                        }
                    }
                    
                    if (!result.Succeeded)
                    {
                        return new AuthorizeUserLookUp
                        {
                            IsLoginFail = true
                        };
                    }

                    var isPermission = await Context.LoginPermissions
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x=>x.UserId== Guid.Parse(user.Id), cancellationToken: cancellationToken);
                    if (isPermission!=null)
                    {
                        return new AuthorizeUserLookUp
                        {
                            ChangePriceDuringSale = isPermission.ChangePriceDuringSale,
                            GiveDiscountDuringSale = isPermission.GiveDicountDuringSale
                        };
                    }

                    return new AuthorizeUserLookUp
                    {
                        IsLoginFail = true
                    };
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

    }
}
