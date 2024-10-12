using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
                .Select(stock => stock.ToStockDto());;
            return Ok(stocks);
        }


        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var stock = _context.Stocks.Find(id);
                
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id, [FromBody]UpdateStockRequestDto updateStockDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(s => s.Id == id);
            
            if (stockModel == null)
            {
                return NotFound();
            }
            
            stockModel.Industry = updateStockDto.Industry;
            stockModel.LastDiv = updateStockDto.LastDiv;
            stockModel.MarketCap = updateStockDto.MarketCap;               
            stockModel.Purchase = updateStockDto.Purchase;
            stockModel.Symbol =  updateStockDto.Symbol;
            stockModel.CompanyName = updateStockDto.CompanyName;
            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
            return NoContent();
        }
    }
}