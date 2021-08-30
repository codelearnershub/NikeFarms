using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IMessageRepository
    {
        public Message Add(Message message);

        public Message FindById(int messageId);

        public Message Update(Message message);

        public List<Message> GetOutbox(string senderEmail);

        public List<Message> GetMessages(int recieverId);

        public void Delete(int messageId);
    }
}
