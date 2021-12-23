using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Turnout;
using MediatR;
using Microsoft.Extensions.Configuration;
using Model;
using nsda_api.Domain.Commands;
using nsda_api.Domain.ViewModels;
using nsda_api.Helper;
using Utility;

namespace nsda_api.Handlers
{
    public class AuthCommandHandler :
        IRequestHandler<AuthCommand, UserAuthenticatedViewModel>
    {
        public readonly DatabaseContext _context;
        public readonly IConfiguration _configuration;
        public readonly IAuthenticatedUser _authenticatedUser;
        //private readonly JobService _jobService;

        public AuthCommandHandler(
            DatabaseContext context,
            IConfiguration configuration,
            IAuthenticatedUser authenticatedUser
            //JobService jobService
            )
        {
            _configuration = configuration;
            _context = context;
            _authenticatedUser = authenticatedUser;
            //_jobService = jobService;
        }

        public async Task<UserAuthenticatedViewModel> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var secretKey = _configuration.GetSection("SecretKey").Value;

            var password = HashGenerator.Hash(request.Password, secretKey);

            var user = _context.User
                .Where(x => !x.IsDeleted && x.Password == password && x.Email == request.Email.ToLower())
                .FirstOrDefault();

            if (user != null)
            {
                return await UserService.UpdateUserSessionWithToken(_context, user, _configuration, request.RememberMe);
            }

            return null;
        }
    }
}
