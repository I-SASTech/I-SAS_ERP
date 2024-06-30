using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Components
{
    class UserComponent : IUserComponent
    {
        private Task<ApplicationUser> _user;

        private readonly IPrincipal _principal;
        private readonly IApplicationDbContext _context;
        private readonly ILogger<UserComponent> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserComponent(IApplicationDbContext context, IPrincipal principal,
            ILogger<UserComponent> logger, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _principal = principal;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public Task DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public string GetDecoratedRoleName(string roleId)
        {
            throw new NotImplementedException();
        }

        public string GetUserFullName(string email)
        {
            throw new NotImplementedException();
        }

        public string GetUserId(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                _user = _userManager.FindByEmailAsync(email);

                return _user.Result.Id;
            }

            return null;
        }

        public Guid GetCompanyId(string email)
        {
            throw new NotImplementedException();
        }

        public string GetUserProfileImage(string email)
        {
            throw new NotImplementedException();
        }

        public string GetUserProfileImages(string id, string email)
        {
            throw new NotImplementedException();
        }

        public ProfileDto GetProfile(string email)
        {
            throw new NotImplementedException();
        }

        public ProfileDto GetProfile(string id, string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfile(ProfileDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfilePictureAsync(ProfileDto dto)
        {
            throw new NotImplementedException();
        }

        public UserDetailDto GetAllUserRoles()
        {
            throw new NotImplementedException();
        }

        public List<UserDetailDto> ListingUsers()
        {

            var users = _userManager.Users.ToList();
            var nobleRoles = _context.NobleUserRoles.AsNoTracking().Include(x => x.Roles).ToList();
            var loginPermissions = _context.LoginPermissions.AsNoTracking().AsQueryable();

            var userList = new List<UserDetailDto>();

            foreach (var user in users)
            {
                if (user.CompanyId == _principal.Identity.CompanyId())
                {
                    if (_context.Companies != null)
                    {
                        var userDto = new UserDetailDto
                        {
                            Id = user.Id,
                            //FullName = user.FirstName + " " + user.LastName,
                            FullName = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            CompanyName = _context.Companies.FirstOrDefault(x => x.Id == user.CompanyId)?.NameEnglish,
                            ImagePath = user.ImagePath,
                            EmployeeId = user.EmployeeId,
                            IsActive = user.IsActive,
                            RoleName = nobleRoles.Where(x => x.UserId == user.Id).Select(x => x.Roles?.Name).FirstOrDefault(),
                            EmployeeNo = user.EmployeeId != null ? _context.EmployeeRegistrations.FirstOrDefault(x => x.Id == user.EmployeeId)?.Code : "",
                            TemporaryCashReceiver = loginPermissions.FirstOrDefault(x => x.UserId.ToString()  == user.Id)?.TemporaryCashReceiver ?? false,
                            TemporaryCashIssuer = loginPermissions.FirstOrDefault(x => x.UserId.ToString() == user.Id)?.TemporaryCashIssuer ?? false,
                            TemporaryCashRequester = loginPermissions.FirstOrDefault(x => x.UserId.ToString() == user.Id)?.TemporaryCashRequester ?? false,
                            Days = loginPermissions.FirstOrDefault(x => x.UserId.ToString() == user.Id)?.Days ?? 0,
                            Limit = loginPermissions.FirstOrDefault(x => x.UserId.ToString() == user.Id)?.Limit ?? 0,
                        };
                        userList.Add(userDto);
                    }
                }
            }

            return userList;
        }
        public List<UserDetailDto> ForRoleUsersList(bool isHistory, bool istransferTerminal, bool isSupervisorTerminal)
        {
            var nobleRoles = _context.NobleUserRoles.Select(x => x.UserId).ToList();

            var users = _userManager.Users.Where(x => x.CompanyId == _principal.Identity.CompanyId()).ToList();

            var userList = new List<UserDetailDto>();
            if (isSupervisorTerminal)
            {
                var superVisorList = _context.LoginPermissions.AsNoTracking().Where(x => x.IsSupervisor)
                    .Select(x => x.UserId).ToList();
                var superVisers = users.Where(x => superVisorList.Contains(Guid.Parse(x.Id))).ToList();
                foreach (var user in superVisers)
                {
                    var userDetail = _context.NobleUserRoles.FirstOrDefault(x => x.UserId == user.Id);
                    if (userDetail != null )
                    {
                        var userDto = new UserDetailDto
                        {
                            Id = user.Id,
                            //FullName = user.FirstName + " " + user.LastName,
                            FullName = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            CompanyName = _context.Companies.FirstOrDefault(x => x.Id == user.CompanyId)?.NameEnglish,
                            ImagePath = user.ImagePath,
                            EmployeeId = user.EmployeeId,
                            IsActive = user.IsActive
                        };
                        userList.Add(userDto);
                    }
                }
            }
            else if (istransferTerminal)
            {
                var dayStartList = _context.DayStarts.AsNoTracking().Where(x => x.IsActive && !x.IsDayStart)
                    .Select(x => x.StartTerminalFor).ToList();
                
                var nonActiveUser = users.Where(x => !dayStartList.Contains(x.UserName)).ToList();
                foreach (var user in nonActiveUser)
                {
                    var userDetail = _context.NobleUserRoles.FirstOrDefault(x => x.UserId == user.Id);
                    if (userDetail != null && userDetail.Roles.Name == "Sales Man")
                    {
                        var userDto = new UserDetailDto
                        {
                            Id = user.Id,
                            //FullName = user.FirstName + " " + user.LastName,
                            FullName = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            CompanyName = _context.Companies.FirstOrDefault(x => x.Id == user.CompanyId)?.NameEnglish,
                            ImagePath = user.ImagePath,
                            EmployeeId = user.EmployeeId,
                            IsActive = user.IsActive
                        };
                        userList.Add(userDto);
                    }
                    
                    
                }
            }

            else if (!isHistory)
            {

                foreach (var user in users)
                {

                    if (!nobleRoles.Contains(user.Id))
                    {
                        if (user.CompanyId == _principal.Identity.CompanyId())
                        {
                            var userDto = new UserDetailDto
                            {
                                Id = user.Id,
                                //FullName = user.FirstName + " " + user.LastName,
                                FullName = user.UserName,
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                                CompanyName = _context.Companies.FirstOrDefault(x => x.Id == user.CompanyId)?.NameEnglish,
                                ImagePath = user.ImagePath,
                                EmployeeId = user.EmployeeId,
                                IsActive = user.IsActive
                            };
                            userList.Add(userDto);

                        }

                    }

                }
            }
            else
            {
                foreach (var user in users)
                {
                    if (user.CompanyId == _principal.Identity.CompanyId())
                    {
                        var userDto = new UserDetailDto
                        {
                            Id = user.Id,
                            //FullName = user.FirstName + " " + user.LastName,
                            FullName = user.UserName,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            CompanyName = _context.Companies.FirstOrDefault(x => x.Id == user.CompanyId)?.NameEnglish,
                            ImagePath = user.ImagePath,
                            EmployeeId = user.EmployeeId,
                            IsActive = user.IsActive
                        };
                        userList.Add(userDto);

                    }

                }
            }

            return userList;
        }

        public Task<bool> UserNameExistsAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTermsAndConditionsSigned(string email)
        {
            throw new NotImplementedException();
        }

        public Task TermsAndConditionsAccepted(string email)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUserName()
        {
            throw new NotImplementedException();
        }

        public UserDetailDto GetUserById(string Id)
        {
            if (Id != string.Empty)
            {
                _user = _userManager.FindByIdAsync(Id);

                var userDetail = new UserDetailDto
                {
                    Id = _user.Result.Id,
                    FirstName = _user.Result.FirstName,
                    LastName = _user.Result.LastName,
                    Email = _user.Result.Email,
                    PhoneNumber = _user.Result.PhoneNumber,
                    ImagePath = _user.Result.ImagePath,
                    CompanyId = _user.Result.CompanyId
                };

                return userDetail;
            }

            return null;
        }



        public async Task<string> GetRoleByUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            /*//var role = _roleManager.Roles;
        //    _roleManager.FindByIdAsync()*/
            if (Id != String.Empty)
            {
                var role = await _userManager.GetRolesAsync(user);
                var rol = "";

                foreach (var r in role)
                {
                    if (!role.Contains(",")) { rol = r; }
                    else { rol = "," + r; }
                }
                return rol;
            }

            return null;
        }

        public async Task UpdateUserAndRole(UserDetailDto userDetailDto)
        {
            var userData = await _userManager.FindByIdAsync(userDetailDto.Id);

            if (userData.Id != string.Empty)
            {
                userData.FirstName = userDetailDto.FirstName;
                userData.LastName = userDetailDto.LastName;
                userData.PhoneNumber = userDetailDto.PhoneNumber;
                userData.Email = userDetailDto.Email;
                userData.ImagePath = userDetailDto.ImagePath;
            }


            await _userManager.UpdateAsync(userData);

            var role = await _userManager.GetRolesAsync(userData);

            var r1 = await _userManager.RemoveFromRoleAsync(userData, role[0]);

            var ret = await _userManager.AddToRoleAsync(userData, userDetailDto.RoleName);

            var result = ret;
        }

        public async Task<bool> DeleteUser(Guid Id)
        {
            var user = _userManager.Users.Where(x => x.Id == Id.ToString()).ToList();
            if (user[0] != null)
            {
                //Remove Claims
                var claims = await _userManager.GetClaimsAsync(user[0]);
                var c = await _userManager.RemoveClaimsAsync(user[0], claims);

                // Remover Role
                var role = await _userManager.GetRolesAsync(user[0]);
                var r1 = await _userManager.RemoveFromRoleAsync(user[0], role[0]);

                var u = await _userManager.DeleteAsync(user[0]);

                if (u.Succeeded && r1.Succeeded && c.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> UpdatePassword(string id, string password)
        {
            var user = await _userManager.FindByIdAsync(id);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
