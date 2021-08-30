using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
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

        public Message Add(MessageDTO messageDTO)
        {
            var message = new Message
            {
                CreatedBy = _userService.FindById(messageDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Title = messageDTO.Title.ToUpper(),
                Content = messageDTO.Content,
                RecieverId = messageDTO.RecieverId,
            };

            return _messageRepository.Add(message);
        }

        public Message FindById(int id)
        {
            return _messageRepository.FindById(id);
        }

        public Message Update(MessageDTO messageDTO)
        {
            var message = _messageRepository.FindById(messageDTO.Id);
            if (message == null)
            {
                return null;
            }

            message.Title = messageDTO.Title.ToUpper();
            message.Content = messageDTO.Content;
            message.RecieverId = messageDTO.RecieverId;
            message.UpdatedAt = DateTime.Now;

            return _messageRepository.Update(message);
        }

        public void Delete(int id)
        {
            _messageRepository.Delete(id);
        }

        public IEnumerable<Message> GetOutbox(string senderEmail)
        {
           return _messageRepository.GetOutbox(senderEmail);
        }

        public List<Message> GetMessages(int recieverId)
        {
            return _messageRepository.GetMessages(recieverId);
        }
    }
}
