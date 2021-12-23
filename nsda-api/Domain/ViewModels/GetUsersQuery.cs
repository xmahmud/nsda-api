using System;
using System.Collections;
using System.Collections.Generic;
using MediatR;

namespace nsda_api.Domain.ViewModels
{
    public class GetUsersQuery : IRequest<IEnumerable<UserInformationViewModel>>
    {
        public int? MembershipIndex { get; set; }  
    }
}
