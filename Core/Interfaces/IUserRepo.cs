using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepo
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> AddUserAsync(User user);
    }
}
