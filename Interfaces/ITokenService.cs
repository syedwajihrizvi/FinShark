using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}