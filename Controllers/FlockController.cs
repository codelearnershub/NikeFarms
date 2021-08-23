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
    public class FlockController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFlockService _flockService;
        private readonly IFlockTypeService _flockTypeService;

        public FlockController(IUserService userService, IFlockService flockService, IFlockTypeService flockTypeService)
        {
            _userService = userService;
            _flockService = flockService;
            _flockTypeService = flockTypeService;
        }

        public IActionResult Index()
        {
            var flocks = _flockService.GetAllFlocks();
            List<ListFlockVM> ListFlock = new List<ListFlockVM>();
            foreach (var flock in flocks)
            {
                ListFlockVM listFlockVM = new ListFlockVM
                {
                    Id = flock.Id,
                    BatchNo = flock.BatchNo,
                    FlockType = _flockTypeService.FindById(flock.FlockTypeId).Name,
                    currentAge = (int)((DateTime.Now - flock.CreatedAt).TotalDays) + flock.Age,
                    TotalAvailable = flock.TotalNo,
                    CurrentAverageWeight = flock.AverageWeight,
                    CreatedBy = $"{_userService.FindByEmail(flock.CreatedBy).FirstName} .{_userService.FindByEmail(flock.CreatedBy).LastName[0]}",
                };

                ListFlock.Add(listFlockVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListFlock);
        }

        public IActionResult AddFlock()
        {
            AddFlockVM flockVM = new AddFlockVM
            {
                FlockTypeList = _flockTypeService.GetAllFlockTypes().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(flockVM);
        }

        [HttpPost]
        public IActionResult AddFlock(AddFlockVM addFlock)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            FlockDTO flockDTO = new FlockDTO
            {
                UserId = userId,
                TotalNo = addFlock.TotalNo,
                Age = addFlock.Age,
                AverageWeight = addFlock.AverageWeight,
                FlockTypeId = addFlock.FlockTypeId,
                IsApproved = false,
            };

            _flockService.Add(flockDTO);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateFlock(int id)
        {
            var flock = _flockService.FindById(id);
            if (flock == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateFlockVM updateFlock = new UpdateFlockVM
                {
                    Id = flock.Id,
                    TotalNo = flock.TotalNo,
                    AverageWeight = flock.AverageWeight,
                    Age = flock.Age,
                    FlockTypeId = flock.FlockTypeId,
                    FlockTypeList = _flockTypeService.GetAllFlockTypes().Select(m => new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.Id.ToString(),
                    }),
                };

                return View(updateFlock);
            }

        }

        [HttpPost]
        public IActionResult UpdateFlock(UpdateFlockVM updateFlock)
        {
            FlockDTO flock = new FlockDTO
            {
                Id = updateFlock.Id,
                TotalNo = updateFlock.TotalNo,
                AverageWeight = updateFlock.AverageWeight,
                Age = updateFlock.Age,
                FlockTypeId = updateFlock.FlockTypeId,
                IsApproved = false,
            };
            _flockService.Update(flock);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var flock = _flockService.FindById(id);
            if (flock == null)
            {
                return NotFound();
            }
            _flockService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
