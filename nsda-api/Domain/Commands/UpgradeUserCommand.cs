using System;
using MediatR;

namespace nsda_api.Domain.Commands
{
    public class UpgradeUserCommand : IRequest<bool>
    {        
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string TransactionId { get; set; }
    }
}
