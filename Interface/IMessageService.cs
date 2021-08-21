﻿using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IMessageService
    {
        public Message Add(MessageDTO messageDTO);

        public Message FindById(int id);

        public Message Update(MessageDTO messageDTO);

        public IEnumerable<Message> GetMessages(string senderEmail);

        public void Delete(int id);
    }
}
