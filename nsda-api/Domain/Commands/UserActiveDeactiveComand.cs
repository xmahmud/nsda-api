using System;
using MediatR;

namespace nsda_api.Domain.Commands
{
    public class UserActiveDeactiveComand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public bool ToActive { get; set; }
    }
}
