using NikeFarms.Context;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly NikeDbContext _dbContext;

        public RoleRepository(NikeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Role Add(Role role)
        {
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();
            return role;
        }

        public void Delete(int roleId)
        {
            var role = FindById(roleId);

            if (role != null)
            {
                _dbContext.Roles.Remove(role);
                _dbContext.SaveChanges();
            }
        }

        public Role FindById(int roleId)
        {
            return _dbContext.Roles.FirstOrDefault(u => u.Id.Equals(roleId));
        }

        public List<Role> GetAllRoles()
        {
            return _dbContext.Roles.ToList();
        }

        public Role Update(Role role)
        {
            _dbContext.Roles.Update(role);
            _dbContext.SaveChanges();
            return role;
        }
    }
}
