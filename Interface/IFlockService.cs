﻿using NikeFarms.v2._0.Models;
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

        public List<Flock> GetApprovedFlocks();

        public Flock FindByBatchNo(string batchNo);

        public IEnumerable<Flock> GetAllFlocks();

        public Flock Update(FlockDTO flockDTO);

        public void Delete(int id);

        public List<Flock> OperationDaily();

        public int Mortality(int flockId);

        public double GetCurrentAverageWeight(int flockId);

        public decimal EstimatedPriceOfFlockPerKg(int flockId);

        public double TotalNoOfStockedBird(int flockId);


        public List<Flock> OperationFlock();

        public Flock UpdateFlockOnly(FlockDTO flockDTO);

        public List<Stock> OperationStock();

        public void CheckFlockFinish(int flockId);
    }
}
