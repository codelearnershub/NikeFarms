using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IUserService
    {
        public void RegisterUser(UserDTO userDTO);
      
        public User LoginUser(UserDTO userDTO);

        public User FindById(int Id);
        

        public User Update(int id, UserDTO userDTO);


        public void Delete(int id);
        
    }
}
