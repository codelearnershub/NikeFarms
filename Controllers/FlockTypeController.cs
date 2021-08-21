using Microsoft.AspNetCore.Mvc;
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
    public class FlockTypeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFlockTypeService _flockTypeService;

        public FlockTypeController(IUserService userService, IFlockTypeService flockTypeService)
        {
            _userService = userService;
            _flockTypeService = flockTypeService;
        }

        public IActionResult Index()
        {
            var flockTypes = _flockTypeService.GetAllFlockTypes();
            List<ListFlockTypeVM> ListFlockType = new List<ListFlockTypeVM>();

            foreach (var flockType in flockTypes)
            {
                ListFlockTypeVM listFlockTypeVM = new ListFlockTypeVM
                {
                    Id = flockType.Id,
                    Name = flockType.Name,
                    Description = flockType.Description,
                    CreatedBy = $"{_userService.FindByEmail(flockType.CreatedBy).FirstName} .{_userService.FindByEmail(flockType.CreatedBy).LastName[0]}",
                };

                ListFlockType.Add(listFlockTypeVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListFlockType);
        }

        public IActionResult AddFlockType()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        [HttpPost]
        public IActionResult AddFlockType(AddFlockTypeVM addFlockType)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            FlockTypeDTO flockTypeDTO = new FlockTypeDTO
            {
                UserId = userId,
                Name = addFlockType.Name,
                Description = addFlockType.Description,
            };

            _flockTypeService.Add(flockTypeDTO);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateFlockType(int id)
        {
            var flockType = _flockTypeService.FindById(id);
            if (flockType == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateFlockTypeVM updateFlockType = new UpdateFlockTypeVM
                {
                    Id = flockType.Id,
                    Name = flockType.Name,
                    Description = flockType.Description,
                };

                return View(updateFlockType);
            }

        }

        [HttpPost]
        public IActionResult UpdateFlockType(UpdateFlockTypeVM updateFlockType)
        {
            FlockTypeDTO flockType = new FlockTypeDTO
            {
                Id = updateFlockType.Id,
                Name = updateFlockType.Name,
                Description = updateFlockType.Description,
            };
            _flockTypeService.Update(flockType);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var flockType = _flockTypeService.FindById(id);
            if (flockType == null)
            {
                return NotFound();
            }
            _flockTypeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
