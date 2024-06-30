using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Focus.Business.Models;

namespace Focus.Business.Interface
{
    public interface IUserComponent
    {
        //List<UsersDto> UserListing(string type);
        Task DeleteUser(string id);
        string GetDecoratedRoleName(string roleId);
        string GetUserFullName(string email);
        string GetUserId(string email);
        Guid GetCompanyId(string email);
        string GetUserProfileImage(string email);
        string GetUserProfileImages(string id, string email);
        ProfileDto GetProfile(string email);
        ProfileDto GetProfile(string id, string email);
        Task UpdateProfile(ProfileDto dto);
        Task UpdateProfilePictureAsync(ProfileDto dto);
        UserDetailDto GetAllUserRoles();
        List<UserDetailDto> ListingUsers();
        List<UserDetailDto> ForRoleUsersList(bool isHistory, bool istransferTerminal, bool isSupervisorTerminal);
        //ManageUserDto EditUserRole(string id);
        //Task UpdateRoleAsync(ManageUserDto dto);
        Task<bool> UserNameExistsAsync(string email);
        Task<bool> IsTermsAndConditionsSigned(string email);
        Task TermsAndConditionsAccepted(string email);
        string GetCurrentUserName();

        UserDetailDto GetUserById(string Id);
        Task<string> GetRoleByUser(string Id);
        Task UpdateUserAndRole(UserDetailDto userDetailDto);
        Task<bool> DeleteUser(Guid Id);
        Task<bool> UpdatePassword(string id, string password);
    }
}
