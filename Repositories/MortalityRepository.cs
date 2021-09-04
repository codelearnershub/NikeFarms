using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class MortalityRepository : IMortalityRepository
    {
        private readonly NikeDbContext _dbContext;

        public MortalityRepository(NikeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Mortality Add(Mortality mortality)
        {
            _dbContext.Mortalities.Add(mortality);
            _dbContext.SaveChanges();
            return mortality;
        }

        public void Delete(int mortalityId)
        {
            var mortality = FindById(mortalityId);

            if (mortality != null)
            {
                _dbContext.Mortalities.Remove(mortality);
                _dbContext.SaveChanges();
            }
        }

        public List<Mortality> GetAllMortality()
        {
            
            return _dbContext.Mortalities.OrderByDescending(r => r.CreatedAt).ToList();
        }

        public List<Mortality> GetMortalityPerFlock(int flockId)
        {
            return _dbContext.Mortalities.Where(m=> m.FlockId == flockId).OrderByDescending(r => r.CreatedAt).ToList();
        }

        public Mortality FindById(int mortalityId)
        {
            return _dbContext.Mortalities.FirstOrDefault(u => u.Id.Equals(mortalityId));
        }

        public Mortality Update(Mortality mortality)
        {
            _dbContext.Mortalities.Update(mortality);
            _dbContext.SaveChanges();
            return mortality;
        }

        public Mortality GetMortalityFlockId(int flockId)
        {
            var date = DateTime.Now.ToShortDateString();
            return _dbContext.Mortalities.FirstOrDefault(d => d.FlockId == flockId && d.Date == date && d.StockId == null);
        }

        public Mortality GetMortalityStockId(int stockId)
        {
            var date = DateTime.Now.ToShortDateString();
            return _dbContext.Mortalities.FirstOrDefault(d => d.StockId == stockId && d.Date == date );
        }
    }
}
