using System;
using System.Collections.Generic;
using System.Text;

namespace YourChoice.Domain
{
    public class Message : BaseEntity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
