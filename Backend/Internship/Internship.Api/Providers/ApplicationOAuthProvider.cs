using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Internship.Api.Models;
using Internship.Data;

namespace Internship.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            UserAccount userAccount = await userManager.FindAsync(context.UserName, context.Password);

            if (userAccount == null)
            {
                context.SetError("invalid_grant", "The userAccount name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await userAccount.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await userAccount.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(userAccount.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            context.AdditionalResponseParameters.Add("userId",context.Identity.GetUserId());
            context.AdditionalResponseParameters.Add("isAuthenticated",context.Identity.IsAuthenticated);

            ApplicationDbContext userscontext = new ApplicationDbContext();
            var userStore = new UserStore<UserAccount>(userscontext);
            var userManager = new UserManager<UserAccount>(userStore);

            context.AdditionalResponseParameters.Add("isStudent", userManager.IsInRole(context.Identity.GetUserId(), "Student"));
            context.AdditionalResponseParameters.Add("isTeacher", userManager.IsInRole(context.Identity.GetUserId(), "Teacher"));
            context.AdditionalResponseParameters.Add("isCompany", userManager.IsInRole(context.Identity.GetUserId(), "Company"));
            context.AdditionalResponseParameters.Add("isCoordinator", userManager.IsInRole(context.Identity.GetUserId(), "Coordinator"));
            context.AdditionalResponseParameters.Add("emailConfirmed", userManager.IsEmailConfirmed(context.Identity.GetUserId()));

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}