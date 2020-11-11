using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
namespace YourChoice.Domain
{
    public class Comment : BaseEntity
    {
        public virtual User User { get; set; }
        public int? UserId { get; set; }
        public virtual Post Post { get; set; }
        public int? PostId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
