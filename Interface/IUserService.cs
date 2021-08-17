using NikeFarms.v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface IUserService
    {
        public void RegisterUser(int userId, string password, string lastName, string firstName, string email, string phoneNo, string address, int roleId);
      
        public User LoginUser(string email, string password);

        public User FindById(int Id);
        

        public User Update(int id, string password, string lastName, string firstName, string email, string phoneNo, string address);


        public void Delete(int id);
        
    }
}
