using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Super Admin, Store Manager")]
    public class FlockTypeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFlockTypeService _flockTypeService;

        public FlockTypeController(IUserService userService, IFlockTypeService flockTypeService)
        {
            _userService = userService;
            _flockTypeService = flockTypeService;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            var flockTypes = _flockTypeService.GetAllFlockTypes();
            ViewData["CurrentSort"] = sortOrder;

            List<ListFlockTypeVM> ListFlockType = new List<ListFlockTypeVM>();
            foreach (var flockType in flockTypes)

            {
                var Created = _userService.FindByEmail(flockType.CreatedBy);

                ListFlockTypeVM listFlockTypeVM = new ListFlockTypeVM
                {
                    Id = flockType.Id,
                    Name = flockType.Name,
                    Description = flockType.Description,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                };

                ListFlockType.Add(listFlockTypeVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";
            int pageSize = 5;

            return View(PaginatedList<ListFlockTypeVM>.CreateAsync(ListFlockType.AsQueryable(), pageNumber ?? 1, pageSize));

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

            var flockType = _flockTypeService.FindByName(addFlockType.Name);
            if(flockType != null)
            {
                ViewBag.Message = "error";
                return View(addFlockType);
            }

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

            var flockTypeC = _flockTypeService.FindByName(updateFlockType.Name);
            if (flockTypeC != null && updateFlockType.Id != flockTypeC.Id)
            {
                ViewBag.Message = "error";
                UpdateFlockTypeVM updateFlockTypeV = new UpdateFlockTypeVM
                {
                    Id = flockType.Id,
                    Name = flockType.Name,
                    Description = flockType.Description,
                };

                return View(updateFlockTypeV);
            }

            _flockTypeService.Update(flockType);
            return RedirectToAction("Index");
        }


    }
}
