using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.Books;
using Archieve.Infrastructure.Models;


namespace Archieve.Domain.Interfaces
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
