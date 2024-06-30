using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Focus.Business.Extensions
{
    public static class IdentityExtension
    {
        public static string FullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string UserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string Organization(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Organization");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static Guid CompanyId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CompanyId");
            return (claim != null) ? Guid.Parse(claim.Value) : Guid.Empty;
        }
        public static Guid NobelCompanyId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("NobleCompanyId");
            return ( claim.Value != "") ? Guid.Parse(claim.Value) : Guid.Empty;
        }
        public static Guid BusinessId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("BusinessId");
            return (claim != null) ? Guid.Parse(claim.Value) : Guid.Empty;
        }
        public static Guid ClientParentId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ClientParentId");
            return ( claim.Value!="") ? Guid.Parse(claim.Value) : Guid.Empty;
        }
        public static string Email(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Email");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string CompanyLogo(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CompanyLogo");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string UserImagePath(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserImagePath");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string CounterId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CounterId");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static bool IsImpersonating(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("IsImpersonating");
            return (claim != null && claim.Value.ToLower() == "true") ? true : false;
        }
    }
}
