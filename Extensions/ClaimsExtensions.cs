using System.Security.Claims;

namespace FinShark.Extensions
{
    public static class ClaimsExtensions
    {
        public static string? GetUsername(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.GivenName);
            Console.WriteLine(claim);
            return claim?.Value;
        }
    }
}