using NikeFarms.v2._0.Models;
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

        public Stock Update(int stockId, StockDTO stockDTO);

        public void Delete(int id);
    }
}
