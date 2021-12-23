using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Users;
using nsda_api.Domain.ViewModels;
using Utility;

namespace nsda_api.Helper
{
    public class UserService
    {
        public async static Task<UserAuthenticatedViewModel> UpdateUserSessionWithToken(DatabaseContext _context, User user, IConfiguration _configuration, bool rememberMe)
        {
            var expires = DateTime.UtcNow.AddDays(2);

            var tokenModel = new Token
            {
                AccessToken = TokenService.CreateToken(user.Id.ToString(), expires, _configuration.GetSection("SecretKey").Value),
                UserId = user.Id,
                RememberMe = rememberMe,
                RefreshToken = HashGenerator.RandomHash(),
                ExpiredBy = expires
            };

            _context.Token.Add(tokenModel);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return new UserAuthenticatedViewModel
                {
                    UserId = user.Id,
                    Token = tokenModel.AccessToken,
                    RefreshToken = tokenModel.RefreshToken,
                    RememberMe = tokenModel.RememberMe,
                    Status = "Success"
                };
            }

            return default(UserAuthenticatedViewModel);
        }
    }
}
