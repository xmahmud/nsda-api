using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Utility.Db.Interface;

namespace Utility.Db {
    public class IncrementalBaseModel : IBaseModel {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public void OnCreate() {
            CreatedAt = DateTime.Now;
        }

        public void OnUpdate() {
            UpdatedAt = DateTime.Now;
        }
    }
}
