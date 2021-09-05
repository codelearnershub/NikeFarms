﻿using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly NikeDbContext _dbContext;

        public MessageRepository(NikeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Message Add(Message message)
        {
            _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
            return message;
        }

        public void Delete(int messageId)
        {
            var message = FindById(messageId);

            if (message != null)
            {
                _dbContext.Messages.Remove(message);
                _dbContext.SaveChanges();
            }
        }

        public Message FindById(int messageId)
        {
            return _dbContext.Messages.FirstOrDefault(u => u.Id.Equals(messageId));
        }

        public List<Message> GetOutbox(string senderEmail)
        {
            return _dbContext.Messages.Where(m=> m.CreatedBy == senderEmail).OrderByDescending(r => r.CreatedAt).ToList();
        }

        public List<Message> GetMessages(int recieverId)
        {
            return _dbContext.Messages.Where(m => m.RecieverId == recieverId).OrderByDescending(r => r.CreatedAt).ToList();
        }

        public Message Update(Message message)
        {
            _dbContext.Messages.Update(message);
            _dbContext.SaveChanges();
            return message;
        }
    }
}
