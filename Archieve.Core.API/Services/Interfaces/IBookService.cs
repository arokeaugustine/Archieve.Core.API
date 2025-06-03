using Archieve.Core.API.Models.Data;
using Archieve.Core.API.Models.DTOs;

namespace Archieve.Core.API.Services.Interfaces
{
    public interface IBookService
    {
        ResponseModel<string> CreateBooks(Books book);
        ResponseModel<List<Book>> GetBooks();
        ResponseModel<UpdateBookDTO> UpdateBook(UpdateBookDTO book);
        ResponseModel<Books> GetBook(int id);
        ResponseModel<string> DeleteBook(int id);
    }
}
