using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolioAsync(string userId);

        Task<Portfolio> CreatePortfolioAsync(AppUser user, Stock stock);

        Task<Portfolio?> DeletePortfolioAsync(AppUser user, Stock stock);
    }
}