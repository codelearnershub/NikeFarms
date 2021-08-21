using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using NikeFarms.v2._0.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Controllers
{
    public class MessageController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMessageService _messageService;

        public MessageController(IUserService userService, IMessageService flockService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _messageService = flockService;
            _userRoleService = userRoleService;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult AddMessage()
        {
            AddMessageVM messageVM = new AddMessageVM
            {
                RecieverList = _userService.GetAllUser().Select(m => new SelectListItem
                {
                    Text = $"{m.LastName} {m.FirstName} ({_userRoleService.FindRole(m.Id)})",
                    Value = m.Id.ToString()
                })
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(messageVM);
        }

        [HttpPost]
        public IActionResult AddMessage(AddMessageVM addMessage)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            MessageDTO messageDTO = new MessageDTO
            {
                UserId = userId,
                Title = addMessage.Title,
                Content = addMessage.Content,
                RecieverId = addMessage.RecieverId,
            };

            _messageService.Add(messageDTO);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateMessage(int id)
        {
            var message = _messageService.FindById(id);
            if (message == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateMessageVM messageFlock = new UpdateMessageVM
                {
                    Id = message.Id,
                    Title = message.Title,
                    Content = message.Content,
                    RecieverList = _userService.GetAllUser().Select(m => new SelectListItem
                    {
                        Text = $"{m.LastName} {m.FirstName} ({_userRoleService.FindRole(m.Id)})",
                        Value = m.Id.ToString(),
                    }),
                };

                return View(messageFlock);
            }

        }

        [HttpPost]
        public IActionResult UpdateFlock(UpdateMessageVM updateMessage)
        {
            MessageDTO message = new MessageDTO
            {
                Id = updateMessage.Id,
                Title = updateMessage.Title,
                Content = updateMessage.Content,
            };
            _messageService.Update(message);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var message = _messageService.FindById(id);
            if (message == null)
            {
                return NotFound();
            }
            _messageService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
