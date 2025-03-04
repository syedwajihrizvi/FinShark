using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.DTOs.Account;
using FinShark.DTOs.Register;

namespace FinShark.Mappers
{
    public static class AccountMapper
    {
        public static CreatedUserDto ToCreatedUserDto(this RegisterDto registerDto, string token)
        {
            return new CreatedUserDto
            {
                Email = registerDto.Email,
                Username = registerDto.Username,
                Token = token
            };
        }
    }
}