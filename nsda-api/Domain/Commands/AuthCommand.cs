using System;
using MediatR;
using nsda_api.Domain.ViewModels;

namespace nsda_api.Domain.Commands
{
    public class AuthCommand : IRequest<UserAuthenticatedViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
