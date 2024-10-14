using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IStockRepository _stockRepository;

        private readonly UserManager<AppUser> _userManager;

        private readonly IFMPService _fMPService;

        public CommentController(ICommentRepository commentRepository, 
            IStockRepository stockRepository, UserManager<AppUser> userManager,
            IFMPService fMPService)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
            _fMPService = fMPService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery]CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllAsync(queryObject);
            var commentsDto = comments.Select(comment => comment.ToCommentDto());
            return Ok(commentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }


        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentDto commentDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var comment = commentDto.ToCommentFromCreate(stock.Id);
            comment.AppUserId = appUser!.Id;
            await _commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateCommentRequestDto updateCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.UpdateAsync(id, updateCommentDto.ToCommentFromUpdate()); 
            if (comment == null)    
            {
                return NotFound("Comment not found");
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("Comment does not exist");
            }
            return Ok(comment);
        }
    }
}