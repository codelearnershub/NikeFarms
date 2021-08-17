using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserService _userService;

        public RoleService(IRoleRepository roleRepository, IUserService userService)
        {
            _roleRepository = roleRepository;
            _userService = userService;
        }

        public Role Add(int userId, string name)
        {
            var role = new Role
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                Name = name,
            };

            return _roleRepository.Add(role);
        }

        public Role FindById(int id)
        {
            return _roleRepository.FindById(id);
        }

        public Role Update(int roleId, string Name)
        {
            var role = _roleRepository.FindById(roleId);
            if (role == null)
            {
                return null;
            }

            role.Name = Name;
            role.UpdatedAt = DateTime.Now;

            return _roleRepository.Update(role);
        }

        public void Delete(int id)
        {
            _roleRepository.Delete(id);
        }

    }
}
