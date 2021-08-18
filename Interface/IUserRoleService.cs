using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IUserRoleService
    {
        public UserRole Add(int userId, int roleId);

        public UserRole FindById(int id);

        public string FindRole(int userId);

        public void Delete(int id);
        
    }
}
