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
    public class MortalityController : Controller
    {

        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;
        private readonly IMortalityService _mortalityService;
        private readonly IStockService _stockService;
        private readonly IFlockService _flockService;
        private readonly IFlockTypeService _flockTypeService;

        public MortalityController(IUserService userService, IUserRoleService userRoleService, IRoleService roleService, IMortalityService mortalityService, IFlockService flockService, IStockService stockService, IFlockTypeService flockTypeService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
            _mortalityService = mortalityService;
            _flockService = flockService;
            _stockService = stockService;
            _flockTypeService = flockTypeService;
        }

        public IActionResult Index()
        {
            var mortalities = _mortalityService.GetAllMortality();
            List<ListMortalityVM> ListMortality = new List<ListMortalityVM>();

            foreach (var mortality in mortalities)
            {
                var Created = _userService.FindByEmail(mortality.CreatedBy);
                Flock flock = null;
                if (mortality.StockId == null)
                {
                   flock = _flockService.FindById((int)mortality.FlockId);
                }
                else
                {
                    flock = _flockService.FindById(_stockService.FindById((int)mortality.StockId).FlockId);
                }
                 

                ListMortalityVM listMortalityVM = new ListMortalityVM
                {

                    Id = mortality.Id,
                    NoOfDeaths = mortality.NoOfDeaths,
                    FlockDescription = $"{_flockTypeService.FindById(flock.FlockTypeId).Name} ({flock.BatchNo})",
                    CreatedAt = mortality.CreatedAt.ToShortDateString(),
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                };

                ListMortality.Add(listMortalityVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListMortality);
        }

        public IActionResult AddMortality()
        {

            AddMortalityVM mortalityVM = new AddMortalityVM
            {
                FlockList = _flockService.OperationFlock().Select(f => new SelectListItem
                {
                    Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Not Stocked: {_flockService.FindById(f.Id).AvailableBirds - _flockService.TotalNoOfStockedBird(f.Id)} Bird(s)",
                    Value = f.Id.ToString()
                }),

                StockList = _flockService.OperationStock().Select(s => new SelectListItem
                {
                    Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} Bird(s)",
                    Value =  s.Id.ToString()
                }),
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(mortalityVM);
        }

        [HttpPost]
        public IActionResult AddMortality(AddMortalityVM addMortality)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(addMortality.StockId == null && addMortality.FlockId == null)
            {
                ViewBag.Message = "AtLeastOne";
                AddMortalityVM mortalityVM = new AddMortalityVM
                {
                    FlockList = _flockService.GetApprovedFlocks().Select(f => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Not Stocked: {_flockService.FindById(f.Id).AvailableBirds - _flockService.TotalNoOfStockedBird(f.Id)} Bird(s)",
                        Value = f.Id.ToString()
                    }),

                    StockList = _stockService.GetBirdStocks().Select(s => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} Bird(s)",
                        Value = s.Id.ToString()
                    }),
                };
                return View(mortalityVM);
            }
            else if(addMortality.StockId != null && addMortality.FlockId != null)
            {
                ViewBag.Message = "AtMostOne";
                AddMortalityVM mortalityVM = new AddMortalityVM
                {
                    FlockList = _flockService.GetApprovedFlocks().Select(f => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Not Stocked: {_flockService.FindById(f.Id).AvailableBirds - _flockService.TotalNoOfStockedBird(f.Id)} Bird(s)",
                    }),

                    StockList = _stockService.GetBirdStocks().Select(s => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} Bird(s)",
                        Value = s.Id.ToString()
                    }),
                };
                return View(mortalityVM);
            };

            if(addMortality.StockId != null)
            {
                var stock = _stockService.FindById((int)addMortality.StockId);
                if(stock.AvailableItem < addMortality.NoOfDeaths)
                {
                    ViewBag.Message = "errorStock";
                    AddMortalityVM mortalityVM = new AddMortalityVM
                    {
                        FlockList = _flockService.GetApprovedFlocks().Select(f => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Not Stocked: {_flockService.FindById(f.Id).AvailableBirds - _flockService.TotalNoOfStockedBird(f.Id)} Bird(s)",
                            Value = f.Id.ToString()
                        }),

                        StockList = _stockService.GetBirdStocks().Select(s => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} Bird(s)",
                            Value = s.Id.ToString()
                        }),
                    };
                    return View(mortalityVM);
                }
                StockDTO stockD = new StockDTO
                {
                    Id = stock.Id,
                    ItemType = stock.ItemType,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerKg = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    AvailableItem = stock.AvailableItem - addMortality.NoOfDeaths,
                };
                _stockService.UpdateMore(stockD);

                var flock = _flockService.FindById(stock.FlockId);

                FlockDTO flockD = new FlockDTO
                {
                    Id = flock.Id,
                    TotalNo = flock.TotalNo,
                    AverageWeight = flock.AverageWeight,
                    Age = flock.Age,
                    AvailableBird = flock.AvailableBirds - addMortality.NoOfDeaths,
                    AmountPurchased = flock.AmountPurchased,
                    FlockTypeId = flock.FlockTypeId,
                    IsApproved = true,
                };
                _flockService.UpdateFlockOnly(flockD);
                _flockService.CheckFlockFinish(stock.FlockId);
                MortalityDTO mortalityDTO = new MortalityDTO
                {
                    UserId = userId,
                    StockId = addMortality.StockId,
                    NoOfDeaths = addMortality.NoOfDeaths,
                    FlockId = _flockService.FindById(_stockService.FindById((int)addMortality.StockId).FlockId).Id,
                   
                };

                _mortalityService.Add(mortalityDTO);
            }
            else if(addMortality.FlockId != null)
            {
                var flock = _flockService.FindById((int)addMortality.FlockId);
                if (flock.AvailableBirds - _flockService.TotalNoOfStockedBird(flock.Id) < addMortality.NoOfDeaths)
                {
                    ViewBag.Message = "errorFlock";
                    AddMortalityVM mortalityVM = new AddMortalityVM
                    {
                        FlockList = _flockService.GetApprovedFlocks().Select(f => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Not Stocked: {_flockService.FindById(f.Id).AvailableBirds - _flockService.TotalNoOfStockedBird(f.Id)} Bird(s)",
                            Value = f.Id.ToString()
                        }),

                        StockList = _stockService.GetBirdStocks().Select(s => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} Bird(s)",
                            Value = s.Id.ToString()
                        }),
                    };
                    return View(mortalityVM);
                }


                FlockDTO flockD = new FlockDTO
                {
                    Id = flock.Id,
                    TotalNo = flock.TotalNo,
                    AverageWeight = flock.AverageWeight,
                    Age = flock.Age,
                    AvailableBird = flock.AvailableBirds - addMortality.NoOfDeaths,
                    AmountPurchased = flock.AmountPurchased,
                    FlockTypeId = flock.FlockTypeId,
                    IsApproved = true,
                };
                _flockService.UpdateFlockOnly(flockD);
                

                MortalityDTO mortalityDTO = new MortalityDTO
                {
                    UserId = userId,
                    FlockId = addMortality.FlockId,
                    NoOfDeaths = addMortality.NoOfDeaths,
                };
                _mortalityService.Add(mortalityDTO);
                _flockService.CheckFlockFinish(flock.Id);
            }
           
            return RedirectToAction("Index");
        }


        //public IActionResult UpdateMortality(int id)
        //{
        //    var mortality = _mortalityService.FindById(id);
        //    if (mortality == null && mortality.CreatedAt.ToShortDateString() != DateTime.Now.ToShortDateString())
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        //        User userlogin = _userService.FindById(userId);
        //        ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

        //        UpdateMortalityVM updateMortality = new UpdateMortalityVM
        //        {
        //            Id = mortality.Id,
        //            NoOfDeaths = mortality.NoOfDeaths,
        //            StockId = mortality.StockId,
        //            FlockId = mortality.FlockId,
        //            FlockList = _flockService.GetApprovedFlocks().Select(f => new SelectListItem
        //            {
        //                Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Not Stocked: {_flockService.FindById(f.Id).AvailableBirds - _flockService.TotalNoOfStockedBird(f.Id)} Bird(s)",
        //                Value = f.Id.ToString()
        //            }),

        //            StockList = _stockService.GetBirdStocks().Select(s => new SelectListItem
        //            {
        //                Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} Bird(s)",
        //                Value = s.Id.ToString()
        //            }),
        //        };

        //        return View(updateMortality);
        //    }

        //}

        //[HttpPost]
        //public IActionResult UpdateMortality(UpdateMortalityVM updateMortality)
        //{
        //    int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    var user = _userService.FindById(userId);

        //    MortalityDTO mortality = new MortalityDTO
        //    {
        //        UserId = userId,
        //        Id = updateMortality.Id,
        //        StockId = updateMortality.StockId,
        //        FlockId = updateMortality.FlockId,
        //        NoOfDeaths = updateMortality.NoOfDeaths,
        //    };

        //    if (updateMortality.StockId == null && updateMortality.FlockId == null)
        //    {
        //        ViewBag.Message = "AtLeastOne";
        //        UpdateMortalityVM updateMortalityVM = new UpdateMortalityVM
        //        {
        //            Id = mortality.Id,
        //            NoOfDeaths = mortality.NoOfDeaths,
        //            StockId = mortality.StockId,
        //            FlockId = mortality.FlockId,
        //            FlockList = _flockService.GetApprovedFlocks().Select(f => new SelectListItem
        //            {
        //                Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Not Stocked: {_flockService.FindById(f.Id).AvailableBirds - _flockService.TotalNoOfStockedBird(f.Id)} Bird(s)",
        //                Value = f.Id.ToString()
        //            }),

        //            StockList = _stockService.GetBirdStocks().Select(s => new SelectListItem
        //            {
        //                Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} Bird(s)",
        //                Value = s.Id.ToString()
        //            }),
        //        };

        //        return View(updateMortalityVM);
        //    }
        //    else if (updateMortality.StockId != null && updateMortality.FlockId != null)
        //    {
        //        ViewBag.Message = "AtMostOne";
        //        UpdateMortalityVM updateMortalityVM = new UpdateMortalityVM
        //        {
        //            Id = mortality.Id,
        //            NoOfDeaths = mortality.NoOfDeaths,
        //            StockId = mortality.StockId,
        //            FlockId = mortality.FlockId,
        //            FlockList = _flockService.GetApprovedFlocks().Select(f => new SelectListItem
        //            {
        //                Text = $"{_flockTypeService.FindById(f.FlockTypeId).Name} Batch No: ({_flockService.FindById(f.Id).BatchNo}) Not Stocked: {_flockService.FindById(f.Id).AvailableBirds - _flockService.TotalNoOfStockedBird(f.Id)} Bird(s)",
        //                Value = f.Id.ToString()
        //            }),

        //            StockList = _stockService.GetBirdStocks().Select(s => new SelectListItem
        //            {
        //                Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} Bird(s)",
        //                Value = s.Id.ToString()
        //            }),
        //        };

        //        return View(updateMortalityVM);
        //    };

        //    if (updateMortality.StockId != null)
        //    {
        //        MortalityDTO mortalityD = new MortalityDTO
        //        {
        //            UserId = userId,
        //            Id = updateMortality.Id,
        //            StockId = updateMortality.StockId,
        //            FlockId = _flockService.FindById(_stockService.FindById((int)updateMortality.StockId).FlockId).Id,
        //            NoOfDeaths = updateMortality.NoOfDeaths,
        //        };
        //        _mortalityService.Update(mortality);
        //    }
        //    else if (updateMortality.FlockId != null)
        //    {
        //        MortalityDTO mortalityD = new MortalityDTO
        //        {
        //            UserId = userId,
        //            Id = updateMortality.Id,
        //            FlockId = updateMortality.FlockId,
        //            NoOfDeaths = updateMortality.NoOfDeaths,
        //        };
        //        _mortalityService.Update(mortality);
        //    }

        //    return RedirectToAction("Index");
        //}


        public IActionResult Delete(int id)
        {
            var mortality = _mortalityService.FindById(id);
            if (mortality == null && mortality.CreatedAt.ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                return NotFound();
            };

            if (mortality.StockId != null)
            {
                var stock = _stockService.FindById((int)mortality.StockId);
                StockDTO stockD = new StockDTO
                {
                    Id = stock.Id,
                    ItemType = stock.ItemType,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerKg = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    AvailableItem = stock.AvailableItem + mortality.NoOfDeaths,
                };
                _stockService.UpdateMore(stockD);

            }
            if (mortality.FlockId != null)
            {
                var flock = _flockService.FindById((int)mortality.FlockId);

                FlockDTO flockD = new FlockDTO
                {
                    Id = flock.Id,
                    TotalNo = flock.TotalNo,
                    AverageWeight = flock.AverageWeight,
                    Age = flock.Age,
                    AvailableBird = flock.AvailableBirds + mortality.NoOfDeaths,
                    AmountPurchased = flock.AmountPurchased,
                    FlockTypeId = flock.FlockTypeId,
                    IsApproved = true,
                };
                _flockService.UpdateFlockOnly(flockD);
            }
            

            _mortalityService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
