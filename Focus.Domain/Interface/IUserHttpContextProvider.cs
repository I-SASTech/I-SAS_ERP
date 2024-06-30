using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Focus.Domain.Interface
{
    public interface IUserHttpContextProvider
    {
        Guid? GetUserId();
        string GetUserName();
        Guid GetCompanyId();
        Guid? GetBusinessId();
        Guid? GetClientId();
        string GetUserFullName();
        Guid? GetCounterId();
        Guid? GetDayId();
        //string ShowUserRoles();
        bool DisableTenantFilter { get; set; }
    }

    public class UserHttpContextProvider : IUserHttpContextProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserHttpContextProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Guid? GetUserId()
        {

            var claimsIdentity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;
            var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "UserId");
            return userIdClaim != null ? new Guid(userIdClaim?.Value) : (Guid?)null;

        }
        public string GetUserName()
        {
            if (_contextAccessor.HttpContext == null) return "Anonymous";

            var claimsIdentity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;
            var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "Email");
            return userIdClaim?.Value;
        }

        public string GetUserFullName()
        {
            if (_contextAccessor.HttpContext == null) return "Anonymous";

            var claimsIdentity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;
            var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "UserName");
            return userIdClaim?.Value;
        }



        public Guid GetCompanyId()
        {
            if (_contextAccessor.HttpContext == null) return Guid.Empty;

            var claimsIdentity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;
            var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "CompanyId");
            return new Guid(userIdClaim?.Value ?? Guid.Empty.ToString());
        }
        public Guid? GetBusinessId()
        {
            if (_contextAccessor.HttpContext == null) return Guid.Empty;

            var claimsIdentity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;
            var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "BusinessId");
            return userIdClaim != null ? new Guid(userIdClaim?.Value) : (Guid?)null;
        }
        public Guid? GetClientId()
        {
            if (_contextAccessor.HttpContext == null) return Guid.Empty;

            var claimsIdentity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;
            var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "ClientParentId");
            return userIdClaim != null ? new Guid(userIdClaim?.Value) : (Guid?)null;
        }

        public Guid? GetCounterId()
        {
            if (_contextAccessor.HttpContext == null) return Guid.Empty;

            var claimsIdentity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;
            var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "CounterId");
            return userIdClaim != null ? new Guid(userIdClaim?.Value) : (Guid?)null;
        }

        public Guid? GetDayId()
        {
            if (_contextAccessor.HttpContext == null) return Guid.Empty;
            
            var claimsIdentity = (ClaimsIdentity) _contextAccessor.HttpContext.User.Identity; 
            var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == "DayId");
            return userIdClaim != null ? new Guid(userIdClaim?.Value) : (Guid?) null;
        }

        public bool DisableTenantFilter { get; set; }
    }
}
