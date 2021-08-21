using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IFlockTypeRepository
    {
        public FlockType Add(FlockType flockType);

        public FlockType FindById(int flockTypeId);

        public List<FlockType> GetAllFlockTypes();

        public FlockType Update(FlockType flockType);

        public void Delete(int flockTypeId);
    }
}
