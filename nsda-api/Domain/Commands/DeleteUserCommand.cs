using System;
using MediatR;

namespace nsda_api.Domain.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
