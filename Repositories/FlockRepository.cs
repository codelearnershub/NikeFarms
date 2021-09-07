using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Flock> GetAllFlocks()
        {
            return _dbContext.Flocks.Include(f=>f.FlockType).Where(f=> f.AvailableBirds > 0).OrderByDescending(r=> r.CreatedAt);
        }

        public List<Flock> GetApprovedFlocks()
        {
            return _dbContext.Flocks.Where(f=> f.IsApproved == true && f.AvailableBirds > 0).OrderBy(r => r.CreatedAt).ToList();
        }

        public Flock FindById(int flockId)
        {
            return _dbContext.Flocks.Include(f=> f.FlockType).FirstOrDefault(u => u.Id.Equals(flockId));
        }

        public Flock FindByBatchNo(string batchNo)
        {
            return _dbContext.Flocks.FirstOrDefault(u => u.BatchNo.Equals(batchNo));
        }


        public Flock Update(Flock flock)
        {
            _dbContext.Flocks.Update(flock);
            _dbContext.SaveChanges();
            return flock;
        }
    }
}
