using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public UserRepo(ApplicationDbContext dbContext, UserManager<User> userManager) 
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<User> GetUserByEmail (string email)
        {
            var user = _dbContext.Users.Where(u => u.UserName == email).FirstOrDefault();
            return user;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            //    _dbContext.Users.Add(user);
            //    return await _dbContext.SaveChangesAsync() > 0;

            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (result.Succeeded)            {
                
                await _userManager.AddToRoleAsync(user, "User");
            }

            return true;
        }
    }
}
