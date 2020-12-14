using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Infrastructure.Models
{
    public class MainPageRequest : PagedRequest
    {
        public AdditionalPredicate additionalPredicate { get; set; } 
    }
}
