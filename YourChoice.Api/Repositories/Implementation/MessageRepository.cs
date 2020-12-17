using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Database;
using YourChoice.Api.Repositories.Interfaces;
using YourChoice.Domain;

namespace YourChoice.Api.Repositories.Implementation
{
    public class MessageRepository : Repository, IMessageRepository
    {

        public MessageRepository(DataBaseContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public List<Message> GetLastMessages(int userId)
        {
            var messages = context.Set<Message>()
                .Where(x => x.UserId == userId && x.Date.AddDays(-1) < DateTime.Now)
                .OrderByDescending(x => x.Date)
                .ToList();



            return messages;
        }
    }
}
