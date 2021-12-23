using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Utility.Db.Interface;

namespace Utility.Db {
    public abstract class BaseModel : IBaseModel {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public void OnCreate() {
            if(Id.ToString() == "00000000-0000-0000-0000-000000000000") {
                Id = Guid.NewGuid();
            }            
            CreatedAt = DateTime.UtcNow;
        }

        public void OnUpdate() {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
