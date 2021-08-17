using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IFlockService
    {
        public Flock Add(int userId, int flockTypeId, int totalNo, int age, double averageWeight);

        public Flock FindById(int id);

        public Flock Update(int flockId, int flockTypeId, int totalNo, int age, double averageWeight);

        public void Delete(int id);

    }
}
