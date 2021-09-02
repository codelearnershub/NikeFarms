using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class MortalityService : IMortalityService
    {
        private readonly IMortalityRepository _mortalityRepository;
        private readonly IUserService _userService;

        public MortalityService(IMortalityRepository mortalityRepository, IUserService userService)
        {
            _mortalityRepository = mortalityRepository;
            _userService = userService;
        }

        public Mortality Add(MortalityDTO mortalityDTO)
        {
            var mortality = new Mortality
            {
                CreatedBy = _userService.FindById(mortalityDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                StockId = mortalityDTO.StockId,
                FlockId = mortalityDTO.FlockId,
                NoOfDeaths = mortalityDTO.NoOfDeaths,
                Date = DateTime.Now.ToShortDateString(),
            };

            return _mortalityRepository.Add(mortality);
        }

        public IEnumerable<Mortality> GetAllMortality()
        {
            return _mortalityRepository.GetAllMortality();
        }

        public Mortality FindById(int id)
        {
            return _mortalityRepository.FindById(id);
        }

        public Mortality Update(MortalityDTO mortalityDTO)
        {
            var mortality = _mortalityRepository.FindById(mortalityDTO.Id);
            if (mortality == null)
            {
                return null;
            }

            mortality.StockId = mortalityDTO.StockId;
            mortality.FlockId = mortalityDTO.FlockId;
            mortality.NoOfDeaths = mortalityDTO.NoOfDeaths;
            mortality.UpdatedAt = DateTime.Now;


            return _mortalityRepository.Update(mortality);
        }

        public void Delete(int id)
        {
            _mortalityRepository.Delete(id);
        }

        public IEnumerable<Mortality> GetMortalityPerFlock(int flockId)
        {
            return _mortalityRepository.GetMortalityPerFlock(flockId);
        }

        public Mortality GetMortalityFlockId(int flockId)
        {
            return _mortalityRepository.GetMortalityFlockId(flockId);
        }

        public Mortality GetMortalityStockId(int stockId)
        {
            return _mortalityRepository.GetMortalityStockId(stockId);
        }
    }
}
