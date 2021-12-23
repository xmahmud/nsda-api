using System;
namespace nsda_api.Domain.ViewModels
{
    public class UserInformationViewModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int MembershipId { get; set; }
        public bool Active { get; set; }
    }
}
