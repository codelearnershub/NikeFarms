using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IMortalityService
    {
        public Mortality Add(MortalityDTO mortalityDTO);

        public IEnumerable<Mortality> GetAllMortality();

        public Mortality FindById(int id);

        public IEnumerable<Mortality> GetMortalityPerFlock(int flockId);

        public Mortality Update(MortalityDTO mortalityDTO);

        public void Delete(int id);

        public Mortality GetMortalityFlockId(int flockId);


        public Mortality GetMortalityStockId(int stockId);

    }
}
