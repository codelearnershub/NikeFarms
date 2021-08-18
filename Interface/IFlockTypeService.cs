using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IFlockTypeService
    {
        public FlockType Add(FlockTypeDTO flockTypeDTO);

        public FlockType FindById(int id);

        public FlockType Update(int flockTypeId, FlockTypeDTO flockTypeDTO);


        public void Delete(int id);
    }
}
