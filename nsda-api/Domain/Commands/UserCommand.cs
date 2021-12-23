using System;
using MediatR;

namespace nsda_api.Domain.Commands
{
    public class UserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
