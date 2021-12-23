using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public interface IAuthenticatedUser
    {
        public string Id { get; }
        public string Token { get; }
    }
}
