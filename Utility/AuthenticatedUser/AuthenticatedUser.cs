using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Utility;

namespace Utility
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _context;
        public string Id { get { return GetUserId(); } }
        public string Token { get { return GetToken(); } }

        public AuthenticatedUser(IHttpContextAccessor context)
        {
            _context = context;
        }

        string GetUserId()
        {
            var token = GetToken();

            if (!string.IsNullOrEmpty(token))
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var Id = tokenS.Payload.Where(x => x.Key == "Id").Select(x => x.Value).First();

                if (Id != null)
                {
                    return Id.ToString();
                }              
            }
            return null;
        }

        string GetToken()
        {
            return _context.HttpContext.Request.Headers["Authorization"].First()?.Replace("Bearer ", "");
        }
    }
}
