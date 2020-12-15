using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YourChoice.Domain.Auth;
namespace YourChoice.Domain
{

    public class Subscription
    {
        public int? WhoId { get; set; }
        public virtual User Who { get; set; }

        public int? ToWhomId { get; set; }
        public virtual User ToWhom { get; set; }

        public bool Value { get; set; }
    }
}
