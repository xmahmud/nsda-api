using System;
using MediatR;
using nsda_api.Domain.ViewModels;

namespace nsda_api.Domain.Queries
{
    public class GetUserInformationQuery : IRequest<UserInformationViewModel>
    {
        public Guid UserId { get; set; }
    }
}
