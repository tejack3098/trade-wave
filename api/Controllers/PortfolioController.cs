using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController: ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;

        private readonly IStockRepository _stockRepository;

        private readonly IPortfolioRepository _portfolioRepository;

        private readonly IFMPService _fMPService;

        public PortfolioController(
            UserManager<AppUser> userManager, 
            IStockRepository stockRepository, 
            IPortfolioRepository portfolioRepository,
            IFMPService fMPService)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _fMPService = fMPService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();

            var user = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(user);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                stock = await _fMPService.FindStockBySymbolAsync(symbol);

                if (stock == null)  
                    return BadRequest("Stock does not exist");
                else{
                    await _stockRepository.CreateAsync(stock);
                }
                
            }

            if (stock == null)
                return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            if (userPortfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower())) 
                return BadRequest("Stock already Exists in Portfolio");

            var portfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };

            await _portfolioRepository.CreatePortfolioAsync(portfolio);

            if(portfolio == null)
                return StatusCode(500, "Could not create portfolio");

            return Created();

            
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            if (!userPortfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower())) 
                return BadRequest("Stock not found in Portfolio");

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if(filteredStock.Count == 1)
            {
                await _portfolioRepository.DeletePortfolio(appUser, symbol);
            }else{
                return BadRequest("Stock not found in Portfolio");
            }
           
            return Ok();
        }

    }
}