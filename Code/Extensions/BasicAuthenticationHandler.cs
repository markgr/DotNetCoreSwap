using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DotNetCoreSwap.Models;
using DotNetCoreSwap.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetCoreSwap.Extensions
{
    /// <summary>
    /// Basic authentication handler.
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        /// <summary>
        /// C'Tor
        /// </summary>
        /// <param name="options">Options.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="encoder">Encoder.</param>
        /// <param name="clock">Clock.</param>
        /// <param name="userService">User service.</param>
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        /// <summary>
        /// Handles the authenticated request
        /// </summary>
        /// <returns>The authenticate async.</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Has the header ?
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            // See if we can get this user from the service
            UserResponse user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];
                user = await _userService.Authenticate(username, password);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            // Bad request then?
            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            // Extract the claims
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            // Return ticket
            return AuthenticateResult.Success(ticket);
        }
    }
}
