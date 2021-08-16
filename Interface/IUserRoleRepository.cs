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

        public UserRole Update(UserRole userRole);

        public void Delete(int userRoleId);
    }
}
