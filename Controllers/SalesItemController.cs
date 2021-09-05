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
    
    [Authorize(Roles = "Super Admin, Sales Manager, Admin")]
    public class SalesItemController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISalesItemService _salesItemService;
        private readonly ICustomerService _customerService;
        private readonly ISalesService _salesService;
        private readonly IStockService _stockService;
        private readonly IFlockService _flockService;
        private readonly IFlockTypeService _flockTypeService;
        private readonly IUserRoleService _userRoleService;

        public SalesItemController(IUserService userService, ISalesItemService salesItemService, ICustomerService customerService, IStockService stockService, IFlockService flockService, IFlockTypeService flockTypeService, IUserRoleService userRoleService, ISalesService salesService)
        {
            _userService = userService;
            _salesItemService = salesItemService;
            _customerService = customerService;
            _stockService = stockService;
            _flockService = flockService;
            _flockTypeService = flockTypeService;
            _userRoleService = userRoleService;
            _salesService = salesService;
        }

       
        public IActionResult Index(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.Role = $"{_userRoleService.FindRole(userId)}";

            var salesItems = _salesItemService.GetSalesItemBySalesId(id);
            List<ListSalesItemVM> ListFlock = new List<ListSalesItemVM>();
            foreach (var salesItem in salesItems)
            {
                var Created = _userService.FindByEmail(salesItem.CreatedBy);
                var stock = _stockService.FindById(salesItem.StockId);
                ListSalesItemVM listSalesItemVM = new ListSalesItemVM
                {
                    Id = salesItem.Id,
                    Item = salesItem.Item,
                    NoOfItem = salesItem.NoOfItem,
                    PricePerItem = salesItem.PricePerItem,
                    CurrentWeight = _flockService.GetCurrentAverageWeight(stock.FlockId),
                    ItemType = stock.ItemType,
                    TotalPrice = salesItem.NoOfItem * salesItem.PricePerItem,
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                };

                ListFlock.Add(listSalesItemVM);
            }

            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(ListFlock);
        }

        [Authorize(Roles = "Super Admin, Sales Manager")]
        public IActionResult AddSalesItem(int id)
        {

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            AddSalesItemVM salesItemVM = new AddSalesItemVM
            {
                StockList = _stockService.GetAllStocks().Select(s => new SelectListItem
                {
                    Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} ({s.ItemType} Weight: {_flockService.GetCurrentAverageWeight(s.FlockId)} Kg)",
                    Value = s.Id.ToString()
                }),

                SalesId = id,

            };

            return View(salesItemVM);
        }

        [HttpPost]
        public IActionResult AddSalesItem(AddSalesItemVM addSalesItem)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            SalesItemDTO SalesItemDTO = new SalesItemDTO
            {
                UserId = userId,
                Item = addSalesItem.Item,
                StockId = addSalesItem.StockId,
                NoOfItem = addSalesItem.NoOfItem,
                SalesId = addSalesItem.SalesId,
            };

            var stock = _stockService.FindById(addSalesItem.StockId);
            if (stock != null)
            {
                if (stock.AvailableItem < addSalesItem.NoOfItem)
                {
                    ViewBag.Message = "moreThan";
                    AddSalesItemVM salesItemVM = new AddSalesItemVM
                    {
                        StockList = _stockService.GetAllStocks().Select(s => new SelectListItem
                        {
                            Text = $"{_flockTypeService.FindById(_flockService.FindById(s.FlockId).FlockTypeId).Name}, Batch No: ({_flockService.FindById(s.FlockId).BatchNo}) Stock: {s.AvailableItem} ({s.ItemType} Weight: {_flockService.GetCurrentAverageWeight(s.FlockId)} Kg)",
                            Value = s.Id.ToString()
                        }),

                        SalesId = addSalesItem.SalesId,

                    };
                    return View(salesItemVM);
                };

                if (stock.ItemType == "Birds")
                {
                    StockDTO stockD = new StockDTO
                    {
                        Id = stock.Id,
                        ItemType = stock.ItemType,
                        NoOfItem = stock.NoOfItem,
                        PricePerCrate = stock.PricePerCrate,
                        PricePerKg = stock.PricePerKg,
                        FlockId = stock.FlockId,
                        AvailableItem = stock.AvailableItem - addSalesItem.NoOfItem,
                    };
                    _stockService.UpdateMore(stockD);

                    var flock = _flockService.FindById(stock.FlockId);

                    FlockDTO flockD = new FlockDTO
                    {
                        Id = flock.Id,
                        TotalNo = flock.TotalNo,
                        AverageWeight = flock.AverageWeight,
                        Age = flock.Age,
                        AvailableBird = flock.AvailableBirds - addSalesItem.NoOfItem,
                        AmountPurchased = flock.AmountPurchased,
                        FlockTypeId = flock.FlockTypeId,
                        IsApproved = true,
                    };
                    _flockService.UpdateFlockOnly(flockD);
                    _flockService.CheckFlockFinish(stock.FlockId);


                }
                else if (stock.ItemType == "Eggs")
                {
                    StockDTO stockD = new StockDTO
                    {
                        Id = stock.Id,
                        ItemType = stock.ItemType,
                        NoOfItem = stock.NoOfItem,
                        PricePerCrate = stock.PricePerCrate,
                        PricePerKg = stock.PricePerKg,
                        FlockId = stock.FlockId,
                        AvailableItem = stock.AvailableItem - addSalesItem.NoOfItem,
                    };
                    _stockService.UpdateMore(stockD);
                }
            }



            var sales = _salesService.FindById(addSalesItem.SalesId);
            decimal totalPrice = (decimal)sales.TotalPrice;
            decimal? pricePerItem = 0;
            if (_stockService.FindById(addSalesItem.StockId).PricePerKg == null)
            {
                pricePerItem = _stockService.FindById(addSalesItem.StockId).PricePerCrate;
            }
            else
            {
                pricePerItem = _stockService.FindById(addSalesItem.StockId).PricePerKg;
            }
            totalPrice += (addSalesItem.NoOfItem * (decimal)pricePerItem);

            SalesDTO sale = new SalesDTO
            {
                Id = sales.Id,
                Item = sales.Item,
                CustomerId = sales.CustomerId,
                TotalPrice = totalPrice,
                IsSold = false,
            };
            _salesService.UpdateMore(sale);
            _salesItemService.Add(SalesItemDTO);
            return RedirectToAction("Index", "Sales");
        }

        [Authorize(Roles = "Super Admin, Sales Manager")]
        public IActionResult Delete(int id)
        {
            var salesItem = _salesItemService.FindById(id);
            var sales = _salesService.FindById(salesItem.SalesId);

            if (salesItem == null || sales.IsSold == true)
            {
                return NotFound();
            }


            var stock = _stockService.FindById(salesItem.StockId);
            if (stock != null)
            {
                if (stock.ItemType == "Birds")
                {
                    StockDTO stockD = new StockDTO
                    {
                        Id = stock.Id,
                        ItemType = stock.ItemType,
                        NoOfItem = stock.NoOfItem,
                        PricePerCrate = stock.PricePerCrate,
                        PricePerKg = stock.PricePerKg,
                        FlockId = stock.FlockId,
                        AvailableItem = stock.AvailableItem + salesItem.NoOfItem,
                    };
                    _stockService.UpdateMore(stockD);

                    var flock = _flockService.FindById(stock.FlockId);

                    FlockDTO flockD = new FlockDTO
                    {
                        Id = flock.Id,
                        TotalNo = flock.TotalNo,
                        AverageWeight = flock.AverageWeight,
                        Age = flock.Age,
                        AvailableBird = flock.AvailableBirds + salesItem.NoOfItem,
                        AmountPurchased = flock.AmountPurchased,
                        FlockTypeId = flock.FlockTypeId,
                        IsApproved = true,
                    };
                    _flockService.UpdateFlockOnly(flockD);
                }
                else if (stock.ItemType == "Eggs")
                {
                    StockDTO stockD = new StockDTO
                    {
                        Id = stock.Id,
                        ItemType = stock.ItemType,
                        NoOfItem = stock.NoOfItem,
                        PricePerCrate = stock.PricePerCrate,
                        PricePerKg = stock.PricePerKg,
                        FlockId = stock.FlockId,
                        AvailableItem = stock.AvailableItem + salesItem.NoOfItem,
                    };
                    _stockService.UpdateMore(stockD);
                }
            }

            var salesD = _salesService.FindById(salesItem.SalesId);
            decimal totalPrice = (decimal)sales.TotalPrice;
            decimal? pricePerItem = 0;
            if (_stockService.FindById(salesItem.StockId).PricePerKg == null)
            {
                pricePerItem = _stockService.FindById(salesItem.StockId).PricePerCrate;
            }
            else
            {
                pricePerItem = _stockService.FindById(salesItem.StockId).PricePerKg;
            }
            totalPrice -= (salesItem.NoOfItem * (decimal)pricePerItem);

            SalesDTO sale = new SalesDTO
            {
                Id = salesD.Id,
                Item = salesD.Item,
                CustomerId = salesD.CustomerId,
                TotalPrice = totalPrice,
                IsSold = false,
            };
            _salesService.UpdateMore(sale);

            _salesItemService.Delete(id);
            return RedirectToAction("Index", "Sales");
        }
    }
}
