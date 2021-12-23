using System;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Db;

namespace Model.Users
{
    [Table(nameof(Transaction), Schema = "user")]
    public class Transaction : BaseModel
    {
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public string TransactionId { get; set; }
        public string TransactionStatus { get; set; }
    }
}
