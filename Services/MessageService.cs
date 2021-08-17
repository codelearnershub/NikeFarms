using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;

        public MessageService(IMessageRepository messageRepository, IUserService userService)
        {
            _messageRepository = messageRepository;
            _userService = userService;
        }

        public Message Add(int userId, string title, string content, int recieverId)
        {
            var message = new Message
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                Title = title.ToUpper(),
                Content = content,
                RecieverId = recieverId,
            };

            return _messageRepository.Add(message);
        }

        public Message FindById(int id)
        {
            return _messageRepository.FindById(id);
        }

        public Message Update(int messageId, string title, string content, int recieverId)
        {
            var message = _messageRepository.FindById(messageId);
            if (message == null)
            {
                return null;
            }

            message.Title = title.ToUpper();
            message.Content = content;
            message.RecieverId = recieverId;
            message.UpdatedAt = DateTime.Now;

            return _messageRepository.Update(message);
        }

        public void Delete(int id)
        {
            _messageRepository.Delete(id);
        }
    }
}
