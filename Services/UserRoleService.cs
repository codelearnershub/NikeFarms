﻿using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserService _userService;

        public UserRoleService(IUserRoleRepository userRoleRepository, IUserService userService)
        {
            _userRoleRepository = userRoleRepository;
            _userService = userService;
        }

        public UserRole Add(int userId, int roleId)
        {
            var userRole = new UserRole
            {
                CreatedBy = _userService.FindById(userId).Email,
                CreatedAt = DateTime.Now,
                UserId = userId,
                RoleId = roleId,
            };

            return _userRoleRepository.Add(userRole);
        }

        public UserRole FindById(int id)
        {
            return _userRoleRepository.FindById(id);
        }

        public void Delete(int id)
        {
            _userRoleRepository.Delete(id);
        }
    }
}