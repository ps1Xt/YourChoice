using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain.Auth;
namespace YourChoice.Domain
{
    public class Message : BaseEntity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }

        public Message()
        {
            Date = DateTime.Now;
        }
    }
}
