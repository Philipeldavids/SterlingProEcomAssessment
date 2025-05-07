using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
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
        public UserRepo(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail (string email)
        {
            var user = _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
            return user;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
