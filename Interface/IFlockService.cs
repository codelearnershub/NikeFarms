using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IFlockService
    {
        public Flock Add(FlockDTO flockDTO);

        public Flock FindById(int id);

        public Flock Update(int flockId, FlockDTO flockDTO);

        public void Delete(int id);

    }
}
