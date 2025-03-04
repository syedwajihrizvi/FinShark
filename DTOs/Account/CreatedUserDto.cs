using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinShark.DTOs.Account
{
    public class CreatedUserDto
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Token { get; set; }
    }
}