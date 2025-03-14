using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinShark.Extensions
{
    public static class ClaimsExtensions
    {
        public static string? GetUsername(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.GivenName);
            Console.WriteLine(claim);
            return claim!.Value;
        }
    }
}