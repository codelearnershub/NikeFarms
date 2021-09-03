﻿using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IRoleService
    {
        public Role Add(RoleDTO roleDTO);

        public Role FindById(int id);

        public Role Update(RoleDTO roleDTO);

        public Role FindByName(string roleName);

        public IEnumerable<Role> GetAllRoles();

        public IEnumerable<Role> GetRolesWithoutAdmin();

        public void Delete(int id);
    }
}
