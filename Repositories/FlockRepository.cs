using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class FlockRepository : IFlockRepository
    {
        private readonly NikeDbContext _dbContext;

        public FlockRepository(NikeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Flock Add(Flock flock)
        {
            _dbContext.Flocks.Add(flock);
            _dbContext.SaveChanges();
            return flock;
        }

        public void Delete(int flockId)
        {
            var flock = FindById(flockId);

            if (flock != null)
            {
                _dbContext.Flocks.Remove(flock);
                _dbContext.SaveChanges();
            }
        }

        public Flock FindById(int flockId)
        {
            return _dbContext.Flocks.FirstOrDefault(u => u.Id.Equals(flockId));
        }

        public Flock Update(Flock flock)
        {
            _dbContext.Flocks.Update(flock);
            _dbContext.SaveChanges();
            return flock;
        }
    }
}
