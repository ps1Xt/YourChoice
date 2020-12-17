using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Repositories.Implementation;
using YourChoice.Domain;

namespace YourChoice.Api.Repositories.Interfaces
{
    public interface IMessageRepository : IRepository
    {
        public List<Message> GetLastMessages(int userId);

    }
}
