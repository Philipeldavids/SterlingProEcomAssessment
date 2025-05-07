using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string email);
    }
}
