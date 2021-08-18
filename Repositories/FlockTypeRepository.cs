
using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System.Linq;

namespace NikeFarms.v2._0.Repositories
{
    public class FlockTypeRepository : IFlockTypeRepository
    {
        private readonly NikeDbContext _dbContext;

        public FlockTypeRepository(NikeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FlockType Add(FlockType flockType)
        {
            _dbContext.FlockTypes.Add(flockType);
            _dbContext.SaveChanges();
            return flockType;
        }

        public void Delete(int flockTypeId)
        {
            var flockType = FindById(flockTypeId);

            if (flockType != null)
            {
                _dbContext.FlockTypes.Remove(flockType);
                _dbContext.SaveChanges();
            }
        }

        public FlockType FindById(int flockTypeId)
        {
            return _dbContext.FlockTypes.FirstOrDefault(u => u.Id.Equals(flockTypeId));
        }

        public FlockType Update(FlockType flockType)
        {
            _dbContext.FlockTypes.Update(flockType);
            _dbContext.SaveChanges();
            return flockType;
        }
    }
}
