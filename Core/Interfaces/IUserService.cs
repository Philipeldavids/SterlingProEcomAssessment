using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, string Token)> AuthenticateUser(string Email, string Password);
        Task<bool> Register(string FirstName, string LastName, string Email, string Password);
    }
}
