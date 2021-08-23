﻿using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IStockService
    {
        public Stock Add(StockDTO stockDTO);

        public Stock FindById(int id);

        public Stock Update(StockDTO stockDTO);

        public IEnumerable<Stock> GetAllStocks();

        public IEnumerable<Stock> GetStocksByManagerEmail(string managerEmail);

        public void Delete(int id);
    }
}
