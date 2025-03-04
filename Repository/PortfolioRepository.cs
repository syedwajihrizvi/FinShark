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
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetUserPortfolioAsync(string userId)
        {
            var stocks = await _context.Portfolios.Where(p => p.AppUserId == userId)
                                       .Select(p => p.Stock).ToListAsync();
            return stocks!;
        }

        public async Task<Portfolio> CreatePortfolioAsync(AppUser appUser, Stock stock)
        {
            var porfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id,
            };
            await _context.AddAsync(porfolio);
            await _context.SaveChangesAsync();
            return porfolio;
        }

        public async Task<Portfolio?> DeletePortfolioAsync(AppUser appUser, Stock stock)
        {
            var portfolio = await _context.Portfolios.Where(p => p.AppUserId == appUser.Id).FirstOrDefaultAsync();
            if (portfolio == null)
                return null;
            _context.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
    }
}