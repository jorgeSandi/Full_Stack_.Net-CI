using Asp.Versioning;
using FullStackCI.Dtos;
using FullStackCI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullStackCI.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Books2Controller : BooksController
    {
        private readonly IBookService _bookService;

        public Books2Controller(IBookService bookService) : base(bookService)
        {
            _bookService = bookService;
        }

        [MapToApiVersion(2.0)]
        [HttpGet("{id}")]
        public new async Task<ActionResult<string>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok("Version 2");
        }
    }
}