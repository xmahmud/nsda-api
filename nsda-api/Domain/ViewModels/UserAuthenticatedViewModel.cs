using System;
namespace nsda_api.Domain.ViewModels
{
    public class UserAuthenticatedViewModel
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool RememberMe { get; set; }
        public string Status { get; set; }
    }
}
