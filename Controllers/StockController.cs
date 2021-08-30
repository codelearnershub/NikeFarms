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
    public class StockController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStockService _stockService;
        private readonly IFlockService _flockService;
        private readonly IFlockTypeService _flockTypeService;

        public StockController(IUserService userService, IStockService stockService, IFlockService flockService, IFlockTypeService flockTypeService)
        {
            _userService = userService;
            _stockService = stockService;
            _flockService = flockService;
            _flockTypeService = flockTypeService;
        }

        public IActionResult Index()
        {
            var stocks = _stockService.GetAllStocks();
            List<ListStockVM> ListStocks = new List<ListStockVM>();

            foreach (var stock in stocks)
            {
                var Created = _userService.FindByEmail(stock.CreatedBy);

                ListStockVM listStockVM = new ListStockVM
                {

                    Id = stock.Id,
                    Item = stock.ItemType,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerKg = stock.PricePerKg,
                    FlockBatchNo = _flockService.FindById(stock.FlockId).BatchNo,
                    AvailableItem = stock.NoOfItem,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                    CreatedAt = stock.CreatedAt.ToShortDateString(),
                };

                ListStocks.Add(listStockVM);
            }


            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListStocks);
        }

        public IActionResult AddStock()
        {
            AddStockVM stockVM = new AddStockVM
            {
                FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
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
                PricePerKg = addStock.PricePerKg,
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

                if (flock.AvailableBirds - noOfStock <= 0)
                {
                    isGreaterThanFlock = true;
                }
            };


            if (isGreaterThanFlock == true && addStock.ItemType == "Birds")
            {
                ViewBag.Message = "errorStock";
                ViewBag.ErrorMss = $"{noOfStock} Birds of Flock with Batch No: {flock.BatchNo} has already been Stocked, the highest you can stock now is {flock.AvailableBirds - noOfStock}";
                AddStockVM stockVM = new AddStockVM
                {
                    FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                        Value = m.Id.ToString()
                    })
                };
                return View(stockVM);
            }
            else if (flock.AvailableBirds < addStock.NoOfItem)
            {
                ViewBag.Message = "errorBirds";
                AddStockVM stockVM = new AddStockVM
                {
                    FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                        Value = m.Id.ToString()
                    })
                };
                return View(stockVM);
            }
            else if (addStock.ItemType == "Eggs")
            {
                if (addStock.PricePerCrate == null || addStock.PricePerKg != null)
                {
                    ViewBag.Message = "errorCrate";
                    AddStockVM stockVM = new AddStockVM
                    {
                        FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                            Value = m.Id.ToString()
                        })
                    };
                    return View(stockVM);
                };
            }
            else if (addStock.ItemType == "Birds")
            {
                if (addStock.PricePerKg == null || addStock.PricePerCrate != null)
                {
                    ViewBag.Message = "errorKg";
                    AddStockVM stockVM = new AddStockVM
                    {
                        FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                            Value = m.Id.ToString()
                        })
                    };
                    return View(stockVM);
                };
            }
            _stockService.Add(stockDTO);
            return RedirectToAction("Index");
        }


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
                    PricePerKg = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
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
                NoOfItem = updateStock.NoOfItem,
                PricePerCrate = updateStock.PricePerCrate,
                PricePerKg = updateStock.PricePerKg,
                FlockId = updateStock.FlockId,
                AvailableItem = updateStock.NoOfItem,
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

                if (flock.AvailableBirds - noOfStock <= 0)
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
                    PricePerKg = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, {m.AvailableBirds}(Birds)",
                        Value = m.Id.ToString(),
                    }),
                };
                return View(updateStockD);
            }

            else if (flock.AvailableBirds < updateStock.NoOfItem)
            {
                ViewBag.Message = "errorBirds";
                UpdateStockVM updateStockD = new UpdateStockVM
                {
                    Id = stock.Id,
                    ItemType = stock.ItemType,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerKg = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                        Value = m.Id.ToString(),
                    }),
                };
                return View(updateStockD);
            }
            else if (updateStock.ItemType == "Eggs")
            {
                if (updateStock.PricePerCrate == null || updateStock.PricePerKg != null)
                {
                    ViewBag.Message = "errorCrate";
                    UpdateStockVM updateStockD = new UpdateStockVM
                    {
                        Id = stock.Id,
                        ItemType = stock.ItemType,
                        NoOfItem = stock.NoOfItem,
                        PricePerCrate = stock.PricePerCrate,
                        PricePerKg = stock.PricePerKg,
                        FlockId = stock.FlockId,
                        FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, Available Birds: {m.AvailableBirds}",
                            Value = m.Id.ToString(),
                        }),
                    };
                    return View(updateStockD);
                };

            }
            else if (updateStock.ItemType == "Birds")
            {
                if (updateStock.PricePerKg == null || updateStock.PricePerCrate != null)
                {
                    ViewBag.Message = "errorKg";
                    UpdateStockVM updateStockD = new UpdateStockVM
                    {
                        Id = stock.Id,
                        ItemType = stock.ItemType,
                        NoOfItem = stock.NoOfItem,
                        PricePerCrate = stock.PricePerCrate,
                        PricePerKg = stock.PricePerKg,
                        FlockId = stock.FlockId,
                        FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}, {m.AvailableBirds}(Birds)",
                            Value = m.Id.ToString(),
                        }),
                    };
                    return View(updateStockD);
                };

            }

            _stockService.Update(stock);
            return RedirectToAction("Index");
        }



        public IActionResult Delete(int id)
        {
            var stock = _stockService.FindById(id);
            if (stock == null)
            {
                return NotFound();
            }
            _stockService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
