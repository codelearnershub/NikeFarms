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
                ListStockVM listStockVM = new ListStockVM
                {

                    Id = stock.Id,
                    Item = stock.Item,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerKg = stock.PricePerKg,
                    FlockBatchNo = _flockService.FindById(stock.FlockId).BatchNo,
                    AvailableItem = stock.NoOfItem,
                    CreatedBy = $"{_userService.FindByEmail(stock.CreatedBy).FirstName} .{_userService.FindByEmail(stock.CreatedBy).LastName[0]}",
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
                    Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}",
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

            StockDTO stockDTO = new StockDTO
            {
                UserId = userId,
                Item = addStock.Item,
                NoOfItem = addStock.NoOfItem,
                PricePerCrate = addStock.PricePerCrate,
                PricePerKg = addStock.PricePerKg,
                FlockId = addStock.FlockId,
                AvailableItem = addStock.NoOfItem,
            };

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
                    Item = stock.Item,
                    NoOfItem = stock.NoOfItem,
                    PricePerCrate = stock.PricePerCrate,
                    PricePerKg = stock.PricePerKg,
                    FlockId = stock.FlockId,
                    FlockList = _flockService.GetAllFlocks().Select(m => new SelectListItem
                    {
                        Text = $"{_flockTypeService.FindById(m.FlockTypeId).Name} Batch No: {m.BatchNo}",
                        Value = m.Id.ToString(),
                    }),
                };

                return View(updateStock);
            }

        }

        [HttpPost]
        public IActionResult UpdateStock(UpdateStockVM updateStock)
        {
            StockDTO stock = new StockDTO
            {
                Id = updateStock.Id,
                Item = updateStock.Item,
                NoOfItem = updateStock.NoOfItem,
                PricePerCrate = updateStock.PricePerCrate,
                PricePerKg = updateStock.PricePerKg,
                AvailableItem = updateStock.NoOfItem,
                FlockId = updateStock.FlockId,
            };
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
