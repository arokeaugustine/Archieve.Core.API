using Archieve.Core.API.Models.Data;
using Archieve.Core.API.Models.DTOs;
using Archieve.Core.API.Services.Interfaces;

namespace Archieve.Core.API.Services
{
    public class BookServices : IBookService
    {
        private readonly ArchieveContext _Context;
        public BookServices(ArchieveContext Context)
        {
            _Context = Context;
        }

        public ResponseModel<string> CreateBooks(Books book)
        {
            //validatebooks
            if (book == null)
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "Invalid data!"
                };
            }

            if (string.IsNullOrWhiteSpace(book.Title) ||
                string.IsNullOrWhiteSpace(book.Author))
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "Title and author of the book must be provided."
                };
            }

            var data = new Book
            {
                Title = book.Title,
                Author = book.Author,
                DateCreated = DateTime.Now,
                Description = book.Description
            };

            _Context.Books.Add(data);
            var save = _Context.SaveChanges();
            if (save <= 0)
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "An error occured while trying to save this record."
                };
            }
            return new ResponseModel<string>
            {
                IsSuccessful = true,
                Message = "successful"
            };

        }


        public ResponseModel<List<Book>> GetBooks()
        {
            var books = _Context.Books.Where(x=> x.IsDeleted == false).ToList();
            if (books.Count < 1)
            {
                return new ResponseModel<List<Book>>
                {
                    IsSuccessful = true,
                    Message = "No books found"
                };
            }

            return new ResponseModel<List<Book>>
            {
                IsSuccessful = true,
                Message = "successful",
                Data = books
            };


        }


        public ResponseModel<UpdateBookDTO> UpdateBook(UpdateBookDTO book)
        {
            if (book == null)
            {
                return new ResponseModel<UpdateBookDTO>
                {
                    IsSuccessful = false,
                    Message = "Invalid data",
                    Data = book
                };
            }

            if (string.IsNullOrWhiteSpace(book.Title)
                || string.IsNullOrWhiteSpace(book.Author))
            {
                return new ResponseModel<UpdateBookDTO>
                {
                    IsSuccessful = false,
                    Message = "title and author must be provided"
                };

            }

            var data = _Context.Books.Where(x => x.Id == book.Id && x.IsDeleted == false).FirstOrDefault();
            if (data == null)
            {
                return new ResponseModel<UpdateBookDTO>
                {
                    IsSuccessful = true,
                    Message = "No data for specified ID",
                    Data = book
                };
            }

            data.Author = book.Author;
            data.Description = book.Description;
            data.Title = book.Title;

            var save = _Context.SaveChanges();
            if (save <= 0)
            {
                return new ResponseModel<UpdateBookDTO>
                {
                    IsSuccessful = false,
                    Message = "Unable to save data",
                    Data = book
                };
            }

            return new ResponseModel<UpdateBookDTO>
            {
                IsSuccessful = true,
                Message = "successful",
                Data = book
            };

        }


        public ResponseModel<Books> GetBook(int id)
        {
            var book = _Context.Books.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (book == null)
            {
                return new ResponseModel<Books>
                {
                    IsSuccessful = false,
                    Message = "Unable to retrieve data for provided Id"
                };
            }

            var data = new Books
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                DateCreated = book.DateCreated
            };
            return new ResponseModel<Books>
            {
                IsSuccessful = true,
                Message = "success",
                Data = data
            };

        }


        public ResponseModel<string> DeleteBook(int id)
        {
            var book = _Context.Books.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
            if (book == null)
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "Unable to retrieve data for provided Id"
                };
            }
            book.IsDeleted = true;
            book.DateDeleted = DateTime.Now;

            var save = _Context.SaveChanges();
            if(save <= 0)
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "Something went wrong while trying to delete"
                };
            }
            return new ResponseModel<string>
            {
                IsSuccessful = true,
                Message = "record deleted successfully."
            };

        }
    }

}
