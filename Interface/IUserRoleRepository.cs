using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IUserRoleRepository
    {
        public UserRole Add(UserRole userRole);

        public UserRole FindById(int userRoleId);

        public string FindRole(int userId);

        public UserRole FindUserRole(int userId);

        public int FindUsersWithParticularRole(int roleId);

        public UserRole Update(UserRole userRole);

        public void Delete(int userRoleId);
    }
}
