using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IFlockTypeService
    {
        public FlockType Add(int userId, string name, string description);

        public FlockType FindById(int id);

        public FlockType Update(int flockTypeId, string Name, string description);


        public void Delete(int id);
    }
}
