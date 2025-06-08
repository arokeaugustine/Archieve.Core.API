using System.Threading.Tasks;
using Archieve.Core.Contracts.Enums;
using Archieve.Core.Contracts.TransferObjects.Books;
using Archieve.Domain.Helpers.Authorizations;
using Archieve.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Archieve.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpPost("create")]
        public IActionResult CreateBook(Books books)
        {
            var response = this.bookService.CreateBooks(books);
            return Ok(response);
        }

        [HttpGet("get-books")]
        [HasPermission(Permissions.CanViewBooks)]
        public IActionResult GetBooks()
        {
            var response = this.bookService.GetBooks();
            return Ok(response);
        }

        [HttpPut("update")]
        public IActionResult UpdateBook(UpdateBookDTO book)
        {
            var response = this.bookService.UpdateBook(book);
            return Ok(response);
        }

        [HttpGet("get{id}")]
        public IActionResult GetBook(int id)
        {
            var response = this.bookService.GetBook(id);
            return Ok(response);
        }

        [HttpDelete("delete{id}")]
        public IActionResult DeleteBook(int id)
        {
            var response = this.bookService.DeleteBook(id);
            return Ok(response);
        }
    }
}
