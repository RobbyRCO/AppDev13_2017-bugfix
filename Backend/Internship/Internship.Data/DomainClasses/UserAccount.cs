using System.Security.Claims;
using System.Threading.Tasks;
using Internship.Data.DomainClasses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Internship.Api.Models
{
    // You can add profile data for the user by adding more properties to your UserAccount class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class UserAccount : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserAccount> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}