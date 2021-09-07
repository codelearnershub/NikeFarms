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
  
    public class SalesController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISalesService _salesService;
        private readonly ICustomerService _customerService;
        private readonly ISalesItemService _salesItemService;
        private readonly IStockService _stockService;
        private readonly IFlockService _flockService;
        private readonly IFlockTypeService _flockTypeService;

        public SalesController(IUserService userService, ISalesService salesService, ICustomerService customerService, ISalesItemService salesItemService, IStockService stockService, IFlockService flockService, IFlockTypeService flockTypeService)
        {
            _userService = userService;
            _salesService = salesService;
            _customerService = customerService;
            _salesItemService = salesItemService;
            _stockService = stockService;
            _flockService = flockService;
            _flockTypeService = flockTypeService;
        }

        [Authorize(Roles = "Super Admin, Sales Manager")]
        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            var sales = _salesService.GetUnSoldSales();
            ViewData["CurrentSort"] = sortOrder;
            List<ListSalesVM> ListSale = new List<ListSalesVM>();
            foreach (var sale in sales)
            {
                var Created = _userService.FindByEmail(sale.CreatedBy);
                var customer = _customerService.FindById(sale.CustomerId);

                ListSalesVM listSalesVM = new ListSalesVM
                {
                    Id = sale.Id,
                    Item = sale.Item,
                    TotalPrice = sale.TotalPrice,
                    CustomerFullName = $"{customer.FirstName} .{customer.LastName[0]}",
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                    Date = sale.CreatedAt.ToShortDateString(),
                    Voucher = sale.Voucher,
                };

                ListSale.Add(listSalesVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            int pageSize = 5;
            return View(PaginatedList<ListSalesVM>.CreateAsync(ListSale.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Super Admin, Admin")]
        public IActionResult ListSold(string sortOrder, int? pageNumber)
        {
            var sales = _salesService.GetSoldSales();
            ViewData["CurrentSort"] = sortOrder;
            List<ListSalesVM> ListSale = new List<ListSalesVM>();
            foreach (var sale in sales)
            {
                var Created = _userService.FindByEmail(sale.CreatedBy);
                var customer = _customerService.FindById(sale.CustomerId);

                ListSalesVM listSalesVM = new ListSalesVM
                {
                    Id = sale.Id,
                    Item = sale.Item,
                    TotalPrice = sale.TotalPrice,
                    CustomerFullName = $"{customer.FirstName} .{customer.LastName[0]}",
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                    CreatedAt = sale.CreatedAt,
                    Voucher = sale.Voucher,
                };

                ListSale.Add(listSalesVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            int pageSize = 5;
            return View(PaginatedList<ListSalesVM>.CreateAsync(ListSale.AsQueryable(), pageNumber ?? 1, pageSize));
        }


        [Authorize(Roles = "Super Admin, Admin")]
        public IActionResult SalesPerCustomer(int id, string sortOrder,string searchString, int? pageNumber)
        {
            var sales = _salesService.FindSalesByCustomerId(id);
            ViewData["CurrentSort"] = sortOrder;
            List<ListSalesVM> ListSale = new List<ListSalesVM>();
            foreach (var sale in sales)
            {
                var Created = _userService.FindByEmail(sale.CreatedBy);
                var customer = _customerService.FindById(sale.CustomerId);

                ListSalesVM listSalesVM = new ListSalesVM
                {
                    Id = sale.Id,
                    Item = sale.Item,
                    TotalPrice = sale.TotalPrice,
                    CustomerFullName = $"{customer.FirstName} .{customer.LastName[0]}",
                    CreatedBy = $"{Created.FirstName} .{Created.LastName[0]}",
                    CreatedAt = sale.CreatedAt,
                    Voucher = sale.Voucher,
                };

                ListSale.Add(listSalesVM);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";
            int pageSize = 5;
            return View(PaginatedList<ListSalesVM>.CreateAsync(ListSale.AsQueryable(), pageNumber ?? 1, pageSize));
        }


        [Authorize(Roles = "Super Admin, Sales Manager")]
        public IActionResult AddSales()
        {
            AddSalesVM salesVM = new AddSalesVM
            {
                CustomerList = _customerService.GetAllCustomers().Select(m => new SelectListItem
                {
                    Text = $"{m.Email}",
                    Value = m.Id.ToString()
                })
            };

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View(salesVM);
        }

        [HttpPost]
        public IActionResult AddSales(AddSalesVM addSales)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));


            SalesDTO SalesDTO = new SalesDTO
            {
                UserId = userId,
                Item = addSales.Item,
                CustomerId = addSales.CustomerId,
                TotalPrice = 0,
                IsSold = false,
            };


            _salesService.Add(SalesDTO);
            return RedirectToAction("Index");


        }

        [Authorize(Roles = "Super Admin, Sales Manager")]
        public IActionResult UpdateSales(int id)
        {
            var sale = _salesService.FindById(id);
            if (sale == null || sale.IsSold == true)
            {
                return NotFound();
            }
            else
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                User userlogin = _userService.FindById(userId);
                ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

                UpdateSalesVM updateSales = new UpdateSalesVM
                {
                    Id = sale.Id,
                    Item = sale.Item,
                    CustomerId = sale.CustomerId,
                    CustomerList = _customerService.GetAllCustomers().Select(m => new SelectListItem
                    {
                        Text = $"{m.Email}",
                        Value = m.Id.ToString()
                    }),

                };

                return View(updateSales);
            }

        }

        [HttpPost]
        public IActionResult UpdateSales(UpdateSalesVM updateSales)
        {
            SalesDTO sale = new SalesDTO
            {
                Id = updateSales.Id,
                Item = updateSales.Item,
                CustomerId = updateSales.CustomerId,
                IsSold = false,
            };
            _salesService.Update(sale);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult IsSold()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult IsSold(CheckSoldVM check)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            var sales = _salesService.FindByVoucher(check.Voucher);
            if (sales == null)
            {
                ViewBag.Message = "Invalid";
            }
            else if (sales != null && sales.IsSold == true)
            {
                ViewBag.Message = "Used";
            }
            else
            {
                return RedirectToAction($"ViewSalesStock", new { id = sales.Id});
            }
            return View(check);

        }

        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult ViewSalesStock(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User userlogin = _userService.FindById(userId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";

            List<ListSalesStockVM> ListSalesStock = new List<ListSalesStockVM>();
            var salesItems = _salesItemService.GetSalesItemBySalesId(id);
            foreach (var salesItem in salesItems)
            {
                var stock = _stockService.FindById(salesItem.StockId);
                var flock = _flockService.FindById(stock.FlockId);
                var flockType = _flockTypeService.FindById(flock.FlockTypeId);

                ViewBag.SalesId = id;
                if(stock.ItemType == "Eggs")
                {
                    ListSalesStockVM listSalesStockVM = new ListSalesStockVM
                    {
                        StockDescription = $"{flockType.Name} ({flock.BatchNo}), Stock: {stock.NoOfItem} Crate(s)",
                        Qty = $"{salesItem.NoOfItem} Crate(s)",
                    };

                    ListSalesStock.Add(listSalesStockVM);
                }
                else if(stock.ItemType == "Birds")
                {
                    ListSalesStockVM listSalesStockVM = new ListSalesStockVM
                    {
                        StockDescription = $"{flockType.Name} ({flock.BatchNo}), Stock: {stock.NoOfItem} Bird(s)",
                        Qty = $"{salesItem.NoOfItem} Bird(s)",
                    };

                    ListSalesStock.Add(listSalesStockVM);
                }
                
            }

            return View(ListSalesStock);
        }

        [Authorize(Roles = "Super Admin, Sales Manager")]
        public IActionResult Receipt(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            User userlogin = _userService.FindById(userId);
            var sales = _salesService.FindById(id);
            var customer = _customerService.FindById(sales.CustomerId);
            ViewBag.UserName = $"{userlogin.FirstName} .{userlogin.LastName[0]}";
            ViewBag.Customer = $"{customer.LastName} {customer.FirstName}";
            ViewBag.TotalPrice = sales.TotalPrice;
            ViewBag.Voucher = sales.Voucher;

            List<ReceiptVM> Receipts = new List<ReceiptVM>();
            var salesItems = _salesItemService.GetSalesItemBySalesId(id);
            foreach (var salesItem in salesItems)
            {
                var stock = _stockService.FindById(salesItem.StockId);
                var flock = _flockService.FindById(stock.FlockId);

                ViewBag.SalesId = id;
                if (stock.ItemType == "Eggs")
                {
                    ReceiptVM receipt = new ReceiptVM
                    {
                        SaleDescription = $"{salesItem.Item}",
                        Qty = $"{salesItem.NoOfItem} Crate(s)",
                        PricePerItem = salesItem.PricePerItem,
                        TotalPricePerSales = salesItem.NoOfItem*salesItem.PricePerItem,
                    };

                    Receipts.Add(receipt);
                }
                else if (stock.ItemType == "Birds")
                {
                    ReceiptVM receipt = new ReceiptVM
                    {
                        SaleDescription = $"{salesItem.Item} ({_flockService.GetCurrentAverageWeight(flock.Id)} Kg)",
                        Qty = $"{salesItem.NoOfItem} Bird(s)",
                        PricePerItem = salesItem.PricePerItem,
                        TotalPricePerSales = salesItem.NoOfItem * salesItem.PricePerItem,
                    };

                    Receipts.Add(receipt);
                }

            }

            return View(Receipts);
        }

        [Authorize(Roles = "Super Admin, Store Manager")]
        public IActionResult ConfirmSales(int id)
        {
            var sales = _salesService.FindById(id);
            if(sales != null)
            {
                SalesDTO sale = new SalesDTO
                {
                    Id = sales.Id,
                    Item = sales.Item,
                    CustomerId = sales.CustomerId,
                    IsSold = true,
                };
                _salesService.Update(sale);
            }

            return RedirectToAction("IsSold");
        }

        [Authorize(Roles = "Super Admin, Sales Manager")]
        public IActionResult Delete(int id)
        {
            var sale = _salesService.FindById(id);
            if (sale == null || sale.IsSold == true)
            {
                return NotFound();
            }

            var salesItems = _salesItemService.GetSalesItemBySalesId(sale.Id);
            if (salesItems != null)
            {
                foreach (var salesItem in salesItems)
                {

                    var sales = _salesService.FindById(salesItem.SalesId);

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

                    SalesDTO saleN = new SalesDTO
                    {
                        Id = salesD.Id,
                        Item = salesD.Item,
                        CustomerId = salesD.CustomerId,
                        TotalPrice = totalPrice,
                        IsSold = false,
                    };
                    _salesService.UpdateMore(saleN);

                    _salesItemService.Delete(id);
                }
            }

            _salesService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
