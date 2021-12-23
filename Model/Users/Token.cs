using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Utility.Db;

namespace Model.Users
{
    [Table(nameof(Token), Schema = "user")]
    public class Token: BaseModel
    {
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string AccessToken { get; set; }
        public DateTime? ExpiredBy { get; set; }
        public bool RememberMe { get; set; }
        public string RefreshToken { get; set; }

    }
}
