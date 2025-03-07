using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolioAsync(string userId);

        Task<int> CreatePortfolioAsync(AppUser user, Stock stock);

        Task<Portfolio?> DeletePortfolioAsync(AppUser user, Stock stock);
    }
}