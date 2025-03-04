using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinShark.Extensions;
using FinShark.Interfaces;
using FinShark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;

namespace FinShark.Controllers
{
    [Route("api/portfolio")]
    [ApiController]

    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(
            UserManager<AppUser> userManager,
            IStockRepository stockRepository,
            IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            if (username == null)
                return Unauthorized("Invalid User token. Please try again");
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound($"User with {username} does not exist");
            var stocks = await _portfolioRepository.GetUserPortfolioAsync(user.Id);
            if (stocks == null)
                return Ok("No Stocks In Portfolio");
            return Ok(stocks);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            if (username == null)
                return Unauthorized("Invalid User token. Please try again");
            var user = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (user == null || stock == null)
                return BadRequest("Provided stock or user was not found");
            var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(user.Id);
            bool exists = userPortfolio.Any(s => s.Symbol.ToLower() == symbol.ToLower());
            if (exists)
                return BadRequest("Already have a stock with this symbol");
            var createdPortfolio = await _portfolioRepository.CreatePortfolioAsync(user, stock);
            if (createdPortfolio == null)
                return BadRequest("Failed to create request");
            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            if (username == null)
                return Unauthorized("Invalid User token. Please try again");
            var user = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null)
                return NotFound($"Stock with symbol name {symbol} not found");

            var deleted = await _portfolioRepository.DeletePortfolioAsync(user!, stock);
            if (deleted == null)
                return StatusCode(500, "Delete failed");
            return NoContent();

        }
    }
}