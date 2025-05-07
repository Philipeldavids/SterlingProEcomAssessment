using Core.Helper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly PasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;
        public UserService(IUserRepo userRepo, PasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
        {
            _userRepo = userRepo;  
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<bool> RegisterUser(User user)
        {
            return true;
        }

        public async Task<(bool Success, string Token)> AuthenticateUser(string Email, string Password)
        {
            var user = await _userRepo.GetUserByEmail(Email);

            if (user == null || !_passwordHasher.VerifyPassword(user, Password))
            {
                return (false, null);
            }

            var token = _jwtTokenService.GenerateToken(user.Id, user.UserName);
          
            return (true, token);
        }

        public async Task<bool> Register(string FirstName, string LastName, string Email, string Password)
        {
            User user = new User();
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Email = Email;
            user.PasswordHash = _passwordHasher.HashPassword(user, Password);
            user.UserName = Email;

            var result = await _userRepo.AddUserAsync(user);

            return result;
        }
    }
}
