using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;

        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser user, string symbol)
        {
            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if (portfolio == null)
                return null;

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            var userId = user.Id;

            return await _context.Portfolios
                .Where(u => u.AppUserId == user.Id)
                .Select( s => new Stock{
                    Id = s.StockId,
                    Symbol = s.Stock.Symbol,
                    CompanyName = s.Stock.CompanyName,
                    Purchase = s.Stock.Purchase,
                    LastDiv = s.Stock.LastDiv,
                    Industry = s.Stock.Industry,
                    MarketCap = s.Stock.MarketCap,
                })
                .ToListAsync();
        }
    }
}