using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IMessageService
    {
        public Message Add(int userId, string title, string content, int recieverId);

        public Message FindById(int id);

        public Message Update(int messageId, string title, string content, int recieverId);

        public void Delete(int id);
    }
}
