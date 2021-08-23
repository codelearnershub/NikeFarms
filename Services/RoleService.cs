using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
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

        public Role Add(RoleDTO roleDTO)
        {
            var role = new Role
            {
                CreatedBy = _userService.FindById(roleDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                Name = roleDTO.Name,
            };

            return _roleRepository.Add(role);
        }

        public Role FindById(int id)
        {
            return _roleRepository.FindById(id);
        }

        public Role Update(RoleDTO roleDTO)
        {
            var role = _roleRepository.FindById(roleDTO.Id);
            if (role == null)
            {
                return null;
            }

            role.Name = roleDTO.Name;
            role.UpdatedAt = DateTime.Now;

            return _roleRepository.Update(role);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public void Delete(int id)
        {
            _roleRepository.Delete(id);
        }

        public Role FindByName(string roleName)
        {
          return  _roleRepository.FindByName(roleName);
        }
    }
}
