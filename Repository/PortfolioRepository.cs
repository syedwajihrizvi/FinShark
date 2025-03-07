using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.Data;
using FinShark.Interfaces;
using FinShark.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetUserPortfolioAsync(string userId)
        {
            var stocks = await _context.Portfolios.Where(p => p.AppUserId == userId)
                                       .Select(p => p.Stock).ToListAsync();
            return stocks!;
        }

        public async Task<int> CreatePortfolioAsync(AppUser user, Stock stock)
        {
            var portfolio = new Portfolio
            {
                AppUserId = user.Id,
                StockId = stock.Id,
            };
            await _context.AddAsync(portfolio);
            return await _context.SaveChangesAsync();
           
        }

        public async Task<Portfolio?> DeletePortfolioAsync(AppUser user, Stock stock)
        {
            var portfolio = await _context.Portfolios.Where(p => p.AppUserId == user.Id).FirstOrDefaultAsync();
            if (portfolio == null)
                return null;
            _context.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
    }
}