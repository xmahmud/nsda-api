using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Turnout;
using MediatR;
using Microsoft.Extensions.Configuration;
using Model;
using Model.Users;
using nsda_api.Domain.Commands;
using nsda_api.Domain.Queries;
using nsda_api.Domain.ViewModels;
using Utility;

namespace nsda_api.Handlers
{
    public class UserCommandHandler :
        IRequestHandler<UserCommand, bool>,
        IRequestHandler<DeleteUserCommand, bool>,
        IRequestHandler<UpdateUserCommand, bool>,
        IRequestHandler<UpgradeUserCommand, bool>,
        IRequestHandler<UserActiveDeactiveComand, bool>,
        IRequestHandler<GetUserInformationQuery, UserInformationViewModel>,
        IRequestHandler<GetUsersQuery, IEnumerable<UserInformationViewModel>>,
        IRequestHandler<GetTransactionList, IEnumerable<TransactionViewModel>>


    {
        public readonly DatabaseContext _context;
        public readonly IConfiguration _configuration;
        public readonly IAuthenticatedUser _authenticatedUser;
        //private readonly JobService _jobService;

        public UserCommandHandler(
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

        public async Task<bool> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            var secretKey = _configuration.GetSection("SecretKey").Value;

            var password = HashGenerator.Hash(request.Password, secretKey);

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
                Password = password
            };

            _context.User.Add(user);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.User.Where(x => x.Id == request.Id).FirstOrDefault();

            if(user != null)
            {
                user.IsDeleted = true;
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.User.Where(x => x.Id == request.Id).FirstOrDefault();

            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Handle(UpgradeUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.User.Where(x => x.Id == request.Id).FirstOrDefault();

            user.MembershipIndex = request.Value;

            if(!string.IsNullOrEmpty(request.TransactionId) && request.Value == 1)
            {
                _context.Transaction.Add(new Transaction
                {
                    UserId = user.Id,
                    TransactionId = request.TransactionId,
                    TransactionStatus = "Paid"
                });
            }


            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Handle(UserActiveDeactiveComand request, CancellationToken cancellationToken)
        {
            var user = _context.User.Where(x => x.Id == request.Id).FirstOrDefault();

            user.Active = request.ToActive;

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<UserInformationViewModel> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
        {
            var user = _context.User.Where(x => x.Id == request.UserId).FirstOrDefault();

            return Task.FromResult(new UserInformationViewModel
            {
                UserId = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Email = user.Email,
                MembershipId = user.MembershipIndex,
                Active = user.Active
            });
        }

        public Task<IEnumerable<UserInformationViewModel>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var q = _context.User.Where(x => !x.IsDeleted);

            if (request.MembershipIndex.HasValue)
            {
                q = q.Where(x => x.MembershipIndex == request.MembershipIndex);
            }

            var result = q.Select(x => new UserInformationViewModel
            {
                UserId = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                MembershipId = x.MembershipIndex,
                Active = x.Active
            }).AsEnumerable();

            return Task.FromResult(result);

        }

        public Task<IEnumerable<TransactionViewModel>> Handle(GetTransactionList request, CancellationToken cancellationToken)
        {
            var q = _context.Transaction.Where(x => !x.IsDeleted);

          

            var result = q.Select(x => new TransactionViewModel
            {
                UserId = x.UserId,
                Name = x.User.Name,
                TransactionId = x.TransactionId,
                TransactionStatus = x.TransactionStatus
            }).AsEnumerable();

            return Task.FromResult(result);
        }
    }
}
