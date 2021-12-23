using System;
using System.Collections;
using System.Collections.Generic;
using MediatR;

namespace nsda_api.Domain.Commands
{
    public class GetTransactionList : IRequest<IEnumerable<TransactionViewModel>>
    {
        
    }

    public class TransactionViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string TransactionId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
