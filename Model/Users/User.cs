using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Utility.Db;

namespace Model.Users
{
    [Table(nameof(User), Schema = "user")]
    public class User : BaseModel
    {       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int MembershipIndex { get; set; }
        public bool Active { get; set; }
          
    }
}
