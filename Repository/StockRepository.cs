using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.Data;
using FinShark.DTOs.Stock;
using FinShark.Helpers;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return null;
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).ThenInclude(a => a.AppUser).AsQueryable();
            Console.WriteLine(query.CompanyName);
            if (query.CompanyName != null)
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            if (query.Symbol != null)
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            if (query.SortBy != null)
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                else if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy(s => s.CompanyName);

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Where(s => s.Id == id).Include(s => s.Comments).FirstOrDefaultAsync();
            return stock;
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            var stock = await _context.Stocks.Where(s => s.Symbol == symbol).FirstOrDefaultAsync();
            return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            var exists = await _context.Stocks.Where(s => s.Id == id).FirstOrDefaultAsync();
            return exists != null;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return null;
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Industry = stockDto.Industry;
            stock.LastDiv = stockDto.LastDiv;
            stock.Purchase = stockDto.Purchase;
            stock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return stock;
        }
    }
}