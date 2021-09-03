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

        public IActionResult Inbox()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string userRole = _userRoleService.FindRole(userId);
            if (userRole == "Admin")
            {
                ViewBag.Role = "Admin";
            }else if (userRole == "Store Manager") { ViewBag.Role = "Store Manager"; } 
            else if (userRole == "Sales Manager") { ViewBag.Role = "Sales Manager"; }
            else if (userRole == "Farm Manager") { ViewBag.Role = "Farm Manager"; }

            var messages = _messageService.GetMessages(userId);
            ViewBag.MssCount = messages.Count();
            List<ListMessageVM> ListMessage = new List<ListMessageVM>();
            foreach (var message in messages)
            {
                var Created = _userService.FindByEmail(message.CreatedBy);

                ListMessageVM listMessageVM = new ListMessageVM
                {
                    Id = message.Id,
                    Title = message.Title,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]} ({_userRoleService.FindRole(Created.Id)})",
                    CreatedAt = message.CreatedAt,
                };

                ListMessage.Add(listMessageVM);
            }


            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListMessage);
        }



        public IActionResult Outbox()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string userRole = _userRoleService.FindRole(userId);
            if (userRole == "Admin")
            {
                ViewBag.Role = "Admin";
            }
            else if (userRole == "Store Manager") { ViewBag.Role = "Store Manager"; }
            else if (userRole == "Sales Manager") { ViewBag.Role = "Sales Manager"; }
            else if (userRole == "Farm Manager") { ViewBag.Role = "Farm Manager"; }

            var messages = _messageService.GetOutbox(_userService.FindById(userId).Email);
            ViewBag.MssCount = messages.Count();
            List<ListMessageVM> ListMessage = new List<ListMessageVM>();
            foreach (var message in messages)
            {
                var receiver = _userService.FindById(message.RecieverId);

                ListMessageVM listMessageVM = new ListMessageVM
                {
                    Id = message.Id,
                    Title = message.Title,
                    RecievedBy = $"{receiver.LastName} {receiver.FirstName} ({_userRoleService.FindRole(receiver.Id)})",
                    CreatedAt = message.CreatedAt,
                };

                ListMessage.Add(listMessageVM);
            }


            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListMessage);
        }



        public IActionResult AddMessage()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            AddMessageVM messageVM = new AddMessageVM
            {
                RecieverList = _userService.GetAllUser(userId).Select(m => new SelectListItem
                {
                    Text = $"{m.LastName} {m.FirstName} ({_userRoleService.FindRole(m.Id)})",
                    Value = m.Id.ToString()
                }),

                Role = _userRoleService.FindRole(userId),
            };



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
            return RedirectToAction("Outbox");
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
                    RecieverId = message.RecieverId,
                    RecieverList = _userService.GetAllUser(userId).Select(m => new SelectListItem
                    {
                        Text = $"{m.LastName} {m.FirstName} ({_userRoleService.FindRole(m.Id)})",
                        Value = m.Id.ToString(),
                    }),
                };

                return View(messageFlock);
            }

        }

        [HttpPost]
        public IActionResult UpdateMessage(UpdateMessageVM updateMessage)
        {
            MessageDTO message = new MessageDTO
            {
                Id = updateMessage.Id,
                Title = updateMessage.Title,
                Content = updateMessage.Content,
                RecieverId = updateMessage.RecieverId,
            };
            _messageService.Update(message);
            return RedirectToAction("Outbox");
        }



        public IActionResult Delete(int id)
        {
            var message = _messageService.FindById(id);
            if (message == null)
            {
                return NotFound();
            }
            _messageService.Delete(id);
            return RedirectToAction("Outbox");
        }


        public IActionResult DeleteInbox(int id)
        {
            var message = _messageService.FindById(id);
            if (message == null)
            {
                return NotFound();
            }
            _messageService.Delete(id);
            return RedirectToAction("Inbox");
        }

        public IActionResult SeeMore(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string userRole = _userRoleService.FindRole(userId);
            if (userRole == "Admin")
            {
                ViewBag.Role = "Admin";
            }
            else if (userRole == "Store Manager") { ViewBag.Role = "Store Manager"; }
            else if (userRole == "Sales Manager") { ViewBag.Role = "Sales Manager"; }
            else if (userRole == "Farm Manager") { ViewBag.Role = "Farm Manager"; }

            var mss = _messageService.FindById(id);
            var created = _userService.FindByEmail(mss.CreatedBy);
            var recieved = _userService.FindById(mss.RecieverId);

            SeeMore seeMore = new SeeMore
            {
                Title = mss.Title,
                Content = mss.Content,
                CreatedAt = mss.CreatedAt,
                CreatedBy = $"{created.LastName} {created.FirstName} ({_userRoleService.FindRole(created.Id)})",
                RecievedBy = $"{recieved.LastName} {recieved.FirstName} ({_userRoleService.FindRole(recieved.Id)})",
            };

            return View(seeMore);
        }
    }
}
