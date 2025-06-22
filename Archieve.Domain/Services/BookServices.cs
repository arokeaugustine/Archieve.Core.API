using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.Books;
using Archieve.Domain.Brokers;
using Archieve.Domain.Interfaces;
using Archieve.Infrastructure.Models;

namespace Archieve.Domain.Services
{
    public class BookServices : IBookService
    {
        private readonly ArchieveContext _Context;
        private readonly IStorageBroker _StorageBroker;
        public BookServices(ArchieveContext context, IStorageBroker storageBroker)
        {
            _Context = context;
            _StorageBroker = storageBroker;
            
        }

        public async Task<ResponseModel<string>> CreateBooks(Books book)
        {
            try
            {
                //validatebooks
               /* if (book == null)
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
                }*/

                var data = new Book
                {
                    Title = book.Title,
                    Author = book.Author,
                    DateCreated = DateTime.Now,
                    Description = book.Description
                };

               var student = await _StorageBroker.InsertBookAsync(data);
               
                return new ResponseModel<string>
                {
                    IsSuccessful = true,
                    Message = "successful"
                };

            }
            catch (Exception patience)
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "An error occured while trying to save this record."
                };
            }

        }


        public async Task<ResponseModel<IQueryable<Book>>> GetBooks()
        {
            try
            {
                //var books = _Context.Books.Where(x => x.IsDeleted == false).ToList();
                var books = await _StorageBroker.SelectAllBooksAsync();
                if (books.Count() < 1)
                {
                    return new ResponseModel<IQueryable<Book>>
                    {
                        IsSuccessful = true,
                        Message = "No books found"
                    };
                }

                return new ResponseModel<IQueryable<Book>>
                {
                    IsSuccessful = true,
                    Message = "successful",
                    Data = books
                };


            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                // Serilog
                // return a response
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new ResponseModel<IQueryable<Book>>
                {
                    IsSuccessful = false,
                    Message = "An error occurred while trying to retrieve books,  reach out to system admin"
                };

            }


        }

        public ResponseModel<UpdateBookDTO> UpdateBook(UpdateBookDTO book)
        {
            try
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
            catch (Exception ex)
            {

                return new ResponseModel<UpdateBookDTO>
                {
                    IsSuccessful = false,
                    Message = "An error occured while trying to update this record."
                };
            }
        }


        public ResponseModel<Books> GetBook(int id)
        {
            try
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
            catch (Exception ex)
            {
                return new ResponseModel<Books>
                {
                    IsSuccessful = false,
                    Message = "An error occured while trying to retrieve this record."
                };
            }
        }


        public ResponseModel<string> DeleteBook(int id)
        {
            try
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
                if (save <= 0)
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
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    IsSuccessful = false,
                    Message = "An error occured while trying to delete this record."
                };
            }

        }
    }
}
