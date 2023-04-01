using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string firstName, string lastName);
    }
}
