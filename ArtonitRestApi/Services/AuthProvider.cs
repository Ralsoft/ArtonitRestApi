using ArtonitRestApi.Models;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArtonitRestApi.Services
{
    internal class AuthProvider : OAuthAuthorizationServerProvider
    {
        private AuthService _auth = new AuthService();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var user = _auth.Login(context.UserName, context.Password);


            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(MyClaimTypes.Flag, user.Flag));
            identity.AddClaim(new Claim(MyClaimTypes.IdOrgCtrl, user.IdOgrCtrl.ToString()));

            context.Validated(identity);
        }
    }
}
