using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Models;
using Microsoft.AspNetCore.Mvc;
using FinShark.DTOs.Comment;
using Microsoft.AspNetCore.Identity;
using FinShark.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace FinShark.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comments = await _commentRepository.GetAllAsync();
            var commentDtos = comments.Select(c => c.ToCommentDto());
            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockId = commentRequestDto.StockId;
            var exists = await _stockRepository.StockExists(stockId);
            if (!exists)
                return BadRequest($"Stock with {stockId} does not exist.");
            var username = User.GetUsername();
            if (username == null)
                return Unauthorized("Username does not exist");
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return Unauthorized("User does not exist");
            var comment = await _commentRepository.CreateAsync(commentRequestDto, user);
            var commentDto = comment.ToCommentDto();
            return CreatedAtAction(nameof(Get), new { id = commentDto.Id }, commentDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentRequestDto updateCommentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentRepository.UpdateAsync(id, updateCommentDto);
            if (comment == null)
                return NotFound("Comment failed to update. Please check Id");
            return Ok(comment);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentRepository.DeleteAsync(id);
            if (comment == null)
                return NotFound($"Commend with {id} not found");
            return NoContent();
        }

    }
}