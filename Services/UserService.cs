using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NikeFarms.v2._0.Interface;
using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }


        public void RegisterUser(UserDTO userDTO)
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);

            string hashedPassword = HashPassword(userDTO.Password, saltString);

            User user = new User
            {
                CreatedBy = _userRepository.FindById(userDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                LastName = userDTO.LastName,
                FirstName = userDTO.FirstName,
                Email = userDTO.Email,
                PhoneNo = userDTO.PhoneNo,
                Address = userDTO.Address,
                HashSalt = saltString,
                PasswordHash = hashedPassword,
            };

            _userRepository.Add(user);
            int id = _userRepository.FindByEmail(userDTO.Email).Id;
            UserRole userRole = new UserRole
            {
                CreatedBy = _userRepository.FindById(userDTO.UserId).Email,
                CreatedAt = DateTime.Now,
                UserId = id,
                RoleId = userDTO.RoleId,

            };

            _userRoleRepository.Add(userRole);
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public User LoginUser(UserDTO userDTO)
        {
            User user = _userRepository.FindByEmail(userDTO.Email);

            if (user == null)
            {

                return null;
            }

            string hashedPassword = HashPassword(userDTO.Password, user.HashSalt);

            if (user.PasswordHash.Equals(hashedPassword))
            {

                return user;
            }

            return null;
        }

        public User FindById(int Id)
        {
            return _userRepository.FindById(Id);
        }

        public User Update(UserDTO userDTO)
        {
            User user = _userRepository.FindById(userDTO.Id);

            if (user == null)
            {
                return null;
            }

            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);

            string hashedPassword = HashPassword(userDTO.Password, saltString);

            user.LastName = userDTO.LastName;
            user.FirstName = userDTO.FirstName;
            user.Email = userDTO.Email;
            user.PhoneNo = userDTO.PhoneNo;
            user.Address = userDTO.Address;
            user.HashSalt = saltString;
            user.PasswordHash = hashedPassword;
            user.UpdatedAt = DateTime.Now;


            
            var userRole = _userRoleRepository.FindUserRole(userDTO.Id);
            if(userRole != null)
            {
                userRole.UserId = userDTO.Id;
                userRole.RoleId = userDTO.RoleId;

                _userRoleRepository.Update(userRole);
            }
            return _userRepository.Update(user);
        }

        public IEnumerable<User> GetAllUser()
        {
            return _userRepository.GetAllUser();
        }

        public User FindByEmail(string userEmail)
        {
            return _userRepository.FindByEmail(userEmail);
        }

        public void Delete(int id)
        {
           

            var userRole = _userRoleRepository.FindUserRole(id);
            _userRepository.Delete(id);
            _userRoleRepository.Delete(userRole.Id);

        }

    }
}
