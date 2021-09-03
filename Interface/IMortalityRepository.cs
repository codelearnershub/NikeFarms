using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IMortalityRepository
    {

        public Mortality Add(Mortality mortality);


        public void Delete(int mortalityId);


        public List<Mortality> GetMortalityPerFlock(int flockId);


        public List<Mortality> GetAllMortality();


        public Mortality FindById(int mortalityId);


        public Mortality Update(Mortality mortality);


        public Mortality GetMortalityFlockId(int flockId);


        public Mortality GetMortalityStockId(int stockId);
    }
}
