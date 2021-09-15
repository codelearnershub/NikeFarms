using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IFlockRepository
    {
        public Flock Add(Flock flock);

        public Flock FindById(int flockId);

        public Flock FindByBatchNo(string batchNo);


        public List<Flock> GetApprovedFlocks();

        public IQueryable<Flock> GetAllFlocks();

        public Flock Update(Flock flock);

        public void Delete(int flockId);
    }
}
