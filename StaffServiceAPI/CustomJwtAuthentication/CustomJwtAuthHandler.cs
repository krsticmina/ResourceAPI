using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace StaffServiceAPI.CustomJwtAuthentication
{
    public class CustomJwtAuthHandler
       : AuthenticationHandler<JwtAuthSchemeOptions>
    {
        public CustomJwtAuthHandler(
            IOptionsMonitor<JwtAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            using (var client = new HttpClient())
            {
                var token = string.Empty;

                var header = Request.Headers["Authorization"];

                if (header.Count == 0) throw new AuthenticationException("Authorization header is empty");

                string[] tokenValue = Convert.ToString(header).Trim().Split(" ");

                if (tokenValue.Length > 1) token = tokenValue[1];

                else throw new AuthenticationException("Authorization token is empty");

                client.BaseAddress = new Uri("https://localhost:7206");

                client.DefaultRequestHeaders.Accept.Clear();
               
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

                var Result = new HttpResponseMessage();

                Result = await client.GetAsync("https://localhost:7150/api/validation");

                if (Result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new AuthenticationException("Invalid token");
                }
     
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var identity = new ClaimsIdentity(jwt.Claims, Scheme.Name);

                var principal = new System.Security.Principal.GenericPrincipal(identity, null);
                
                Thread.CurrentPrincipal = principal;

                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            
        }
    }
}
