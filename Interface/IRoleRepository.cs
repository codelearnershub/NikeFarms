using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IRoleRepository
    {
        public Role Add(Role role);

        public Role FindById(int roleId);

        public Role Update(Role role);

        public void Delete(int roleId);
    }
}
