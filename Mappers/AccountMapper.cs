using FinShark.DTOs.Account;

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