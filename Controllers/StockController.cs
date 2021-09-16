using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize(Roles = "Store Manager, Admin, Super Admin, Sales Manager")]
    public class StockController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStockService _stockService;
        private readonly IFlockService _flockService;
        private readonly IUserRoleService _userRoleService;
        private readonly IFlockTypeService _flockTypeService;

        public StockController(IUserService userService, IStockService stockService, IFlockService flockService, IFlockTypeService flockTypeService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _stockService = stockService;
            _flockService = flockService;
            _flockTypeService = flockTypeService;
            _userRoleService = userRoleService;
        }

       
        public IActionResult Index(string sortOrder, string searchString, int? pageNumber)
        {
            var stocks = _stockService.GetAllStocks();
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                stocks = stocks.Where(s => s.ItemType.Contains(searchString));
            }
            List<ListStockVM> ListStocks = new List<ListStockVM>();
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.Role = $"{_userRoleService.FindRole(userId)}";
            foreach (var stock in stocks)
            {
                var Created = _userService.FindByEmail(stock.CreatedBy);

                ListStockVM listStockVM = new ListStockVM
                {

                    Id = stock.Id,
                    Item = stock.ItemType,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerBird = stock.PricePerKg,
                    FlockBatchNo = $"{_flockTypeService.FindById(_flockService.FindById(stock.FlockId).FlockTypeId).Name} ({_flockService.FindById(stock.FlockId).BatchNo})",
                    AvailableItem = stock.AvailableItem,
                    EstimatedPricePerBird = _flockService.EstimatedPriceOfFlockPerBird(stock.FlockId),
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                    CreatedAt = stock.CreatedAt.ToShortDateString(),
                    UpdatedAt = stock.UpdatedAt,
                };

                ListStocks.Add(listStockVM);
            }

            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";
            int pageSize = 5;
            return View(PaginatedList<ListStockVM>.CreateAsync(ListStocks.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult AddStock()
        {
            AddStockVM stockVM = new AddStockVM
            {
                FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                {
                    Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                    Value = m.Id.ToString()
                })
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(stockVM);
        }

        [HttpPost]
        public IActionResult AddStock(AddStockVM addStock)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //ViewBag.EstimatedPrice = _stockService.EstimatedPriceOfFlockPerKg(addStock.FlockId);
            StockDTO stockDTO = new StockDTO
            {
                UserId = userId,
                NoOfItem = addStock.NoOfItem,
                ItemType = addStock.ItemType,
                PricePerCrate = addStock.PricePerCrate,
                PricePerKg = addStock.PricePerBird,
                FlockId = addStock.FlockId,
                AvailableItem = addStock.NoOfItem,
            };

            var stockList = _stockService.GetStocksByFlockId(addStock.FlockId);
            var flock = _flockService.FindById(addStock.FlockId);
            double noOfStock = 0;
            bool isGreaterThanFlock = false;
            if (stockList != null)
            {
                foreach (var stock in stockList)
                {
                    noOfStock += stock.NoOfItem;
                };

                if (flock.AvailableBirds - noOfStock < addStock.NoOfItem && addStock.ItemType == "Birds")
                {
                    isGreaterThanFlock = true;
                }
            };

             if (flock.AvailableBirds < addStock.NoOfItem && addStock.ItemType == "Birds")
            {
                ViewBag.Message = "errorBirds";
                AddStockVM stockVM = new AddStockVM
                {
                    FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                        Value = m.Id.ToString()
                    })
                };
                return View(stockVM);
            }
            else if (isGreaterThanFlock == true && addStock.ItemType == "Birds")
            {
                ViewBag.Message = "errorStock";
                ViewBag.ErrorMss = $"{noOfStock} Birds of Flock with Batch No: {flock.BatchNo} has already been Stocked, the highest you can stock now is {flock.AvailableBirds - noOfStock}";
                AddStockVM stockVM = new AddStockVM
                {
                    FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                        Value = m.Id.ToString()
                    })
                };
                return View(stockVM);
            }
            else if (addStock.ItemType == "Eggs")
            {
                if (addStock.PricePerCrate == null || addStock.PricePerBird != null)
                {
                    ViewBag.Message = "errorCrate";
                    AddStockVM stockVM = new AddStockVM
                    {
                        FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                        {
                            Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                            Value = m.Id.ToString()
                        })
                    };
                    return View(stockVM);
                };
            }
            else if (addStock.ItemType == "Birds")
            {
                if (addStock.PricePerBird == null || addStock.PricePerCrate != null)
                {
                    ViewBag.Message = "errorKg";
                    AddStockVM stockVM = new AddStockVM
                    {
                        FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                        {
                            Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                            Value = m.Id.ToString()
                        })
                    };
                    return View(stockVM);
                };
            }
            _stockService.Add(stockDTO);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult UpdateStock(int id)
        {
            var stock = _stockService.FindById(id);
            if (stock == null)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateStockVM updateStock = new UpdateStockVM
                {
                    Id = stock.Id,
                    NoOfItem = stock.NoOfItem,
                    ItemType = stock.ItemType,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerBird = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                        Value = m.Id.ToString(),
                    }),
                };

                return View(updateStock);
            }

        }

        [HttpPost]
        public IActionResult UpdateStock(UpdateStockVM updateStock)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            StockDTO stock = new StockDTO
            {
                Id = updateStock.Id,
                ItemType = updateStock.ItemType,
                UserId = userId,
                PricePerCrate = updateStock.PricePerCrate,
                PricePerKg = updateStock.PricePerBird,
                FlockId = updateStock.FlockId,
            };

            var stockList = _stockService.GetStocksByFlockId(updateStock.FlockId);
            var flock = _flockService.FindById(updateStock.FlockId);
            double noOfStock = 0;
            bool isGreaterThanFlock = false;
            if (stockList != null)
            {
                foreach (var stockL in stockList)
                {
                    noOfStock += stockL.NoOfItem;
                };

                if (flock.AvailableBirds - noOfStock < updateStock.NoOfItem && updateStock.ItemType == "Birds")
                {
                    isGreaterThanFlock = true;
                }
            };


            if (isGreaterThanFlock == true && updateStock.ItemType == "Birds")
            {
                ViewBag.Message = "errorStock";
                ViewBag.ErrorMss = $"{noOfStock} Birds of Flock with Batch No: {flock.BatchNo} has already been Stocked, the highest you can stock now is {flock.AvailableBirds - noOfStock}";
                UpdateStockVM updateStockD = new UpdateStockVM
                {
                    Id = stock.Id,
                    ItemType = stock.ItemType,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerBird = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, {m.AvailableBirds}(Birds)",
                        Value = m.Id.ToString(),
                    }),
                };
                return View(updateStockD);
            }

            else if (flock.AvailableBirds < updateStock.NoOfItem && updateStock.ItemType == "Birds")
            {
                ViewBag.Message = "errorBirds";
                UpdateStockVM updateStockD = new UpdateStockVM
                {
                    Id = stock.Id,
                    ItemType = stock.ItemType,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerBird = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                        Value = m.Id.ToString(),
                    }),
                };
                return View(updateStockD);
            }
            else if (updateStock.ItemType == "Eggs")
            {
                if (updateStock.PricePerCrate == null || updateStock.PricePerBird != null)
                {
                    ViewBag.Message = "errorCrate";
                    UpdateStockVM updateStockD = new UpdateStockVM
                    {
                        Id = stock.Id,
                        ItemType = stock.ItemType,
                        NoOfItem = stock.NoOfItem,
                        PricePerCrate = stock.PricePerCrate,
                        PricePerBird = stock.PricePerKg,
                        FlockId = stock.FlockId,
                        FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                        {
                            Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                            Value = m.Id.ToString(),
                        }),
                    };
                    return View(updateStockD);
                };

            }
            else if (updateStock.ItemType == "Birds")
            {
                if (updateStock.PricePerBird == null || updateStock.PricePerCrate != null)
                {
                    ViewBag.Message = "errorKg";
                    UpdateStockVM updateStockD = new UpdateStockVM
                    {
                        Id = stock.Id,
                        ItemType = stock.ItemType,
                        NoOfItem = stock.NoOfItem,
                        PricePerCrate = stock.PricePerCrate,
                        PricePerBird = stock.PricePerKg,
                        FlockId = stock.FlockId,
                        FlockList = _flockService.GetApprovedFlocks().Select(m => new SelectListItem
                        {
                            Text = $"{m.FlockType.Name} Batch No: {m.BatchNo}, {m.AvailableBirds}(Birds)",
                            Value = m.Id.ToString(),
                        }),
                    };
                    return View(updateStockD);
                };

            }

            _stockService.Update(stock);
            return RedirectToAction("Index");
        }


        //[Authorize(Roles = "Super Admin, Store Manager")]
        //public IActionResult Delete(int id)
        //{
        //    var stock = _stockService.FindById(id);
        //    if (stock == null)
        //    {
        //        return NotFound();
        //    }
        //    _stockService.Delete(id);
        //    return RedirectToAction("Index");
        //}
    }
}
