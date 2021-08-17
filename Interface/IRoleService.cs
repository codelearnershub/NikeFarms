using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IRoleService
    {
        public Role Add(int userId, string name);

        public Role FindById(int id);

        public Role Update(int roleId, string Name);

        public void Delete(int id);
    }
}
